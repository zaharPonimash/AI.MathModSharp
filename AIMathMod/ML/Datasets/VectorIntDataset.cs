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
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.Charts;
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
		
		/// <summary>
		/// Представляет структуру вектор-класс
		/// </summary>
		/// <param name="vector">Вектор</param>
		/// <param name="mark">Метка класса</param>
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
		/// Средний вектор
		/// </summary>
		public Vector mean; 
		/// <summary>
		/// Дисперсия по выборке
		/// </summary>
		public Vector disp;
	
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
		
		
		/// <summary>
		/// Датасет
		/// </summary>
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
			net.Moment = 0;
			
			
			MenegerNNW mnnw = new MenegerNNW(net, this);
			mnnw.Train(2);
			
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
		
		/// <summary>
		/// Визуализация датасета
		/// </summary>
		public void Visualis()
		{
			VisualData vd = new VisualData(this);
			vd.Show();
		}
		
		/// <summary>
		/// Корреляционная матрица признаков
		/// </summary>
		/// <returns>Нормированная кор. матрица</returns>
		public Matrix CorrMatrFeatures()
		{
			Vector[] vects = new Vector[Count];
			
			for (int i = 0; i < vects.Length; i++)
			{
				vects[i] = this[i].InpVector.Copy();
			}
			
			
			Vector[] vects2 = new Vector[vects[0].N];
			
			for (int i = 0; i < vects2.Length; i++)
			{
				vects2[i] = new Vector(vects.Length);
				
				
				for (int j = 0; j < vects.Length; j++)
				{
					vects2[i][j] = vects[j][i];
				}
			}
			
			
			return Matrix.CorrelationMatrixNorm(vects2);
		}
		
		
		/// <summary>
		/// Получение вектора дисперсии и среднего вектора
		/// </summary>
	    public void DispMeanResult()
		{
			Vector[] vects = new Vector[Count];
			
			for (int i = 0; i < vects.Length; i++)
			{
				vects[i] = this[i].InpVector;
			}
			
			mean = Statistic.MeanVector(vects);
			disp = Statistic.EnsembleDispersion(vects);
		}
	    
	    
	    /// <summary>
	    /// Нормализация датасета
	    /// </summary>
	    /// <returns>Датасет</returns>
	    public VectorIntDataset Normalise()
	    {
	    	
	    	DispMeanResult();
	    	
	    	disp = disp.TransformVector(d => (d==0)?1e-109:d);
	    	
	    	VectorIntDataset vid = new VectorIntDataset();
	    	Vector std = MathFunc.sqrt(disp);
	    	
	    	for (int i = 0; i < Count; i++) 
	    	{
	    		vid.Add(new VectorClass
	    		        (
	    		        	(this[i].InpVector-mean)/std,
	    		        	this[i].ClassMark
	    		        )
	    		       );
	    	}
	    	
	    	return vid;
	    }
	    
	    /// <summary>
	    /// Удаление похожих векторов из разных классов
	    /// </summary>
	    /// <param name="simCoef">Коэффициент схожести</param>
	    public VectorIntDataset GetDatasetDelSim(double simCoef = 0.9)
	    {
	    	VectorIntDataset vid = new VectorIntDataset();
	    	List<int> simIndex = new List<int>();
	    	VectorClass[] vc;
	    	
	    	for (int i = 0; i < Count-1; i++) 
	    	{
	    		for (int j = i+1; j < Count; j++) 
	    		{
	    			if(this[i].ClassMark != this[j].ClassMark)
	    				if(Statistic.CorrelationCoefficient(this[i].InpVector, this[j].InpVector)>simCoef)
	    					if(IsNotSerch(simIndex, j)) simIndex.Add(j);
	    		}
	    		
	    	}
	    	
	    	
	    	vc = new VectorClass[Count-simIndex.Count];
	    	
	    	for (int i = 0, k = 0; i < Count; i++)
	    	{
	    		if(IsNotSerch(simIndex, i)) 
	    			vc[k++] = this[i];
	    	}
	    	
	    	vid.AddRange(vc);
	    	
	    	return vid;
	    }
	    
	    
	   static bool IsNotSerch(List<int> simIndex, int i)
	    {
	   		 	for (int j = 0; j < simIndex.Count; j++)
	    		{
	    			if(i == simIndex[j]) return false;
	    		}
	   		 	
	   		 	return true;
	    }
	   
	   
	   /// <summary>
	   /// 
	   /// </summary>
	   /// <param name="path"></param>
	   /// <param name="separator"></param>
	   /// <returns></returns>
	   public static VectorIntDataset CsvToVid(string path, char separator = ',')
	   {
	   		string[] content = File.ReadAllLines(path);
			VectorClass[] vC = new VectorClass[content.Length];
			string[] vectorData = new string[1], data;
		
			
			
			for (int i = 0; i < content.Length; i++)
			{
				data = content[i].Split(separator);
				Array.Copy(data, vectorData, data.Length - 1);
				
				vC[i] = new VectorClass(
					new Vector(vectorData),
					Convert.ToInt32(data[data.Length - 1]));
			}
			
			VectorIntDataset vid = new VectorIntDataset();
			
			 vid.AddRange(vC); 
			 
			 return vid;
	   }
	   
	   
	   
	   /// <summary>
	   /// 
	   /// </summary>
	   public static VectorIntDataset CsvToVid(string path, int leng, char separator = ',')
	   {
	   		string[] content = File.ReadAllLines(path);
			VectorClass[] vC = new VectorClass[leng];
			string[] vectorData , data;
			Vector vect;
			
			
			for (int i = 0; i <leng; i++)
			{
				
				data = content[i].Split(separator);
				vectorData  = new string[data.Length - 1];
				Array.Copy(data, vectorData, data.Length - 1);
				vect = new Vector(vectorData);
				vect[29] = 1;
				
				vect = vect.TransformVector(d => (double.IsNaN(d)? 0:d));
				
				vC[i] = new VectorClass(
					vect,
					Convert.ToInt32(data[data.Length - 1])-1);
				
			}
			
			VectorIntDataset vid = new VectorIntDataset();
			
			 vid.AddRange(vC); 
			 
			 return vid;
	   }
	   
	   
	
	   
	}
}
