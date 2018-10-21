/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 10.08.2018
 * Время: 13:35
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.Algebra;


namespace AI.MathMod.ML.Regression
{
	/// <summary>
	/// Множественная регрессия
	/// </summary>
	public class MultipleRegression
	{
		
		Matrix A;
		Vector B;
		Vector _param;
		Vector[] _x;
		double[] _y;
		
		int n, m;
		
		/// <summary>
		/// Множественная линейная регрессия
		/// </summary>
		/// <param name="X">Вектора входа</param>
		/// <param name="Y">Выходы</param>
		public MultipleRegression(Vector[] X, double[] Y)
		{
			n = X.Length;
			m = X[0].N;
			_x = X;
			_y = Y;
			//Xadd1();
			GenA();
			GenB();
			GenParam();
		}
		
		// Составление матрицы
		void GenA()
		{
			A = new Matrix(m,m);
			double c = 0;
			
			for (int i = 0; i < m; i++) 
				for (int j = 0; j < m; j++) {
					
				    c = 0;
					
					for (int k = 0; k < n; k++)
						c+= _x[k][i]*_x[k][j];
					
					A[i,j] = c;
				}
			
			//A.Visual();
		}
		
		
		void Xadd1()
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] = _x[i].Shift(1);
				_x[i][0] = 1;
			}
		
		}
		
		
		// Вектор ответа
		void GenB()
		{
			B = new Vector(m);
			double c = 0;
			
			
			for (int i = 0; i < m; i++) {
				c = 0;
				
				for (int j = 0; j < n; j++)
					c += _x[j][i]*_y[j];
			
				B[i] = c;
			}
			
			//B.Visual();
		}
		
		// Генерация параметров системы
		void GenParam()
		{
			Kramer kram = new Kramer();
			_param = kram.GetAnswer(A, B);
			//_param = _param.Revers();
			//_param.Visual();
		}
		
		/// <summary>
		/// Прогноз
		/// </summary>
		/// <param name="vect">Вектор входа</param>
		/// <returns>Выход</returns>
		public double Predict(Vector vect)
		{
			return GeomFunc.ScalarProduct(vect, _param);
		}
		
		
		
		/// <summary>
		/// Прогноз
		/// </summary>
		/// <param name="inp">Вектора входа</param>
		/// <returns>Вектор выхода</returns>
		public Vector Predict(Vector[] inp)
		{
			Vector outp = new Vector(inp.Length);
			
			for (int i = 0; i < inp.Length; i++) 
			{
				outp[i] = Predict(inp[i]);
			}
			
			return outp;
		}
	}
}
