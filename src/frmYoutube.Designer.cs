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
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAudio = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClick = new System.Windows.Forms.Button();
            this.btnLoadVid = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.btnAutoSelect = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangePath = new System.Windows.Forms.Button();
            this.btnCheckFormat = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lvDownload = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColGroup = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColFps = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnYtdl = new System.Windows.Forms.Button();
            this.btnMp4Format = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lvDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(489, 12);
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
            this.colSize,
            this.columnHeader2});
            this.lvVideo.FullRowSelect = true;
            this.lvVideo.GridLines = true;
            this.lvVideo.HideSelection = false;
            this.lvVideo.Location = new System.Drawing.Point(12, 139);
            this.lvVideo.MultiSelect = false;
            this.lvVideo.Name = "lvVideo";
            this.lvVideo.Size = new System.Drawing.Size(285, 289);
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
            this.colSize.Text = "Mbps";
            this.colSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSize.Width = 78;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
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
            this.lvAudio.Location = new System.Drawing.Point(12, 166);
            this.lvAudio.MultiSelect = false;
            this.lvAudio.Name = "lvAudio";
            this.lvAudio.Size = new System.Drawing.Size(285, 88);
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
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(396, 12);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(87, 27);
            this.btnClick.TabIndex = 0;
            this.btnClick.Text = "Delete";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnLoadVid
            // 
            this.btnLoadVid.Location = new System.Drawing.Point(303, 12);
            this.btnLoadVid.Name = "btnLoadVid";
            this.btnLoadVid.Size = new System.Drawing.Size(87, 27);
            this.btnLoadVid.TabIndex = 0;
            this.btnLoadVid.Text = "Load format";
            this.btnLoadVid.UseVisualStyleBackColor = true;
            this.btnLoadVid.Click += new System.EventHandler(this.btnLoadVid_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(582, 12);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(87, 27);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // cbGroup
            // 
            this.cbGroup.Location = new System.Drawing.Point(155, 15);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(142, 23);
            this.cbGroup.TabIndex = 3;
            this.cbGroup.TextUpdate += new System.EventHandler(this.cbGroup_TextUpdate);
            this.cbGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbGroup_KeyDown);
            this.cbGroup.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbGroup_KeyUp);
            // 
            // btnAutoSelect
            // 
            this.btnAutoSelect.Location = new System.Drawing.Point(106, 45);
            this.btnAutoSelect.Name = "btnAutoSelect";
            this.btnAutoSelect.Size = new System.Drawing.Size(80, 27);
            this.btnAutoSelect.TabIndex = 4;
            this.btnAutoSelect.Text = "Auto format";
            this.btnAutoSelect.UseVisualStyleBackColor = true;
            this.btnAutoSelect.Click += new System.EventHandler(this.btnAutoSelect_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.BackColor = System.Drawing.Color.DodgerBlue;
            this.lbStatus.ForeColor = System.Drawing.Color.White;
            this.lbStatus.Location = new System.Drawing.Point(12, 431);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(285, 21);
            this.lbStatus.TabIndex = 5;
            this.lbStatus.Text = "label1";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbChannel
            // 
            this.cbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannel.FormattingEnabled = true;
            this.cbChannel.Location = new System.Drawing.Point(12, 15);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Size = new System.Drawing.Size(137, 23);
            this.cbChannel.TabIndex = 6;
            this.cbChannel.SelectedIndexChanged += new System.EventHandler(this.cbChannel_SelectedIndexChanged);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 110);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(253, 23);
            this.txtPath.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Download Path";
            // 
            // btnChangePath
            // 
            this.btnChangePath.AutoSize = true;
            this.btnChangePath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnChangePath.Location = new System.Drawing.Point(271, 108);
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.Size = new System.Drawing.Size(26, 25);
            this.btnChangePath.TabIndex = 9;
            this.btnChangePath.Text = "...";
            this.btnChangePath.UseVisualStyleBackColor = true;
            this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
            // 
            // btnCheckFormat
            // 
            this.btnCheckFormat.Location = new System.Drawing.Point(201, 44);
            this.btnCheckFormat.Name = "btnCheckFormat";
            this.btnCheckFormat.Size = new System.Drawing.Size(96, 27);
            this.btnCheckFormat.TabIndex = 10;
            this.btnCheckFormat.Text = "Check format";
            this.btnCheckFormat.UseVisualStyleBackColor = true;
            this.btnCheckFormat.Click += new System.EventHandler(this.btnCheckFormat_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 27);
            this.button1.TabIndex = 10;
            this.button1.Text = "Video Manager";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnVidMan_Click);
            // 
            // lvDownload
            // 
            this.lvDownload.AllColumns.Add(this.olvColumn1);
            this.lvDownload.AllColumns.Add(this.olvColStatus);
            this.lvDownload.AllColumns.Add(this.olvColGroup);
            this.lvDownload.AllColumns.Add(this.olvColumn4);
            this.lvDownload.AllColumns.Add(this.olvColumn5);
            this.lvDownload.AllColumns.Add(this.olvColFps);
            this.lvDownload.AllColumns.Add(this.olvColSize);
            this.lvDownload.AllColumns.Add(this.olvColumn8);
            this.lvDownload.AllColumns.Add(this.olvColDate);
            this.lvDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDownload.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColStatus,
            this.olvColGroup,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColFps,
            this.olvColSize,
            this.olvColumn8,
            this.olvColDate});
            this.lvDownload.FullRowSelect = true;
            this.lvDownload.GridLines = true;
            this.lvDownload.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDownload.HideSelection = false;
            this.lvDownload.IsSimpleDropSink = true;
            this.lvDownload.Location = new System.Drawing.Point(303, 45);
            this.lvDownload.Name = "lvDownload";
            this.lvDownload.ShowGroups = false;
            this.lvDownload.Size = new System.Drawing.Size(366, 407);
            this.lvDownload.TabIndex = 11;
            this.lvDownload.UseCompatibleStateImageBehavior = false;
            this.lvDownload.View = System.Windows.Forms.View.Details;
            this.lvDownload.VirtualMode = true;
            this.lvDownload.CanDrop += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.lvDownload_CanDrop);
            this.lvDownload.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.lvDownload_Dropped);
            this.lvDownload.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.lvDownload_FormatRow);
            this.lvDownload.Click += new System.EventHandler(this.lvDownload_Click);
            this.lvDownload.DoubleClick += new System.EventHandler(this.lvDownload_DoubleClick);
            this.lvDownload.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvDownload_KeyUp);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "vid";
            this.olvColumn1.Text = "VID";
            // 
            // olvColStatus
            // 
            this.olvColStatus.AspectName = "status";
            this.olvColStatus.Text = "status";
            this.olvColStatus.Width = 25;
            // 
            // olvColGroup
            // 
            this.olvColGroup.AspectName = "group";
            this.olvColGroup.Text = "Group";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "resolution";
            this.olvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Text = "Resolution";
            this.olvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Width = 75;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "ext";
            this.olvColumn5.Text = "Ext";
            this.olvColumn5.Width = 48;
            // 
            // olvColFps
            // 
            this.olvColFps.AspectName = "fps60";
            this.olvColFps.Text = "fps";
            this.olvColFps.Width = 28;
            // 
            // olvColSize
            // 
            this.olvColSize.AspectName = "size";
            this.olvColSize.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColSize.Text = "Size";
            this.olvColSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "title";
            this.olvColumn8.Text = "Title";
            // 
            // olvColDate
            // 
            this.olvColDate.AspectName = "date_format";
            this.olvColDate.Text = "Date format";
            // 
            // btnYtdl
            // 
            this.btnYtdl.Location = new System.Drawing.Point(106, 77);
            this.btnYtdl.Name = "btnYtdl";
            this.btnYtdl.Size = new System.Drawing.Size(80, 27);
            this.btnYtdl.TabIndex = 10;
            this.btnYtdl.Text = "Youtube-dl";
            this.btnYtdl.UseVisualStyleBackColor = true;
            this.btnYtdl.Click += new System.EventHandler(this.btnYtdl_Click);
            // 
            // btnMp4Format
            // 
            this.btnMp4Format.Location = new System.Drawing.Point(12, 45);
            this.btnMp4Format.Name = "btnMp4Format";
            this.btnMp4Format.Size = new System.Drawing.Size(80, 27);
            this.btnMp4Format.TabIndex = 4;
            this.btnMp4Format.Text = "Check Mp4";
            this.btnMp4Format.UseVisualStyleBackColor = true;
            this.btnMp4Format.Click += new System.EventHandler(this.btnMp4Format_Click);
            // 
            // frmYoutube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 461);
            this.Controls.Add(this.lvDownload);
            this.Controls.Add(this.btnYtdl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCheckFormat);
            this.Controls.Add(this.btnChangePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.cbChannel);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnMp4Format);
            this.Controls.Add(this.btnAutoSelect);
            this.Controls.Add(this.cbGroup);
            this.Controls.Add(this.lvVideo);
            this.Controls.Add(this.btnClick);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnLoadVid);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.lvAudio);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmYoutube";
            this.Text = "YoutubeDL v2.68 build 02/02/2016";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmYoutube_FormClosed);
            this.Load += new System.EventHandler(this.frmYoutube_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvDownload)).EndInit();
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
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Button btnLoadVid;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Button btnAutoSelect;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangePath;
        private System.Windows.Forms.Button btnCheckFormat;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.FastObjectListView lvDownload;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColStatus;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColFps;
        private BrightIdeasSoftware.OLVColumn olvColSize;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColDate;
        private BrightIdeasSoftware.OLVColumn olvColGroup;
        private System.Windows.Forms.Button btnYtdl;
        private System.Windows.Forms.Button btnMp4Format;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}