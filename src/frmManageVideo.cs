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

namespace YoutubeDL
{
    public partial class frmManageVideo : Form
    {
        RepositoryLite repos = new RepositoryLite();

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
            lvDownload.BeginUpdate();

            lvDownload.Items.Clear();
            var listVid = repos.LoadDownloadVideo(channel_id, group).OrderBy(v => v.filename);
            foreach (var vid in listVid)
                InsertVidtoLV(vid);

            lvDownload.EndUpdate();

            // load group list to combobox
            if (reloadGroup)
            {
                cbGroup.Items.Clear();
                cbGroup.Items.AddRange(listVid.Select(v => v.group ?? "").Distinct().OrderBy(g => g).ToArray());
            }

            lbStatus.Text = string.Format("Total: {0} videos", listVid.Count());
        }

        Dictionary<int, Channel> dicChannel;
        private void ManageVideo_Load(object sender, EventArgs e)
        {
            ListViewHelper.EnableDoubleBuffer(lvDownload);

            var channels = repos.Get_Channel_list();
            dicChannel = channels.ToDictionary(s => s.id);

            cbChannel.Items.Add(new Channel { id = 0, name = "All" });
            cbChannel.Items.AddRange(channels);
            cbChannel.SelectedIndex = 0;
        }

        ListViewItem InsertVidtoLV(DownloadVid vid)
        {
            var item = CreateLVItem();
            UpdateLVItem(item, vid);
            lvDownload.Items.Add(item);

            return item;
        }
        ListViewItem CreateLVItem()
        {
            ListViewItem item = new ListViewItem();
            item.SubItems.AddRange(
                new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, null) { Name = "ext" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "size" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "resolution" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "folder" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "vid" },
                }
            );
            return item;
        }
        void UpdateLVItem(ListViewItem item, DownloadVid vid)
        {
            var subitems = item.SubItems;
            subitems["ext"].Text = vid.ext;
            subitems["size"].Text = vid.size.ToReadableSize();
            subitems["resolution"].Text = vid.resolution;
            subitems["folder"].Text = dicChannel[vid.channel_id].folder;
            subitems["vid"].Text = vid.vid;

            if (!File.Exists(getFullfilename(vid)))
                item.ForeColor = Color.Red;

            item.Text = item.Name = vid.filename;
            item.Tag = vid;
        }

        private void lvDownload_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var currentLVItem = (ListViewItem)e.Item;
            var vid = (DownloadVid)currentLVItem.Tag;
            var selection = new string[] { getFullfilename(vid) };
            DataObject data = new DataObject(DataFormats.FileDrop, selection);
            this.DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move);
        }

        string vidFolder = @"i:\YDL\";
        private void lvDownload_DoubleClick(object sender, EventArgs e)
        {
            var currentLVItem = lvDownload.SelectedItems[0];
            var vid = (DownloadVid)currentLVItem.Tag;

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
            return string.Format(@"{0}{1}\{2}\{3}", 
                vidFolder, dicChannel[vid.channel_id].folder, vid.group, vid.filename);
        }

        private void lvDownload_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var currentLVItem = lvDownload.SelectedItems[0];
                var vid = (DownloadVid)currentLVItem.Tag;

                FileSystem.DeleteFile(getFullfilename(vid), UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);

                if (!File.Exists(getFullfilename(vid)))
                {
                    vid.status = -1;
                    repos.UpdateStatus(vid);

                    currentLVItem.Remove();
                }

            }
        }

        private void btnRemoveMissing_Click(object sender, EventArgs e)
        {
            IEnumerable<ListViewItem> itemParse;
            if (lvDownload.SelectedItems.Count == 0)
                itemParse = lvDownload.Items.Cast<ListViewItem>().Where(i =>
                {
                    var vid = (DownloadVid)i.Tag;
                    return vid.status == 4;
                });
            else
                itemParse = lvDownload.SelectedItems.Cast<ListViewItem>();

            lvDownload.BeginUpdate();

            foreach (var item in itemParse)
            {
                var vid = (DownloadVid)item.Tag;

                if (!File.Exists(getFullfilename(vid)))
                {
                    vid.status = -1;
                    repos.UpdateStatus(vid);

                    item.Remove();
                }
            }

            lvDownload.EndUpdate();
        }

    }
}
