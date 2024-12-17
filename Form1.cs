using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT5
{
    public partial class frmSoanVanBan : Form
    {
        public frmSoanVanBan()
        {
            InitializeComponent();

            // Liên kết sự kiện SelectedIndexChanged với ComboBox
            cmbFont.SelectedIndexChanged += new EventHandler(cmbFont_SelectedIndexChanged);
            cmbSize.SelectedIndexChanged += new EventHandler(cmbSize_SelectedIndexChanged);

            // Gọi các phương thức khởi tạo dữ liệu cho ComboBox
            this.Load += new EventHandler(frmSoanVanBan_Load);
        }

        private void loadFont()
        {
            foreach (FontFamily fontFamily in new InstalledFontCollection().Families)
            {
                cmbFont.Items.Add(fontFamily.Name);
            }
            cmbFont.SelectedItem = "Tahoma";
        }

        private void loadSize()
        {
            int[] sizeValues = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            cmbSize.ComboBox.DataSource = sizeValues;
            cmbSize.SelectedItem = 14;
        }
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                rtbVanban.ForeColor = fontDlg.Color;
                rtbVanban.Font = fontDlg.Font;
            }
        }

        private void frmSoanVanBan_Load(object sender, EventArgs e)
        {
            loadFont();
            loadSize();
            rtbVanban.Font = new Font("Tahoma", 14, FontStyle.Regular);
        }
        private void TạoVănBảnMới()
        {
            // Xóa nội dung hiện có trên RichTextBox
            rtbVanban.Clear();

            // Đặt lại font và size mặc định
            rtbVanban.Font = new Font("Tahoma", 14, FontStyle.Regular);

            // (Nếu có thêm các giá trị mặc định khác, thêm chúng ở đây)
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            TạoVănBảnMới();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TạoVănBảnMới();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbVanban.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|RichText files (*.rtf)|*.rtf";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                try
                {
                    if (Path.GetExtension(selectedFileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        rtbVanban.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        rtbVanban.LoadFile(selectedFileName, RichTextBoxStreamType.RichText);
                    }
                    MessageBox.Show("Tập tin đã được mở thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình mở tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string currentFilePath = string.Empty;
        private void LưuNộiDung()
        {
            if (string.IsNullOrEmpty(currentFilePath)) // Nếu là văn bản mới chưa được lưu
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Rich Text Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";
                saveFileDialog.DefaultExt = "rtf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    rtbVanban.SaveFile(currentFilePath);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else // Nếu là văn bản đã được lưu trước đó
            {
                rtbVanban.SaveFile(currentFilePath);
                MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LưuNộiDung();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LưuNộiDung();
        }

        private void cmbFont_Click(object sender, EventArgs e)
        {

        }

        private void cmbSize_Click(object sender, EventArgs e)
        {

        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (rtbVanban.SelectionFont != null)
            {
                Font oldFont = rtbVanban.SelectionFont;
                FontStyle newStyle;

                // Nếu văn bản đã đậm, xóa thuộc tính Bold khỏi FontStyle hiện tại
                if (oldFont.Style.HasFlag(FontStyle.Bold))
                    newStyle = oldFont.Style & ~FontStyle.Bold;
                else
                    // Nếu văn bản chưa đậm, thêm thuộc tính Bold vào FontStyle hiện tại
                    newStyle = oldFont.Style | FontStyle.Bold;

                rtbVanban.SelectionFont = new Font(oldFont.FontFamily, oldFont.Size, newStyle);
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (rtbVanban.SelectionFont != null)
            {
                Font oldFont = rtbVanban.SelectionFont;
                FontStyle newStyle;

                // Nếu văn bản đã đậm, xóa thuộc tính Bold khỏi FontStyle hiện tại
                if (oldFont.Style.HasFlag(FontStyle.Italic))
                    newStyle = oldFont.Style & ~FontStyle.Italic;
                else
                    // Nếu văn bản chưa đậm, thêm thuộc tính Bold vào FontStyle hiện tại
                    newStyle = oldFont.Style | FontStyle.Italic;

                rtbVanban.SelectionFont = new Font(oldFont.FontFamily, oldFont.Size, newStyle);
            }

        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (rtbVanban.SelectionFont != null)
            {
                Font oldFont = rtbVanban.SelectionFont;
                FontStyle newStyle;

                // Nếu văn bản đã đậm, xóa thuộc tính Bold khỏi FontStyle hiện tại
                if (oldFont.Style.HasFlag(FontStyle.Underline))
                    newStyle = oldFont.Style & ~FontStyle.Underline;
                else
                    // Nếu văn bản chưa đậm, thêm thuộc tính Bold vào FontStyle hiện tại
                    newStyle = oldFont.Style | FontStyle.Underline;

                rtbVanban.SelectionFont = new Font(oldFont.FontFamily, oldFont.Size, newStyle);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbVanban.SelectionFont != null && cmbFont.SelectedItem != null)
            {
                Font currentFont = rtbVanban.SelectionFont;
                string newFontFamily = cmbFont.SelectedItem.ToString();
                rtbVanban.SelectionFont = new Font(newFontFamily, currentFont.Size, currentFont.Style);
            }
        }
        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbVanban.SelectionFont != null && cmbSize.SelectedItem != null)
            {
                Font currentFont = rtbVanban.SelectionFont;
                float newSize;
                if (float.TryParse(cmbSize.SelectedItem.ToString(), out newSize))
                {
                    rtbVanban.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                }
            }
        }
    }
}
