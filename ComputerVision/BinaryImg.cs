/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.09.2018
 * Время: 9:33
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ComputerVision
{
	/// <summary>
	/// Description of BinaryImg.
	/// </summary>
	public class BinaryImg
	{
		bool[,] img;
		public int M, N;
		
		
		public bool this[int i, int j]
		{
			set
			{
				img[i,j] = value;
			}
			
			get
			{
				return img[i,j];
			}
		}
		
		public BinaryImg(Matrix matr)
		{
			ToBools(matr);
			M = matr.M;
			N = matr.N;
		}
		
		public BinaryImg(Bitmap bm)
		{
			Matrix matr = ImgConverter.BmpToMatr(bm);
			matr = NeuroFunc.Porog(matr, 0.85);
			ToBools(matr);
			M = matr.M;
			N = matr.N;
		}
		
		
		public Matrix ToMatrix()
		{
			Matrix matr = new Matrix(M, N);
			
			for (int i = 0; i < matr.M; i++)
			{
				for (int j = 0; j < matr.N; j++)
				{
					matr[i,j] = img[i, j]? 1:0;
				}
			}
			
			return matr;
		}
		
		
		
		public Matrix ToMatrixInvers()
		{
			Matrix matr = new Matrix(M, N);
			
			for (int i = 0; i < matr.M; i++)
			{
				for (int j = 0; j < matr.N; j++)
				{
					matr[i,j] = img[i, j]? -1:0;
				}
			}
			
			return matr;
		}
		
		public Bitmap ToBmp()
		{
			return ImgConverter.MatrixToBitmap(ToMatrix());
		}
		
		void ToBools(Matrix matr)
		{
			img = new bool[matr.M, matr.N];
			
			for (int i = 0; i < matr.M; i++)
			{
				for (int j = 0; j < matr.N; j++)
				{
					img[i, j] = Math.Abs(matr[i, j] - 1) < 0.1;
				}
			}
		}
	}
}
