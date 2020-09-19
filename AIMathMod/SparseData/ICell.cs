/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 03.09.2018
 * Время: 0:13
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */

namespace AI.MathMod.SparseData
{
    /// <summary>
    /// Description of ICell.
    /// </summary>
    public interface ICell<T>
    {
        /// <summary>
        /// Значение
        /// </summary>
        T Value { get; set; }
        /// <summary>
        /// Координаты
        /// </summary>
        int[] Coordinats { get; set; }
    }
}
