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


namespace AI.MathMod.Charts
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
                double[] y = funcItemClass.DataInVector;
                double[] x = MathFunc.GenerateTheSequence(0, y.Length).DataInVector;

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
                double[] y = funcItemClass.DataInVector;
                double[] x = MathFunc.GenerateTheSequence(0, y.Length).DataInVector;

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
		/// Строит график в контроле graph, по отсчетам funcItemClass
		/// </summary>
		/// <param name="graph">Элемент интерфейса для вывода графика</param>
		/// <param name="funcItemClass">Значения y</param>
		 /// <param name="xV">Значение по Y</param>
		public static void Bar(ZedGraphControl graph, Vector funcItemClass, Vector xV)
        {
            try
            {
                double[] y = funcItemClass.DataInVector;
                double[] x = xV.DataInVector;

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
		/// Строит график из столбцов в контроле graph, по отсчетам funcItemClass
		/// </summary>
		/// <param name="graph">Элемент интерфейса для вывода графика</param>
		/// <param name="funcItemClass">Значения y</param>
		/// <param name="xV">Значения х</param>
		/// <param name="name">Имя</param>
		/// <param name="xN">Имя Х</param>
		/// <param name="yN">Имя Y</param>
		/// <param name="col">Цвет</param>
		public static void Bar(ZedGraphControl graph, Vector funcItemClass, Vector xV, string name, string xN, string yN, Color col)
        {
            try
            {
                double[] y = funcItemClass.DataInVector;
                double[] x = xV.DataInVector;

                graph.GraphPane.CurveList.Clear();
                graph.GraphPane.XAxis.Title.Text = xN;
                graph.GraphPane.YAxis.Title.Text = yN;
                graph.GraphPane.Title.Text = name;
                graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
                graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
                graph.GraphPane.AddBar("Гистограмма", x, y, col);
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
			double[] y1 = y.DataInVector;
			double[] x1 = x.DataInVector;
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
        /// Скатерограмма и график
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="ySc"></param>
        /// <param name="yPl"></param>
        /// <param name="x"></param>
        /// <param name="nameFunc"></param>
        /// <param name="nameX"></param>
        /// <param name="nameY"></param>
        /// <param name="colorLine"></param>
        /// <param name="colorC"></param>
        public static void ScatterPlot(ZedGraphControl graph, Vector ySc, Vector yPl, Vector x, string nameFunc, string nameX, string nameY, Color colorLine, Color colorC)
        {
        	try{
			double[] y1 = ySc.DataInVector;
			double[] y2 = yPl.DataInVector;
			double[] x1 = x.DataInVector;
			graph.GraphPane.CurveList.Clear();
			graph.GraphPane.XAxis.Title.Text = nameX;
			graph.GraphPane.YAxis.Title.Text = nameY;
			graph.GraphPane.Title.Text = nameFunc;
			graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
			LineItem myCurve1 = graph.GraphPane.AddCurve(nameFunc, x1, y2, colorLine, SymbolType.None);
			myCurve1.Line.Width = 3f;
			LineItem myCurve = graph.GraphPane.AddCurve(nameFunc, x1, y1, colorC, SymbolType.Circle);
			myCurve.Symbol.Fill = new Fill(colorC);
			myCurve.Line.IsVisible = false;	
			
			if(x.N < 20)
			myCurve.Symbol.Size = (int)(100.0/x.N);
			else if(x.N < 2000)
				myCurve.Symbol.Size = (int)(100.0/Math.Sqrt(x.N));
			else 
				myCurve.Symbol.Size = 2;
			
			graph.AxisChange ();
    		graph.Invalidate ();
			}
			catch{}
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
        public static void Scatter(ZedGraphControl graph, Vector y, Vector x, string nameFunc, string nameX, string nameY, Color colorLine)
		{
			try{
			double[] y1 = y.DataInVector;
			double[] x1 = x.DataInVector;
			graph.GraphPane.CurveList.Clear();
			graph.GraphPane.XAxis.Title.Text = nameX;
			graph.GraphPane.YAxis.Title.Text = nameY;
			graph.GraphPane.Title.Text = nameFunc;
			graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
			graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
			LineItem myCurve = graph.GraphPane.AddCurve(nameFunc, x1, y1, colorLine, SymbolType.Circle);
			myCurve.Symbol.Fill = new Fill(colorLine);
			myCurve.Symbol.Size = 3;
			myCurve.Line.IsVisible = false;
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
			double[] y1 = y.DataInVector;
			double[] x1 = x.DataInVector;
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
			double[] y1 = y.DataInVector;
			double[] x1 = x.DataInVector;
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
			double[] y1 = y.DataInVector;
			double[] x1 = x.DataInVector;
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
        public static void PlotD(Vector funcItemClass)
		{
			VisualPlot vp = new VisualPlot(funcItemClass);
			vp.ShowDialog();
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
        public static void Plot(Vector y, Vector x, Description desc)
		{
			VisualPlot vp = new VisualPlot(y,  x, desc);
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
		
		/// <summary>
		/// Визуализация точек
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="xy"></param>
		/// <param name="colors"></param>
		/// <param name="nameX"></param>
		/// <param name="nameY"></param>
		public static void ScattersVis(ZedGraphControl graph, Vector[] xy, Color[] colors, string nameX, string nameY)
		{
			int n = xy.Length/2;
			
			try
			{
				graph.GraphPane.CurveList.Clear();
				graph.GraphPane.XAxis.Title.Text = nameX;
				graph.GraphPane.YAxis.Title.Text = nameY;
				graph.GraphPane.Title.Text = "R^n -> R^2";
				graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
				graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
			
				for (int i = 0, k = 0; i < n; i++) 
				{
					LineItem myCurve = graph.GraphPane.AddCurve("Class #"+i, xy[k++].DataInVector, xy[k++].DataInVector, colors[i%n], SymbolType.Circle);
					myCurve.Symbol.Fill = new Fill(colors[i%n]);
					myCurve.Symbol.Size = 3;
					myCurve.Line.IsVisible = false;
				}
			
				graph.AxisChange ();
    			graph.Invalidate ();
			}
			catch{}
		}
	}
}
