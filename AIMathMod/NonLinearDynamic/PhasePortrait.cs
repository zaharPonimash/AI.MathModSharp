/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 04.11.2018
 * Время: 16:47
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.NonLinearDynamic
{
	/// <summary>
	/// Description of PhasePortrait.
	/// </summary>
	public class PhasePortrait
	{
		/// <summary>
		/// Фазовый портрет
		/// </summary>
		public PhasePortrait()
		{
		}
		
		/// <summary>
		/// Фазовый портрет, метод не реализован
		/// </summary>
		/// <param name="sempels">Отсчеты</param>
		public static Vector GetPhasePor(Vector sempels)
		{
			Vector difSemples = Functions.Diff(sempels);
			return difSemples;
		}
	}
}
