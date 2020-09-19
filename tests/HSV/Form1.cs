using AI.MathMod;
using AI.MathMod.ComputerVision;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(bitmap, 510, 450);
            pictureBox1.Image = bitmap;

        }

        private readonly Bitmap bitmap = new Bitmap("4.jpg");

        private void button1_Click(object sender, EventArgs e)
        {
            Matrix hM = ImgConverter.BmpToHMatr(bitmap);
            pictureBox1.Image = ImgConverter.MatrixToBitmap(hM);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = bitmap;
        }
    }
}
