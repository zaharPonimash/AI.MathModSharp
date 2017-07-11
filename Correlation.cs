/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 31.01.2016
 * Time: 11:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Numerics;
using System.Collections.Generic;

namespace AI.MathMod
{
	/// <summary>
	/// Класс реализует авто- и взаимо- кореляционные функции
	/// Для действительных и комплексных векторов
	/// </summary>

	public static class Correlation
	{
			#region Взаимокорелляция
				/// <summary>
				/// Взаимокорелляция двух действительных векторов
				/// </summary>
				/// <param name="A">Первый вектор</param>
				/// <param name="B">Второй вектор</param>
				/// <returns>Возвращает отсчеты ВКФ</returns>
				public static Vector CrossCorrelation(Vector A, Vector B)
				{ 
					int N = A.N + B.N-1, k = A.N-1;
					
					Vector C = new Vector(N), st, s2t;
					
						for(int n = 0; n<k; n++){
						st = A.CutAndZero(N);
						s2t = B.Shift(n);
						s2t = s2t.CutAndZero(N);
						C.Vecktor[n] = Functions.Summ(st*s2t);
						}
					
					Statistic stat = new Statistic(A);
					Statistic stat2 = new Statistic(B);
					
					return C/(stat.SCO*stat2.SCO);
				}
				
				
				
				
				/// <summary>
				/// Взаимокорелляция двух действительных векторов
				/// </summary>
				/// <param name="A">Первый вектор</param>
				/// <param name="B">Второй вектор</param>
				/// <returns>Возвращает отсчеты ВКФ</returns>
				public static Vector CrossCorrelationF(Vector A, Vector B)
				{ 
					int N = (A.N>B.N)? Functions.NextPow2(A.N): Functions.NextPow2(B.N);
					Vector newA, newB;
					
						newA = A.CutAndZero(N);
						newB = B.CutAndZero(N);
					
					ComplexVector SwA = Furie.fft(newA);
					ComplexVector SwB = Furie.fft(newB);
					
					return Furie.ifft(SwA*SwB).RealToVector()/Math.Sqrt(Statistic.Dispers(newA)*Statistic.Dispers(newB))/newA.N;
				}
				
				/// <summary>
				/// Взаимокорелляция двух комплексных векторов
				/// </summary>
				/// <param name="A">Первый вектор</param>
				/// <param name="B">Второй вектор</param>
				/// <returns>Возвращает отсчеты ВКФ</returns>
					public static ComplexVector CrossCorrelation(ComplexVector A, ComplexVector B)
					{ 
					int N = A.N + B.N-1, k = A.N-1;
					
					ComplexVector C = new ComplexVector(N), st, s2t;
					
						for(int n = 0; n<k; n++){
						st = (!A).CutAndZero(N);
						s2t = B.Shift(n);
						s2t = s2t.CutAndZero(N);
						C.Vecktor[n] = Functions.Summ(st*s2t);
						}
					
					return C;
					}
				
			#endregion
			
			
			
			#region Авто-корреляция
			/// <summary>
				/// Автокорелляция действительного векторов
				/// </summary>
				/// <param name="A">Вектор</param>
				/// <returns>Возвращает осчеты АКФ</returns>
				public static Vector AutoCorrelation(Vector A)
				{
					return CrossCorrelation(A,A);
				}
				
				/// <summary>
				/// Автокорелляция комплексного векторов
				/// </summary>
				/// <param name="A">Вектор</param>
				/// <returns>Возвращает осчеты АКФ</returns>	
				public static ComplexVector AutoCorrelation(ComplexVector A)
				{
					return CrossCorrelation(A,A);
				}
				
				
				/// <summary>
				/// Автокорелляция действительного векторов
				/// </summary>
				/// <param name="A">Вектор</param>
				/// <returns>Возвращает осчеты АКФ</returns>
				public static Vector AutoCorrelationF(Vector A)
				{
					return CrossCorrelationF(A,A);
				}
			#endregion
			
			
			
			/// <summary>
			/// Поиск паттернов в векторе
			/// </summary>
			/// <param name="vect">вектор</param>
			/// <param name="pattern">паттерн</param>
			/// <returns>Вектор описывающий похожесть сигнала на патерн</returns>
			public static Vector PatternSerch(Vector vect, Vector pattern)
			{
			
				
				int window = (int)(9*pattern.N);
				window = Functions.NextPow2(window);
				
				
				Vector input, vect1 = vect.CutAndZero(Functions.NextPow2(vect.N));
				int n = vect1.N-window;
				List<double> DoubList = new List<double>();
				double[] data = new double[window];
			
			
					for(int i = 0; i < n; i+= window)
					{
						data = new double[window];
						input = vect1.CutAndZero(i+window);
						Array.Copy(input.Vecktor,i,data,0,window);
						input = new Vector(data);
						DoubList.AddRange(CrossCorrelationF(input,pattern).Vecktor);
					}
			
				return Vector.ListToVector(DoubList);
			
			}
			
			
			
			/// <summary>
			/// Поиск паттернов в векторе
			/// </summary>
			/// <param name="vect">Вектор</param>
			/// <param name="pattern">Паттерн</param>
			/// <param name="windowSize">Окно для поиска</param>
			/// <returns>Вектор описывающий похожесть сигнала на патерн</returns>
			public static Vector PatternSerch(Vector vect, Vector pattern, int windowSize)
			{
				int window = Functions.NextPow2(windowSize);
				Vector input;
				int n = vect.N-window;
				List<double> DoubList = new List<double>();
				double[] data = new double[window];
			
			
					for(int i = 0; i < n; i+= window)
					{
						data = new double[window];
						input = vect.CutAndZero(i+window);
						Array.Copy(input.Vecktor,i,data,0,window);
						input = new Vector(data);
						DoubList.AddRange(CrossCorrelationF(input,pattern).Vecktor);
					}
			
				return Vector.ListToVector(DoubList);
			
			}
			
			
			
			
			
			
			
			
			
			
	}
}
