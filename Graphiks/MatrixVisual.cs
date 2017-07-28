/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 06.06.2017
 * Время: 22:16
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Graphiks
{
	/// <summary>
	/// Description of MatrixVisual.
	/// </summary>
	public partial class MatrixVisual : Form
	{
        /// <summary>
        /// Визуализация матриц
        /// </summary>
        /// <param name="matr"></param>
		public MatrixVisual(Matrix matr)
		{
			_matr = matr;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			bmp = new Bitmap(matr.M, matr.N);
			
			Vector a = matr.Spagetiz();
			max = new Statistic(MathFunc.abs(a)).MaxValue;
			k = 250.0/max;
			Visualiz();
			sf.Filter = "Картинка|*.png";
		}
		
		
		double intensiv = 0;
		double max, k;
		Bitmap bmp;
		Matrix _matr;
		Color color;
		
		
		int BiueInt()
		{
			return 120/((int)intensiv+1);
		}
		
		int RedInt()
		{
			try
			{
			return (int)(intensiv)/220;
			}
			catch{return 0;}
		}
		
		
		
		/// <summary>
        /// Визуализация матрицы
        /// </summary>
		public void Visualiz()
		{
			for (int i = 0; i < _matr.M; i++)
			{
				for (int j = 0; j < _matr.N; j++)
				{
					intensiv = Math.Abs( k*_matr.Matr[i,j]);
					try{
					color = Color.FromArgb((int)(RedInt()*intensiv),(int)(0.2*intensiv), (int)(BiueInt()*intensiv));
					}
					catch{color = Color.Coral;}
					bmp.SetPixel(i,j, color);
				}
			}
			
			
			pictureBox1.Image = bmp;
		}
		
		SaveFileDialog sf = new SaveFileDialog();
		
		void button1_Click(object sender, EventArgs e)
		{
			try
			{
				sf.ShowDialog();
				bmp.Save(sf.FileName);
			}
			
			catch
			{
			
			}
		}
		
	}
}
