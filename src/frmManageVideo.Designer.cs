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
            this.lvDownload = new System.Windows.Forms.ListView();
            this.colFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVidsize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResolution = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.btnRemoveMissing = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvDownload
            // 
            this.lvDownload.AllowDrop = true;
            this.lvDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDownload.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFilename,
            this.colExt,
            this.colVidsize,
            this.colResolution,
            this.colFolder,
            this.colID});
            this.lvDownload.FullRowSelect = true;
            this.lvDownload.GridLines = true;
            this.lvDownload.HideSelection = false;
            this.lvDownload.Location = new System.Drawing.Point(12, 42);
            this.lvDownload.Name = "lvDownload";
            this.lvDownload.Size = new System.Drawing.Size(463, 233);
            this.lvDownload.TabIndex = 2;
            this.lvDownload.UseCompatibleStateImageBehavior = false;
            this.lvDownload.View = System.Windows.Forms.View.Details;
            this.lvDownload.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvDownload_ItemDrag);
            this.lvDownload.DoubleClick += new System.EventHandler(this.lvDownload_DoubleClick);
            this.lvDownload.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvDownload_KeyUp);
            // 
            // colFilename
            // 
            this.colFilename.Text = "File name";
            this.colFilename.Width = 193;
            // 
            // colExt
            // 
            this.colExt.Text = "Ext";
            this.colExt.Width = 36;
            // 
            // colVidsize
            // 
            this.colVidsize.Text = "Size";
            this.colVidsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colVidsize.Width = 53;
            // 
            // colResolution
            // 
            this.colResolution.Text = "Resolution";
            this.colResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colResolution.Width = 71;
            // 
            // colFolder
            // 
            this.colFolder.Text = "Folder";
            // 
            // colID
            // 
            this.colID.Text = "VID";
            this.colID.Width = 55;
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
            this.lbStatus.Location = new System.Drawing.Point(12, 280);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(38, 15);
            this.lbStatus.TabIndex = 10;
            this.lbStatus.Text = "label1";
            // 
            // frmManageVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 302);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnRemoveMissing);
            this.Controls.Add(this.cbChannel);
            this.Controls.Add(this.cbGroup);
            this.Controls.Add(this.lvDownload);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmManageVideo";
            this.Text = "ManageVideo";
            this.Load += new System.EventHandler(this.ManageVideo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDownload;
        private System.Windows.Forms.ColumnHeader colFilename;
        private System.Windows.Forms.ColumnHeader colExt;
        private System.Windows.Forms.ColumnHeader colVidsize;
        private System.Windows.Forms.ColumnHeader colResolution;
        private System.Windows.Forms.ColumnHeader colFolder;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Button btnRemoveMissing;
        private System.Windows.Forms.Label lbStatus;
    }
}