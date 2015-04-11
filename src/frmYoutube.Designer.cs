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
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResolution = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVidsize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col60fps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClick = new System.Windows.Forms.Button();
            this.btnLoadVid = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.ckAutoparse = new System.Windows.Forms.CheckBox();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.btnAutoSelect = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
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
            this.lvVideo.Location = new System.Drawing.Point(12, 139);
            this.lvVideo.MultiSelect = false;
            this.lvVideo.Name = "lvVideo";
            this.lvVideo.Size = new System.Drawing.Size(304, 198);
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
            this.lvAudio.Size = new System.Drawing.Size(304, 88);
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
            this.colID,
            this.colStatus,
            this.colFormat,
            this.colResolution,
            this.colExt,
            this.colVidsize,
            this.colFilename,
            this.colGroup,
            this.colDateFormat,
            this.col60fps});
            this.lvDownload.FullRowSelect = true;
            this.lvDownload.GridLines = true;
            this.lvDownload.HideSelection = false;
            this.lvDownload.Location = new System.Drawing.Point(322, 45);
            this.lvDownload.Name = "lvDownload";
            this.lvDownload.Size = new System.Drawing.Size(366, 316);
            this.lvDownload.TabIndex = 1;
            this.lvDownload.UseCompatibleStateImageBehavior = false;
            this.lvDownload.View = System.Windows.Forms.View.Details;
            this.lvDownload.Click += new System.EventHandler(this.lvDownload_Click);
            this.lvDownload.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvDownload_DragDrop);
            this.lvDownload.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvDownload_DragEnter);
            this.lvDownload.DoubleClick += new System.EventHandler(this.lvDownload_DoubleClick);
            this.lvDownload.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvDownload_KeyUp);
            // 
            // colID
            // 
            this.colID.Text = "VID";
            this.colID.Width = 55;
            // 
            // colStatus
            // 
            this.colStatus.Text = "status";
            this.colStatus.Width = 29;
            // 
            // colFormat
            // 
            this.colFormat.Text = "FID";
            // 
            // colResolution
            // 
            this.colResolution.Text = "Resolution";
            this.colResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colResolution.Width = 71;
            // 
            // colExt
            // 
            this.colExt.Text = "Ext";
            this.colExt.Width = 48;
            // 
            // colVidsize
            // 
            this.colVidsize.Text = "Size";
            this.colVidsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colVidsize.Width = 71;
            // 
            // colFilename
            // 
            this.colFilename.Text = "File name";
            // 
            // colGroup
            // 
            this.colGroup.Text = "Group";
            // 
            // colDateFormat
            // 
            this.colDateFormat.Text = "Date format";
            // 
            // col60fps
            // 
            this.col60fps.Text = "fps";
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
            this.cbGroup.Location = new System.Drawing.Point(148, 15);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(79, 23);
            this.cbGroup.TabIndex = 3;
            this.cbGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbGroup_KeyDown);
            // 
            // btnAutoSelect
            // 
            this.btnAutoSelect.Location = new System.Drawing.Point(12, 12);
            this.btnAutoSelect.Name = "btnAutoSelect";
            this.btnAutoSelect.Size = new System.Drawing.Size(130, 27);
            this.btnAutoSelect.TabIndex = 4;
            this.btnAutoSelect.Text = "Auto select format";
            this.btnAutoSelect.UseVisualStyleBackColor = true;
            this.btnAutoSelect.Click += new System.EventHandler(this.btnAutoSelect_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.BackColor = System.Drawing.Color.DodgerBlue;
            this.lbStatus.ForeColor = System.Drawing.Color.White;
            this.lbStatus.Location = new System.Drawing.Point(12, 340);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(304, 21);
            this.lbStatus.TabIndex = 5;
            this.lbStatus.Text = "label1";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmYoutube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 370);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnAutoSelect);
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
            this.Text = "YoutubeDL";
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
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colResolution;
        private System.Windows.Forms.ColumnHeader colExt;
        private System.Windows.Forms.ColumnHeader colVidsize;
        private System.Windows.Forms.ColumnHeader colFormat;
        private System.Windows.Forms.ColumnHeader colFilename;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Button btnLoadVid;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.CheckBox ckAutoparse;
        private System.Windows.Forms.ColumnHeader colGroup;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Button btnAutoSelect;
        private System.Windows.Forms.ColumnHeader colDateFormat;
        private System.Windows.Forms.ColumnHeader col60fps;
        private System.Windows.Forms.Label lbStatus;
    }
}