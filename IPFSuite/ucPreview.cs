using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using FastColoredTextBoxNS;
using IPFSuite.FileFormats.FSB;
using IPFSuite.FileFormats.IES;
using IPFSuite.FileFormats.XAC;
using KUtility;
using Paloma;
using static System.Windows.Forms.AxHost;

namespace IPFSuite
{
    // Token: 0x0200001A RID: 26
    public class ucPreview : UserControl
    {
        // Token: 0x060000BD RID: 189 RVA: 0x00008383 File Offset: 0x00006583
        public ucPreview()
        {
            this.InitializeComponent();
            this.elementHost1.Child = new Model3DUserControl();
            this.elementHost1.HostContainer.MouseDown += delegate (object sender, MouseButtonEventArgs e)
            {
                this.elementHost1.Focus();
            };
        }

        // Token: 0x060000BE RID: 190 RVA: 0x000083C0 File Offset: 0x000065C0
        public async Task<bool> LoadFile(string filename, byte[] content, byte[] contentDds,string ddsName)
        {
            this._previewType = ucPreview.PreviewType.None;
            this._extension = Path.GetExtension(filename).ToLowerInvariant();
            string extension = this._extension;
            switch (extension)
            {
                case ".fx":
                case ".fxh":
                case ".txt":
                case ".h":
                case ".export":
                    this.txtPreview.Language = Language.Custom;
                    this._previewType = ucPreview.PreviewType.Text;
                    goto IL_2F1;
                case ".lua":
                    this.txtPreview.Language = Language.Lua;
                    this._previewType = ucPreview.PreviewType.Text;
                    goto IL_2F1;
                case ".skn":
                case ".xml":
                case ".xlf":
                case ".fdp":
                case ".xsd":
                case ".sprbin":
                case ".sani":
                case ".3deffect":
                case ".3drender":
                case ".3dprop":
                case ".3dworld":
                    this.txtPreview.Language = Language.XML;
                    this._previewType = ucPreview.PreviewType.Text;
                    goto IL_2F1;
                case ".bmp":
                case ".png":
                case ".tga":
                case ".tif":
                case ".jpg":
                case ".dds":
                    this._previewType = ucPreview.PreviewType.Image;
                    goto IL_2F1;
                case ".ies":
                case ".fsb":
                    this._previewType = ucPreview.PreviewType.Table;
                    goto IL_2F1;
                case ".xac":
                case ".xpm":
                    this._previewType = ucPreview.PreviewType.Model;
                    goto IL_2F1;
            }
            this._previewType = ucPreview.PreviewType.Hex;
        IL_2F1:
            switch (this._previewType)
            {
                case ucPreview.PreviewType.Text:
                    this.txtPreview.Text = await this.GetUTF8String(content);
                    break;
                case ucPreview.PreviewType.Image:
                    if (!this.BuildImagePreview(content))
                    {
                        return false;
                    }
                    break;
                case ucPreview.PreviewType.Table:
                    this.BuildTablePreview(content);
                    break;
                case ucPreview.PreviewType.Hex:
                    this.BuildHexPreview(content);
                    break;
                case ucPreview.PreviewType.Model:
                    this.BuildModelPreview(filename, content,contentDds,ddsName);
                    break;
            }
            this.txtPreview.Visible = (this._previewType == ucPreview.PreviewType.Text);
            this.picPreview.Visible = (this._previewType == ucPreview.PreviewType.Image);
            this.panPreview.Visible = (this._previewType == ucPreview.PreviewType.Image);
            this.grdPreview.Visible = (this._previewType == ucPreview.PreviewType.Hex | this._previewType == ucPreview.PreviewType.Table);
            this.elementHost1.Visible = (this._previewType == ucPreview.PreviewType.Model);
            return true;
        }

        // Token: 0x060000BF RID: 191 RVA: 0x00008414 File Offset: 0x00006614
        private void BuildModelPreview(string filename, byte[] content, byte[] contentDds,string ddsName)
        {
            using (MemoryStream memoryStream = new MemoryStream(content))
            {
                try
                {
                    XAC xac = new XAC(memoryStream);
                    ((Model3DUserControl)this.elementHost1.Child).SetModel(filename, xac, contentDds,ddsName);
                }
                catch (XAC_LoaderException)
                {
                    ((Model3DUserControl)this.elementHost1.Child).SetModel(filename, null,null,null);
                    this._previewType = ucPreview.PreviewType.None;
                }
            }
        }

        // Token: 0x060000C0 RID: 192 RVA: 0x000084A4 File Offset: 0x000066A4
        private void BuildTablePreview(byte[] content)
        {
            if (this._extension == ".ies")
            {
                base.Invoke(new MethodInvoker(delegate ()
                {
                    this.grdPreview.DataSource = null;
                    this.grdPreview.Rows.Clear();
                    this.grdPreview.Columns.Clear();
                    FileIes iesfile = new FileIes(content);
                    foreach (IesColumn iesColumn in iesfile.Columns)
                    {
                        this.grdPreview.Columns.Add(iesColumn.Name, iesColumn.Name);
                    }
                    foreach (IesRow iesRow in iesfile.Rows)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(this.grdPreview);
                        int j = 0;
                        foreach (IesColumn iesColumn2 in iesfile.Columns)
                        {
                            row.Cells[j++].Value = iesRow[iesColumn2.Name];
                        }
                        this.grdPreview.Rows.Add(row);
                    }
                    this.grdPreview.Visible = true;
                }));
                return;
            }
            if (this._extension == ".fsb")
            {
                using (MemoryStream memoryStream = new MemoryStream(content))
                {
                    FSB fsb = null;
                    try
                    {
                        fsb = new FSB(memoryStream);
                    }
                    catch (FSB_LoaderException)
                    {
                        fsb = null;
                    }
                    if (fsb == null)
                    {
                        this._previewType = ucPreview.PreviewType.None;
                    }
                    else
                    {
                        this.grdPreview.DataSource = null;
                        this.grdPreview.Rows.Clear();
                        this.grdPreview.Columns.Clear();
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("Index");
                        dataTable.Columns.Add("Name");
                        dataTable.Columns.Add("Offset");
                        dataTable.Columns.Add("Size");
                        dataTable.Columns.Add("Flags");
                        dataTable.Columns.Add("Frequency");
                        dataTable.Columns.Add("Samples");
                        dataTable.Columns.Add("Format");
                        List<FSB.Sample> samples = fsb.Samples;
                        for (int i = 0; i < samples.Count; i++)
                        {
                            FSB.Sample sample = samples[i];
                            DataRow dataRow2 = dataTable.NewRow();
                            dataRow2[0] = i;
                            dataRow2[1] = sample.Name;
                            dataRow2[2] = string.Format("0x{0:X8}", sample.Offset);
                            dataRow2[3] = sample.Size;
                            dataRow2[4] = string.Format("0x{0:X2}", sample.Type);
                            dataRow2[5] = sample.Frequency;
                            dataRow2[6] = sample.Samples;
                            dataRow2[7] = sample.Codec;
                            dataTable.Rows.Add(dataRow2);
                        }
                        this.grdPreview.DataSource = dataTable;
                    }
                }
            }
        }

        // Token: 0x060000C1 RID: 193 RVA: 0x00008720 File Offset: 0x00006920
        private bool BuildImagePreview(byte[] content)
        {
            if (this._extension == ".tga")
            {
                this.picPreview.Image = new TargaImage(content).Image;
            }
            else
            {
                if (this._extension == ".dds")
                {
                    try
                    {
                        this.picPreview.Image = new DDSImage(content).images[0];
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                using (MemoryStream memoryStream = new MemoryStream(content))
                {
                    this.picPreview.Image = new Bitmap(memoryStream);
                }
            }
            return true;
        }

        // Token: 0x060000C2 RID: 194 RVA: 0x000087CC File Offset: 0x000069CC
        private void BuildHexPreview(byte[] content)
        {
            this.grdPreview.DataSource = null;
            this.grdPreview.Rows.Clear();
            this.grdPreview.Columns.Clear();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Offset");
            for (int i = 0; i < 16; i++)
            {
                dataTable.Columns.Add(string.Format("{0:X2}", i));
            }
            int num = 0;
            int num2 = 0;
            while ((double)num2 < Math.Ceiling((double)content.Length / 16.0) && num < content.Length)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = string.Format("{0:X8}", num2 * 16);
                int j = 0;
                while (j < 16 && num < content.Length)
                {
                    dataRow[j + 1] = string.Format("{0:X2}", content[num]);
                    num++;
                    j++;
                }
                dataTable.Rows.Add(dataRow);
                num2++;
            }
            this.grdPreview.DataSource = dataTable;
            foreach (object obj in this.grdPreview.Columns)
            {
                DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewColumn.Width = 30;
            }
            this.grdPreview.Columns[0].Width = 80;
        }

        // Token: 0x060000C3 RID: 195 RVA: 0x0000895C File Offset: 0x00006B5C
        private async Task<string> GetUTF8String(byte[] content)
        {
            return await Task.Factory.StartNew<string>(() => Encoding.UTF8.GetString(content));
        }

        // Token: 0x060000C4 RID: 196 RVA: 0x0000899F File Offset: 0x00006B9F
        public void Clear()
        {
            this.txtPreview.Visible = false;
            this.picPreview.Visible = false;
            this.grdPreview.Visible = false;
            this.panPreview.Visible = false;
            this.elementHost1.Visible = false;
        }

        // Token: 0x060000C5 RID: 197 RVA: 0x000089DD File Offset: 0x00006BDD
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000089FC File Offset: 0x00006BFC
        private void InitializeComponent()
        {
            this.components = new Container();
            this.txtPreview = new FastColoredTextBox();
            this.grdPreview = new DataGridView();
            this.elementHost1 = new ElementHost();
            this.panPreview = new Panel();
            this.picPreview = new PictureBox();
            ((ISupportInitialize)this.txtPreview).BeginInit();
            ((ISupportInitialize)this.grdPreview).BeginInit();
            this.panPreview.SuspendLayout();
            ((ISupportInitialize)this.picPreview).BeginInit();
            base.SuspendLayout();
            this.txtPreview.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.txtPreview.AutoCompleteBracketsList = new char[]
            {
                '(',
                ')',
                '{',
                '}',
                '[',
                ']',
                '"',
                '"',
                '\'',
                '\''
            };
            this.txtPreview.AutoScrollMinSize = new Size(2, 14);
            this.txtPreview.BackBrush = null;
            this.txtPreview.CharHeight = 14;
            this.txtPreview.CharWidth = 8;
            this.txtPreview.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPreview.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            this.txtPreview.IsReplaceMode = false;
            this.txtPreview.Location = new Point(0, 0);
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.Paddings = new Padding(0);
            this.txtPreview.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            this.txtPreview.Size = new Size(400, 300);
            this.txtPreview.TabIndex = 1;
            this.txtPreview.Visible = false;
            this.txtPreview.Zoom = 100;
            this.grdPreview.AllowUserToAddRows = false;
            this.grdPreview.AllowUserToDeleteRows = false;
            this.grdPreview.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.grdPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPreview.Location = new Point(0, 0);
            this.grdPreview.Name = "grdPreview";
            this.grdPreview.ReadOnly = true;
            this.grdPreview.Size = new Size(400, 300);
            this.grdPreview.TabIndex = 3;
            this.grdPreview.Visible = false;
            this.elementHost1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.elementHost1.Location = new Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new Size(400, 300);
            this.elementHost1.TabIndex = 4;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Visible = false;
            this.elementHost1.Child = null;
            this.panPreview.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.panPreview.AutoScroll = true;
            this.panPreview.Controls.Add(this.picPreview);
            this.panPreview.Location = new Point(0, 0);
            this.panPreview.Name = "panPreview";
            this.panPreview.Size = new Size(400, 300);
            this.panPreview.TabIndex = 4;
            this.picPreview.Location = new Point(0, 0);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new Size(400, 300);
            this.picPreview.SizeMode = PictureBoxSizeMode.AutoSize;
            this.picPreview.TabIndex = 3;
            this.picPreview.TabStop = false;
            this.picPreview.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.elementHost1);
            base.Controls.Add(this.panPreview);
            base.Controls.Add(this.grdPreview);
            base.Controls.Add(this.txtPreview);
            base.Name = "ucPreview";
            base.Size = new Size(400, 300);
            ((ISupportInitialize)this.txtPreview).EndInit();
            ((ISupportInitialize)this.grdPreview).EndInit();
            this.panPreview.ResumeLayout(false);
            this.panPreview.PerformLayout();
            ((ISupportInitialize)this.picPreview).EndInit();
            base.ResumeLayout(false);
        }

        // Token: 0x0400014C RID: 332
        private ucPreview.PreviewType _previewType;

        // Token: 0x0400014D RID: 333
        private string _extension;

        // Token: 0x0400014E RID: 334
        private IContainer components;

        // Token: 0x0400014F RID: 335
        private FastColoredTextBox txtPreview;

        // Token: 0x04000150 RID: 336
        private DataGridView grdPreview;

        // Token: 0x04000151 RID: 337
        private ElementHost elementHost1;

        // Token: 0x04000152 RID: 338
        private Panel panPreview;

        // Token: 0x04000153 RID: 339
        private PictureBox picPreview;

        // Token: 0x0200004B RID: 75
        public enum PreviewType
        {
            // Token: 0x0400023C RID: 572
            None,
            // Token: 0x0400023D RID: 573
            Text,
            // Token: 0x0400023E RID: 574
            Image,
            // Token: 0x0400023F RID: 575
            Table,
            // Token: 0x04000240 RID: 576
            Hex,
            // Token: 0x04000241 RID: 577
            Model
        }
    }
}
