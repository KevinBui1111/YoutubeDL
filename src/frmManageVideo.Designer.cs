﻿namespace YoutubeDL
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
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.btnRemoveMissing = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnWrongDel = new System.Windows.Forms.Button();
            this.ckCompletedVideo = new System.Windows.Forms.CheckBox();
            this.cbColornew = new System.Windows.Forms.ComboBox();
            this.olvDownload = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColFolder = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
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
            this.lbStatus.Location = new System.Drawing.Point(12, 508);
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
            this.cbColornew.Location = new System.Drawing.Point(161, 39);
            this.cbColornew.Name = "cbColornew";
            this.cbColornew.Size = new System.Drawing.Size(100, 23);
            this.cbColornew.TabIndex = 8;
            // 
            // olvDownload
            // 
            this.olvDownload.AllColumns.Add(this.olvColumn1);
            this.olvDownload.AllColumns.Add(this.olvColumn2);
            this.olvDownload.AllColumns.Add(this.olvColSize);
            this.olvDownload.AllColumns.Add(this.olvColumn4);
            this.olvDownload.AllColumns.Add(this.olvColFolder);
            this.olvDownload.AllColumns.Add(this.olvColumn6);
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
            this.olvDownload.IsSimpleDragSource = true;
            this.olvDownload.Location = new System.Drawing.Point(12, 68);
            this.olvDownload.Name = "olvDownload";
            this.olvDownload.ShowGroups = false;
            this.olvDownload.Size = new System.Drawing.Size(535, 437);
            this.olvDownload.TabIndex = 13;
            this.olvDownload.UseCompatibleStateImageBehavior = false;
            this.olvDownload.View = System.Windows.Forms.View.Details;
            this.olvDownload.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvDownload_FormatRow);
            this.olvDownload.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.olvDownload_ItemDrag);
            this.olvDownload.KeyUp += new System.Windows.Forms.KeyEventHandler(this.olvDownload_KeyUp);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "filename";
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
            // frmManageVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 530);
            this.Controls.Add(this.olvDownload);
            this.Controls.Add(this.ckCompletedVideo);
            this.Controls.Add(this.btnWrongDel);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnRemoveMissing);
            this.Controls.Add(this.cbColornew);
            this.Controls.Add(this.cbChannel);
            this.Controls.Add(this.cbGroup);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmManageVideo";
            this.Text = "ManageVideo";
            this.Load += new System.EventHandler(this.ManageVideo_Load);
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
        private BrightIdeasSoftware.ObjectListView olvDownload;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColSize;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColFolder;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
    }
}