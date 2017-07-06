/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 03.06.2017
 * Время: 14:29
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using AI.MathMod;
using ZedGraph;

namespace AI.MathMod.Graphiks
{
	/// <summary>
	/// Description of VisualPlot.
	/// </summary>
	public partial class VisualPlot : Form
	{
		public VisualPlot(Vector funcSempl)
		{
			InitializeComponent();
			GraphicsView.Plot(zedGraphControl1, funcSempl);
		}
		
		
		
		public VisualPlot(Vector y, Vector x, string nameX, string nameY, Color colorLine)
		{
			InitializeComponent();
			GraphicsView.Plot(zedGraphControl1, y, x, nameX, nameY, colorLine);
		}
		
		public VisualPlot(Vector y, Vector x, string nameFunc, string nameX, string nameY, Color colorLine)
		{
			InitializeComponent();
			GraphicsView.Plot(zedGraphControl1, y, x, nameFunc, nameX, nameY, colorLine);
		}
			
			
		public VisualPlot(Vector y, Vector x, Color colorLine)
		{
			InitializeComponent();
			GraphicsView.Plot(zedGraphControl1, y, x, colorLine);
		}
		
		
		public VisualPlot(Vector y, Vector x)
		{
			InitializeComponent();
			GraphicsView.Plot(zedGraphControl1, y, x);
		}
			
			
	}
}
