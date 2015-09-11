using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YoutubeDL.Models;
using BrightIdeasSoftware;
using Microsoft.WindowsAPICodePack.Shell;
using System.Threading;

namespace YoutubeDL
{
    public partial class frmManageVideo : Form
    {
        RepositoryLite repos = new RepositoryLite();
        Dictionary<int, Channel> dicChannel;
        int new_channel_id;
        string vidFolder = @"i:\YDL\";
        Random r = new Random();
        BackgroundWorker bwLoadImage;
        Queue<DownloadVid> queueVidNeedLoad;
        AutoResetEvent eventHasItem;

        public frmManageVideo()
        {
            InitializeComponent();
        }
        private void frmManageVideo_Load(object sender, EventArgs e)
        {
            queueVidNeedLoad = new Queue<DownloadVid>();
            eventHasItem = new AutoResetEvent(false);

            bwLoadImage = new BackgroundWorker();
            bwLoadImage.DoWork += bwLoadImage_DoWork;

            olvColSize.AspectToStringConverter = delegate(object size) { return ((long?)size).ToReadableSize(); };
            olvColFolder.AspectToStringConverter = delegate(object channel_id) { return dicChannel[(int)channel_id].folder; };
            this.olvColumn1.ImageGetter = delegate(object row)
            {
                var vid = (DownloadVid)row;
                string key = vid.vid;

                if (!olvDownload.LargeImageList.Images.ContainsKey(key))
                    queueVidNeedLoad.Enqueue(vid);

                return key;
            };
            olvColumn1.AspectGetter = delegate(object rowObject)
            {
                var vid = (DownloadVid)rowObject;
                if (olvDownload.View == View.LargeIcon)
                    return string.Format("{0}: {1}", dicChannel[vid.channel_id].folder, vid.filename);
                else
                    return vid.filename;
            };
            var channels = repos.Get_Channel_list();
            dicChannel = channels.ToDictionary(s => s.id);

            cbColornew.Items.Add(new Channel { id = 0, name = "All" });
            cbColornew.Items.AddRange(channels);
            cbColornew.SelectedIndex = 0;

            cbChannel.Items.Add(new Channel { id = 0, name = "All" });
            cbChannel.Items.AddRange(channels);
            cbChannel.SelectedIndex = 0;
        }

        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var channel = (Channel)cbChannel.SelectedItem;
            LoadVideoChannel(channel.id, "All", true);
        }
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var channel = (Channel)cbChannel.SelectedItem;
            var group = (string)cbGroup.SelectedItem;
            LoadVideoChannel(channel.id, group);
        }

        private void olvDownload_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var vid = (DownloadVid)((OLVListItem)e.Item).RowObject;
            var selection = new string[] { getFullfilename(vid) };
            DataObject data = new DataObject(DataFormats.FileDrop, selection);
            this.DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move);
        }
        private void olvDownload_DoubleClick(object sender, EventArgs e)
        {
            var vid = (DownloadVid)olvDownload.SelectedObject;

            try
            {
                Process.Start(getFullfilename(vid));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void olvDownload_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var vid = (DownloadVid)olvDownload.SelectedObject;

                try
                {
                    FileSystem.DeleteFile(getFullfilename(vid), UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);

                    if (!File.Exists(getFullfilename(vid)))
                    {
                        vid.status = -1;
                        repos.UpdateStatus(vid);

                        olvDownload.RemoveObject(vid);
                        olvDownload.FocusedItem.Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void olvDownload_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            DownloadVid vid = (DownloadVid)e.Model;
            if (!File.Exists(getFullfilename(vid)))
                e.Item.ForeColor = Color.Red;
            else if (new_channel_id > 0 && vid.channel_id >= new_channel_id)
                e.Item.ForeColor = Color.RoyalBlue;
        }

        private void btnRemoveMissing_Click(object sender, EventArgs e)
        {
            Bitmap newImage = new Bitmap("aa.bmp");
            Image resizeImg = ResizeBitmap(newImage, imageList1.ImageSize.Width);
            olvDownload.LargeImageList.Images.Add("aa.bmp", resizeImg);
            newImage.Dispose();

            newImage = new Bitmap("bb.bmp");
            resizeImg = ResizeBitmap(newImage, imageList1.ImageSize.Width);
            newImage.Dispose();
            olvDownload.LargeImageList.Images.Add("bb.bmp", resizeImg);

            return;
            IEnumerable<DownloadVid> itemParse;
            if (olvDownload.SelectedItems.Count == 0)
                itemParse = olvDownload.Objects.Cast<DownloadVid>().Where(vid => vid.status == 4);
            else
                itemParse = olvDownload.SelectedObjects.Cast<DownloadVid>();

            foreach (var vid in itemParse.ToArray())
            {
                if (!File.Exists(getFullfilename(vid)))
                {
                    vid.status = -1;
                    repos.UpdateStatus(vid);

                    olvDownload.RemoveObject(vid);
                }
            }
        }
        private void btnWrongDel_Click(object sender, EventArgs e)
        {
            var deletedVids = repos.LoadDeletedVideo();
            List<string> wrongdel = new List<string>();
            foreach (var vid in deletedVids)
            {
                if (File.Exists(getFullfilename(vid)))
                {
                    vid.status = 4;
                    repos.UpdateStatus(vid);

                    wrongdel.Add(vid.vid);
                }
            }

            MessageBox.Show(string.Format("Restore successfully {0} videos: {1}",
                wrongdel.Count, string.Join(", ", wrongdel.ToArray())));
        }

        private void ckThumbview_CheckedChanged(object sender, EventArgs e)
        {
            if (ckThumbview.Checked)
                olvDownload.View = View.LargeIcon;
            else
                olvDownload.View = View.Details;
        }
        private void bwLoadImage_DoWork(object sender, DoWorkEventArgs e)
        {
            while (queueVidNeedLoad.Count > 0)
            {
                var vid = queueVidNeedLoad.Dequeue();
                GetThumbnailVideo(vid);
            }
        }

        void LoadVideoChannel(int channel_id, string group, bool reloadGroup = false)
        {
            new_channel_id = ((Channel)cbColornew.SelectedItem).id;

            var listVid = repos.LoadDownloadVideo(channel_id, group, ckCompletedVideo.Checked).OrderBy(v => v.filename);

            // load group list to combobox
            if (reloadGroup)
            {
                cbGroup.Items.Clear();
                cbGroup.Items.Add("All");
                cbGroup.Items.AddRange(listVid.Select(v => v.group ?? "").Distinct().OrderBy(g => g).ToArray());

                olvDownload.ClearObjects();
            }
            else
            {
                queueVidNeedLoad.Clear();
                olvDownload.SetObjects(listVid);
                if (!bwLoadImage.IsBusy)
                    bwLoadImage.RunWorkerAsync();
            }

            lbStatus.Text = string.Format("Total: {0} videos", listVid.Count());
        }
        string getFullfilename(DownloadVid vid)
        {
            if (string.IsNullOrEmpty(vid.group))
                return string.Format(@"{0}{1}\{2}",
                    vidFolder, dicChannel[vid.channel_id].folder, vid.filename);
            else
                return string.Format(@"{0}{1}\{2}\{3}", 
                    vidFolder, dicChannel[vid.channel_id].folder, vid.group, vid.filename);

        }
        void Checkgroup()
        {
            var allvid = repos.LoadDownloadVideo(0, null, false);
            var allfile = new DirectoryInfo(vidFolder).GetFiles("*", System.IO.SearchOption.AllDirectories);
            var query = from file in allfile
                        join vid in allvid on file.Name equals vid.filename into gj
                        from g in gj.DefaultIfEmpty()
                        select new { file, g };

            //int c = query.Count(q => q.g != null);
            List<string> resfail = new List<string>();
            foreach (var r in query)
            {
                bool res = false;
                if (r.file.Directory.Parent.Name == "YDL")
                    res = r.file.Directory.Name == dicChannel[r.g.channel_id].folder;
                else
                    res = r.file.Directory.Parent.Name == dicChannel[r.g.channel_id].folder
                        && r.file.Directory.Name == r.g.group;

                if (!res)
                {
                    r.g.group = r.file.Directory.Name;
                    repos.UpdateGroup(r.g);
                    //resfail.Add(r.file.FullName);
                }
            }

            //Clipboard.SetText(string.Join("\r\n", resfail.ToArray()));
        }

        void AddToImageList(string key, Image img)
        {
            if (imageList1.Images.ContainsKey(key)) return;

            if (olvDownload.InvokeRequired)
            {
                olvDownload.Invoke(new Action(() => AddToImageList(key, img)));
            }
            else
            {
                imageList1.Images.Add(key, img);
            }

        }
        void GetThumbnailVideo(DownloadVid vid)
        {
            Image img = GetLargeImageFromStorage(vid);
            if (img != null)
                AddToImageList(vid.vid, img);
                //imageList1.Images.Add(vid.vid, img);

        }
        Image GetLargeImageFromStorage(DownloadVid vid)
        {
            try
            {
                //Thread.Sleep(10000);
                //return null;
                ShellFile shellFile = ShellFile.FromFilePath(getFullfilename(vid));
                Bitmap shellThumb = shellFile.Thumbnail.ExtraLargeBitmap;
                Image thumb = ResizeBitmap(shellThumb, imageList1.ImageSize.Width);
                shellThumb.Dispose();

                return thumb;
            }
            catch
            {
                return null;
            }
        }
        Image ResizeBitmap(Bitmap b, int nSize)
        {
            int newW, newH, left = 0, top = 0;
            if (b.Size.Width > b.Size.Height)
            {
                newW = nSize;
                newH = newW * b.Size.Height / b.Size.Width;
                top = (nSize - newH) / 2;
            }
            else
            {
                newH = nSize;
                newW = newH * b.Size.Width / b.Size.Height;
                left = (nSize - newW) / 2;
            }

            Bitmap result = new Bitmap(nSize, nSize);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, left, top, newW, newH);
            return result;
        }
    }
}
