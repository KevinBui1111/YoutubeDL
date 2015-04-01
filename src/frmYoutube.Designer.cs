namespace YoutubeDL
{
    partial class frmYoutube
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnParse = new System.Windows.Forms.Button();
            this.lvVideo = new System.Windows.Forms.ListView();
            this.colFID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCodec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAudio = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDownload = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClick = new System.Windows.Forms.Button();
            this.btnLoadVid = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.ckAutoparse = new System.Windows.Forms.CheckBox();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(508, 12);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(87, 27);
            this.btnParse.TabIndex = 0;
            this.btnParse.Text = "Download";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lvVideo
            // 
            this.lvVideo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvVideo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFID,
            this.colRes,
            this.colCodec,
            this.colFps,
            this.colSize});
            this.lvVideo.FullRowSelect = true;
            this.lvVideo.GridLines = true;
            this.lvVideo.HideSelection = false;
            this.lvVideo.Location = new System.Drawing.Point(12, 156);
            this.lvVideo.MultiSelect = false;
            this.lvVideo.Name = "lvVideo";
            this.lvVideo.Size = new System.Drawing.Size(304, 202);
            this.lvVideo.TabIndex = 1;
            this.lvVideo.UseCompatibleStateImageBehavior = false;
            this.lvVideo.View = System.Windows.Forms.View.Details;
            this.lvVideo.Click += new System.EventHandler(this.lvVideo_Click);
            this.lvVideo.DoubleClick += new System.EventHandler(this.lvVideo_DoubleClick);
            // 
            // colFID
            // 
            this.colFID.Text = "FID";
            this.colFID.Width = 35;
            // 
            // colRes
            // 
            this.colRes.Text = "Resolution";
            this.colRes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colRes.Width = 76;
            // 
            // colCodec
            // 
            this.colCodec.Text = "Codec";
            this.colCodec.Width = 47;
            // 
            // colFps
            // 
            this.colFps.Text = "fps";
            this.colFps.Width = 32;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSize.Width = 78;
            // 
            // lvAudio
            // 
            this.lvAudio.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvAudio.FullRowSelect = true;
            this.lvAudio.GridLines = true;
            this.lvAudio.HideSelection = false;
            this.lvAudio.Location = new System.Drawing.Point(12, 45);
            this.lvAudio.MultiSelect = false;
            this.lvAudio.Name = "lvAudio";
            this.lvAudio.Size = new System.Drawing.Size(304, 105);
            this.lvAudio.TabIndex = 1;
            this.lvAudio.UseCompatibleStateImageBehavior = false;
            this.lvAudio.View = System.Windows.Forms.View.Details;
            this.lvAudio.DoubleClick += new System.EventHandler(this.lvVideo_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "FID";
            this.columnHeader1.Width = 33;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Codec";
            this.columnHeader3.Width = 61;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "bitrate";
            this.columnHeader4.Width = 61;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            this.columnHeader5.Width = 71;
            // 
            // lvDownload
            // 
            this.lvDownload.AllowDrop = true;
            this.lvDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDownload.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader11,
            this.columnHeader9,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader10,
            this.colGroup});
            this.lvDownload.FullRowSelect = true;
            this.lvDownload.GridLines = true;
            this.lvDownload.HideSelection = false;
            this.lvDownload.Location = new System.Drawing.Point(322, 45);
            this.lvDownload.Name = "lvDownload";
            this.lvDownload.Size = new System.Drawing.Size(366, 313);
            this.lvDownload.TabIndex = 1;
            this.lvDownload.UseCompatibleStateImageBehavior = false;
            this.lvDownload.View = System.Windows.Forms.View.Details;
            this.lvDownload.Click += new System.EventHandler(this.lvDownload_Click);
            this.lvDownload.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvDownload_DragDrop);
            this.lvDownload.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvDownload_DragEnter);
            this.lvDownload.DoubleClick += new System.EventHandler(this.lvDownload_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "VID";
            this.columnHeader2.Width = 55;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "status";
            this.columnHeader11.Width = 29;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "FID";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Resolution";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 71;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Ext";
            this.columnHeader7.Width = 48;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Size";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader8.Width = 71;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "File name";
            // 
            // colGroup
            // 
            this.colGroup.Text = "Group";
            // 
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(415, 12);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(87, 27);
            this.btnClick.TabIndex = 0;
            this.btnClick.Text = "Delete";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnLoadVid
            // 
            this.btnLoadVid.Location = new System.Drawing.Point(322, 12);
            this.btnLoadVid.Name = "btnLoadVid";
            this.btnLoadVid.Size = new System.Drawing.Size(87, 27);
            this.btnLoadVid.TabIndex = 0;
            this.btnLoadVid.Text = "Parse thread";
            this.btnLoadVid.UseVisualStyleBackColor = true;
            this.btnLoadVid.Click += new System.EventHandler(this.btnLoadVid_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(601, 12);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(87, 27);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // ckAutoparse
            // 
            this.ckAutoparse.AutoSize = true;
            this.ckAutoparse.Location = new System.Drawing.Point(233, 17);
            this.ckAutoparse.Name = "ckAutoparse";
            this.ckAutoparse.Size = new System.Drawing.Size(83, 19);
            this.ckAutoparse.TabIndex = 2;
            this.ckAutoparse.Text = "Auto parse";
            this.ckAutoparse.UseVisualStyleBackColor = true;
            // 
            // cbGroup
            // 
            this.cbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(106, 15);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(121, 23);
            this.cbGroup.TabIndex = 3;
            this.cbGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // frmYoutube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 370);
            this.Controls.Add(this.cbGroup);
            this.Controls.Add(this.ckAutoparse);
            this.Controls.Add(this.lvDownload);
            this.Controls.Add(this.lvAudio);
            this.Controls.Add(this.lvVideo);
            this.Controls.Add(this.btnClick);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnLoadVid);
            this.Controls.Add(this.btnParse);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmYoutube";
            this.Text = "frmYoutube";
            this.Load += new System.EventHandler(this.frmYoutube_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.ListView lvVideo;
        private System.Windows.Forms.ColumnHeader colFID;
        private System.Windows.Forms.ColumnHeader colRes;
        private System.Windows.Forms.ColumnHeader colCodec;
        private System.Windows.Forms.ColumnHeader colFps;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ListView lvAudio;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ListView lvDownload;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Button btnLoadVid;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.CheckBox ckAutoparse;
        private System.Windows.Forms.ColumnHeader colGroup;
        private System.Windows.Forms.ComboBox cbGroup;
    }
}