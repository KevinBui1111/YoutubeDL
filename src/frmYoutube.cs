﻿using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeDL.Models;
using YoutubeDL.Properties;

namespace YoutubeDL
{
    public partial class frmYoutube : Form
    {
        const string namePattern = "[\\\\/?:*\"><|]",
            file_name_format = "{0}_{1}.{2}",
            ffmpeg_format = "-i \"{0}\" -i \"{1}\" -vcodec copy -acodec copy -y \"{2}\"";
        public const string YoutubeLink = "https://www.youtube.com/watch?v=";

        RepositoryLite repos;
        DownloadVid currentVid;
        IEnumerable<string> suggestGroup;
        static string vidFolder = (string)Settings.Default["DownloadPath"];
        public static Dictionary<int, Channel> dicChannel;

        List<Task> tasks;
        Task mergeTask;
        ConcurrentQueue<DownloadVid> queueItem = new ConcurrentQueue<DownloadVid>();
        CancellationTokenSource cancelTokenSource;

        Color DELETED = Color.DarkGray;
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
            ListViewHelper.EnableDoubleBuffer(lvVideo);
            ListViewHelper.EnableDoubleBuffer(lvAudio);

            olvColGroup.DisplayIndex = 0;
            olvColStatus.AspectGetter = delegate(object rowObject)
            {
                var vid = (DownloadVid)rowObject;
                switch (vid.downloadstatus)
                {
                    case 1: return "waiting";
                    case 2: return "running";
                    default: return vid.status.ToString();
                }
            };
            olvColSize.AspectToStringConverter = delegate(object size) { return ((long?)size).ToReadableSize(); };
            olvColFps.AspectToStringConverter = delegate(object fps) { return (bool)fps ? "60" : null; };
            olvColDate.AspectToStringConverter = delegate(object date_format) { return ((DateTime?)date_format).ToHumanDate(); };

            colorStatus = new Dictionary<int, Color>();
            colorStatus.Add(-1, DELETED);
            colorStatus.Add(0, UNLOADED);
            colorStatus.Add(1, LOADED);
            colorStatus.Add(2, FORMATTED);
            colorStatus.Add(3, DOWNLOADING);
            colorStatus.Add(4, COMPLETE);

            repos = new RepositoryLite();

            // load channel list
            var channels = repos.Get_Channel_list();
            dicChannel = channels.ToDictionary(s => s.id);

            cbChannel.Items.Add(new Channel { id = 0, name = "All" });
            cbChannel.Items.AddRange(channels);
            cbChannel.SelectedIndex = 0;

            txtPath.Text = vidFolder;
        }
        private void frmYoutube_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmManageVideo.CloseInstance();
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

            UpdateFormat(currentVid, vF, aF);
            lvDownload.RefreshObject(currentVid);
            repos.UpdateFormat(currentVid);
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

            if (lvDownload.SelectedIndices.Count == 0) return;

            SetStatusSuccess(lvDownload.SelectedIndices.Count >= 2 ?
                string.Format("{0} - {1} videos / Total: {2} - {3} video",
                    lvDownload.SelectedObjects.Cast<DownloadVid>().Sum(v => v.size).ToReadableSize(),
                    lvDownload.SelectedIndices.Count,
                    lvDownload.Objects.Cast<DownloadVid>().Sum(item => item.size).ToReadableSize(),
                    lvDownload.GetItemCount()
                    )
                :
                string.Format("Total: {0} - {1} videos",
                    lvDownload.Objects.Cast<DownloadVid>().Sum(item => item.size).ToReadableSize(),
                    lvDownload.GetItemCount()
                    )
            );

            currentVid = (DownloadVid)lvDownload.SelectedObjects[0];
            cbGroup.Text = currentVid.group;

            if (string.IsNullOrEmpty(currentVid.jsonYDL)) return;

            var vidInfo = JsonConvert.DeserializeObject<YoutubeDlInfo>(currentVid.jsonYDL);
            var vidFormats = vidInfo.Formats.Where(f => f.Format_Note == "DASH video").OrderByDescending(f => f.FileSize);
            var audFormats = vidInfo.Formats.Where(f => f.Format_Note == "DASH audio" && f.FileSize.HasValue).OrderByDescending(f => f.FileSize);

            var listitem = new List<ListViewItem>();
            foreach (Formats f in vidFormats)
            {
                try
                {
                    ListViewItem item = new ListViewItem(new string[]{
                        f.Format_Id, f.Width + " x " + f.Height, f.Ext, f.Fps > 30 ? f.Fps.ToString() : null, (f.FileSize * 8.0 / vidInfo.Duration / 1024 / 1024).Value.ToString("0.##") + "Mbps", f.FileSize.ToReadableSize()
                    });
                    item.Name = f.Format_Id;
                    item.Tag = f;
                    item.Selected = currentVid.vidFID == f.Format_Id;

                    listitem.Add(item);
                }
                catch { }
            }

            lvVideo.Items.AddRange(listitem.ToArray());

            listitem.Clear();
            foreach (Formats f in audFormats)
            {
                ListViewItem item = new ListViewItem(new string[]{
                    f.Format_Id, f.Ext, f.Abr.ToString(), f.FileSize.ToReadableSize()
                });
                item.Name = f.Format_Id;
                item.Tag = f;
                item.Selected = currentVid.audFID == f.Format_Id;

                listitem.Add(item);
            }

            lvAudio.Items.AddRange(listitem.ToArray());
        }
        private void lvDownload_DoubleClick(object sender, EventArgs e)
        {
            currentVid = (DownloadVid)lvDownload.SelectedObjects[0];
            Clipboard.SetText(YoutubeLink + currentVid.vid);
        }
        private void lvDownload_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            DownloadVid vid = (DownloadVid)e.Model;
            e.Item.ForeColor = vid.downloadstatus == 3 ? Color.Red : colorStatus[vid.status];
            e.Item.BackColor = (vid.downloadstatus == 2) ? Color.LightBlue : Color.Transparent;
        }

        private void lvDownload_Dropped(object sender, BrightIdeasSoftware.OlvDropEventArgs e)
        {
            lvDownload.DeselectAll();

            var channel = (Channel)cbChannel.SelectedItem;
            string web_url = (string)((DataObject)e.DataObject).GetData(DataFormats.Text);
            string[] vidIDs = YoutubeDlInfo.GetVideoIDFromUrl(web_url);
            foreach (string id in vidIDs)
            {
                var vid = repos.GetVideo(id);
                if (vid == null)
                {
                    vid = new DownloadVid { vid = id, group = (string)cbGroup.Text, channel_id = channel.id, date_add = DateTime.Now };
                    repos.Insert(vid);
                    lvDownload.AddObject(vid);
                }
                else if (vid.status == -1)
                {
                    vid.status = 0;
                    repos.UpdateStatus(vid);
                    lvDownload.AddObject(vid);
                }
            }

            lvDownload.Items[lvDownload.Items.Count - 1].EnsureVisible();
            lvDownload.Items[lvDownload.Items.Count - 1].Selected = true;
        }
        private void lvDownload_CanDrop(object sender, BrightIdeasSoftware.OlvDropEventArgs e)
        {
            if (((DataObject)e.DataObject).GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
        }

        private void btnLoadVid_Click(object sender, EventArgs e)
        {
            // check any worker is running
            if (isLoadingVid())
            {
                DownloadVid vid;
                List<DownloadVid> vids = new List<DownloadVid>();
                while (queueItem.TryDequeue(out vid))
                {
                    vid.downloadstatus = 0;
                    vids.Add(vid);
                }
                lvDownload.RefreshObjects(vids);

                return;
            }

            // check connection
            if (!Helper.CheckForYoutubeConnection())
            {
                SetStatusError("Can't connect to Youtube!");
                return;
            }

            // check any item remain in queue
            if (queueItem.Count == 0)
            {
                IEnumerable<DownloadVid> itemParse;
                if (lvDownload.SelectedIndices.Count == 0)
                    itemParse = lvDownload.Objects.Cast<DownloadVid>().Where(vid => vid.status == 0);
                else
                    itemParse = lvDownload.SelectedObjects.Cast<DownloadVid>();

                EnqueueItem(itemParse);
            }

            download_vid_format_In_Queue();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to remove videos?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            foreach (DownloadVid item in lvDownload.SelectedObjects)
            {
                repos.DeleteVid(item.vid);
                lvDownload.RemoveObject(item);
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            var itemDown = lvDownload.SelectedIndices.Count == 0 ?
                lvDownload.Objects.Cast<DownloadVid>() : lvDownload.SelectedObjects.Cast<DownloadVid>();

            string idm_format = "-a /d {0} /p \"{1}\" /f \"{2}\"";

            repos.BeginUpdate();
            foreach (var vid in itemDown.ToArray())
            {
                if (vid.status != 2 && vid.status != 3) continue;

                string download_path = Path.Combine(vidFolder, dicChannel[vid.channel_id].folder);
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

                if (vid.status != 3)
                {
                    vid.status = 3;
                    repos.UpdateStatus(vid);
                }
                lvDownload.RefreshObject(vid);
            }
            BeginAsync(repos.Commit, "Saving video info...", "Saved successful.");
        }
        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (mergeTask != null && !mergeTask.IsCompleted)
                cancelTokenSource.Cancel();
            else
            {
                SetStatusError("Start merging video...");
                btnMerge.Text = "Stop";
                cancelTokenSource = new CancellationTokenSource();
                mergeTask = Task.Factory.StartNew(delegate { task_merging(cancelTokenSource.Token); })
                    .ContinueWith(delegate
                    {
                        SetStatusSuccess("Merging video success.");
                        btnMerge.Text = "Merge";
                    }
                    , TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }
        private void btnAutoSelect_Click(object sender, EventArgs e)
        {
            IEnumerable<DownloadVid> itemParse;
            if (lvDownload.SelectedIndices.Count == 0)
                itemParse = lvDownload.Objects.Cast<DownloadVid>().Where(vid => vid.status == 1);
            else
                itemParse = lvDownload.SelectedObjects.Cast<DownloadVid>();

            //long diffSize = 50 * 1024 * 1024;

            repos.BeginUpdate();
            lvDownload.BeginUpdate();
            List<DownloadVid> errorItem = new List<DownloadVid>();
            foreach (DownloadVid vid in itemParse.ToArray())
            {
                var vidInfo = JsonConvert.DeserializeObject<YoutubeDlInfo>(vid.jsonYDL);
                if (vidInfo == null) continue;

                //var maxWebm = vidInfo.Formats.Where(f => f.Format_Note == "DASH video" && f.Ext == "webm").OrderByDescending(f => f.FileSize).FirstOrDefault();
                var maxMp4 = vidInfo.Formats.Where(f => f.Format_Note == "DASH video" && f.Ext == "mp4").OrderByDescending(f => f.FileSize).FirstOrDefault();

                Formats vF = maxMp4;

                //if (maxWebm == null) vF = maxMp4;
                //else if (maxMp4 == null) vF = maxWebm;
                //else if (maxMp4.FileSize + diffSize < maxWebm.FileSize) vF = maxWebm;
                //else vF = maxMp4;

                var aF = vidInfo.Formats.OrderByDescending(f => f.FileSize).FirstOrDefault(f => f.Format_Note == "DASH audio" && f.FileSize.HasValue && f.Ext == (vF.Ext == "webm" ? "webm" : "m4a"));
                if (aF == null)
                    errorItem.Add(vid);
                else
                {
                    UpdateFormat(vid, vF, aF);
                    lvDownload.RefreshObject(vid);
                    repos.UpdateFormat(vid);
                }
            }
            lvDownload.EndUpdate();
            repos.Commit();
            //BeginAsync(repos.Commit, "Saving video info...", "Saved successful.");

            if (errorItem.Count != 0
                && MessageBox.Show(string.Format(
                        "Following item has corrupt format: {0}\n\nDo you want to reload format for them?",
                        string.Join(", ", errorItem.Select(it => it.vid).ToArray())
                    ),
                    "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes)
            {
                EnqueueItem(errorItem);

                download_vid_format_In_Queue(true);
            }

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
        private void cbGroup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                cbGroup.Text = cbGroup.Text.Substring(0, cbGroup.SelectionStart);
                cbGroup.SelectionStart = cbGroup.Text.Length;
            }
        }
        private void cbGroup_TextUpdate(object sender, EventArgs e)
        {
            var txt = cbGroup.Text;
            string suggest = suggestGroup.FirstOrDefault(s => s.ToLower().StartsWith(txt.ToLower()));
            if (!string.IsNullOrEmpty(suggest))
            {
                cbGroup.Text = suggest;
                cbGroup.SelectionStart = txt.Length;
                cbGroup.SelectionLength = suggest.Length - txt.Length;
            }
        }
        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var channel = (Channel)cbChannel.SelectedItem;
            LoadVideoChannel(channel.id, "All");

            lvDownload.AllowDrop = channel.id > 0;
        }

        public static string getFullfilename(DownloadVid vid)
        {
            return Path.Combine(vidFolder, dicChannel[vid.channel_id].folder, vid.group ?? "", vid.filename);
        }

        void download_vid_format_In_Queue(bool single_thread = false)
        {
            if (queueItem.Count == 0) return;

            SetStatusError("Start loading video...");
            btnLoadVid.Text = "Stop";

            int num_thread = single_thread ? 1 : 4;
            tasks = new List<Task>();
            for (int i = 0; i < num_thread; ++i)
                tasks.Add(Task.Factory.StartNew(() => task_load_vid()));

            Task.Factory.ContinueWhenAll(tasks.ToArray(), (res) =>
            {
                SetStatusSuccess("Complete loading video.");
                btnLoadVid.Text = "Load format";
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
        bool isLoadingVid()
        {
            return tasks != null && tasks.Any(t => !t.IsCompleted);
        }

        void task_load_vid()
        {
            DownloadVid vid;
            while (queueItem.TryDequeue(out vid))
            {
                vid.downloadstatus = 2; //running
                lvDownload.RefreshObject(vid);
                this.Invoke((MethodInvoker)delegate { lvDownload.EnsureModelVisible(vid); });

                YoutubeDlInfo vidInfo = LoadVideoInfo(vid.vid);

                if (vidInfo.error)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        var frmLog = frmYTLog.GetInstance(this);
                        frmLog.AddLog(vidInfo.error_message);
                    });

                    vid.downloadstatus = 3;
                }
                else
                {
                    vid.title = vidInfo.Title;
                    vid.status = vid.status > 1 ? 2 : 1;
                    vid.jsonYDL = JsonConvert.SerializeObject(vidInfo);
                    vid.date_format = DateTime.Now;
                    vid.fps60 = vidInfo.Formats.Exists(f => f.Fps > 30);
                    vid.downloadstatus = 0;

                    repos.UpdateAfterLoading(vid);
                }

                lvDownload.RefreshObject(vid);
            }
        }
        void task_merging(CancellationToken token)
        {
            foreach (DownloadVid vid in lvDownload.Objects.Cast<DownloadVid>().ToArray())
            {
                if (token.IsCancellationRequested) return;
                if (vid.status != 3) continue;

                this.Invoke((MethodInvoker)delegate { lvDownload.EnsureModelVisible(vid); });

                string download_path = Path.Combine(vidFolder, dicChannel[vid.channel_id].folder);
                var vidFI = new FileInfo(Path.Combine(download_path, vid.vidFilename));
                var audFI = new FileInfo(Path.Combine(download_path, vid.audFilename));
                bool complete = vidFI.Exists && vidFI.Length == vid.vidSize &&
                                audFI.Exists && audFI.Length == vid.audSize;

                if (!complete) continue;

                vid.downloadstatus = 2;
                lvDownload.RefreshObject(vid);

                string desFolder = Path.Combine(download_path, vid.group ?? "");
                string desFilename = Path.Combine(desFolder, vid.filename);

                if (!Directory.Exists(desFolder)) Directory.CreateDirectory(desFolder);

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
                    this.Invoke((MethodInvoker)delegate
                    {
                        var frmLog = frmYTLog.GetInstance(this);
                        frmLog.AddLog("ERROR on video: " + vid.vid + " - " + error);
                    });
                }
                else
                {
                    vidFI.Delete();
                    audFI.Delete();

                    vid.status = 4;
                    vid.date_merge = DateTime.Now;
                    repos.UpdateAfterMerging(vid);
                }

                vid.downloadstatus = 0;
                lvDownload.RefreshObject(vid);
            }

        }

        YoutubeDlInfo LoadVideoInfo(string vidID)
        {
            //System.Threading.Thread.Sleep(rnd.Next(200, 1000));
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

                vidInfo = JsonConvert.DeserializeObject<YoutubeDlInfo>(res);

                Regex reg = new Regex(@"^\d+$");
                var vidFormats = vidInfo.Formats.Where(f => reg.IsMatch(f.Format_Id) && f.Format_Note == "DASH video").OrderByDescending(f => f.FileSize);
                var audFormats = vidInfo.Formats.Where(f => reg.IsMatch(f.Format_Id) && f.Format_Note == "DASH audio").OrderByDescending(f => f.FileSize);
                vidInfo.Formats = vidFormats.Union(audFormats).ToList();
            }

            vidInfo.error_message = sb.ToString();
            return vidInfo;
        }
        void EnqueueItem(IEnumerable<DownloadVid> items)
        {
            DownloadVid vid;
            while (queueItem.TryDequeue(out vid)) ;

            foreach (var item in items)
            {
                item.downloadstatus = 1;
                queueItem.Enqueue(item);
            }
            lvDownload.RefreshObjects(items.ToList());

        }
        void ChangeGroupVid()
        {
            string group = cbGroup.Text;
            foreach (DownloadVid vid in lvDownload.SelectedObjects)
            {
                vid.group = group;
                repos.UpdateGroup(vid);
            }
            lvDownload.RefreshObjects(lvDownload.SelectedObjects);
        }
        void UpdateFormat(DownloadVid vid, Formats vF, Formats aF)
        {
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
            vid.ext = vF.Ext;
            vid.filename = new Regex(namePattern).Replace(vid.title, "_") + "." + vF.Ext;
            vid.size = vF.FileSize + aF.FileSize;
            vid.status = 2;
        }
        void LoadVideoChannel(int channel_id, string group)
        {
            lvDownload.BeginUpdate();

            var listVid = repos.LoadDownloadVideo(channel_id, group, false);
            lvDownload.SetObjects(listVid);

            lvDownload.EndUpdate();

            // load group list to combobox
            if (channel_id == 0)
            {
                cbGroup.Items.Clear();
                cbGroup.Items.AddRange(listVid.Select(v => v.group ?? "").Distinct().OrderBy(g => g).ToArray());
                suggestGroup = listVid
                    .Where(g => !string.IsNullOrEmpty(g.group))
                    .GroupBy(g => g.group)
                    .Select(g => new { group = g.Key, count = g.Count() })
                    .OrderByDescending(g => g.count)
                    .Select(g => g.group);
            }

            lbStatus.Text = string.Format("Total {0} vids. Total size {1}", listVid.Length, listVid.Sum(v => v.size).ToReadableSize());
        }

        IAsyncResult BeginAsync(Action action, string beginText, string endText)
        {
            SetStatusError(beginText);
            return action.BeginInvoke(new AsyncCallback(CompleteAsyncCallback), endText);
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

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = vidFolder;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Settings.Default["DownloadPath"] = txtPath.Text = vidFolder = dlg.SelectedPath;
                Settings.Default.Save();
            }
        }
        private void btnCheckFormat_Click(object sender, EventArgs e)
        {
            IEnumerable<DownloadVid> itemParse;
            if (lvDownload.SelectedIndices.Count == 0)
                itemParse = lvDownload.Objects.Cast<DownloadVid>().Where(vid => vid.status >= 1);
            else
                itemParse = lvDownload.SelectedObjects.Cast<DownloadVid>();

            List<DownloadVid> errorItem = new List<DownloadVid>();
            //List<string> errorFormat = new List<string>();

            foreach (var vid in itemParse)
            {
                bool res = true;

                var vidInfo = JsonConvert.DeserializeObject<YoutubeDlInfo>(vid.jsonYDL);
                var vidFormats = vidInfo.Formats.Where(f => f.Format_Note == "DASH video").OrderByDescending(f => f.FileSize);

                foreach (Formats f in vidFormats)
                {
                    if (f.Width == null || f.Height == null || f.Fps == null || f.FileSize == null)
                    {
                        errorItem.Add(vid);
                        //errorFormat.Add(f.Format_Id);
                        res = false;
                        break;
                    }
                }

                if (!res) continue;

                if (vidInfo.Formats.Any(f => f.Format_Note == "DASH audio" && f.FileSize == null))
                    errorItem.Add(vid);
            }

            if (errorItem.Count == 0)
                MessageBox.Show("All items are proper.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (MessageBox.Show(
                string.Format(
                    "Following item has corrupt format: {0}\n\nDo you want to reload format for them?",
                    string.Join(", ", errorItem.Select(it => it.vid).ToArray())
                ),
                "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes)
            {
                EnqueueItem(errorItem);

                download_vid_format_In_Queue(true);
            }

            //Clipboard.SetText(string.Join(", ", errorFormat.Distinct().ToArray()));
        }
        private void btnVidMan_Click(object sender, EventArgs e)
        {
            frmManageVideo.ShowInstance();
        }
        private void btnYtdl_Click(object sender, EventArgs e)
        {
            frmYTdl.ShowInstance();
        }
        private void btnMp4Format_Click(object sender, EventArgs e)
        {
            IEnumerable<DownloadVid> itemParse;
            if (lvDownload.SelectedIndices.Count == 0)
                itemParse = lvDownload.Objects.Cast<DownloadVid>().Where(vid => vid.status >= 1);
            else
                itemParse = lvDownload.SelectedObjects.Cast<DownloadVid>();

            List<string> strange = new List<string>();
            foreach (var vid in itemParse)
            {
                if (vid.jsonYDL == null)
                {
                    strange.Add(vid.vid);
                    continue;
                }
                var vidInfo = JsonConvert.DeserializeObject<YoutubeDlInfo>(vid.jsonYDL);
                var vidformat = vidInfo.Formats.Where(f => f.Format_Note == "DASH video");
                var maxw = vidformat.Max(f => f.Width);
                var maxMp4 = vidformat.Where(f => f.Ext == "mp4").OrderByDescending(f => f.FileSize).FirstOrDefault();

                if (maxMp4.Width != maxw)
                    strange.Add(vid.vid);
            }

            if (strange.Count > 0)
            {
                MessageBox.Show(string.Join("\n", strange));
            }
        }
    }
}
