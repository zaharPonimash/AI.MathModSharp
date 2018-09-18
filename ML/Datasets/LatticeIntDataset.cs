/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 07.09.2018
 * Время: 19:11
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.IO;
using AI.MathMod.SparseData;

namespace AI.MathMod.ML.Datasets
{
	/// <summary>
	/// Представляет структуру вектор-класс
	/// </summary>
	public class LatticeClass
	{
		/// <summary>
		/// Вектор для классификации
		/// </summary>
		public Lattice1DDouble InpLat{get; set;}
		/// <summary>
		/// Метка класса
		/// </summary>
		public Int32 ClassMark{get; set;}
		
		
		public LatticeClass(Vector vector, int mark)
		{
			InpLat = new Lattice1DDouble(vector);
			ClassMark = mark;
		}
		
		public LatticeClass(Lattice1DDouble lat, int mark)
		{
			InpLat = lat;
			ClassMark = mark;
		}
	}
	
	/// <summary>
	/// Датасет
	/// </summary>
	public class LatticeIntDataset : List<LatticeClass>
	{
		Random rnd = new Random();
	
		/// <summary>
		/// Загрузка датасета из файла
		/// </summary>
		/// <param name="path">Путь</param>
		public LatticeIntDataset(string path)
		{
			string[] content = File.ReadAllLines(path);
			var vC = new LatticeClass[content.Length];
			
			for (int i = 0; i < content.Length; i++)
			{
				vC[i] = new LatticeClass(
					new Vector(content[i].Split(';')[0].Split(' ')),
					Convert.ToInt32(content[i].Split(';')[1]));
			}
			
			AddRange(vC); 
		}
		
		
		
		/// <summary>
		/// Загрузка датасета из файла
		/// </summary>
		public LatticeIntDataset()
		{
		}
		
		
		
		
		/// <summary>
		/// Случайный представитель датасета
		/// </summary>
		public LatticeClass GetRandomData()
		{
			return this[rnd.Next(Count)];
		}
		
		/// <summary>
		/// Случайный представитель датасета
		/// </summary>
		public LatticeClass GetRandomDataDel()
		{
			int ind = rnd.Next(Count);
			LatticeClass lC = this[ind];
			RemoveAt(ind);
			return lC;
		}
		
		/// <summary>
		/// Сохранение датасета
		/// </summary>
		/// <param name="path">Путь до датасета</param>
		public void Save(string path)
		{
			string[] content = new string[Count];
			
			for (int i = 0; i < content.Length; i++) 
			{
				content[i] = this[i].ToString()+";"+this[i].ClassMark;
			}
			
			File.WriteAllLines(path, content);
 		}
		
		
		public Lattice1DDouble[] GetLat()
		{
			Lattice1DDouble[] outp = new Lattice1DDouble[Count];
			
			for(int i = 0; i<Count;  i++)
				outp[i] = this[i].InpLat;
			
			return outp;
		}
		
		public int[] GetInts()
		{
			int[] outp = new int[Count];
			
			for(int i = 0; i<Count;  i++)
				outp[i] = this[i].ClassMark;
			
			return outp;
		}
		
	}
}
