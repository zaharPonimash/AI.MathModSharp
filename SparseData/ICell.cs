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
	/// Description of ICell.
	/// </summary>
	
	public interface ICell<T>
	{
		T Value {get; set;}
		int[] Coordinats{get; set;}
	}
}
