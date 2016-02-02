namespace YoutubeDL
{
    partial class frmManageVideo
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
            this.components = new System.ComponentModel.Container();
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.btnRemoveMissing = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnWrongDel = new System.Windows.Forms.Button();
            this.ckCompletedVideo = new System.Windows.Forms.CheckBox();
            this.cbColornew = new System.Windows.Forms.ComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ckThumbview = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColFolder = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDownload = new BrightIdeasSoftware.ObjectListView();
            this.txtAfter = new System.Windows.Forms.TextBox();
            this.dtpDateMerge = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.olvDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // cbChannel
            // 
            this.cbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannel.FormattingEnabled = true;
            this.cbChannel.Location = new System.Drawing.Point(12, 12);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Size = new System.Drawing.Size(100, 23);
            this.cbChannel.TabIndex = 8;
            this.cbChannel.SelectedIndexChanged += new System.EventHandler(this.cbChannel_SelectedIndexChanged);
            // 
            // cbGroup
            // 
            this.cbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(118, 12);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(92, 23);
            this.cbGroup.TabIndex = 7;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // btnRemoveMissing
            // 
            this.btnRemoveMissing.Location = new System.Drawing.Point(216, 10);
            this.btnRemoveMissing.Name = "btnRemoveMissing";
            this.btnRemoveMissing.Size = new System.Drawing.Size(145, 25);
            this.btnRemoveMissing.TabIndex = 9;
            this.btnRemoveMissing.Text = "Remove missing vid";
            this.btnRemoveMissing.UseVisualStyleBackColor = true;
            this.btnRemoveMissing.Click += new System.EventHandler(this.btnRemoveMissing_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(12, 785);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(38, 15);
            this.lbStatus.TabIndex = 10;
            this.lbStatus.Text = "label1";
            // 
            // btnWrongDel
            // 
            this.btnWrongDel.Location = new System.Drawing.Point(367, 10);
            this.btnWrongDel.Name = "btnWrongDel";
            this.btnWrongDel.Size = new System.Drawing.Size(108, 25);
            this.btnWrongDel.TabIndex = 11;
            this.btnWrongDel.Text = "Wrong deletion";
            this.btnWrongDel.UseVisualStyleBackColor = true;
            this.btnWrongDel.Click += new System.EventHandler(this.btnWrongDel_Click);
            // 
            // ckCompletedVideo
            // 
            this.ckCompletedVideo.AutoSize = true;
            this.ckCompletedVideo.Checked = true;
            this.ckCompletedVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckCompletedVideo.Location = new System.Drawing.Point(12, 43);
            this.ckCompletedVideo.Name = "ckCompletedVideo";
            this.ckCompletedVideo.Size = new System.Drawing.Size(143, 19);
            this.ckCompletedVideo.TabIndex = 12;
            this.ckCompletedVideo.Text = "Only completed video";
            this.ckCompletedVideo.UseVisualStyleBackColor = true;
            // 
            // cbColornew
            // 
            this.cbColornew.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColornew.FormattingEnabled = true;
            this.cbColornew.Location = new System.Drawing.Point(261, 39);
            this.cbColornew.Name = "cbColornew";
            this.cbColornew.Size = new System.Drawing.Size(100, 23);
            this.cbColornew.TabIndex = 8;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ckThumbview
            // 
            this.ckThumbview.AutoSize = true;
            this.ckThumbview.Location = new System.Drawing.Point(161, 43);
            this.ckThumbview.Name = "ckThumbview";
            this.ckThumbview.Size = new System.Drawing.Size(92, 19);
            this.ckThumbview.TabIndex = 14;
            this.ckThumbview.Text = "Thumb view";
            this.ckThumbview.UseVisualStyleBackColor = true;
            this.ckThumbview.CheckedChanged += new System.EventHandler(this.ckThumbview_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(481, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 25);
            this.button1.TabIndex = 11;
            this.button1.Text = "Rename";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "filename";
            this.olvColumn1.AspectToStringFormat = "";
            this.olvColumn1.ImageAspectName = "";
            this.olvColumn1.Text = "File name";
            this.olvColumn1.Width = 238;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ext";
            this.olvColumn2.Text = "Ext";
            this.olvColumn2.Width = 37;
            // 
            // olvColSize
            // 
            this.olvColSize.AspectName = "size";
            this.olvColSize.Text = "Size";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "resolution";
            this.olvColumn4.Text = "Resolution";
            this.olvColumn4.Width = 69;
            // 
            // olvColFolder
            // 
            this.olvColFolder.AspectName = "channel_id";
            this.olvColFolder.Text = "Folder";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "vid";
            this.olvColumn6.Text = "VID";
            // 
            // olvDownload
            // 
            this.olvDownload.AllColumns.Add(this.olvColumn1);
            this.olvDownload.AllColumns.Add(this.olvColumn2);
            this.olvDownload.AllColumns.Add(this.olvColSize);
            this.olvDownload.AllColumns.Add(this.olvColumn4);
            this.olvDownload.AllColumns.Add(this.olvColFolder);
            this.olvDownload.AllColumns.Add(this.olvColumn6);
            this.olvDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvDownload.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColSize,
            this.olvColumn4,
            this.olvColFolder,
            this.olvColumn6});
            this.olvDownload.FullRowSelect = true;
            this.olvDownload.GridLines = true;
            this.olvDownload.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvDownload.HideSelection = false;
            this.olvDownload.IsSimpleDragSource = true;
            this.olvDownload.LargeImageList = this.imageList1;
            this.olvDownload.Location = new System.Drawing.Point(12, 68);
            this.olvDownload.Name = "olvDownload";
            this.olvDownload.ShowGroups = false;
            this.olvDownload.Size = new System.Drawing.Size(713, 714);
            this.olvDownload.TabIndex = 13;
            this.olvDownload.UseCompatibleStateImageBehavior = false;
            this.olvDownload.View = System.Windows.Forms.View.Details;
            this.olvDownload.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.olvDownload_CellRightClick);
            this.olvDownload.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvDownload_FormatRow);
            this.olvDownload.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.olvDownload_ItemDrag);
            this.olvDownload.Click += new System.EventHandler(this.olvDownload_Click);
            this.olvDownload.DoubleClick += new System.EventHandler(this.olvDownload_DoubleClick);
            this.olvDownload.KeyUp += new System.Windows.Forms.KeyEventHandler(this.olvDownload_KeyUp);
            // 
            // txtAfter
            // 
            this.txtAfter.Location = new System.Drawing.Point(367, 39);
            this.txtAfter.Name = "txtAfter";
            this.txtAfter.Size = new System.Drawing.Size(108, 23);
            this.txtAfter.TabIndex = 0;
            // 
            // dtpDateMerge
            // 
            this.dtpDateMerge.Checked = false;
            this.dtpDateMerge.CustomFormat = "";
            this.dtpDateMerge.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateMerge.Location = new System.Drawing.Point(481, 39);
            this.dtpDateMerge.Name = "dtpDateMerge";
            this.dtpDateMerge.Size = new System.Drawing.Size(108, 23);
            this.dtpDateMerge.TabIndex = 15;
            // 
            // frmManageVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 807);
            this.Controls.Add(this.dtpDateMerge);
            this.Controls.Add(this.txtAfter);
            this.Controls.Add(this.ckThumbview);
            this.Controls.Add(this.olvDownload);
            this.Controls.Add(this.ckCompletedVideo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnWrongDel);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnRemoveMissing);
            this.Controls.Add(this.cbColornew);
            this.Controls.Add(this.cbChannel);
            this.Controls.Add(this.cbGroup);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmManageVideo";
            this.Text = "ManageVideo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManageVideo_FormClosed);
            this.Load += new System.EventHandler(this.frmManageVideo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.olvDownload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Button btnRemoveMissing;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btnWrongDel;
        private System.Windows.Forms.CheckBox ckCompletedVideo;
        private System.Windows.Forms.ComboBox cbColornew;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox ckThumbview;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColSize;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColFolder;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.ObjectListView olvDownload;
        private System.Windows.Forms.TextBox txtAfter;
        private System.Windows.Forms.DateTimePicker dtpDateMerge;
    }
}