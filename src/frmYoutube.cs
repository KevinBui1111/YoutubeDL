using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using YoutubeDL.Models;

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

        BackgroundWorker[] bws = new BackgroundWorker[4];
        Queue<ListViewItem> queueItem = new Queue<ListViewItem>();
        int freeBW_number;

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
            for (int i = 0; i < bws.Length; ++i)
            {
                bws[i] = new BackgroundWorker();
                bws[i].DoWork += new DoWorkEventHandler(download_vid_work);
                bws[i].RunWorkerCompleted += new RunWorkerCompletedEventHandler(download_vid_complete);
            }
            freeBW_number = bws.Length;

            ListViewHelper.EnableDoubleBuffer(lvDownload);
            ListViewHelper.EnableDoubleBuffer(lvVideo);
            ListViewHelper.EnableDoubleBuffer(lvAudio);

            colGroup.DisplayIndex = 0;
            colFormat.Width = 0;
            col60fps.DisplayIndex = 6;

            colorStatus = new Dictionary<int,Color>();
            colorStatus.Add(0, UNLOADED);
            colorStatus.Add(1, LOADED);
            colorStatus.Add(2, FORMATTED);
            colorStatus.Add(3, DOWNLOADING);
            colorStatus.Add(4, COMPLETE);

            repos = new RepositoryLite();
            var listVid = repos.LoadDownloadVideo();
            foreach (var vid in listVid)
                InsertVidtoLV(vid);

            // load group list to combobox
            cbGroup.Items.AddRange(listVid.Select(v => v.group ?? "").Distinct().OrderBy(g => g).ToArray());

            lbStatus.Text = string.Format("Total {0} vids.", listVid.Length);
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

            UpdateFormat(vid, vF, aF);
            UpdateLVItem(currentLVItem, vid);
            repos.InsertOrUpdate(vid);
        }

        private void lvDownload_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.None) return;
            if (e.KeyCode == Keys.Up
                || e.KeyCode == Keys.Down
                || e.KeyCode == Keys.Home
                || e.KeyCode == Keys.End)
                lvDownload_Click(null, null);
        }
        private void lvDownload_Click(object sender, EventArgs e)
        {
            lvVideo.Items.Clear();
            lvAudio.Items.Clear();
            
            if (lvDownload.SelectedItems.Count == 0) return;
        
            currentLVItem = lvDownload.SelectedItems[0];
            var vid = (DownloadVid)currentLVItem.Tag;
            cbGroup.Text = vid.group;

            if (string.IsNullOrEmpty(vid.jsonYDL)) return;

            var vidInfo = new JavaScriptSerializer().Deserialize<YoutubeDlInfo>(vid.jsonYDL);
            var vidFormats = vidInfo.Formats.Where(f => f.Format_Note == "DASH video").OrderByDescending(f => f.FileSize);
            var audFormats = vidInfo.Formats.Where(f => f.Format_Note == "DASH audio" && f.FileSize.HasValue).OrderByDescending(f => f.FileSize);
            
            var listitem = new List<ListViewItem>();
            foreach (Formats f in vidFormats)
            {
                ListViewItem item = new ListViewItem(new string[]{
                    f.Format_Id, f.Width + " x " + f.Height, f.Ext, f.Fps > 30 ? f.Fps.ToString() : null, (1.0 * f.FileSize / 1024 / 1024).Value.ToString("0.00") + " MB"
                });
                item.Name = f.Format_Id;
                item.Tag = f;
                item.Selected = vid.vidFID == f.Format_Id;

                listitem.Add(item);
            }

            lvVideo.Items.AddRange(listitem.ToArray());

            listitem.Clear();
            foreach (Formats f in audFormats)
            {
                ListViewItem item = new ListViewItem(new string[]{
                    f.Format_Id, f.Ext, f.Abr.ToString(), (1.0 * f.FileSize / 1024 / 1024).Value.ToString("0.00") + " MB"
                });
                item.Name = f.Format_Id;
                item.Tag = f;
                item.Selected = vid.audFID == f.Format_Id;

                listitem.Add(item);
            }

            lvAudio.Items.AddRange(listitem.ToArray());
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
            lvDownload.SelectedItems.Clear();

            bool autoparse = ckAutoparse.Checked;
            string web_url = (string)e.Data.GetData(DataFormats.Text);
            string[] vidIDs = YoutubeDlInfo.GetVideoIDFromUrl(web_url);
            foreach (string id in vidIDs)
            {
                if (lvDownload.Items.ContainsKey(id)) continue;

                var vid = new DownloadVid { vid = id, group = (string)cbGroup.Text, date_add = DateTime.Now };
                var item = InsertVidtoLV(vid);
                repos.InsertOrUpdate(vid);

                if (autoparse)
                    EnqueueItem(item);
            }

            lvDownload.Items[lvDownload.Items.Count - 1].EnsureVisible();
            lvDownload.Items[lvDownload.Items.Count - 1].Selected = true;

            if (autoparse)
                download_vid_format_In_Queue();
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
                    new ListViewItem.ListViewSubItem(item, null) { Name = "status" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "FID" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "resolution" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "ext" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "size" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "title" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "group" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "dateformat" },
                    new ListViewItem.ListViewSubItem(item, null) { Name = "fps60" },
                }
            );
            return item;
        }
        void UpdateLVItem(ListViewItem item, DownloadVid vid)
        {
            var subitems = item.SubItems;
            subitems["status"].Text = vid.status.ToString();
            subitems["FID"].Text = string.IsNullOrEmpty(vid.vidFID) ? null : vid.vidFID + "+" + vid.audFID;
            subitems["resolution"].Text = vid.resolution;
            subitems["ext"].Text = vid.ext;
            subitems["size"].Text = vid.size.HasValue ? (vid.size.Value * 1.0 / 1024 / 1024).ToString("0.00") + " MB" : null;
            subitems["title"].Text = vid.title;
            subitems["group"].Text = vid.group;
            subitems["dateformat"].Text = vid.date_format.ToHumanDate();
            subitems["fps60"].Text = vid.fps60 ? "60" : null;
            item.ForeColor = colorStatus[vid.status];
            item.Text = item.Name = vid.vid;
            item.Tag = vid;
        }
        void UpdateFormat(DownloadVid vid, Formats vF, Formats aF)
        {
            vid.ext = vF.Ext;
            vid.filename = new Regex(namePattern).Replace(vid.title, "_") + "." + vF.Ext;
            vid.vidFID = vF.Format_Id;
            vid.vidUrl = vF.Url;
            vid.vidFilename = string.Format(file_name_format, vid.vid, vid.vidFID, vF.Ext);
            if (vid.vid.StartsWith("-")) vid.vidFilename = "_" + vid.vidFilename;
            vid.vidSize = vF.FileSize;
            vid.audFID = aF.Format_Id;
            vid.audUrl = aF.Url;
            vid.audFilename = string.Format(file_name_format, vid.vid, vid.audFID, aF.Ext);
            if (vid.vid.StartsWith("-")) vid.audFilename = "_" + vid.audFilename;
            vid.audSize = aF.FileSize;
            vid.resolution = vF.Width + " x " + vF.Height;
            vid.size = vF.FileSize + aF.FileSize;
            vid.status = 2;
        }

        private void btnLoadVid_Click(object sender, EventArgs e)
        {
            if (freeBW_number == 0) return;

            if (!Helper.CheckForYoutubeConnection())
            {
                SetStatusError("Can't connect to Youtube!");
                return;
            }

            IEnumerable<ListViewItem> itemParse;
            if (lvDownload.SelectedItems.Count == 0)
                itemParse = lvDownload.Items.Cast<ListViewItem>().Where(i =>
                {
                    var vid = (DownloadVid)i.Tag;
                    return vid.status == 0;
                });
            else
                itemParse = lvDownload.SelectedItems.Cast<ListViewItem>();

            foreach (var item in itemParse)
            {
                EnqueueItem(item);
            }

            download_vid_format_In_Queue();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to remove videos?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

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

            repos.BeginUpdate();
            foreach (var item in itemDown)
            {
                var vid = (DownloadVid)item.Tag;
                if (vid.status != 2) continue;

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
            BeginAsync(repos.Commit, "Saving video info...", "Saved successful.");
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
                    vid.date_merge = DateTime.Now;
                    UpdateLVItem(item, vid);
                    repos.InsertOrUpdate(vid);
                }
            }
        }
        private void btnAutoSelect_Click(object sender, EventArgs e)
        {
            IEnumerable<ListViewItem> itemParse;
            if (lvDownload.SelectedItems.Count == 0)
                itemParse = lvDownload.Items.Cast<ListViewItem>().Where(i =>
                {
                    var vid = (DownloadVid)i.Tag;
                    return vid.status == 1;
                });
            else
                itemParse = lvDownload.SelectedItems.Cast<ListViewItem>();

            long diffSize = 50 * 1024 * 1024;

            repos.BeginUpdate();
            lvDownload.BeginUpdate();
            foreach (ListViewItem item in itemParse)
            {
                var vid = (DownloadVid)item.Tag;

                var vidInfo = new JavaScriptSerializer().Deserialize<YoutubeDlInfo>(vid.jsonYDL);
                if (vidInfo == null) continue;

                var maxWebm = vidInfo.Formats.Where(f => f.Format_Note == "DASH video" && f.Ext == "webm").OrderByDescending(f => f.FileSize).FirstOrDefault();
                var maxMp4 = vidInfo.Formats.Where(f => f.Format_Note == "DASH video" && f.Ext == "mp4").OrderByDescending(f => f.FileSize).FirstOrDefault();

                Formats vF;

                if (maxWebm == null) vF = maxMp4;
                else if (maxMp4 == null) vF = maxWebm;
                else if (maxMp4.FileSize + diffSize < maxWebm.FileSize) vF = maxWebm;
                else vF = maxMp4;

                var aF = vidInfo.Formats.OrderByDescending(f => f.FileSize).First(f => f.Format_Note == "DASH audio" && f.FileSize.HasValue && f.Ext == (vF.Ext == "webm" ? "webm" : "m4a"));

                UpdateFormat(vid, vF, aF);
                UpdateLVItem(item, vid);
                repos.InsertOrUpdate(vid);
            }
            lvDownload.EndUpdate();

            BeginAsync(repos.Commit, "Saving video info...", "Saved successful.");
        }
        private void cbGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!cbGroup.Items.Contains(cbGroup.Text))
                    cbGroup.Items.Add(cbGroup.Text);
                ChangeGroupVid();
            }
        }
        
        void download_vid_format_In_Queue()
        {
            SetStatusError("Start loading video...");

            for (int i = 0; i < bws.Length && queueItem.Count > 0; ++i)
                download_next_vid(bws[i]);
        }
        void download_next_vid(BackgroundWorker bw)
        {
            if (bw.IsBusy) return;

            --freeBW_number;
            var item = queueItem.Dequeue();
            item.BackColor = Color.LightBlue;
            item.SubItems["status"].Text = "running";

            bw.RunWorkerAsync(new object[] { item, ((DownloadVid)item.Tag).vid });
        }

        private void download_vid_work(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];

            var vidInfo = LoadVideoInfo((string)parameters[1]);
            e.Result = new object[] { parameters[0], vidInfo };
        }
        private void download_vid_complete(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] parameters = (object[])e.Result;

            var item = (ListViewItem)parameters[0];
            var vidInfo = (YoutubeDlInfo)parameters[1];
            var vid = (DownloadVid)item.Tag;

            item.BackColor = Color.Transparent;

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
                vid.date_format = DateTime.Now;
                vid.fps60 = vidInfo.Formats.Exists(f => f.Fps > 30);
                repos.InsertOrUpdate(vid);
                UpdateLVItem(item, vid);
            }

            ++freeBW_number;

            if (queueItem.Count > 0)
                download_next_vid((BackgroundWorker)sender);
            else
                backgroundworker_free();
        }
        private void backgroundworker_free()
        {
            if (freeBW_number == bws.Length)
                SetStatusSuccess("Complete loading video.");
        }
        
        YoutubeDlInfo LoadVideoInfo(string vidID)
        {
            //System.Threading.Thread.Sleep(500);
            //return new YoutubeDlInfo { error = true };

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
        bool ItemInProcessing(ListViewItem item)
        {
            return Regex.IsMatch(item.SubItems["status"].Text, "running|waiting");
        }
        void EnqueueItem(ListViewItem item)
        {
            if (ItemInProcessing(item))
                return;

            item.SubItems["status"].Text = "waiting";
            queueItem.Enqueue(item);
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

        void BeginAsync(Action action, string beginText, string endText)
        {
            SetStatusError(beginText);
            action.BeginInvoke(new AsyncCallback(CompleteAsyncCallback), endText);
        }
        void CompleteAsyncCallback(IAsyncResult result)
        {
            if (lbStatus.InvokeRequired)
                lbStatus.Invoke(new AsyncCallback(CompleteAsyncCallback), result);
            else
            {
                string endText = (string)result.AsyncState;
                SetStatusSuccess(endText);
            }
        }

        void SetStatusError(string text)
        {
            lbStatus.BackColor = Color.IndianRed;
            lbStatus.Text = text;
        }
        void SetStatusSuccess(string text)
        {
            lbStatus.BackColor = Color.DodgerBlue;
            lbStatus.Text = text;
        }
    }
}
