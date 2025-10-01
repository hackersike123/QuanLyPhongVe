namespace QuanLyPhongVe
{
    partial class login
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button(); // Đăng nhập
            this.button2 = new System.Windows.Forms.Button(); // Thoát
            this.button3 = new System.Windows.Forms.Button(); // Đăng ký
            this.SuspendLayout();
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 60);
            this.label1.Text = "Tên tài khoản:";
            // 
            this.textBox1.Location = new System.Drawing.Point(200, 57);
            this.textBox1.Size = new System.Drawing.Size(220, 22);
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 110);
            this.label2.Text = "Mật khẩu:";
            // 
            this.textBox2.Location = new System.Drawing.Point(200, 107);
            this.textBox2.Size = new System.Drawing.Size(220, 22);
            this.textBox2.PasswordChar = '*';
            // 
            this.button1.Location = new System.Drawing.Point(100, 170);
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.Text = "Đăng nhập";
            this.button1.UseVisualStyleBackColor = true;
            // 
            this.button2.Location = new System.Drawing.Point(320, 170);
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.Text = "Thoát";
            this.button2.UseVisualStyleBackColor = true;
            // 
            this.button3.Location = new System.Drawing.Point(210, 220);
            this.button3.Size = new System.Drawing.Size(100, 30);
            this.button3.Text = "Đăng ký";
            this.button3.UseVisualStyleBackColor = true;
            // 
            this.ClientSize = new System.Drawing.Size(520, 300);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập hệ thống";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
