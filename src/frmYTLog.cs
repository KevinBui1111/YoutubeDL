using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YoutubeDL
{
    public partial class frmYTLog : Form
    {
        public frmYTLog()
        {
            InitializeComponent();
        }

        private static frmYTLog openForm = null;

        // No need for locking - you'll be doing all this on the UI thread...
        public static frmYTLog GetInstance(IWin32Window parent)
        {
            if (openForm == null)
            {
                openForm = new frmYTLog();
                openForm.FormClosed += delegate { openForm = null; };
                openForm.Show(parent);
            }
            //else openForm.BringToFront();

            return openForm;
        }
        public void AddLog(string log)
        {
            txtLog.AppendText(log + "\n");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
    }
}
