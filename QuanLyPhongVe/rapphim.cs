using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class rapphim : Form
    {
        private int[] giaVe = new int[21]; // giá vé theo số ghế
        private List<Button> gheDangChon = new List<Button>(); // cho phép chọn nhiều ghế
        private string defaultFile = "khachhang.csv"; // file lưu mặc định

        public rapphim()
        {
            InitializeComponent();
            TaoBangGiaVe();
            GanSuKienChoGhe();

            // Gắn sự kiện cho các nút chính
            button21.Click -= button21_Click; button21.Click += button21_Click; // Thêm
            button22.Click -= button22_Click; button22.Click += button22_Click; // Xóa
            button23.Click -= button23_Click; button23.Click += button23_Click; // Chi tiết

            lưuToolStripMenuItem.Click -= lưuToolStripMenuItem_Click; lưuToolStripMenuItem.Click += lưuToolStripMenuItem_Click;
            thoátToolStripMenuItem.Click -= thoátToolStripMenuItem_Click; thoátToolStripMenuItem.Click += thoátToolStripMenuItem_Click;

            // Load dữ liệu từ file nếu có
            LoadDataFromFile();
        }

        private void TaoBangGiaVe()
        {
            for (int i = 1; i <= 5; i++) giaVe[i] = 30000;   // Ghế 1-5: 30k
            for (int i = 6; i <= 10; i++) giaVe[i] = 40000; // Ghế 6-10: 40k
            for (int i = 11; i <= 15; i++) giaVe[i] = 50000; // Ghế 11-15: 50k
            for (int i = 16; i <= 20; i++) giaVe[i] = 80000; // Ghế 16-20: 80k
        }

        private void GanSuKienChoGhe()
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is Button btn && btn.Name.StartsWith("button"))
                {
                    // Lấy số từ tên control: "button1" -> 1
                    string soStr = new string(btn.Name.Where(char.IsDigit).ToArray());
                    if (int.TryParse(soStr, out int soGhe))
                    {
                        btn.Text = soGhe.ToString();
                        btn.Tag = soGhe;
                        btn.BackColor = Color.White;

                        btn.Click -= BtnGhe_Click;
                        btn.Click += BtnGhe_Click;
                    }
                }
            }
        }

        private void BtnGhe_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn == null) return;

            if (btn.BackColor == Color.Yellow)
            {
                MessageBox.Show("Ghế " + btn.Text + " đã có người mua.");
                return;
            }

            // Nếu ghế đang chọn thì bỏ chọn
            if (btn.BackColor == Color.LightBlue)
            {
                btn.BackColor = Color.White;
                gheDangChon.Remove(btn);
            }
            else
            {
                btn.BackColor = Color.LightBlue;
                gheDangChon.Add(btn);
            }
            CapNhatTongTienTamThoi();

        }
        private void CapNhatTongTienTamThoi()
        {
            int tong = 0;
            foreach (var ghe in gheDangChon)
            {
                tong += giaVe[(int)ghe.Tag];
            }
            labelTongTien.Text = tong.ToString("N0") + " VND";
        }


        private void button21_Click(object sender, EventArgs e) // Thêm
        {
            if (gheDangChon.Count == 0)
            {
                MessageBox.Show("Hãy chọn ít nhất một ghế trước!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Hãy nhập đủ Tên và SĐT khách hàng!");
                return;
            }

            string ten = textBox1.Text.Trim();
            string sdt = textBox2.Text.Trim();
            string gioiTinh = comboBox1.Text;
            string tinhThanh = comboBox2.Text;

            List<string> gheList = new List<string>();
            int tongTien = 0;

            foreach (var ghe in gheDangChon)
            {
                ghe.BackColor = Color.Yellow;
                gheList.Add(ghe.Text);
                tongTien += giaVe[(int)ghe.Tag];
            }

            string gheStr = string.Join(";", gheList);
            dataGridView1.Rows.Add(ten, sdt, gioiTinh, tinhThanh, gheList.Count, gheStr, tongTien);

            // Reset input
            gheDangChon.Clear();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            CapNhatTongTienTamThoi();

        }

        private void button22_Click(object sender, EventArgs e) // Xóa
        {
            // Nếu bảng không có dữ liệu (chỉ có dòng trống) thì thoát luôn
            if (dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Không có dữ liệu để xóa!");
                return;
            }

            if (dataGridView1.SelectedRows.Count > 0)
            {
                var rows = dataGridView1.SelectedRows.Cast<DataGridViewRow>().ToArray();
                foreach (DataGridViewRow row in rows)
                {
                    if (row.IsNewRow) continue; // bỏ qua dòng trống

                    string gheList = row.Cells["soghe"].Value?.ToString();
                    if (!string.IsNullOrEmpty(gheList))
                    {
                        foreach (string ghe in gheList.Split(';'))
                        {
                            foreach (Control ctrl in groupBox1.Controls)
                            {
                                if (ctrl is Button btn && btn.Text == ghe)
                                {
                                    btn.BackColor = Color.White; // trả ghế về trạng thái trống
                                    break;
                                }
                            }
                        }
                    }

                    dataGridView1.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn khách hàng trong bảng để xóa!");
            }
        }

        private void button23_Click(object sender, EventArgs e) // Chi tiết ghế
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                string msg = $"Tên KH: {row.Cells["tenKH"].Value}\n" +
                             $"SĐT: {row.Cells["SDT"].Value}\n" +
                             $"Giới tính: {row.Cells["gioitinh"].Value}\n" +
                             $"Tỉnh/TP: {row.Cells["tinhtp"].Value}\n" +
                             $"Số lượng ghế: {row.Cells["slghe"].Value}\n" +
                             $"Danh sách ghế: {row.Cells["soghe"].Value}\n" +
                             $"Tổng tiền: {row.Cells["tongtien"].Value}";
                MessageBox.Show(msg, "Chi tiết ghế");
            }
            else
            {
                MessageBox.Show("Hãy chọn khách hàng trong bảng để xem chi tiết!");
            }
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV file (*.csv)|*.csv";
            save.Title = "Lưu danh sách khách hàng";
            save.FileName = defaultFile;
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveToFile(save.FileName);
                    MessageBox.Show("Đã lưu danh sách vào file: " + save.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message);
                }
            }
        }

        private void SaveToFile(string path)
        {
            StringBuilder sb = new StringBuilder();

            // Header
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                sb.Append(dataGridView1.Columns[i].HeaderText);
                if (i < dataGridView1.Columns.Count - 1) sb.Append(",");
            }
            sb.AppendLine();

            // Data
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    var val = row.Cells[i].Value;
                    string cell = val == null ? "" : val.ToString();
                    if (cell.Contains("\"")) cell = cell.Replace("\"", "\"\"");
                    if (cell.Contains(",") || cell.Contains("\"") || cell.Contains("\n"))
                        cell = $"\"{cell}\"";
                    sb.Append(cell);
                    if (i < dataGridView1.Columns.Count - 1) sb.Append(",");
                }
                sb.AppendLine();
            }

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }

        private string[] ParseCsvLine(string line)
        {
            var values = new List<string>();
            bool inQuotes = false;
            var cur = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"') { cur.Append('"'); i++; }
                        else inQuotes = false;
                    }
                    else cur.Append(c);
                }
                else
                {
                    if (c == '"') inQuotes = true;
                    else if (c == ',') { values.Add(cur.ToString()); cur.Clear(); }
                    else cur.Append(c);
                }
            }
            values.Add(cur.ToString());
            return values.ToArray();
        }

        private void LoadDataFromFile()
        {
            if (!File.Exists(defaultFile)) return;

            string[] lines = File.ReadAllLines(defaultFile, Encoding.UTF8);
            if (lines.Length <= 1) return;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] parts = ParseCsvLine(lines[i]);
                if (parts.Length >= 7)
                {
                    dataGridView1.Rows.Add(parts[0], parts[1], parts[2],
                                           parts[3], parts[4], parts[5], parts[6]);

                    // Đổi màu ghế đã mua
                    string soghe = parts[5];
                    foreach (string ghe in soghe.Split(';'))
                    {
                        foreach (Control ctrl in groupBox1.Controls)
                        {
                            if (ctrl is Button btn && btn.Text == ghe)
                            {
                                btn.BackColor = Color.Yellow;
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                SaveToFile(defaultFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu tự động: " + ex.Message);
            }
            base.OnFormClosing(e);
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // sự kiện rỗng
        }
    }
}
