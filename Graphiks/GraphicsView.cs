/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 03.06.2017
 * Время: 14:12
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod;
using ZedGraph;
using AI.MathMod.AdditionalFunctions;
using System.Drawing;
using System.Windows.Forms;


namespace AI.MathMod.Graphiks
{
	/// <summary>
	/// Description of GraphicsView.
	/// </summary>
	public static class GraphicsView
	{






        /// <summary>
        /// Строит график в контроле graph, по отсчетам funcItemClass
        /// </summary>
        /// <param name="graph">Элемент интерфейса для вывода графика</param>
        /// <param name="funcItemClass">Значения y</param>
        public static void Plot(ZedGraphControl graph, Vector funcItemClass)
        {
            try
            {
                double[] y = funcItemClass.Vecktor;
                double[] x = MathFunc.GenerateTheSequence(0, y.Length).Vecktor;

                graph.GraphPane.CurveList.Clear();
                graph.GraphPane.XAxis.Title.Text = "x";
                graph.GraphPane.YAxis.Title.Text = "f(x)";
                graph.GraphPane.Title.Text = "Функция";
                graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
                graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
                graph.GraphPane.AddCurve("Функция", x, y, Color.Red, SymbolType.None);
                graph.AxisChange();
                graph.Invalidate();
            }
            catch { }
        }



        /// <summary>
		/// Строит график в контроле graph, по отсчетам funcItemClass
		/// </summary>
		/// <param name="graph">Элемент интерфейса для вывода графика</param>
		/// <param name="funcItemClass">Значения y</param>
		public static void Bar(ZedGraphControl graph, Vector funcItemClass)
        {
            try
            {
                double[] y = funcItemClass.Vecktor;
                double[] x = MathFunc.GenerateTheSequence(0, y.Length).Vecktor;

                graph.GraphPane.CurveList.Clear();
                graph.GraphPane.XAxis.Title.Text = "x";
                graph.GraphPane.YAxis.Title.Text = "f(x)";
                graph.GraphPane.Title.Text = "Гистограмма";
                graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
                graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
                graph.GraphPane.AddBar("Гистограмма", x, y, Color.Green);
                graph.AxisChange();
                graph.Invalidate();
            }
            catch { }
        }


        /// <summary>
        /// Вывод графика
        /// </summary>
        /// <param name="graph">Поле для вывода</param>
        /// <param name="y">Вектор Y</param>
        /// <param name="x">Вектор X</param>
        /// <param name="nameFunc">Имя ф-и</param>
        /// <param name="nameX">Имя оси X</param>
        /// <param name="nameY">Имя оси Y</param>
        /// <param name="colorLine">Цвет линии</param>
        public static void Plot(ZedGraphControl graph, Vector y, Vector x, string nameFunc, string nameX, string nameY, Color colorLine)
		{
			try{
			double[] y1 = y.Vecktor;
			double[] x1 = x.Vecktor;
			graph.GraphPane.CurveList.Clear();
			graph.GraphPane.XAxis.Title.Text = nameX;
			graph.GraphPane.YAxis.Title.Text = nameY;
			graph.GraphPane.Title.Text = nameFunc;
			graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.AddCurve(nameFunc, x1, y1, colorLine, SymbolType.None);
			graph.AxisChange ();
    		graph.Invalidate ();
			}
			catch{}
		}



        /// <summary>
        /// График от одной переменной
        /// </summary>
        public static void Plot(ZedGraphControl graph, Vector y, Vector x, string nameX, string nameY, Color colorLine)
		{
			try{
			double[] y1 = y.Vecktor;
			double[] x1 = x.Vecktor;
			graph.GraphPane.CurveList.Clear();
			graph.GraphPane.XAxis.Title.Text = nameX;
			graph.GraphPane.YAxis.Title.Text = nameY;
			graph.GraphPane.Title.Text = "Функция";
			graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.AddCurve("Функция", x1, y1, colorLine, SymbolType.None);
			graph.AxisChange ();
    		graph.Invalidate ();
			}
			catch{}
		}


        /// <summary>
        /// График от одной переменной
        /// </summary>
        public static void Plot(ZedGraphControl graph, Vector y, Vector x, Color colorLine)
		{
			try{
			double[] y1 = y.Vecktor;
			double[] x1 = x.Vecktor;
			graph.GraphPane.CurveList.Clear();
			graph.GraphPane.XAxis.Title.Text = "x";
			graph.GraphPane.YAxis.Title.Text = "f(x)";
			graph.GraphPane.Title.Text = "Функция";
			graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.AddCurve("Функция", x1, y1, colorLine, SymbolType.None);
			graph.AxisChange ();
    		graph.Invalidate ();
			}
			catch{}
		}



        /// <summary>
        /// График от одной переменной
        /// </summary>
        public static void Plot(ZedGraphControl graph, Vector y, Vector x)
		{
			try{
			double[] y1 = y.Vecktor;
			double[] x1 = x.Vecktor;
			graph.GraphPane.CurveList.Clear();
			graph.GraphPane.XAxis.Title.Text = "x";
			graph.GraphPane.YAxis.Title.Text = "f(x)";
			graph.GraphPane.Title.Text = "Функция";
			graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.AddCurve("Функция", x1, y1, Color.Red, SymbolType.None);
			graph.AxisChange ();
    		graph.Invalidate ();
			}
			catch{}
		}



        /// <summary>
        /// График от одной переменной
        /// </summary>
        public static void Plot(Vector funcItemClass)
		{
			VisualPlot vp = new VisualPlot(funcItemClass);
			vp.Show();
		}
        /// <summary>
        /// График от одной переменной
        /// </summary>
        public static void Plot(Vector y, Vector x, string nameX, string nameY, Color colorLine)
		{
			VisualPlot vp = new VisualPlot( y,  x, nameX, nameY, colorLine);
			vp.Show();
		}
        /// <summary>
        /// График от одной переменной
        /// </summary>
        public static void Plot(Vector y, Vector x, string nameFunc, string nameX, string nameY, Color colorLine)
		{
			VisualPlot vp = new VisualPlot(y,  x, nameFunc, nameX, nameY, colorLine);
			vp.Show();
		}
        /// <summary>
        /// График от одной переменной
        /// </summary>
        public static void Plot(Vector y, Vector x, Color colorLine)
		{
			VisualPlot vp = new VisualPlot(y,  x,  colorLine);
			vp.Show();
		}
		
		/// <summary>
        /// График от одной переменной
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
		public static void Plot(Vector y, Vector x)
		{
			VisualPlot vp = new VisualPlot(y,  x);
			vp.Show();
		}
	}
}
