/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 07.09.2018
 * Время: 18:18
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.SparseData
{
	/// <summary>
	/// Description of Cell2D.
	/// </summary>
	public class Cell2D<T> : ICell<T>
	{
		#region ICell implementation

		public T Value {get; set;}

		public int[] Coordinats {get; set;}
		
		public int X{get;set;}
		public int Y{get; set;}

	#endregion

		public Cell2D(int x, int y, T value)
		{
			X = x;
			Y = y;
			Value = value;
		}
	}
}
