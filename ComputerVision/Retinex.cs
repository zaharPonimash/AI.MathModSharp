/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 11.12.2017
 * Время: 21:43
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using AI.MathMod.AdditionalFunctions;


namespace AI.MathMod.ComputerVision
{
	/// <summary>
	/// Description of Retinex.
	/// </summary>
	public class Retinex
	{
		/// <summary>
		/// Ретинекс
		/// </summary>
		public Retinex()
		{
		}
		
		/// <summary>
		/// Ретинекс
		/// </summary>
		/// <param name="bm">Картинка</param>
		/// <returns></returns>
		public static Bitmap Retin(Bitmap bm)
		{
			Matrix m = ImgConverter.BmpToMatr(bm);
			Matrix filter = new Matrix(5,5)+10;
			Matrix filter2 = new Matrix(5,5);
			filter2[2,2] = 1;
			double sum = 0;
			
			
			for (int i = 1; i < 4; i++) 
			{
				for (int j = 1; j < 4; j++) {
					filter[i,j] = 12;
				}
			}
			
			
			
			
			filter[2,2] = 18;
			
			for (int i = 0; i < 5; i++) 
			{
				for (int j = 0; j < 5; j++) {
					sum += filter[i,j];
				}
			}
			
			filter/= 1.7*sum;
			Matrix bb = ImgFilters.SpaceFilter(m, filter);
			Matrix G = MathFunc.lg(bb+0.001);
			m = ImgFilters.SpaceFilter(m, filter2);
			double mean, sigm;
			
			m = MathFunc.lg(m+0.001);
			m -= G;
			m -= Statistic.MinimalValue(m.Spagetiz());
			m /= Statistic.MaximalValue(m.Spagetiz());
			mean = Statistic.ExpectedValue(m.Spagetiz());
			sigm = 0.9/Statistic.Std(m.Spagetiz());
			m = NeuroFunc.Sigmoid(sigm*(m-mean));
			return ImgConverter.MatrixToBitmap(m);
		}
		
		
	}
}
