/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 03.09.2018
 * Время: 0:13
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.SparseData
{
	/// <summary>
	/// Description of Cell.
	/// </summary>
	public class Cell1D<T> : ICell<T>
	{
		#region ICell implementation

	/// <summary>
	/// Значение
	/// </summary>
	public T Value {get; set;}

	/// <summary>
	/// Коорд.
	/// </summary>
	public int[] Coordinats {
		get 
		{
			return new int[]{coordinate};
		}
		set 
		{
			coordinate = value[0];
		}
	}
	/// <summary>
	/// Коорд.
	/// </summary>
	public int coordinate;

	#endregion
		/// <summary>
		/// одномерная клетка
		/// </summary>
		public Cell1D(){}
		/// <summary>
		/// одномерная клетка
		/// </summary>
		public Cell1D(T val, int position)
		{
			Value = val;
			coordinate = position;
		}
	}
}
