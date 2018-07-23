/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 03.06.2017
 * Время: 17:17
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod;
using System.Numerics;

namespace AI.MathMod.Signals
{
	/// <summary>
	/// Description of Hilbert.
	/// </summary>
	public static class FastHilbert
	{
			/// <summary>
			/// Сигнал сопряженный по Гильберту
			/// </summary>
			/// <param name="st">Исходный сигнал</param>
			public static Vector ConjugateToTheHilbert(Vector st)
			{
				Vector stNew = st.CutAndZero(Functions.NextPow2(st.N));
				ComplexVector cv = Furie.fft(stNew);
				Complex j = new Complex(0,1);
				int n1 = stNew.N/2, n2 = stNew.N;
				
				
				for (int i = 0; i < n1; i++) 
				{
					cv.Vecktor[i] = cv.Vecktor[i]*(-j);
				}
				
				
				for (int i = n1; i < n2; i++) 
				{
					cv.Vecktor[i] = cv.Vecktor[i]*j;
				}
				
				cv = Furie.ifft(cv).CutAndZero(st.N);
				return cv.RealToVector();
			}
			
			
			/// <summary>
			/// Аналитический сигнал
			/// </summary>
			/// <param name="st">Входной сигнал</param>
			public static ComplexVector GetAnalSig(Vector st)
			{
				ComplexVector cv = new ComplexVector(st.N);
				Vector stH = ConjugateToTheHilbert(st);
				
				for (int i = 0; i < st.N; i++) 
				{
					cv.Vecktor[i] = new Complex(st.Vecktor[i], stH.Vecktor[i]);
				}
				
				return cv;
			}
			
			
			/// <summary>
			/// Огибающая
			/// </summary>
			/// <param name="st">Входной сигнал</param>
			public static Vector Ogib(Vector st)
			{
				return GetAnalSig(st).MagnitudeToVector();
			}
			
			/// <summary>
			/// Мгновенная фаза
			/// </summary>
			/// <param name="st">Входной сигнал</param>
			public static Vector Phase(Vector st)
			{
				return GetAnalSig(st).PhaseToVector();
			}
			
			/// <summary>
			/// Мгновенная частота
			/// </summary>
			/// <param name="st">Входной сигнал</param>
			public static Vector Frequency(Vector st)
			{
				return Functions.Diff(GetAnalSig(st).PhaseToVector());
			}
			
			
			
			public static Vector OgibNew(Vector t,Vector st, double f0)
			{
				ComplexVector cV = new ComplexVector(t.N);
				
				for (int i = 0; i < t.N; i++) 
				{
					cV[i] = Complex.Exp(-new Complex(0,1)*Math.PI*2*f0*t[i]);
				}
				
				ComplexVector newSt = st*cV;
				
				
				return newSt.RealToVector()+newSt.ImgToVector();
				
			}
	}
}
