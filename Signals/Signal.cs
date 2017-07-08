/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 05.06.2017
 * Время: 11:12
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Specialized;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod;

namespace AI.MathMod.Signals
{
	/// <summary>
	/// Description of Signal.
	/// </summary>
	public static class Signal
	{
		
		#region Синус
		public static Vector Sin(Vector t, double A, double f, double fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}
		
		public static Vector Sin(Vector t, double A, Vector f, double fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}
		
		public static Vector Sin(Vector t, double A, double f, Vector fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}
		
		public static Vector Sin(Vector t, Vector A, double f, double fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}
		
		public static Vector Sin(Vector t, double A, double f)
		{
			return A*MathFunc.sin(t*2*Math.PI*f);
		}
		
		/// <summary>
		/// Массив частот
		/// </summary>
		/// <param name="N">Кол-во значений</param>
		/// <param name="fd">Частота дискретизации</param>
		/// <returns>Вектор частот</returns>
		public static Vector Frequency(int N, double fd)
		{
			double dt = 1.0/fd, df = 1/(N*dt);
			return MathFunc.GenerateTheSequence(0,df,(N-1)*df).CutAndZero(N);
		}
		
		
		public static Vector Sin(Vector t, double A, Vector f)
		{
			return A*MathFunc.sin(t*2*Math.PI*f);
		}
		
		
		public static Vector Sin(Vector t, Vector A, double f)
		{
			return A*MathFunc.sin(t*2*Math.PI*f);
		}
		
		
		public static Vector Sin(Vector t, double f)
		{
			return MathFunc.sin(t*2*Math.PI*f);
		}
		
		public static Vector Sin(Vector t, Vector f)
		{
			return MathFunc.sin(t*2*Math.PI*f);
		}
		#endregion
		
		
		#region Прямоугольный
				
		public static Vector Rect(Vector t, double A, double f, double fi)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f,fi),0.1);
		}
		
		
		public static Vector Rect(Vector t, Vector A, double f, double fi)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f,fi),0.1);
		}
		
		
		public static Vector Rect(Vector t, double A, Vector f, double fi)
		{
			return A*NeuroFunc.Porog(Sin(t,A,1,fi),0.1);
		}
		
		
		public static Vector Rect(Vector t, double A, double f, Vector fi)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f,fi),0.1);
		}
		
		
		
		public static Vector Rect(Vector t, double A, double f)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f),0.1);
		}
		
		public static Vector Rect(Vector t, double A, Vector f)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f),0.1);
		}
		
		public static Vector Rect(Vector t, Vector A, double f)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f),0.1);
		}
		
		public static Vector Rect(Vector t, double f)
		{
			return NeuroFunc.Porog(Sin(t,f),0.1);
		}
		
		
		public static Vector Rect(Vector t, Vector f)
		{
			return NeuroFunc.Porog(Sin(t,f),0.1);
		}
		#endregion
		
		#region Радиоимпульс
		public static Vector AmkRect(Vector t, double A, double f1, double fi1, double f2, double fi2, double k)
		{
			Vector modul = Rect(t,A,f2,fi2)+k;
			return Sin(t, modul,f1,fi1);
		}
		
		
		public static Vector AmkRect(Vector t, double A, double f1, double f2, double fi2, double k)
		{
			Vector modul = Rect(t,A,f2,fi2)+k;
			return Sin(t, modul,f1);
		}
		
		
		public static Vector AmkRect(Vector t, double A, double f1, double f2, double k)
		{
			Vector modul = Rect(t,A,f2)+k;
			return Sin(t, modul,f1);
		}
		
		
		public static Vector AmkRectK(Vector t, double f1, double f2, double k)
		{
			Vector modul = Rect(t,f2)+k;
			return Sin(t, modul,f1);
		}
		
		public static Vector AmkRectA(Vector t, double A, double f1, double f2)
		{
			Vector modul = Rect(t,A,f2);
			return Sin(t, modul,f1);
		}
		
		
		public static Vector AmkRect(Vector t, double f1, double f2)
		{
			Vector modul = Rect(t,f2);
			return Sin(t, modul,f1);
		}
		#endregion
		
		#region Затухающие колебания
		public static Vector DampedOscillations(Vector t, double f=1, double kDamp = -0.01, double A = 1, double fi = 0)
		{
			return MathFunc.exp(t*kDamp)*Sin(t,A,f,fi);
		}
		#endregion
		
		
		
	}
	
	
	
}
