/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 19.02.2016
 * Time: 16:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using AI.MathMod;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Approximation
{
	/// <summary>
	/// Класс содержит методы для вычисления погрешности аппроксимации
	/// </summary>
	public static class ApproximationError
	{
		/// <summary>
		/// Высчитывает среднеквадратичную ошибку апроксимации
		/// </summary>
		/// <param name="real">Вектор реальных отсчетов</param>
		/// <param name="approcs">Вектор отсчетов аппроксимации</param>
		/// <returns></returns>
		public static double SQE(Vector real, Vector approcs)
		{
			Vector err = (real-approcs)^2;
			return  Math.Sqrt(Functions.Summ(err)/err.N);
		}
		
		
		
		/// <summary>
		/// Относительная ошибка по энергии спектра 
		/// </summary>
		/// <param name="real">Реальный</param>
		/// <param name="approcs">Аппроксимированный</param>
		/// <returns></returns>
		public static double PowerError(Vector real, Vector approcs)
		{
			double realSpE =  Functions.Integral(Furie.DPF(real).MagnitudeToVector()).DataInVector[real.N-1],
			appSpE = Functions.Integral(Furie.DPF(approcs).MagnitudeToVector()).DataInVector[real.N-1];
			return (realSpE > appSpE)? (realSpE/appSpE)*(realSpE/appSpE) : (appSpE/realSpE)*(appSpE/realSpE);
		}
		
	}
}
