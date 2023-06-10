namespace IPFSuite
{
	// Token: 0x02000025 RID: 37
	public partial class fMain : global::System.Windows.Forms.Form
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00006EAC File Offset: 0x000050AC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00006EE4 File Offset: 0x000050E4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::IPFSuite.fMain));
			this.ssStatus = new global::System.Windows.Forms.StatusStrip();
			this.tsProgress = new global::System.Windows.Forms.ToolStripProgressBar();
			this.tsslStatus = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.tsTools = new global::System.Windows.Forms.ToolStrip();
			this.tsbNew = new global::System.Windows.Forms.ToolStripButton();
			this.tsbOpen = new global::System.Windows.Forms.ToolStripButton();
			this.tsbClose = new global::System.Windows.Forms.ToolStripButton();
			this.tsbSave = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.tsbExtract = new global::System.Windows.Forms.ToolStripButton();
			this.tsbAddFile = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.tsbNewContainer = new global::System.Windows.Forms.ToolStripButton();
			this.tsbAddFolder = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.tsbPreview = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.tsbAbout = new global::System.Windows.Forms.ToolStripButton();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.pgIPFFooter = new global::System.Windows.Forms.PropertyGrid();
			this.splitter3 = new global::System.Windows.Forms.Splitter();
			this.tvDirectories = new global::System.Windows.Forms.TreeView();
			this.imageList = new global::System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new global::System.Windows.Forms.Splitter();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.lvFiles = new global::System.Windows.Forms.ListView();
			this.colName = new global::System.Windows.Forms.ColumnHeader();
			this.colSize = new global::System.Windows.Forms.ColumnHeader();
			this.colType = new global::System.Windows.Forms.ColumnHeader();
			this.tsPreviewMenu = new global::System.Windows.Forms.ToolStrip();
			this.tsbFileSave = new global::System.Windows.Forms.ToolStripButton();
			this.tsbFileCSVExport = new global::System.Windows.Forms.ToolStripButton();
			this.splitter2 = new global::System.Windows.Forms.Splitter();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.ofd = new global::System.Windows.Forms.OpenFileDialog();
			this.sfd = new global::System.Windows.Forms.SaveFileDialog();
			this.fbd = new global::System.Windows.Forms.FolderBrowserDialog();
			this.txtFilter = new global::System.Windows.Forms.ToolStripTextBox();
			this.tsbFilter = new global::System.Windows.Forms.ToolStripButton();
			this.ucPreview = new global::IPFSuite.ucPreview();
			this.ssStatus.SuspendLayout();
			this.tsTools.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tsPreviewMenu.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.ssStatus.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.tsProgress,
				this.tsslStatus
			});
			this.ssStatus.Location = new global::System.Drawing.Point(0, 579);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new global::System.Drawing.Size(992, 22);
			this.ssStatus.TabIndex = 0;
			this.ssStatus.Text = "statusStrip1";
			this.tsProgress.Name = "tsProgress";
			this.tsProgress.Size = new global::System.Drawing.Size(100, 16);
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new global::System.Drawing.Size(0, 17);
			this.tsTools.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.tsTools.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.tsbNew,
				this.tsbOpen,
				this.tsbClose,
				this.tsbSave,
				this.toolStripSeparator1,
				this.tsbExtract,
				this.tsbAddFile,
				this.toolStripSeparator3,
				this.tsbNewContainer,
				this.tsbAddFolder,
				this.toolStripSeparator2,
				this.tsbPreview,
				this.toolStripSeparator4,
				this.tsbAbout
			});
			this.tsTools.Location = new global::System.Drawing.Point(0, 0);
			this.tsTools.Name = "tsTools";
			this.tsTools.Size = new global::System.Drawing.Size(992, 52);
			this.tsTools.TabIndex = 1;
			this.tsTools.Text = "toolStrip1";
			this.tsbNew.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbNew.Image");
			this.tsbNew.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbNew.Name = "tsbNew";
			this.tsbNew.Size = new global::System.Drawing.Size(36, 49);
			this.tsbNew.Text = "&New";
			this.tsbNew.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbNew.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbNew.Click += new global::System.EventHandler(this.tsbNew_Click);
			this.tsbOpen.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbOpen.Image");
			this.tsbOpen.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbOpen.Name = "tsbOpen";
			this.tsbOpen.Size = new global::System.Drawing.Size(37, 49);
			this.tsbOpen.Text = "&Open";
			this.tsbOpen.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbOpen.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbOpen.Click += new global::System.EventHandler(this.tsbOpen_Click);
			this.tsbClose.Enabled = false;
			this.tsbClose.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbClose.Image");
			this.tsbClose.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbClose.Name = "tsbClose";
			this.tsbClose.Size = new global::System.Drawing.Size(37, 49);
			this.tsbClose.Text = "&Close";
			this.tsbClose.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbClose.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbClose.Click += new global::System.EventHandler(this.tsbClose_Click);
			this.tsbSave.Enabled = false;
			this.tsbSave.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbSave.Image");
			this.tsbSave.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new global::System.Drawing.Size(36, 49);
			this.tsbSave.Text = "&Save";
			this.tsbSave.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbSave.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbSave.Click += new global::System.EventHandler(this.tsbSave_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 52);
			this.tsbExtract.Enabled = false;
			this.tsbExtract.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbExtract.Image");
			this.tsbExtract.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbExtract.Name = "tsbExtract";
			this.tsbExtract.Size = new global::System.Drawing.Size(60, 49);
			this.tsbExtract.Text = "&Extract All";
			this.tsbExtract.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbExtract.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbExtract.Click += new global::System.EventHandler(this.tsbExtract_Click);
			this.tsbAddFile.Enabled = false;
			this.tsbAddFile.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbAddFile.Image");
			this.tsbAddFile.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbAddFile.Name = "tsbAddFile";
			this.tsbAddFile.Size = new global::System.Drawing.Size(36, 49);
			this.tsbAddFile.Text = "&Add";
			this.tsbAddFile.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbAddFile.Click += new global::System.EventHandler(this.tsbAddFile_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(6, 52);
			this.tsbNewContainer.Enabled = false;
			this.tsbNewContainer.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbNewContainer.Image");
			this.tsbNewContainer.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbNewContainer.Name = "tsbNewContainer";
			this.tsbNewContainer.Size = new global::System.Drawing.Size(82, 49);
			this.tsbNewContainer.Text = "Ne&w Container";
			this.tsbNewContainer.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbNewContainer.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbNewContainer.Click += new global::System.EventHandler(this.tsbNewContainer_Click);
			this.tsbAddFolder.Enabled = false;
			this.tsbAddFolder.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbAddFolder.Image");
			this.tsbAddFolder.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbAddFolder.Name = "tsbAddFolder";
			this.tsbAddFolder.Size = new global::System.Drawing.Size(63, 49);
			this.tsbAddFolder.Text = "Add &Folder";
			this.tsbAddFolder.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbAddFolder.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbAddFolder.Click += new global::System.EventHandler(this.tsbAddFolder_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 52);
			this.tsbPreview.Checked = true;
			this.tsbPreview.CheckOnClick = true;
			this.tsbPreview.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.tsbPreview.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbPreview.Image");
			this.tsbPreview.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbPreview.Name = "tsbPreview";
			this.tsbPreview.Size = new global::System.Drawing.Size(49, 49);
			this.tsbPreview.Text = "&Preview";
			this.tsbPreview.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbPreview.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new global::System.Drawing.Size(6, 52);
			this.tsbAbout.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbAbout.Image");
			this.tsbAbout.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbAbout.Name = "tsbAbout";
			this.tsbAbout.Size = new global::System.Drawing.Size(40, 49);
			this.tsbAbout.Text = "A&bout";
			this.tsbAbout.TextAlign = global::System.Drawing.ContentAlignment.BottomCenter;
			this.tsbAbout.TextImageRelation = global::System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbAbout.Click += new global::System.EventHandler(this.tsbAbout_Click);
			this.panel1.Controls.Add(this.pgIPFFooter);
			this.panel1.Controls.Add(this.splitter3);
			this.panel1.Controls.Add(this.tvDirectories);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new global::System.Drawing.Point(0, 52);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(300, 527);
			this.panel1.TabIndex = 2;
			this.pgIPFFooter.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.pgIPFFooter.Location = new global::System.Drawing.Point(0, 274);
			this.pgIPFFooter.Name = "pgIPFFooter";
			this.pgIPFFooter.Size = new global::System.Drawing.Size(300, 253);
			this.pgIPFFooter.TabIndex = 2;
			this.splitter3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.splitter3.Location = new global::System.Drawing.Point(0, 271);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new global::System.Drawing.Size(300, 3);
			this.splitter3.TabIndex = 5;
			this.splitter3.TabStop = false;
			this.tvDirectories.AllowDrop = true;
			this.tvDirectories.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.tvDirectories.ImageIndex = 0;
			this.tvDirectories.ImageList = this.imageList;
			this.tvDirectories.Location = new global::System.Drawing.Point(0, 0);
			this.tvDirectories.Name = "tvDirectories";
			this.tvDirectories.SelectedImageIndex = 0;
			this.tvDirectories.Size = new global::System.Drawing.Size(300, 271);
			this.tvDirectories.StateImageList = this.imageList;
			this.tvDirectories.TabIndex = 0;
			this.tvDirectories.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.tvDirectories_AfterSelect);
			this.tvDirectories.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.tvDirectories_DragDrop);
			this.tvDirectories.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.tvDirectories_DragEnter);
			this.imageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList.ImageStream");
			this.imageList.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "folder");
			this.imageList.Images.SetKeyName(1, "archive");
			this.imageList.Images.SetKeyName(2, "database");
			this.splitter1.BackColor = global::System.Drawing.SystemColors.Control;
			this.splitter1.Location = new global::System.Drawing.Point(300, 52);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new global::System.Drawing.Size(3, 527);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			this.panel2.Controls.Add(this.lvFiles);
			this.panel2.Controls.Add(this.tsPreviewMenu);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new global::System.Drawing.Point(303, 52);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(689, 271);
			this.panel2.TabIndex = 4;
			this.lvFiles.AllowDrop = true;
			this.lvFiles.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.colName,
				this.colSize,
				this.colType
			});
			this.lvFiles.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lvFiles.FullRowSelect = true;
			this.lvFiles.GridLines = true;
			this.lvFiles.Location = new global::System.Drawing.Point(0, 25);
			this.lvFiles.MultiSelect = false;
			this.lvFiles.Name = "lvFiles";
			this.lvFiles.Size = new global::System.Drawing.Size(689, 246);
			this.lvFiles.TabIndex = 0;
			this.lvFiles.UseCompatibleStateImageBehavior = false;
			this.lvFiles.View = global::System.Windows.Forms.View.Details;
			this.lvFiles.SelectedIndexChanged += new global::System.EventHandler(this.lvFiles_SelectedIndexChanged);
			this.lvFiles.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.lvFiles_DragDrop);
			this.lvFiles.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.lvFiles_DragEnter);
			this.colName.Text = "Name";
			this.colSize.Text = "Size";
			this.colType.Text = "Type";
			this.tsPreviewMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.tsbFileSave,
				this.tsbFileCSVExport,
				this.txtFilter,
				this.tsbFilter
			});
			this.tsPreviewMenu.Location = new global::System.Drawing.Point(0, 0);
			this.tsPreviewMenu.Name = "tsPreviewMenu";
			this.tsPreviewMenu.Size = new global::System.Drawing.Size(689, 25);
			this.tsPreviewMenu.TabIndex = 0;
			this.tsPreviewMenu.Text = "Save";
			this.tsbFileSave.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFileSave.Enabled = false;
			this.tsbFileSave.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbFileSave.Image");
			this.tsbFileSave.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbFileSave.Name = "tsbFileSave";
			this.tsbFileSave.Size = new global::System.Drawing.Size(23, 22);
			this.tsbFileSave.Text = "Save File";
			this.tsbFileSave.Click += new global::System.EventHandler(this.tsbFileSave_Click);
			this.tsbFileCSVExport.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFileCSVExport.Enabled = false;
			this.tsbFileCSVExport.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbFileCSVExport.Image");
			this.tsbFileCSVExport.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbFileCSVExport.Name = "tsbFileCSVExport";
			this.tsbFileCSVExport.Size = new global::System.Drawing.Size(23, 22);
			this.tsbFileCSVExport.Text = "CSV Export";
			this.tsbFileCSVExport.Click += new global::System.EventHandler(this.tsbFileCSVExport_Click);
			this.splitter2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new global::System.Drawing.Point(303, 323);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new global::System.Drawing.Size(689, 5);
			this.splitter2.TabIndex = 5;
			this.splitter2.TabStop = false;
			this.panel3.Controls.Add(this.ucPreview);
			this.panel3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new global::System.Drawing.Point(303, 328);
			this.panel3.Name = "panel3";
			this.panel3.Size = new global::System.Drawing.Size(689, 251);
			this.panel3.TabIndex = 6;
			this.ofd.Filter = "IPF-Files|*.ipf";
			this.ofd.Title = "Open IPF File";
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new global::System.Drawing.Size(100, 25);
			this.txtFilter.TextChanged += new global::System.EventHandler(this.txtFilter_TextChanged);
			this.tsbFilter.CheckOnClick = true;
			this.tsbFilter.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFilter.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("tsbFilter.Image");
			this.tsbFilter.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.tsbFilter.Name = "tsbFilter";
			this.tsbFilter.Size = new global::System.Drawing.Size(23, 22);
			this.tsbFilter.CheckStateChanged += new global::System.EventHandler(this.tsbFilter_CheckStateChanged);
			this.ucPreview.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.ucPreview.Location = new global::System.Drawing.Point(0, 0);
			this.ucPreview.Name = "ucPreview";
			this.ucPreview.Size = new global::System.Drawing.Size(689, 251);
			this.ucPreview.TabIndex = 1;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(992, 601);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.splitter2);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.splitter1);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.tsTools);
			base.Controls.Add(this.ssStatus);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "fMain";
			this.Text = "IPF Suite";
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			this.tsTools.ResumeLayout(false);
			this.tsTools.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tsPreviewMenu.ResumeLayout(false);
			this.tsPreviewMenu.PerformLayout();
			this.panel3.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000151 RID: 337
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000152 RID: 338
		private global::System.Windows.Forms.StatusStrip ssStatus;

		// Token: 0x04000153 RID: 339
		private global::System.Windows.Forms.ToolStrip tsTools;

		// Token: 0x04000154 RID: 340
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000155 RID: 341
		private global::System.Windows.Forms.Splitter splitter1;

		// Token: 0x04000156 RID: 342
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000157 RID: 343
		private global::System.Windows.Forms.TreeView tvDirectories;

		// Token: 0x04000158 RID: 344
		private global::System.Windows.Forms.ToolStripButton tsbOpen;

		// Token: 0x04000159 RID: 345
		private global::System.Windows.Forms.ToolStripButton tsbPreview;

		// Token: 0x0400015A RID: 346
		private global::System.Windows.Forms.ListView lvFiles;

		// Token: 0x0400015B RID: 347
		private global::System.Windows.Forms.Splitter splitter2;

		// Token: 0x0400015C RID: 348
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x0400015D RID: 349
		private global::System.Windows.Forms.OpenFileDialog ofd;

		// Token: 0x0400015E RID: 350
		private global::System.Windows.Forms.ImageList imageList;

		// Token: 0x0400015F RID: 351
		private global::System.Windows.Forms.ColumnHeader colName;

		// Token: 0x04000160 RID: 352
		private global::System.Windows.Forms.ColumnHeader colSize;

		// Token: 0x04000161 RID: 353
		private global::System.Windows.Forms.ColumnHeader colType;

		// Token: 0x04000162 RID: 354
		private global::System.Windows.Forms.ToolStripButton tsbExtract;

		// Token: 0x04000163 RID: 355
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000164 RID: 356
		private global::System.Windows.Forms.ToolStripButton tsbAddFile;

		// Token: 0x04000165 RID: 357
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000166 RID: 358
		private global::System.Windows.Forms.ToolStripButton tsbClose;

		// Token: 0x04000167 RID: 359
		private global::System.Windows.Forms.ToolStripButton tsbAddFolder;

		// Token: 0x04000168 RID: 360
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x04000169 RID: 361
		private global::System.Windows.Forms.ToolStripButton tsbNewContainer;

		// Token: 0x0400016A RID: 362
		private global::System.Windows.Forms.ToolStrip tsPreviewMenu;

		// Token: 0x0400016B RID: 363
		private global::System.Windows.Forms.ToolStripButton tsbFileSave;

		// Token: 0x0400016C RID: 364
		private global::IPFSuite.ucPreview ucPreview;

		// Token: 0x0400016D RID: 365
		private global::System.Windows.Forms.SaveFileDialog sfd;

		// Token: 0x0400016E RID: 366
		private global::System.Windows.Forms.ToolStripButton tsbFileCSVExport;

		// Token: 0x0400016F RID: 367
		private global::System.Windows.Forms.FolderBrowserDialog fbd;

		// Token: 0x04000170 RID: 368
		private global::System.Windows.Forms.ToolStripProgressBar tsProgress;

		// Token: 0x04000171 RID: 369
		private global::System.Windows.Forms.ToolStripStatusLabel tsslStatus;

		// Token: 0x04000172 RID: 370
		private global::System.Windows.Forms.ToolStripButton tsbNew;

		// Token: 0x04000173 RID: 371
		private global::System.Windows.Forms.PropertyGrid pgIPFFooter;

		// Token: 0x04000174 RID: 372
		private global::System.Windows.Forms.Splitter splitter3;

		// Token: 0x04000175 RID: 373
		private global::System.Windows.Forms.ToolStripButton tsbSave;

		// Token: 0x04000176 RID: 374
		private global::System.Windows.Forms.ToolStripButton tsbAbout;

		// Token: 0x04000177 RID: 375
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x04000178 RID: 376
		private global::System.Windows.Forms.ToolStripTextBox txtFilter;

		// Token: 0x04000179 RID: 377
		private global::System.Windows.Forms.ToolStripButton tsbFilter;
	}
}
