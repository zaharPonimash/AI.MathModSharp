/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 07.09.2018
 * Время: 18:18
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */

namespace AI.MathMod.SparseData
{
    /// <summary>
    /// Description of Cell2D.
    /// </summary>
    public class Cell2D<T> : ICell<T>
    {
        #region ICell implementation
        /// <summary>
        /// Значение клетки
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Координаты
        /// </summary>
        public int[] Coordinats { get; set; }
        /// <summary>
        /// Коорд. X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Коорд. Y
        /// </summary>
        public int Y { get; set; }

        #endregion
        /// <summary>
        /// Клетка решетки
        /// </summary>
        /// <param name="x"> Коорд. X</param>
        /// <param name="y">Коорд. Y</param>
        /// <param name="value">Значение клетки</param>
        public Cell2D(int x, int y, T value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }
}
