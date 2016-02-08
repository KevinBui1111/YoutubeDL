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
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace YoutubeDL
{
    public partial class frmManageVideo : Form
    {
        RepositoryLite repos = new RepositoryLite();
        Dictionary<int, Channel> dicChannel;
        int new_channel_id;
        BlockingCollection<DownloadVid> queueVidNeedLoad;
        Dictionary<string, Image> dicImage = new Dictionary<string, Image>();

        static frmManageVideo openForm = null;
        // No need for locking - you'll be doing all this on the UI thread...
        public static void ShowInstance()
        {
            if (openForm == null)
            {
                openForm = new frmManageVideo();
                openForm.FormClosed += delegate { openForm = null; };
                openForm.Show();
            }
        }
        public static void CloseInstance()
        {
            if (openForm != null) openForm.Close();
        }

        public frmManageVideo()
        {
            InitializeComponent();
        }
        private void frmManageVideo_Load(object sender, EventArgs e)
        {
            //GC.Collect();
            repos.LoadImage().ForEach(f => dicImage[f._key] = f._image);

            queueVidNeedLoad = new BlockingCollection<DownloadVid>();
            Task.Factory.StartNew(bwLoadImage_DoWork);
            Task.Factory.StartNew(bwLoadImage_DoWork);
            Task.Factory.StartNew(bwLoadImage_DoWork);
            Task.Factory.StartNew(bwLoadImage_DoWork);

            olvColSize.AspectToStringConverter = delegate(object size) { return ((long?)size).ToReadableSize(); };
            olvColFolder.AspectToStringConverter = delegate(object channel_id) { return dicChannel[(int)channel_id].folder; };
            this.olvColumn1.ImageGetter = delegate(object row)
            {
                var vid = (DownloadVid)row;

                if (!imageList1.Images.ContainsKey(vid.vid))
                    queueVidNeedLoad.Add(vid);

                return vid.vid;
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
        private void frmManageVideo_FormClosed(object sender, FormClosedEventArgs e)
        {
            queueVidNeedLoad.CompleteAdding();
            dicImage.Clear();
            imageList1.Images.Clear();
            imageList1.Dispose();
            GC.Collect();
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
            var selection = new string[] { frmYoutube.getFullfilename(vid) };
            DataObject data = new DataObject(DataFormats.FileDrop, selection);
            this.DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move);
        }
        private void olvDownload_DoubleClick(object sender, EventArgs e)
        {
            var vid = (DownloadVid)olvDownload.SelectedObject;

            try
            {
                Process.Start(frmYoutube.getFullfilename(vid));
            }
            catch (Exception ex)
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
                    FileSystem.DeleteFile(frmYoutube.getFullfilename(vid), UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);

                    if (!File.Exists(frmYoutube.getFullfilename(vid)))
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
            if (!File.Exists(frmYoutube.getFullfilename(vid)))
                e.Item.ForeColor = Color.Red;
            else if ((new_channel_id > 0 || txtAfter.TextLength > 0 || vid.date_merge > dtpDateMerge.Value) &&
                (new_channel_id == 0 || vid.channel_id >= new_channel_id) &&
                (txtAfter.TextLength == 0 || vid.filename.CompareTo(txtAfter.Text) > 0) &&
                vid.date_merge > dtpDateMerge.Value
                )
                e.Item.ForeColor = Color.RoyalBlue;
        }
        private void olvDownload_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            //var vid = (DownloadVid)e.Model;
            //if (vid != null) Clipboard.SetText(vid.title);
            if (olvDownload.SelectedObjects.Count > 0)
                Clipboard.SetText(
                    string.Join(Environment.NewLine, olvDownload.SelectedObjects.Cast<DownloadVid>().Select(d => d.filename))
                );
        }
        private void olvDownload_Click(object sender, EventArgs e)
        {
            if (olvDownload.SelectedIndices.Count >= 2)
                lbStatus.Text = string.Format("{0} - {1} videos / Total: {2} - {3} video",
                    olvDownload.SelectedObjects.Cast<DownloadVid>().Sum(v => v.size).ToReadableSize(),
                    olvDownload.SelectedIndices.Count,
                    olvDownload.Objects.Cast<DownloadVid>().Sum(item => item.size).ToReadableSize(),
                    olvDownload.GetItemCount()
                    );
            else
            {
                lbStatus.Text = string.Format("Total: {0} - {1} videos",
                    olvDownload.Objects.Cast<DownloadVid>().Sum(item => item.size).ToReadableSize(),
                    olvDownload.GetItemCount()
                    );
            }
        }

        private void btnRemoveMissing_Click(object sender, EventArgs e)
        {
            IEnumerable<DownloadVid> itemParse;
            if (olvDownload.SelectedItems.Count == 0)
                itemParse = olvDownload.Objects.Cast<DownloadVid>().Where(vid => vid.status == 4);
            else
                itemParse = olvDownload.SelectedObjects.Cast<DownloadVid>();

            foreach (var vid in itemParse.ToArray())
            {
                if (!File.Exists(frmYoutube.getFullfilename(vid)))
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
                if (File.Exists(frmYoutube.getFullfilename(vid)))
                {
                    vid.status = 4;
                    repos.UpdateStatus(vid);

                    wrongdel.Add(vid.vid);
                }
            }

            MessageBox.Show(string.Format("Restore successfully {0} videos: {1}",
                wrongdel.Count, string.Join(", ", wrongdel.ToArray())));
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            var newfilenames = Clipboard.GetText().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var vids = olvDownload.SelectedObjects.Cast<DownloadVid>().ToArray();
            if (vids.Length != newfilenames.Length)
                MessageBox.Show("Unmatch number of file!");
            else
            {
                for (int i = 0; i < vids.Length; ++i)
                {
                    var oldname = frmYoutube.getFullfilename(vids[i]);
                    vids[i].filename = newfilenames[i];
                    var newname = frmYoutube.getFullfilename(vids[i]);

                    try
                    {
                        File.Move(oldname, newname);
                        repos.UpdateFilename(vids[i]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(vids[i].vid + Environment.NewLine + ex.Message);
                    }
                }

                olvDownload.RefreshObjects(olvDownload.SelectedObjects);
            }
        }

        private void ckThumbview_CheckedChanged(object sender, EventArgs e)
        {
            if (ckThumbview.Checked)
                olvDownload.View = View.LargeIcon;
            else
                olvDownload.View = View.Details;
        }
        private void bwLoadImage_DoWork()
        {
            foreach (var vid in queueVidNeedLoad.GetConsumingEnumerable())
            {
                try
                {
                    // Get Thumbnail Video;
                    Bitmap shellThumb = ShellFile
                        .FromFilePath(frmYoutube.getFullfilename(vid))
                        .Thumbnail
                        .ExtraLargeBitmap;

                    Image thumb = ResizeBitmap(shellThumb, imageList1.ImageSize.Width);
                    shellThumb.Dispose();

                    AddToImageList(vid.vid, thumb);

                    repos.SaveImage(vid.vid, Helper.ImageToByte(thumb));
                }
                catch (FileNotFoundException) { }
                catch { }
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
                DownloadVid vid;
                imageList1.Images.Clear();
                foreach (var v in listVid)
                {
                    if (dicImage.ContainsKey(v.vid))
                        try
                        {
                            imageList1.Images.Add(v.vid, dicImage[v.vid]);
                        }
                        catch { break; }
                }

                while (queueVidNeedLoad.TryTake(out vid)) ;
                olvDownload.SetObjects(listVid);
            }

            lbStatus.Text = string.Format("Total: {0} videos", listVid.Count());
        }

        void AddToImageList(string key, Image img)
        {
            if (!dicImage.ContainsKey(key))
                dicImage.Add(key, img);
            try
            {
                olvDownload.Invoke(new Action(() =>
                    {
                        try
                        {
                            imageList1.Images.Add(key, img);
                        }
                        catch { }
                    }));
            }
            catch { }

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

    [Serializable()]
    public class FlatImage
    {
        public Image _image { get; set; }
        public string _key { get; set; }

        internal static void Deserialize(ImageList imglist, string pathImagebin)
        {
            RepositoryLite repo = new RepositoryLite();
            List<FlatImage> ilc = repo.LoadImage();

            for (int index = 0; index < ilc.Count; index++)
            {
                Image i = ilc[index]._image;
                string key = ilc[index]._key;
                imglist.Images.Add(key, i);
                Application.DoEvents();
            }
        }
    }
}
