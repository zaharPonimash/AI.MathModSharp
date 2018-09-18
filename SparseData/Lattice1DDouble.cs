/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 04.09.2018
 * Время: 0:16
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;

namespace AI.MathMod.SparseData
{
	/// <summary>
	/// Description of Lattice1DDouble.
	/// </summary>
	public class Lattice1DDouble: Lattice1D<double>
	{
		public Lattice1DDouble(Cell1D<double>[] cels)
		{
			Cells = new List<Cell1D<double>>();
			Cells.AddRange(cels);
		}
		
		
		public Lattice1DDouble(Vector vect)
		{
			Cells = new List<Cell1D<double>>();
			for (int i = 0; i < vect.N; i++) 
			{
				if(vect[i]!=0) Cells.Add(new Cell1D<double>(vect[i], i));
			}
			
		}
		
		
		
		
		/// <summary>
		/// Умножение решетки матрицу 
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static Vector operator* (Lattice1DDouble A, Matrix B)
		{
			Vector output = new Vector(B.N);
			
			
		   	for (int j = 0; j < B.N; j++) 
		   	{
		   	
		   		for (int i = 0; i < A.Cells.Count; i++)
		   		{
		   			output[j] += B[A.Cells[i].coordinate, j]*A.Cells[i].Value;
		   		}
				   		
		   	}
		   	
		   	return output;
		}
	}
}
