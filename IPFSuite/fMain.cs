using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPFSuite.FileFormats.IES;
using IPFSuite.FileFormats.IPF;

namespace IPFSuite
{
	// Token: 0x02000025 RID: 37
	public partial class fMain : Form
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00004FF7 File Offset: 0x000031F7
		public fMain()
		{
			this.InitializeComponent();
			this.ResizeListView(this.lvFiles);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000515C File Offset: 0x0000335C
		private async void LoadIpf()
		{
			fMain.IpfDirectory = new DirectoryInfo(this._filename).Parent.FullName;
			this._ipf = new IPF(this._filename);
			if (await this._ipf.Load())
			{
				this.RefreshTreeByIpf();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000052D4 File Offset: 0x000034D4
		private async void RefreshTreeByIpf()
		{
			this.pgIPFFooter.SelectedObject = this._ipf.Footer;
			this.lvFiles.BeginUpdate();
			this.tvDirectories.BeginUpdate();
			this.lvFiles.Items.Clear();
			this.tvDirectories.Nodes.Clear();
			this.ucPreview.Clear();
			TreeNode node = this.BuildTree();
			this.tvDirectories.Nodes.Add(node);
			this.lvFiles.EndUpdate();
			this.tvDirectories.EndUpdate();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00005310 File Offset: 0x00003510
		private TreeNode BuildTree()
		{
			TreeNode treeNode = new TreeNode();
			this._rootNode = treeNode;
			treeNode.Text = this._filename;
			treeNode.ImageKey = "database";
			treeNode.SelectedImageKey = treeNode.ImageKey;
			treeNode.Name = "root";
			string b = string.Empty;
			string b2 = string.Empty;
			for (int i = 0; i < this._ipf.FileTable.Length; i++)
			{
				this.SetStatusInfo(new int?(this._ipf.FileTable.Length), i, "Loading ipf file");
				IPFFileTable ipffileTable = this._ipf.FileTable[i];
				if (!(ipffileTable.containerName == b) || !(ipffileTable.directoryName == b2))
				{
					TreeNode treeNode2 = null;
					TreeNode[] array = this._rootNode.Nodes.Find(ipffileTable.containerName + '\\', false);
					TreeNode treeNode3;
					if (array.Length == 0)
					{
						treeNode3 = new TreeNode();
						treeNode3.Text = ipffileTable.containerName;
						treeNode3.Name = ipffileTable.containerName + '\\';
						treeNode3.ImageKey = "archive";
						treeNode3.SelectedImageKey = treeNode3.ImageKey;
						this._rootNode.Nodes.Add(treeNode3);
					}
					else
					{
						if (array.Length != 1)
						{
							MessageBox.Show(this, "You found a Bug! Found multiple containers with same name.", "Bugalert!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return null;
						}
						treeNode3 = array[0];
					}
					string text = ipffileTable.containerName + '\\';
					string[] array2 = ipffileTable.directoryName.Split(new char[]
					{
						'\\'
					});
					foreach (string text2 in array2)
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text = text + text2 + '\\';
							TreeNode[] array4 = treeNode3.Nodes.Find(text, true);
							if (array4.Length == 0)
							{
								if (treeNode2 == null)
								{
									treeNode2 = treeNode3.Nodes.Add(text, text2, "folder", "folder");
								}
								else
								{
									treeNode2 = treeNode2.Nodes.Add(text, text2, "folder", "folder");
								}
							}
							else
							{
								treeNode2 = array4[0];
							}
						}
					}
					b = ipffileTable.containerName;
					b2 = ipffileTable.directoryName;
				}
			}
			this.SetStatusInfo(null, 0, string.Empty);
			return treeNode;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000055F4 File Offset: 0x000037F4
		private void tvDirectories_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				this.RefreshFileList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000595C File Offset: 0x00003B5C
		private async void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.lvFiles.SelectedItems.Count == 1)
				{
					ListViewItem item = this.lvFiles.SelectedItems[0];
					int idx = (int)item.Tag;
					if (this.tsbPreview.Checked)
					{
						if (idx>0)
						{
                            if (!(await this.ucPreview.LoadFile(this._ipf.FileTable[idx].fileName, await this._ipf.ExtractAsync(idx), await this._ipf.ExtractAsync((idx - 1)), this._ipf.FileTable[(idx - 1)].fileName)))
                            {
                                this.SetStatusInfo(new int?(100), 0, "Sorry, but we could not display the selected file in preview.");
                            }
                        }

						if (idx<=0)
						{
                            if (!(await this.ucPreview.LoadFile(this._ipf.FileTable[idx].fileName, await this._ipf.ExtractAsync(idx), await this._ipf.ExtractAsync(idx), this._ipf.FileTable[idx].fileName)))
                            {
                                this.SetStatusInfo(new int?(100), 0, "Sorry, but we could not display the selected file in preview.");
                            }
                        }
					}
					bool csvExport = Path.GetExtension(this._ipf.FileTable[idx].fileName).ToLowerInvariant() == ".ies";
					this.tsbFileCSVExport.Enabled = csvExport;
					this.tsbFileSave.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005A08 File Offset: 0x00003C08
		private void RefreshFileList()
		{
			this.lvFiles.BeginUpdate();
			this.tsbFileSave.Enabled = false;
			this.lvFiles.Items.Clear();
			if (this.tvDirectories.SelectedNode != null)
			{
				foreach (IPFFileTable ipffileTable in this._ipf.FileTable.Where(delegate(IPFFileTable x)
				{
					string text = x.containerName + '\\';
					if (!string.IsNullOrEmpty(x.directoryName))
					{
						text = text + x.directoryName + '\\';
					}
					return text == this.tvDirectories.SelectedNode.Name;
				}))
				{
					if (!this.tsbFilter.Checked || ipffileTable.fileName.Contains(this.txtFilter.Text))
					{
						ListViewItem listViewItem = new ListViewItem();
						listViewItem.Text = ipffileTable.fileName;
						listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem
						{
							Text = this.GetFileSize(ipffileTable.fileSizeUncompressed)
						});
						listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem
						{
							Text = Path.GetExtension(ipffileTable.fileName)
						});
						listViewItem.Tag = ipffileTable.idx;
						this.lvFiles.Items.Add(listViewItem);
					}
				}
				this.ResizeListView(this.lvFiles);
				this.lvFiles.EndUpdate();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005B98 File Offset: 0x00003D98
		private void LoadFile(string filename)
		{
			if (this._ipf != null)
			{
				this.CloseFile();
			}
			this._filename = filename;
			this.LoadIpf();
			this.tsbSave.Enabled = false;
			this.tsbClose.Enabled = true;
			this.tsbExtract.Enabled = true;
			this.tsbAddFile.Enabled = true;
			this.tsbAddFolder.Enabled = true;
			this.tsbNewContainer.Enabled = true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005C18 File Offset: 0x00003E18
		private void CloseFile()
		{
			this._ipf.Close();
			this.tsbSave.Enabled = false;
			this.tsbAddFile.Enabled = false;
			this.tsbAddFolder.Enabled = false;
			this.tsbClose.Enabled = false;
			this.tsbExtract.Enabled = false;
			this.tsbFileCSVExport.Enabled = false;
			this.tsbFileSave.Enabled = false;
			this.tsbNewContainer.Enabled = false;
			this.lvFiles.Items.Clear();
			this.tvDirectories.Nodes.Clear();
			this.ucPreview.Clear();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005CF0 File Offset: 0x00003EF0
		private void SetStatusInfo(int? maxValue, int value, string text)
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new Action(delegate()
				{
					this.SetStatusInfo(maxValue, value, text);
				}));
			}
			else
			{
				if (maxValue != null)
				{
					this.tsProgress.Maximum = maxValue.Value;
				}
				this.tsProgress.Value = value;
				this.tsslStatus.Text = text;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005DA0 File Offset: 0x00003FA0
		private void AddFile(string filename, TreeNode node)
		{
			FileAttributes attributes = File.GetAttributes(filename);
			if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
			{
				string text = Path.GetFileName(filename);
				TreeNode treeNode = new TreeNode();
				treeNode.Text = text;
				treeNode.Name = node.Name + text + '\\';
				treeNode.ImageKey = "folder";
				treeNode.SelectedImageKey = treeNode.ImageKey;
				node.Nodes.Add(treeNode);
				foreach (string filename2 in Directory.GetDirectories(filename))
				{
					this.AddFile(filename2, treeNode);
				}
				foreach (string filename3 in Directory.GetFiles(filename))
				{
					this.AddFile(filename3, treeNode);
				}
			}
			else
			{
				string text = node.Name;
				string text2 = text.Substring(0, text.IndexOf("\\", StringComparison.Ordinal));
				string path = text.Substring(text2.Length + 1);
				this._ipf.Add(text2, Path.Combine(path, Path.GetFileName(filename)), File.ReadAllBytes(filename));
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005EDC File Offset: 0x000040DC
		private void tvDirectories_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					e.Effect = DragDropEffects.All;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005F40 File Offset: 0x00004140
		private void tvDirectories_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
					if (array.Length > 1)
					{
						MessageBox.Show(this, "Can only open one file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						this.LoadFile(array[0]);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005FD8 File Offset: 0x000041D8
		private void lvFiles_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					if (this.tvDirectories.SelectedNode == null || this.tvDirectories.SelectedNode == this._rootNode)
					{
						MessageBox.Show(this, "Please select a container or a folder to drop files.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
						foreach (string filename in array)
						{
							this.AddFile(filename, this.tvDirectories.SelectedNode);
						}
						this.RefreshFileList();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000060C4 File Offset: 0x000042C4
		private void lvFiles_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					e.Effect = DragDropEffects.All;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00006128 File Offset: 0x00004328
		public static string Version
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00006150 File Offset: 0x00004350
		public static DialogResult InputBox(string title, string promptText, ref string value)
		{
			Form form = new Form();
			Label label = new Label();
			TextBox textBox = new TextBox();
			Button button = new Button();
			Button button2 = new Button();
			form.Text = title;
			label.Text = promptText;
			textBox.Text = value;
			button.Text = "OK";
			button2.Text = "Cancel";
			button.DialogResult = DialogResult.OK;
			button2.DialogResult = DialogResult.Cancel;
			label.SetBounds(9, 20, 372, 13);
			textBox.SetBounds(12, 36, 372, 20);
			button.SetBounds(228, 72, 75, 23);
			button2.SetBounds(309, 72, 75, 23);
			label.AutoSize = true;
			textBox.Anchor |= AnchorStyles.Right;
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			form.ClientSize = new Size(396, 107);
			form.Controls.AddRange(new Control[]
			{
				label,
				textBox,
				button,
				button2
			});
			form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = button;
			form.CancelButton = button2;
			DialogResult result = form.ShowDialog();
			value = textBox.Text;
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000062E8 File Offset: 0x000044E8
		private string GetFileSize(uint size)
		{
			string[] array = new string[]
			{
				"B",
				"KB",
				"MB",
				"GB"
			};
			int num = 0;
			while (size >= 1024U && num + 1 < array.Length)
			{
				num++;
				size /= 1024U;
			}
			return string.Format("{0:0.##} {1}", size, array[num]);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00006364 File Offset: 0x00004564
		private void ResizeListView(ListView lv)
		{
			lv.BeginUpdate();
			for (int i = 0; i < lv.Columns.Count; i++)
			{
				lv.Columns[i].Width = -1;
				int width = lv.Columns[i].Width;
				lv.Columns[i].Width = -2;
				int width2 = lv.Columns[i].Width;
				if (width > width2)
				{
					lv.Columns[i].Width = -1;
				}
				else
				{
					lv.Columns[i].Width = -2;
				}
			}
			lv.EndUpdate();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00006428 File Offset: 0x00004628
		private void tsbNew_Click(object sender, EventArgs e)
		{
			try
			{
				this._ipf = new IPF();
				this._filename = "New IPF";
				this.RefreshTreeByIpf();
				this.tsbSave.Enabled = true;
				this.tsbClose.Enabled = true;
				this.tsbExtract.Enabled = true;
				this.tsbAddFile.Enabled = true;
				this.tsbAddFolder.Enabled = true;
				this.tsbNewContainer.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000064D0 File Offset: 0x000046D0
		private void tsbOpen_Click(object sender, EventArgs e)
		{
			try
			{
				this.ofd.Filter = "IPF-Files|*.ipf";
				if (this.ofd.ShowDialog() == DialogResult.OK)
				{
					this.LoadFile(this.ofd.FileName);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00006548 File Offset: 0x00004748
		private void tsbClose_Click(object sender, EventArgs e)
		{
			try
			{
				this.CloseFile();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000658C File Offset: 0x0000478C
		private void tsbSave_Click(object sender, EventArgs e)
		{
			try
			{
				this.sfd.FileName = "New IPF";
				this.sfd.Filter = "*.ipf|*.ipf";
				if (this.sfd.ShowDialog() == DialogResult.OK)
				{
					this._ipf.Save(this.sfd.FileName);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00006618 File Offset: 0x00004818
		private void tsbNewContainer_Click(object sender, EventArgs e)
		{
			try
			{
				string empty = string.Empty;
				if (fMain.InputBox("Container Name", "Please enter the name of the container (e.g. ui.ipf)", ref empty) == DialogResult.OK)
				{
					TreeNode[] array = this._rootNode.Nodes.Find(empty + '\\', false);
					if (array.Length != 0)
					{
						MessageBox.Show(this, "The container already exists and will not be created twice.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						TreeNode treeNode = new TreeNode();
						treeNode.Text = empty;
						treeNode.Name = empty + '\\';
						treeNode.ImageKey = "archive";
						treeNode.SelectedImageKey = treeNode.ImageKey;
						this._rootNode.Nodes.Add(treeNode);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000670C File Offset: 0x0000490C
		private void tsbAddFolder_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tvDirectories.SelectedNode == null || this.tvDirectories.SelectedNode == this._rootNode)
				{
					MessageBox.Show(this, "To add a new Folder please select a existing parent folder or a container.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					string empty = string.Empty;
					if (fMain.InputBox("Folder Name", "Please enter the name of the folder", ref empty) == DialogResult.OK)
					{
						TreeNode[] array = this.tvDirectories.SelectedNode.Nodes.Find(this.tvDirectories.SelectedNode.Name + empty + '\\', false);
						if (array.Length != 0)
						{
							MessageBox.Show(this, "The folder already exists in the parent you selected and will not be created twice.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							TreeNode treeNode = new TreeNode();
							treeNode.Text = empty;
							treeNode.Name = this.tvDirectories.SelectedNode.Name + empty + '\\';
							treeNode.ImageKey = "folder";
							treeNode.SelectedImageKey = treeNode.ImageKey;
							this.tvDirectories.SelectedNode.Nodes.Add(treeNode);
							this.tvDirectories.SelectedNode.Expand();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00006890 File Offset: 0x00004A90
		private void tsbAddFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tvDirectories.SelectedNode == null || this.tvDirectories.SelectedNode == this._rootNode)
				{
					MessageBox.Show(this, "Please select a container or a folder to drop files.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					this.ofd.Filter = "All Files|*.*";
					if (this.ofd.ShowDialog() == DialogResult.OK)
					{
						this.AddFile(this.ofd.FileName, this.tvDirectories.SelectedNode);
						this.RefreshFileList();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000695C File Offset: 0x00004B5C
		private void tsbFileSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lvFiles.SelectedItems.Count == 1)
				{
					ListViewItem listViewItem = this.lvFiles.SelectedItems[0];
					int num = (int)listViewItem.Tag;
					this.sfd.FileName = this._ipf.FileTable[num].fileName;
					this.sfd.Filter = string.Format("*{0}|*{0}", Path.GetExtension(this._ipf.FileTable[num].fileName));
					if (this.sfd.ShowDialog() == DialogResult.OK)
					{
						File.WriteAllBytes(this.sfd.FileName, this._ipf.Extract(num));
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00006A54 File Offset: 0x00004C54
		private void tsbFileCSVExport_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lvFiles.SelectedItems.Count == 1)
				{
					ListViewItem listViewItem = this.lvFiles.SelectedItems[0];
					int num = (int)listViewItem.Tag;
					this.sfd.FileName = this._ipf.FileTable[num].fileName;
					this.sfd.Filter = string.Format("*.csv|*.csv", new object[0]);
					if (this.sfd.ShowDialog() == DialogResult.OK)
					{
						using (IESFile iesfile = new IESFile(this._ipf.Extract(num)))
						{
							StringBuilder stringBuilder = new StringBuilder();
							for (int i = 0; i < iesfile.Columns.Count; i++)
							{
								stringBuilder.Append(iesfile.Columns[i].Name + "\t");
							}
							stringBuilder.AppendLine("");
							foreach (object obj in iesfile.Rows)
							{
								List<object> list = (List<object>)obj;
								List<object> list2 = list;
								for (int i = 0; i < iesfile.Columns.Count; i++)
								{
									stringBuilder.Append(list2[i].ToString().Replace("\t", " ") + "\t");
								}
								stringBuilder.AppendLine();
							}
							File.WriteAllText(this.sfd.FileName, stringBuilder.ToString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00006DA4 File Offset: 0x00004FA4
		private void tsbExtract_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.fbd.ShowDialog() == DialogResult.OK)
				{
					Task.Factory.StartNew(delegate()
					{
						for (int i = 0; i < this._ipf.FileTable.Length; i++)
						{
							IPFFileTable ipffileTable = this._ipf.FileTable[i];
							this.SetStatusInfo(new int?(this._ipf.FileTable.Length - 1), i, "Extracting " + Path.Combine(ipffileTable.directoryName, ipffileTable.fileName));
							FileInfo fileInfo = new FileInfo(Path.Combine(new string[]
							{
								this.fbd.SelectedPath,
								"data",
								ipffileTable.containerName,
								ipffileTable.directoryName,
								ipffileTable.fileName
							}));
							if (!fileInfo.Directory.Exists)
							{
								fileInfo.Directory.Create();
							}
							File.WriteAllBytes(fileInfo.FullName, this._ipf.Extract(ipffileTable.idx));
						}
						this.SetStatusInfo(null, 0, string.Empty);
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00006E18 File Offset: 0x00005018
		private void tsbAbout_Click(object sender, EventArgs e)
		{
			try
			{
				MessageBox.Show(this, string.Format("Version {0}\nCredits\n - mrdiablo\n - curiosity\n - Yommy\n - TwoLaid\n - Latheesan", fMain.Version), "About", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00006E74 File Offset: 0x00005074
		private void tsbFilter_CheckStateChanged(object sender, EventArgs e)
		{
			this.RefreshFileList();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00006E80 File Offset: 0x00005080
		private void txtFilter_TextChanged(object sender, EventArgs e)
		{
			if (this.tsbFilter.Checked)
			{
				this.RefreshFileList();
			}
		}

		// Token: 0x04000148 RID: 328
		private const string CREDITS = "Version {0}\nCredits\n - mrdiablo\n - curiosity\n - Yommy\n - TwoLaid\n - Latheesan";

		// Token: 0x04000149 RID: 329
		private const string IMAGEKEY_IPF = "database";

		// Token: 0x0400014A RID: 330
		private const string IMAGEKEY_CONTAINER = "archive";

		// Token: 0x0400014B RID: 331
		private const string IMAGEKEY_FOLDER = "folder";

		// Token: 0x0400014C RID: 332
		private const char PATH_SEPARATOR = '\\';

		// Token: 0x0400014D RID: 333
		private TreeNode _rootNode;

		// Token: 0x0400014E RID: 334
		private string _filename;

		// Token: 0x0400014F RID: 335
		private IPF _ipf;

		// Token: 0x04000150 RID: 336
		public static string IpfDirectory;
	}
}
