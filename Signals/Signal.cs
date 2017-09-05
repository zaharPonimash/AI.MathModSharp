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
        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">частота</param>
        /// <param name="fi">Начальная фаза</param>
		public static Vector Sin(Vector t, double A, double f, double fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">частота</param>
        /// <param name="fi">Начальная фаза</param>
        public static Vector Sin(Vector t, double A, Vector f, double fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">частота</param>
        /// <param name="fi">Начальная фаза</param>
        public static Vector Sin(Vector t, double A, double f, Vector fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">частота</param>
        /// <param name="fi">Начальная фаза</param>
        public static Vector Sin(Vector t, Vector A, double f, double fi)
		{
			return A*MathFunc.sin(t*2*Math.PI*f+fi);
		}

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">частота</param>
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

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">частота</param>
        public static Vector Sin(Vector t, double A, Vector f)
		{
			return A*MathFunc.sin(t*2*Math.PI*f);
		}

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">частота</param>
        public static Vector Sin(Vector t, Vector A, double f)
		{
			return A*MathFunc.sin(t*2*Math.PI*f);
		}

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="f">частота</param>
        public static Vector Sin(Vector t, double f)
		{
			return MathFunc.sin(t*2*Math.PI*f);
		}

        /// <summary>
        /// Синусоидальные колебания
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="f">частота</param>
        public static Vector Sin(Vector t, Vector f)
		{
			return MathFunc.sin(t*2*Math.PI*f);
		}
		#endregion
		
		
		#region Прямоугольный
		
        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">Частота</param>
        /// <param name="fi">Фаза</param>
        /// <returns>Отсчеты сигнала</returns>
		public static Vector Rect(Vector t, double A, double f, double fi)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f,fi),0.1);
		}

        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="A">Вектор амплитуд</param>
        /// <param name="f">Частота</param>
        /// <param name="fi">Фаза</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, Vector A, double f, double fi)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f,fi),0.1);
		}

        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">Вектор частот</param>
        /// <param name="fi">Фаза</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, double A, Vector f, double fi)
		{
			return A*NeuroFunc.Porog(Sin(t,A,1,fi),0.1);
		}

        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">Частота</param>
        /// <param name="fi">Вектор фаз</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, double A, double f, Vector fi)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f,fi),0.1);
		}


        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">Частота</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, double A, double f)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f),0.1);
		}


        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f">Вектор частот</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, double A, Vector f)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f),0.1);
		}


        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="A">Вектор амплитуда</param>
        /// <param name="f">Вектор частот</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, Vector A, double f)
		{
			return A*NeuroFunc.Porog(Sin(t,1,f),0.1);
		}


        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="f">Частота</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, double f)
		{
			return NeuroFunc.Porog(Sin(t,f),0.1);
		}

        /// <summary>
        /// Прямоугольный сигнал
        /// </summary>
        /// <param name="t">Вектор отсчетов времени</param>
        /// <param name="f">Вектор частот</param>
        /// <returns>Отсчеты сигнала</returns>
        public static Vector Rect(Vector t, Vector f)
		{
			return NeuroFunc.Porog(Sin(t,f),0.1);
		}
        #endregion

        #region Радиоимпульс

        /// <summary>
        /// Амплитудно-модулированые колебания (прямоугольное модулирующее колебание)
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f1">Несущая частота</param>
        /// <param name="fi1">Фаза модулирующего сигала</param>
        /// <param name="f2">Частота модулятора</param>
        /// <param name="fi2">Фаза модулируемого сигала</param>
        /// <param name="k">Коэффициент модуляции</param>
        /// <returns>Вектор отсчетов</returns>
        public static Vector AmkRect(Vector t, double A, double f1, double fi1, double f2, double fi2, double k)
		{
			Vector modul = Rect(t,A,f2,fi2)+k;
			return Sin(t, modul,f1,fi1);
		}

        /// <summary>
        /// Амплитудно-модулированые колебания (прямоугольное модулирующее колебание)
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f1">Несущая частота</param>
        /// <param name="f2">Частота модулятора</param>
        /// <param name="fi2">Фаза модулируемого сигала</param>
        /// <param name="k">Коэффициент модуляции</param>
        /// <returns>Вектор отсчетов</returns>
        public static Vector AmkRect(Vector t, double A, double f1, double f2, double fi2, double k)
		{
			Vector modul = Rect(t,A,f2,fi2)+k;
			return Sin(t, modul,f1);
		}

        /// <summary>
        /// Амплитудно-модулированые колебания (прямоугольное модулирующее колебание)
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f1">Несущая частота</param>
        /// <param name="f2">Частота модулятора</param>
        /// <param name="k">Коэффициент модуляции</param>
        /// <returns>Вектор отсчетов</returns>
        public static Vector AmkRect(Vector t, double A, double f1, double f2, double k)
		{
			Vector modul = Rect(t,A,f2)+k;
			return Sin(t, modul,f1);
		}

        /// <summary>
        /// Амплитудно-модулированые колебания (прямоугольное модулирующее колебание)
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="f1">Несущая частота</param>
        /// <param name="f2">Частота модулятора</param>
        /// <param name="k">Коэффициент модуляции</param>
        /// <returns>Вектор отсчетов</returns>
        public static Vector AmkRectK(Vector t, double f1, double f2, double k)
		{
			Vector modul = Rect(t,f2)+k;
			return Sin(t, modul,f1);
		}

        /// <summary>
        /// Амплитудно-модулированые колебания (прямоугольное модулирующее колебание)
        /// </summary>
        /// <param name="t">Вектор времени</param>
        /// <param name="A">Амплитуда</param>
        /// <param name="f1">Несущая частота</param>
        /// <param name="f2">Частота модулятора</param>
        /// <returns>Вектор отсчетов</returns>
        public static Vector AmkRectA(Vector t, double A, double f1, double f2)
		{
			Vector modul = Rect(t,A,f2);
			return Sin(t, modul,f1);
		}
		
		/// <summary>
		/// Амплитудно-модулированые колебания
		/// </summary>
		/// <param name="t"></param>
		/// <param name="f1"></param>
		/// <param name="f2"></param>
		/// <returns></returns>
		public static Vector AmkRect(Vector t, double f1, double f2)
		{
			Vector modul = Rect(t,f2);
			return Sin(t, modul,f1);
		}
		#endregion
		
		#region Затухающие колебания
		/// <summary>
		/// Затухающие колебания
		/// </summary>
		/// <param name="t">Время симуляции</param>
		/// <param name="f">частота</param>
		/// <param name="kDamp">Коэффициент затухания</param>
		/// <param name="A">Амплитуда(начальная)</param>
		/// <param name="fi">Фаза</param>
		public static Vector DampedOscillations(Vector t, double f=1, double kDamp = -0.01, double A = 1, double fi = 0)
		{
			return MathFunc.exp(t*kDamp)*Sin(t,A,f,fi);
		}
        #endregion

        #region Параметры сигналов

        /// <summary>
        /// Энергия выделяемая на едичном резисторе за все время
        /// </summary>
        /// <param name="signal">Сигнал отсчеты</param>
        /// <param name="fd">Частота дискретизация</param>
        /// <returns></returns>
        public static double Energe(Vector signal, double fd)
        {
            double energe = Functions.Summ(signal);
            return energe / fd;
        }


        /// <summary>
        /// Норма сигнала
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="fd"></param>
        /// <returns></returns>
        public static double Norm(Vector signal, double fd)
        {
            double norm = Energe(signal, fd);
            return Math.Sqrt(norm);
        }
        #endregion



    }
	
	
	
}
