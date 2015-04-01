using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using YoutubeDL.Models;
using System.Threading;
using YoutubeDL.Properties;
using System.IO;

namespace YoutubeDL
{
    public partial class frmYoutube : Form
    {
        const string namePattern = "[\\\\/?:*\"><|]";
        const string YoutubeLink = "https://www.youtube.com/watch?v=";
        RepositoryLite repos;
        ListViewItem currentLVItem;
        string download_path = @"F:\Downloads\Video\YDL";
        const string file_name_format = "{0}_{1}.{2}";
        const string ffmpeg_format = "-i \"{0}\" -i \"{1}\" -vcodec copy -acodec copy -y \"{2}\"";

        Color UNLOADED = Color.Gray;
        Color LOADED = Color.Black;
        Color FORMATTED = Color.RoyalBlue;
        Color DOWNLOADING = Color.Crimson;
        Color COMPLETE = Color.Teal;
        Dictionary<int, Color> colorStatus;
        public frmYoutube()
        {
            InitializeComponent();
        }
        private void frmYoutube_Load(object sender, EventArgs e)
        {
            colGroup.DisplayIndex = 0;
            colorStatus = new Dictionary<int,Color>();
            colorStatus.Add(0, UNLOADED);
            colorStatus.Add(1, LOADED);
            colorStatus.Add(2, FORMATTED);
            colorStatus.Add(3, DOWNLOADING);
            colorStatus.Add(4, COMPLETE);

            repos = new RepositoryLite();
            var listVid = repos.LoadDownloadVideo();
            foreach (var vid in listVid)
            {
                InsertVidtoLV(vid);
            }

            // load group list to combobox
            cbGroup.Items.AddRange(listVid.Select(v => v.group ?? "").Distinct().OrderBy(g => g).ToArray());
        }

        private void lvVideo_Click(object sender, EventArgs e)
        {
            string ext = "m4a";
            if (lvVideo.SelectedItems[0].SubItems[2].Text == "webm")
                ext = "webm";

            foreach (ListViewItem item in lvAudio.Items)
            {
                Formats f = (Formats)item.Tag;
                if (f.Ext == ext)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        private void lvVideo_DoubleClick(object sender, EventArgs e)
        {
            var vF = (Formats)lvVideo.SelectedItems[0].Tag;
            var aF = (Formats)lvAudio.SelectedItems[0].Tag;

            var vid = (DownloadVid)currentLVItem.Tag;
            vid.ext = vF.Ext;
            vid.filename = new Regex(namePattern).Replace(vid.title, "_") + "." + vF.Ext;
            vid.vidFID = vF.Format_Id;
            vid.vidUrl = vF.Url;
            vid.vidFilename = string.Format(file_name_format, vid.vid, vid.vidFID, vF.Ext);
            vid.vidSize = vF.FileSize;
            vid.audFID = aF.Format_Id;
            vid.audUrl = aF.Url;
            vid.audFilename = string.Format(file_name_format, vid.vid, vid.audFID, aF.Ext);
            vid.audSize = aF.FileSize;
            vid.resolution = vF.Width + " x " + vF.Height;
            vid.size = vF.FileSize + aF.FileSize;
            vid.status = 2;

            UpdateLVItem(currentLVItem, vid);
            repos.InsertOrUpdate(vid);
        }

        private void lvDownload_Click(object sender, EventArgs e)
        {
            //txtYtDownload.Text = string.Format("youtube-dl -f {0} {1}", items[1].Text, items[0].Text);
            lvVideo.BeginUpdate();
            lvAudio.BeginUpdate();

            lvVideo.Items.Clear();
            lvAudio.Items.Clear();

            currentLVItem = lvDownload.SelectedItems[0];
            var vid = (DownloadVid)currentLVItem.Tag;
            cbGroup.Text = vid.group;

            if (string.IsNullOrEmpty(vid.jsonYDL))
            {
                lvVideo.EndUpdate();
                lvAudio.EndUpdate();
                return;
            }

            var vidInfo = new JavaScriptSerializer().Deserialize<YoutubeDlInfo>(vid.jsonYDL);

            var vidFormats = vidInfo.Formats.Where(f => f.Format_Note == "DASH video").OrderByDescending(f => f.FileSize);
            var audFormats = vidInfo.Formats.Where(f => f.Format_Note == "DASH audio").OrderByDescending(f => f.FileSize);

            foreach (Formats f in vidFormats)
            {
                if (lvVideo.Items.ContainsKey(f.Format_Id))
                {
                    lvVideo.Items[f.Format_Id].ForeColor = Color.DodgerBlue;
                    continue;
                }

                ListViewItem item = new ListViewItem(new string[]{
                    f.Format_Id, f.Width + " x " + f.Height, f.Ext, f.Fps > 30 ? f.Fps.ToString() : null, (1.0 * f.FileSize / 1024 / 1024).Value.ToString("0.00") + " MB"
                });
                item.Name = f.Format_Id;
                item.Tag = f;

                lvVideo.Items.Add(item);
            }

            foreach (Formats f in audFormats)
            {
                if (lvAudio.Items.ContainsKey(f.Format_Id))
                {
                    lvAudio.Items[f.Format_Id].ForeColor = Color.DodgerBlue;
                    continue;
                }

                ListViewItem item = new ListViewItem(new string[]{
                    f.Format_Id, f.Ext, f.Abr.ToString(), (1.0 * f.FileSize / 1024 / 1024).Value.ToString("0.00") + " MB"
                });
                item.Name = f.Format_Id;
                item.Tag = f;

                lvAudio.Items.Add(item);
            }

            lvVideo.EndUpdate();
            lvAudio.EndUpdate();
        }
        private void lvDownload_DoubleClick(object sender, EventArgs e)
        {
            currentLVItem = lvDownload.SelectedItems[0];
            var vid = (DownloadVid)currentLVItem.Tag;
            Clipboard.SetText(YoutubeLink + vid.vid);
        }

        private void lvDownload_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
        }
        private void lvDownload_DragDrop(object sender, DragEventArgs e)
        {
            bool autoparse = ckAutoparse.Checked;
            string web_url = (string)e.Data.GetData(DataFormats.Text);
            string[] vidIDs = YoutubeDlInfo.GetVideoIDFromUrl(web_url);
            foreach (string id in vidIDs)
            {
                if (lvDownload.Items.ContainsKey(id)) continue;

                var vid = new DownloadVid { vid = id, group = (string)cbGroup.SelectedItem };
                var item = InsertVidtoLV(vid);

                if (autoparse)
                    EnqueueItem(item);
            }

            lvDownload.Items[lvDownload.Items.Count - 1].EnsureVisible();
            lvDownload.Items[lvDownload.Items.Count - 1].Selected = true;

            if (autoparse)
                download_vid_format_In_Queue();
        }


        private ListViewItem CreateLVItem()
        {
            ListViewItem item = new ListViewItem();
            item.SubItems.AddRange(
                new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, null) { Name = "status" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "FID" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "resolution" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "ext" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "size" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "title" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "group" },
                }
            );
            return item;
        }
        private void UpdateLVItem(ListViewItem item, DownloadVid vid)
        {
            var subitems = item.SubItems;
            subitems["status"].Text = vid.status.ToString();
            subitems["FID"].Text = string.IsNullOrEmpty(vid.vidFID) ? null : vid.vidFID + "+" + vid.audFID;
            subitems["resolution"].Text = vid.resolution;
            subitems["ext"].Text = vid.ext;
            subitems["size"].Text = vid.size.HasValue ? (vid.size.Value * 1.0 / 1024 / 1024).ToString("0.00") + " MB" : null;
            subitems["title"].Text = vid.title;
            subitems["group"].Text = vid.group;
            item.ForeColor = colorStatus[vid.status];
            item.Text = item.Name = vid.vid;
            item.Tag = vid;
        }
        private ListViewItem InsertVidtoLV(DownloadVid vid)
        {
            var item = CreateLVItem();
            lvDownload.Items.Add(item);
            UpdateLVItem(item, vid);
            repos.InsertOrUpdate(vid);

            return item;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvDownload.SelectedItems)
            {
                repos.DeleteVid(item.Name);
                item.Remove();
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            var itemDown = lvDownload.SelectedItems.Count == 0 ?
                lvDownload.Items.Cast<ListViewItem>() : lvDownload.SelectedItems.Cast<ListViewItem>();

            string idm_format = "-a /d {0} /p \"{1}\" /f \"{2}\"";
            foreach (var item in itemDown)
            {
                var vid = (DownloadVid)item.Tag;
                if (vid.status < 2) continue;

                var p = new Process
                {
                    StartInfo =
                    {
                        FileName = @"C:\Program Files (x86)\Internet Download Manager\IDMan.exe",
                        Arguments = string.Format(idm_format, vid.vidUrl, download_path, vid.vidFilename),
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };

                p.Start();
                p.WaitForExit();
                p.Close();

                p.StartInfo.Arguments = string.Format(idm_format, vid.audUrl, download_path, vid.audFilename);
                p.Start();
                p.WaitForExit();
                p.Close();

                vid.status = 3;
                repos.InsertOrUpdate(vid);
                UpdateLVItem(item, vid);
            }
        }

        BackgroundWorker[] bws = new BackgroundWorker[4];
        Queue<ListViewItem> queueItem = new Queue<ListViewItem>();

        private void btnLoadVid_Click(object sender, EventArgs e)
        {
            IEnumerable<ListViewItem> itemParse;
            if (lvDownload.SelectedItems.Count == 0)
                itemParse = lvDownload.Items.Cast<ListViewItem>().Where(i =>
                            {
                                var vid = (DownloadVid)i.Tag;
                                return vid.status == 0;
                            });
            else
                itemParse = new Queue<ListViewItem>(lvDownload.SelectedItems.Cast<ListViewItem>());

            foreach (var item in itemParse) {
                EnqueueItem(item);
            }

            download_vid_format_In_Queue();
        }
        private void download_vid_format_In_Queue()
        {
            for (int i = 0; i < bws.Length && queueItem.Count > 0; ++i)
            {
                if (bws[i] == null)
                {
                    bws[i] = new BackgroundWorker();
                    bws[i].DoWork += new DoWorkEventHandler(download_vid_work);
                    bws[i].RunWorkerCompleted += new RunWorkerCompletedEventHandler(download_vid_complete);
                }

                if (bws[i].IsBusy) continue;

                var item = queueItem.Dequeue();
                item.BackColor = Color.LightBlue;
                item.SubItems["status"].Text = "running";
                var vid = (DownloadVid)item.Tag;

                bws[i].RunWorkerAsync(new object[]{
                    item, vid.vid
                });
            }
        }

        private void download_vid_work(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];

            var vidInfo = LoadVideoInfo((string)parameters[1]);
            e.Result = new object[]{
                    parameters[0], vidInfo
            };
        }
        private void download_vid_complete(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] parameters = e.Result as object[];

            var item = (ListViewItem)parameters[0];
            var vidInfo = parameters[1] as YoutubeDlInfo;
            var vid = item.Tag as DownloadVid;

            if (vidInfo.error)
            {
                var frmLog = frmYTLog.GetInstance(this);
                frmLog.AddLog(vidInfo.error_message);
                UpdateLVItem(item, vid);

                item.ForeColor = Color.Red;
            }
            else
            {
                vid.title = vidInfo.Title;
                vid.status = vid.status > 1 ? 2 : 1;
                vid.jsonYDL = new JavaScriptSerializer().Serialize(vidInfo);
                repos.InsertOrUpdate(vid);
                UpdateLVItem(item, vid);
            }

            item.BackColor = Color.Transparent;

            var bw = (BackgroundWorker)sender;
            if (queueItem.Count > 0)
            {
                var nextItem = queueItem.Dequeue();
                nextItem.BackColor = Color.LightBlue;
                nextItem.SubItems["status"].Text = "running";
                var nextvid = (DownloadVid)nextItem.Tag;
                
                bw.RunWorkerAsync(new object[]{
                    nextItem, nextvid.vid
                });
            }
        }
        
        private YoutubeDlInfo LoadVideoInfo(string vidID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Start LoadVideoInfo: youtube-dl -j " + vidID);

            var p = new Process
            {
                StartInfo =
                {
                    FileName = "youtube-dl",
                    Arguments = string.Format("-j {0}", YoutubeLink + vidID),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            p.Start();
            string res = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();

            YoutubeDlInfo vidInfo = null;

            if (string.IsNullOrEmpty(error) == false)
            {
                vidInfo = new YoutubeDlInfo { error = true };
                sb.AppendLine(error);
            }
            else
            {
                sb.AppendLine("Success LoadVideoInfo: " + vidID);

                vidInfo = new JavaScriptSerializer().Deserialize<YoutubeDlInfo>(res);

                Regex reg = new Regex(@"^\d+$");
                var vidFormats = vidInfo.Formats.Where(f => reg.IsMatch(f.Format_Id) && f.Format_Note == "DASH video").OrderByDescending(f => f.FileSize);
                var audFormats = vidInfo.Formats.Where(f => reg.IsMatch(f.Format_Id) && f.Format_Note == "DASH audio").OrderByDescending(f => f.FileSize);
                vidInfo.Formats = vidFormats.Union(audFormats).ToList();
            }

            vidInfo.error_message = sb.ToString();
            return vidInfo;
        }
        private bool ItemInProcessing(ListViewItem item)
        {
            return Regex.IsMatch(item.SubItems["status"].Text, "running|waiting");
        }
        private void EnqueueItem(ListViewItem item)
        {
            if (ItemInProcessing(item)) return;

            item.SubItems["status"].Text = "waiting";
            queueItem.Enqueue(item);
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvDownload.Items)
            {
                var vid = (DownloadVid)item.Tag;
                if (vid.status < 2) continue;

                var vidFI = new FileInfo(download_path + "\\" + vid.vidFilename);
                var audFI = new FileInfo(download_path + "\\" + vid.audFilename);
                bool complete = vidFI.Exists && vidFI.Length == vid.vidSize &&
                                audFI.Exists && audFI.Length == vid.audSize;

                if (!complete) continue;

                string desFolder = download_path
                    + (string.IsNullOrEmpty(vid.group) ? "" : ("\\" + vid.group));
                string desFilename = desFolder + "\\" + vid.filename;

                if (!Directory.Exists(desFolder))
                    Directory.CreateDirectory(desFolder);

                var p = new Process
                {
                    StartInfo =
                    {
                        FileName = "ffmpeg",
                        Arguments = string.Format(ffmpeg_format, vid.vidFilename, vid.audFilename, desFilename),
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        WorkingDirectory = download_path
                    }
                };

                p.Start();
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();
                int exitcode = p.ExitCode;
                p.Close();

                if (exitcode != 0 || File.Exists(desFilename) == false)
                {
                    var frmLog = frmYTLog.GetInstance(this);
                    frmLog.AddLog("ERROR on video: " + vid.vid + " - " + error);
                }
                else
                {
                    File.Delete(download_path + "\\" + vid.vidFilename);
                    File.Delete(download_path + "\\" + vid.audFilename);
                    vid.status = 4;
                    UpdateLVItem(item, vid);
                    repos.InsertOrUpdate(vid);
                }
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!cbGroup.Items.Contains(cbGroup.Text))
                    cbGroup.Items.Add(cbGroup.Text);
                ChangeGroupVid();
            }
        }

        void ChangeGroupVid()
        {
            string group = cbGroup.Text;
            foreach (ListViewItem item in lvDownload.SelectedItems)
            {
                var vid = (DownloadVid)item.Tag;
                vid.group = group;
                UpdateLVItem(item, vid);
                repos.InsertOrUpdate(vid);
            }
        }
    }
}
