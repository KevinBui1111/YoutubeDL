using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace YoutubeDL
{
    public partial class frmYTdl : Form
    {
        static frmYTdl openForm = null;
        // No need for locking - you'll be doing all this on the UI thread...
        public static void ShowInstance()
        {
            if (openForm == null)
            {
                openForm = new frmYTdl();
                openForm.FormClosed += delegate { openForm = null; };
                openForm.Show();
            }
        }

        public frmYTdl()
        {
            InitializeComponent();
        }
        Process p;
        private void btnFormat_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            p = new Process
            {
                StartInfo =
                {
                    FileName = "youtube-dl",
                    Arguments = sender == btnFormat ?
                        string.Format("-F {0}", txtID.Text) :
                        string.Format("-f {0} {1}", txtFormat.Text, txtID.Text),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                SynchronizingObject = this
            };
            p.OutputDataReceived += p_OutputDataReceived;
            p.ErrorDataReceived += p_OutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            Clipboard.SetText(p.StartInfo.FileName + " " + p.StartInfo.Arguments);
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
                txtOutput.AppendText(e.Data + Environment.NewLine);
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                if (e.Data.StartsWith("[download]  "))
                {
                    txtOutput.Lines = txtOutput.Lines.Take(txtOutput.Lines.Length - 1).ToArray();
                    txtOutput.AppendText(Environment.NewLine + e.Data);
                }
                else
                    txtOutput.AppendText(e.Data + Environment.NewLine);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (p != null && !p.HasExited)
                p.Kill();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            var vid = Clipboard.GetText();
            txtID.Text = vid.Replace(frmYoutube.YoutubeLink, null);
        }
    }
}
