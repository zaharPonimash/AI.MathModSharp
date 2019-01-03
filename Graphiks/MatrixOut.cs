/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 30.06.2018
 * Время: 16:05
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AI.MathMod.Graphiks
{
	/// <summary>
	/// Description of MatrixOut.
	/// </summary>
	public partial class MatrixOut : Form
	{
		Matrix mtr;
		
		/// <summary>
		/// Отображение Матрицы
		/// </summary>
		/// <param name="matr">Матрица</param>
		public MatrixOut(Matrix matr)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			mtr = matr;
			ShowMatrix();
		}
		
		
		void ShowMatrix()
		{
			dataGridView1.ColumnCount = mtr.N;
			dataGridView1.RowCount = mtr.M;
			
			for (int i = 0; i < mtr.N; i++) {
				for (int j = 0; j < mtr.M; j++) {
					dataGridView1[i,j].Value =  mtr[j,i];
				}
			}
		}
		
		
		void ВизуализацияClick(object sender, EventArgs e)
		{
			mtr.Visual();
		}
	}
}
