/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 06.06.2017
 * Время: 21:31
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;


namespace AI.MathMod.Signals
{
	/// <summary>
	/// Дискретно-косинусное преобразование
	/// </summary>
	public class DCT
	{


        /// <summary> 
        /// Матрица прямого преобразования
        /// </summary>
        public Matrix MainMatrix { get; set; }
        /// <summary> 
        /// Матрица обратного преобразования
        /// </summary>
        public Matrix InvMatrix { get; set; }


        /// <summary>
        /// Дискретно-косинусное преобразование
        /// </summary>
        public DCT()
		{
		}
		
		/// <summary>
		/// Дискретно-косинусное преобразование
		/// </summary>
		/// <param name="countInp">Кол-во входов</param>
		/// <param name="countOutp">Код-во Выходов</param>
		public DCT(int countInp, int countOutp)
		{
			MainMatrix = GetMatrW(countInp, countOutp);
			InvMatrix = MainMatrix.Tr();
		}
		
		
		
		
		
		/// <summary>
		/// Матрица
		/// </summary>
		/// <param name="N"></param>
		/// <param name="M"></param>
		/// <returns></returns>
		public static Matrix GetMatrW(int N, int M)
		{
			Matrix W = new Matrix(M,N);
			double lambda = 1/Math.Sqrt(N);
			double arg;
			
			for (int j = 0; j < W.N; j++)
			{
				W.Matr[0,j] = lambda;
			}
			
			lambda = Math.Sqrt(2.0/N);
			
			for (int i = 1; i < W.M; i++)
			{
				for (int j = 0; j < W.N; j++)
				{
					arg = (i*Math.PI*(2*j+1))/(2*N);
					
					W.Matr[i,j] = lambda*Math.Cos(arg);
				}	
			}
			
			
			return W;
		}
		
		
		
		
		
		
		
		
		/// <summary>
		/// Прямое ДКТ
		/// </summary>
		/// <param name="inp"></param>
		/// <returns></returns>
		public Vector FDCT(Vector inp)
		{
			Matrix inpM = inp.ToMatrix().Tr();
			return (MainMatrix*inpM).Tr().ToVector();
		}
		
		
		
		/// <summary>
		/// Обратное Дкт
		/// </summary>
		/// <param name="inp"></param>
		/// <returns></returns>
		public Vector IDCT(Vector inp)
		{
			Matrix inpM = inp.ToMatrix().Tr();
			return (InvMatrix*inpM).Tr().ToVector();
		}
		
	}
}
