/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 07.06.2017
 * Время: 11:46
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Signals
{
	/// <summary>
	///Кепстральный анализ
	/// </summary>
	public static class Kepstr
	{
        /// <summary>
        /// Быстрое кепстральное преобразование
        /// </summary>
        /// <param name="signal">Сигнал</param>
        /// <returns></returns>
		public static Vector FKT(Vector signal)
		{
			Vector signalNew = signal.CutAndZero(Functions.NextPow2(signal.N));
			ComplexVector spectr = Furie.fft(signalNew);
			Vector ampSpectLog = MathFunc.lg(spectr.MagnitudeToVector()^2);
			DCT dct = new DCT(signalNew.N, signal.N);
			Vector outp = dct.FDCT(ampSpectLog);
			return outp;
		}
	}
}
