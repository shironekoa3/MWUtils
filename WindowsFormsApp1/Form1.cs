using MWUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static Win32.RECT rect = new Win32.RECT();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnReadWin_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Interval = 500;
                timer1.Enabled = true;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            RefreshCapture();
        }

        public void RefreshCapture()
        {
            rect = Win32.GetGameWindow(null, txtWinName.Text);
            int offsetX = (int)txtOffsetX.Value;
            int offsetY = (int)txtOffsetY.Value;
            int.TryParse(txtWidth.Text, out int width);
            int.TryParse(txtHeight.Text, out int height);

            var img = IMGUtil.GetScreenCapture(rect.Left + offsetX, rect.Top + offsetY, width, height);

            this.pictureBox1.Image = img;
        }

        private void txtOffsetX_ValueChanged(object sender, EventArgs e)
        {
            if (ckbAutoCapture.Checked)
            {
                RefreshCapture();
            }
        }
        private void txtOffsetY_ValueChanged(object sender, EventArgs e)
        {
            if (ckbAutoCapture.Checked)
            {
                RefreshCapture();
            }
        }
        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            if (ckbAutoCapture.Checked)
            {
                RefreshCapture();
            }
        }
        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (ckbAutoCapture.Checked)
            {
                RefreshCapture();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr ptr = Win32.FindWindow(null, txtWinName.Text);

            rect = Win32.GetGameWindow(null, txtWinName.Text);

            txtWinInfo.Text = $"窗口信息：\r\n\r\n" +
                $"窗口句柄：{ptr.ToString()}\r\n" +
                $"窗口句柄(HEX)：{ptr.ToString("X")}\r\n" +
                $"上:{rect.Top}\r\n" +
                $"下:{rect.Bottom}\r\n" +
                $"左:{rect.Left}\r\n" +
                $"右:{rect.Right}\r\n" +
                $"宽:{rect.Right - rect.Left}\r\n" +
                $"高:{rect.Bottom - rect.Top}\r\n" +
                "";
        }

        
    }
}
