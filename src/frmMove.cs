using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YoutubeDL.Models;

namespace YoutubeDL
{
    public partial class frmMove : Form
    {
        RepositoryLite repos = new RepositoryLite();
        IEnumerable<DownloadVid> moveVids;

        public frmMove(IEnumerable<DownloadVid> moveVids)
        {
            this.moveVids = moveVids;
            InitializeComponent();
        }

        private void frmMove_Load(object sender, EventArgs e)
        {
            var channels = repos.Get_Channel_list();
            cbChannel.Items.Add(new Channel { id = 0, name = "As is" });
            cbChannel.Items.AddRange(channels);
            cbChannel.SelectedItem = channels.First(c => c.id == moveVids.First().channel_id);

            var listGroup = repos.LoadDownloadVideo(0, "All", true).Select(v => v.group ?? "").Distinct().OrderBy(g => g).ToArray();
            cbGroup.Items.AddRange(listGroup);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string newfolderGroup = (string)cbGroup.SelectedItem;
            int selectedChannelID = ((Channel)cbChannel.SelectedItem).id;
            foreach (DownloadVid vid in moveVids)
            {
                string oldFile = frmYoutube.getFullfilename(vid);

                if (selectedChannelID > 0) vid.channel_id = selectedChannelID;
                vid.group = newfolderGroup;

                string newDes = frmYoutube.getFullfilename(vid);

                if (!File.Exists(newDes))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(newDes)))
                        Directory.CreateDirectory(Path.GetDirectoryName(newDes));

                    File.Move(oldFile, newDes);
                    repos.UpdateGroup(vid);
                }
            }

            this.Close();
        }
    }
}
