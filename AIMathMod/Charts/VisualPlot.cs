﻿/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 03.06.2017
 * Время: 14:29
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System.Drawing;
using System.Windows.Forms;

namespace AI.MathMod.Charts
{
    /// <summary>
    /// Description of VisualPlot.
    /// </summary>
    public partial class VisualPlot : Form
    {
        /// <summary>
        /// Инициализация формы
        /// </summary>
		public VisualPlot(Vector funcItemClass)
        {
            InitializeComponent();
            GraphicsView.Plot(zedGraphControl1, funcItemClass);
        }


        /// <summary>
        /// Инициализация формы
        /// </summary>
        /// <param name="y">Вектор </param>
        /// <param name="x"></param>
        /// <param name="nameX"></param>
        /// <param name="nameY"></param>
        /// <param name="colorLine"></param>
        public VisualPlot(Vector y, Vector x, string nameX, string nameY, Color colorLine)
        {
            InitializeComponent();
            GraphicsView.Plot(zedGraphControl1, y, x, nameX, nameY, colorLine);
        }

        /// <summary>
        /// Отображение графика
        /// </summary>
        /// <param name="y">Y</param>
        /// <param name="x">X</param>
        /// <param name="desc">Описание</param>
        public VisualPlot(Vector y, Vector x, Description desc)
        {
            InitializeComponent();
            GraphicsView.Plot(zedGraphControl1, y, x, desc.Name, desc.X, desc.Y, Color.Black);
        }

        /// <summary>
        /// Инициализация формы
        /// </summary>
        public VisualPlot(Vector y, Vector x, string nameFunc, string nameX, string nameY, Color colorLine)
        {
            InitializeComponent();
            GraphicsView.Plot(zedGraphControl1, y, x, nameFunc, nameX, nameY, colorLine);
        }


        /// <summary>
        /// Инициализация формы
        /// </summary>
        public VisualPlot(Vector y, Vector x, Color colorLine)
        {
            InitializeComponent();
            GraphicsView.Plot(zedGraphControl1, y, x, colorLine);
        }

        /// <summary>
        /// Инициализация формы
        /// </summary>
        public VisualPlot(Vector y, Vector x)
        {
            InitializeComponent();
            GraphicsView.Plot(zedGraphControl1, y, x);
        }


    }
}
