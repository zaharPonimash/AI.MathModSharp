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
			sigm = 0.9/Statistic.Sco(m.Spagetiz());
			m = NeuroFunc.Sigmoid(sigm*(m-mean));
			return ImgConverter.MatrixToBitmap(m);
		}
		
		
		/// <summary>
		/// Запуск алгоритма
		/// </summary>
		/// <param name="input">Тензор цветного изображения</param>
		/// <param name="sigma"></param>
		/// <returns></returns>
		public static Tensor Run(Tensor input, double sigma =3)
		{
			Tensor inp = Run1(input, sigma);
			
			for (int i = 0; i <0; i++) {
				inp = Run1(inp, sigma+2*i+1);
			}
			
			return inp;
		}
		
		
		public static Tensor Run1(Tensor input, double sigma =3.6)
		{
			Tensor inp = input*255;
			Tensor m1 = NeuroFunc.Log10(inp+1);
			double sig = 3*Statistic.Sco(m1.ToVector());
			double m =  Statistic.ExpectedValue(m1.ToVector());
			m1 =  (m1-m)/sig;
			return NeuroFunc.Sigmoid(m1, sigma);
		}
			
			
			public static Bitmap PNV(Bitmap bmp)
			{
				Matrix matr = ImgConverter.BmpToMatr(bmp);
				matr = ImgFilters.FC(matr);
				
				return ImgConverter.MatrixToBitmap(matr);
			}
		
	}
}
