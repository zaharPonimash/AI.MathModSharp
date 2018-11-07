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
using AI.MathMod.Graphiks;
using AI.MathMod.ML.NeuronNetwork;

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
		
		
		/// <summary>
		/// Данные для визуализации
		/// </summary>
		/// <param name="n">Число классов</param>
		/// <returns></returns>
		public Vector[] DataVisual(int n)
		{
			
			
			
			Net net = new Net(rnd);
			
			
			net.Add(new LinearLayer(this[0].InpVector.N, 2));
			net.Add(new Softmax(n));
			
			net.LerningRate = 0.0001;
			
			
			MenegerNNW mnnw = new MenegerNNW(net, this);
			mnnw.Train(40);
			
			Vector[] vects = new Vector[2*n];
			Vector nnwOut;
		
			List<double>[] x = new List<double>[n];
			List<double>[] y = new List<double>[n];
			
			for (int i = 0; i < n; i++) 
			{
				x[i] = new List<double>();
				y[i] = new List<double>();
			}
			
			
			for (int i = 0; i < Count; i++)
			{
				for (int j = 0; j < n; j++)
				{
					nnwOut = net._layers[0].Output(this[i].InpVector);
					if(this[i].ClassMark == j)
					{
						x[j].Add(nnwOut[0]);
						y[j].Add(nnwOut[1]);
					}
						
				}
			}
			
			for (int i = 0, k = 0; i < n; i++)
			{
				vects[k++] = Vector.ListToVector(x[i]);
				vects[k++] = Vector.ListToVector(y[i]);
			}
			
			return vects;
		}
		
		
		public void Visualis()
		{
			VisualData vd = new VisualData(this);
			vd.Show();
		}
		
	}
}
