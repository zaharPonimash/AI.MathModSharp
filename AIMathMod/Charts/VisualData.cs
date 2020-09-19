/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 05.11.2018
 * Время: 13:57
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.ML.Datasets;
using System.Drawing;
using System.Windows.Forms;

namespace AI.MathMod.Charts
{
    /// <summary>
    /// Description of VisualData.
    /// </summary>
    public partial class VisualData : Form
    {

        /// <summary>
        /// Форма для визуализации данных
        /// </summary>
        /// <param name="vid">Выборка где вектору сопоставлена метка класса</param>
        public VisualData(VectorIntDataset vid)
        {
            InitializeComponent();

            int n = -1;

            for (int i = 0; i < vid.Count; i++)
            {
                if (n < vid[i].ClassMark)
                {
                    n = vid[i].ClassMark;
                }
            }

            n++;

            Vector[] vects = vid.DataVisual(n);

            Color[] colors =
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Black,
                Color.Brown,
                Color.Gray,
                Color.Yellow,
                Color.YellowGreen,
                Color.DarkSalmon,
                Color.DarkOrange,
                Color.Gold,
                Color.Magenta
            };

            GraphicsView.ScattersVis(zedGraphControl1, vects, colors, "X", "Y");

        }
    }
}
