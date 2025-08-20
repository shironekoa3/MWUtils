namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtWinInfo = new System.Windows.Forms.TextBox();
            this.btnReadWin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWinName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbAutoCapture = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtOffsetX = new System.Windows.Forms.NumericUpDown();
            this.txtOffsetY = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffsetY)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(689, 554);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(681, 528);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基础功能";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(681, 528);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "窗口检测";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtWinInfo);
            this.groupBox1.Controls.Add(this.btnReadWin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtWinName);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 502);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "窗口初始化";
            // 
            // txtWinInfo
            // 
            this.txtWinInfo.Location = new System.Drawing.Point(34, 113);
            this.txtWinInfo.Multiline = true;
            this.txtWinInfo.Name = "txtWinInfo";
            this.txtWinInfo.Size = new System.Drawing.Size(197, 368);
            this.txtWinInfo.TabIndex = 11;
            this.txtWinInfo.Text = "窗口信息：";
            // 
            // btnReadWin
            // 
            this.btnReadWin.Location = new System.Drawing.Point(34, 60);
            this.btnReadWin.Name = "btnReadWin";
            this.btnReadWin.Size = new System.Drawing.Size(197, 37);
            this.btnReadWin.TabIndex = 10;
            this.btnReadWin.Text = "读取窗口";
            this.btnReadWin.UseVisualStyleBackColor = true;
            this.btnReadWin.Click += new System.EventHandler(this.btnReadWin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "窗口名称：";
            // 
            // txtWinName
            // 
            this.txtWinName.Location = new System.Drawing.Point(113, 29);
            this.txtWinName.Name = "txtWinName";
            this.txtWinName.Size = new System.Drawing.Size(118, 21);
            this.txtWinName.TabIndex = 8;
            this.txtWinName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOffsetY);
            this.groupBox2.Controls.Add(this.txtOffsetX);
            this.groupBox2.Controls.Add(this.ckbAutoCapture);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnCapture);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtHeight);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtWidth);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(293, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 502);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "屏幕截取";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(58, 210);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(269, 235);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "窗口偏移Y：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "窗口偏移X：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "截取高度：";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(287, 65);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(61, 21);
            this.txtHeight.TabIndex = 15;
            this.txtHeight.Text = "100";
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "截取宽度：";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(287, 28);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(61, 21);
            this.txtWidth.TabIndex = 13;
            this.txtWidth.Text = "100";
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(24, 113);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(226, 37);
            this.btnCapture.TabIndex = 12;
            this.btnCapture.Text = "读取画面";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "画面预览：";
            // 
            // ckbAutoCapture
            // 
            this.ckbAutoCapture.AutoSize = true;
            this.ckbAutoCapture.Checked = true;
            this.ckbAutoCapture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoCapture.Location = new System.Drawing.Point(276, 134);
            this.ckbAutoCapture.Name = "ckbAutoCapture";
            this.ckbAutoCapture.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoCapture.TabIndex = 24;
            this.ckbAutoCapture.Text = "自动读取";
            this.ckbAutoCapture.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtOffsetX
            // 
            this.txtOffsetX.Location = new System.Drawing.Point(99, 29);
            this.txtOffsetX.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.txtOffsetX.Name = "txtOffsetX";
            this.txtOffsetX.Size = new System.Drawing.Size(65, 21);
            this.txtOffsetX.TabIndex = 25;
            this.txtOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOffsetX.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtOffsetX.ValueChanged += new System.EventHandler(this.txtOffsetX_ValueChanged);
            // 
            // txtOffsetY
            // 
            this.txtOffsetY.Location = new System.Drawing.Point(99, 66);
            this.txtOffsetY.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.txtOffsetY.Name = "txtOffsetY";
            this.txtOffsetY.Size = new System.Drawing.Size(65, 21);
            this.txtOffsetY.TabIndex = 26;
            this.txtOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOffsetY.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtOffsetY.ValueChanged += new System.EventHandler(this.txtOffsetY_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 554);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "〇";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffsetY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWinInfo;
        private System.Windows.Forms.Button btnReadWin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWinName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckbAutoCapture;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown txtOffsetX;
        private System.Windows.Forms.NumericUpDown txtOffsetY;
    }
}

