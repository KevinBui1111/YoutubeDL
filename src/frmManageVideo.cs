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

namespace YoutubeDL
{
    public partial class frmManageVideo : Form
    {
        RepositoryLite repos = new RepositoryLite();
        int new_channel_id;

        public frmManageVideo()
        {
            InitializeComponent();
        }

        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var channel = (Channel)cbChannel.SelectedItem;
            LoadVideoChannel(channel.id, null, true);

        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var channel = (Channel)cbChannel.SelectedItem;
            var group = (string)cbGroup.SelectedItem;
            LoadVideoChannel(channel.id, group);

        }

        void LoadVideoChannel(int channel_id, string group, bool reloadGroup = false)
        {
            new_channel_id = ((Channel)cbColornew.SelectedItem).id;

            var listVid = repos.LoadDownloadVideo(channel_id, group, ckCompletedVideo.Checked).OrderBy(v => v.filename);
            olvDownload.SetObjects(listVid);

            // load group list to combobox
            if (reloadGroup)
            {
                cbGroup.Items.Clear();
                cbGroup.Items.Add("zOthers");
                cbGroup.Items.AddRange(listVid.Select(v => v.group ?? "").Distinct().OrderBy(g => g).ToArray());
            }

            lbStatus.Text = string.Format("Total: {0} videos", listVid.Count());
        }

        Dictionary<int, Channel> dicChannel;
        private void ManageVideo_Load(object sender, EventArgs e)
        {
            olvColSize  .AspectToStringConverter = delegate(object size)       { return ((long?)size).ToReadableSize(); };
            olvColFolder.AspectToStringConverter = delegate(object channel_id) { return dicChannel[(int)channel_id].folder; };
            
            var channels = repos.Get_Channel_list();
            dicChannel = channels.ToDictionary(s => s.id);

            cbColornew.Items.Add(new Channel { id = 0, name = "All" });
            cbColornew.Items.AddRange(channels);
            cbColornew.SelectedIndex = 0;

            cbChannel.Items.Add(new Channel { id = 0, name = "All" });
            cbChannel.Items.AddRange(channels);
            cbChannel.SelectedIndex = 0;
        }

        private void olvDownload_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var vid = (DownloadVid)((OLVListItem)e.Item).RowObject;
            var selection = new string[] { getFullfilename(vid) };
            DataObject data = new DataObject(DataFormats.FileDrop, selection);
            this.DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move);
        }

        string vidFolder = @"i:\YDL\";
        private void lvDownload_DoubleClick(object sender, EventArgs e)
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

        string getFullfilename(DownloadVid vid)
        {
            if (string.IsNullOrEmpty(vid.group))
                return string.Format(@"{0}{1}\{2}",
                    vidFolder, dicChannel[vid.channel_id].folder, vid.filename);
            else
                return string.Format(@"{0}{1}\{2}\{3}", 
                    vidFolder, dicChannel[vid.channel_id].folder, vid.group, vid.filename);

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
                if(File.Exists(getFullfilename(vid)))
                {
                    vid.status = 4;
                    repos.UpdateStatus(vid);

                    wrongdel.Add(vid.vid);
                }
            }

            MessageBox.Show(string.Format("Restore successfully {0} videos: {1}",
                wrongdel.Count, string.Join(", ", wrongdel.ToArray())));
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

        private void olvDownload_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            DownloadVid vid = (DownloadVid)e.Model;
            if (!File.Exists(getFullfilename(vid)))
                e.Item.ForeColor = Color.Red;
            else if (new_channel_id > 0 && vid.channel_id >= new_channel_id)
                e.Item.ForeColor = Color.RoyalBlue;
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
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
