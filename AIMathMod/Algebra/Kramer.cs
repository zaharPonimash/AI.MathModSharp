/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 14.07.2017
 * Время: 14:58
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Threading.Tasks;


namespace AI.MathMod.Algebra
{
	/// <summary>
	/// Description of Kramer.
	/// </summary>
	public class Kramer
	{
		Matrix _a;
		Vector _b, _x;
		double _detA;
		
		
		
		
		
		void Loop(int i)
		{
			_x.DataInVector[i] = NewDet(i)/_detA;
		}
		
		
		/// <summary>
		/// Вывод вектора решений системы уравнений
		/// </summary>
		/// <param name="A">Матрица коэфициентов системы</param>
		/// <param name="B">Вектор ответов</param>
		/// <returns>Вектор неизвестных</returns>
		public Vector GetAnswer(Matrix A, Vector B)
		{
			_a = A;
			_detA = _a.Determinant();
			_b = B;
			_x = new Vector(_b.N);
			
			Parallel.For(0, _b.N, Loop);
			
			return _x;
		}
		
		
		
		double NewDet(int index)
		{
			Matrix newA = _a.Copy();
			
			for(int i = 0; i<_b.N; i++)
			{
				newA.Matr[i,index] = _b.DataInVector[i];
			}
			
			return newA.Determinant();
		}	
	}
}
