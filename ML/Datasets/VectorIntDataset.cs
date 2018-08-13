/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 28.07.2018
 * Время: 21:54
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace AI.MathMod.ML.Datasets
{
	/// <summary>
	/// Представляет структуру вектор-класс
	/// </summary>
	public class VectorClass
	{
		/// <summary>
		/// Вектор для классификации
		/// </summary>
		public Vector InpVector{get; set;}
		/// <summary>
		/// Метка класса
		/// </summary>
		public Int32 ClassMark{get; set;}
		
		
		public VectorClass(Vector vector, int mark)
		{
			InpVector = vector;
			ClassMark = mark;
		}
	}
	
	/// <summary>
	/// Датасет
	/// </summary>
	public class VectorIntDataset : List<VectorClass>
	{
		Random rnd = new Random();
	
		/// <summary>
		/// Загрузка датасета из файла
		/// </summary>
		/// <param name="path">Путь</param>
		public VectorIntDataset(string path)
		{
			string[] content = File.ReadAllLines(path);
			VectorClass[] vC = new VectorClass[content.Length];
			
			for (int i = 0; i < content.Length; i++)
			{
				vC[i] = new VectorClass(
					new Vector(content[i].Split(';')[0].Split(' ')),
					Convert.ToInt32(content[i].Split(';')[1]));
			}
			
			AddRange(vC); 
		}
		
		
		
		public VectorIntDataset(){}
		
		/// <summary>
		/// Случайный представитель датасета
		/// </summary>
		public VectorClass GetRandomData()
		{
			return this[rnd.Next(Count)];
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
				content[i] = this[i].InpVector+";"+this[i].ClassMark;
			}
			
			File.WriteAllLines(path, content);
 		}
		
		
	}
}
