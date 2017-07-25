/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 11.10.2016
 * Time: 0:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Combinatorics
{
	/// <summary>
	/// Базовые функции коммбинарики
	/// </summary>
	public static class CombinatoricsBaseFunction
	{
		
		
		
		/// <summary>
		/// Размещение без повторов
		/// </summary>
		/// <param name="k">Количество элементов</param>
		/// <param name="n">Количество возможных позиций</param>
		public static double PlacingWithoutRepetition(int k, int n)
		{
			int newN = n+1;
			Vector vect = MathFunc.GenerateTheSequence(newN-k,newN);
			return Functions.Multiplication(vect);
		}
		
		
		
		/// <summary>
		/// Количество комбинаций
		/// </summary>
		/// <param name="k">Количество элементов</param>
		/// <param name="n">Количество возможных позиций</param>
		public static double NumberOfCombinations(int k, int n)
		{
			double Akn = PlacingWithoutRepetition(k,n);
			return Akn/MathFunc.factorial(k);
		}
		
	}
}
