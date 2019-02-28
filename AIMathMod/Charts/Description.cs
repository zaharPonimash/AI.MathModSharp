/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 14.09.2018
 * Время: 10:42
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.Charts
{
	/// <summary>
	/// Описание для графика
	/// </summary>
	public class Description
	{
		/// <summary>
		/// Название оси X
		/// </summary>
		public string X;
		/// <summary>
		/// Название оси Y
		/// </summary>
		public string Y;
		/// <summary>
		/// Название графика
		/// </summary>
		public string Name;
		
		/// <summary>
		/// Описание графика
		/// </summary>
		/// <param name="xL">Название оси X</param>
		/// <param name="yL">Название оси Y</param>
		/// <param name="name">Название графика</param>
		public Description(string xL, string yL, string name)
		{
			X = xL;
			Y = yL;
			Name = name;
		}
	}
}
