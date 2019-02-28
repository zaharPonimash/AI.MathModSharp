using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AI.MathMod;
using AI.MathMod.ComputerVision;

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

        Bitmap bitmap = new Bitmap("4.jpg");

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
