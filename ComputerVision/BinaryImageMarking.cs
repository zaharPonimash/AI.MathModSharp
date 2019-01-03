/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 15.09.2018
 * Время: 18:36
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ComputerVision
{
	/// <summary>
	/// Description of BinaryImageMarking.
	/// </summary>
	public class BinaryImageMarking
	{
//		
//		public BinaryImg img;
		
		int couter = 0, x, y, xa, ya, m, n;
	
		
		/// <summary>
		/// Маркирование бинарных изображений
		/// </summary>
		public BinaryImageMarking()
		{
		}
		
		/// <summary>
		/// Маркирование
		/// </summary>
		/// <param name="bm">Изображение</param>
		/// <returns>Регионы</returns>
		public Bitmap Marking(Bitmap bm)
		{
			
			Matrix matr = ImgConverter.BmpToMatr(bm);
			Matrix filter = new Matrix()-1;
			filter[1,1] = 8;
			matr = NeuroFunc.Threshold(matr, 0.5);
			matr = 1-ImgFilters.SpaceFilter(matr, filter);
			matr = NeuroFunc.Threshold(matr, 0.9);
			
			BinaryImg bi = new BinaryImg(1-matr);
			Matrix img = bi.ToMatrixInvers();
			//img.MatrixShow();
			x = 0;
			y = 0;
			m = img.M;
			n = img.N;
			couter = 0;
			
			while(x<m-1&&y<n-1)
			{
				SerchNotMark(img);
				
				while(Area(img)){}
			}
			
			img /= couter;
			
			return ImgConverter.MatrixToBitmap(img);
		}
		
		// Поиск не маркированной области
		void SerchNotMark(Matrix img)
		{
		
		bool isB;
		
		for (int j = 0; j < n; j++) 
		{
			isB = false;
					for (int i = 0; i < m; i++)
				{
					x = i;
					y = j;
					
					if (img.Matr[i, j] == (-1)) {
						couter++;
						xa = i;
						ya = j;
						img[xa, ya] = couter;
						isB = true;
						break;
					}
					
					
				}
			if (isB) {
						break;
			}
		}
			
			//img.MatrixShow();
		}
		
		// определение области
		bool Area(Matrix img)
		{
			int nxa, nya;
			
			for(int i = -4; i<8;  i++)
				for (int j = -4; j < 8; j++) 
			{
				if(i !=0 && j != 0)
				{
				
				nxa = nX(xa+i);
				nya = nY(ya+j);
				
				if(img[nxa, nya] == -1)
				{
					xa = nxa;
					ya = nya;
					img[xa, ya] = couter;
					return true;
				}
				}
			}
			
			return false;
		}
		
		int nX(int ox)
		{
			int nx;
			if(ox <0) nx = 0;
			else if(ox>=m) nx = m-1;
			else nx = ox;
			return nx;
		}
		
		int nY(int oy)
		{
			int ny;
			if(oy <0) ny = 0;
			else if(oy>=m) ny = m-1;
			else ny = oy;
			return ny;
		}
		
		
		
		
	}
}
