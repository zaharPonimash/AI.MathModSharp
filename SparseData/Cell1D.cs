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

	public T Value {get; set;}

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
	
	public int coordinate;

	#endregion

		public Cell1D(){}
	
		public Cell1D(T val, int position)
		{
			Value = val;
			coordinate = position;
		}
	}
}
