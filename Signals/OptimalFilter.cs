/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 21.09.2018
 * Время: 9:30
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Signals
{
	/// <summary>
	/// Description of OptimalFilter.
	/// </summary>
	public class OptimalFilter
	{
		
		ComplexVector corFunc;
		Furie fur;
		
		/// <summary>
		/// Оптимальный (согласованный) фильтр
		/// </summary>
		/// <param name="signal"></param>
		/// <param name="n"></param>
		public OptimalFilter(Vector signal, int n)
		{
			fur = new Furie(n);
			corFunc = fur.FFT(signal.Revers().CutAndZero(n))/fur._n;
		
			
		}
		
		/// <summary>
		/// Прохождение фильтра
		/// </summary>
		/// <param name="signal">Сигнал на входе</param>
		public Vector Result(Vector signal)
		{
			ComplexVector signalFFT = fur.FFT(signal);
			return fur.RealIFFT(signalFFT*corFunc);
		}
		
		/// <summary>
		/// Сжатие ЛЧМ по спектру
		/// </summary>
		public Vector SpectrCompressLFM(Vector signal, int fd)
		{
			Vector signal2 = signal*signal;
			return Filters.FilterLow(signal2, 2000, Signal.Frequency(signal2.N,fd));
		}
		
		
		
	}
}
