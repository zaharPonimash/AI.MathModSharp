/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 31.03.2016
 * Time: 18:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod;
using System.IO;
using System.Drawing;
using AI.MathMod.ML.Clasterisators;

namespace AI.MathMod.ML.Classifire
{
	/// <summary>
	/// Структура для хранения класса
	/// </summary>
	[Serializable]
	public class StructClass
	{
        /// <summary>
        /// Имя класса
        /// </summary>
		public string _strName = string.Empty; // Имя класса
        /// <summary>
        /// Центр гиперсферы
        /// </summary>
		 public Vector _centGiperSfer = new Vector();// Координата центра гиперсферы


         /// <summary>
         /// Имя класса
         /// </summary>
		 public string StrName
		 {
		 	get{return _strName;}
		 	set{_strName = value;}
		 }

         /// <summary>
         /// Центр гиперсферы
         /// </summary>
		public  Vector CentGiperSfer
		 {
			 get{return _centGiperSfer;}
		 	set{_centGiperSfer = value;}
		 }
	}
	
	
	/// <summary>
	/// Структура классификатора
	/// </summary>
	[Serializable]
	public class StructClasses 
	{
		/// <summary>
		/// Коллекция классов
		/// </summary>
		public List<StructClass> _classes = new List<StructClass>();
		
        /// <summary>
        /// Список классов
        /// </summary>
		public List<StructClass> Classes
		{
				 get{return _classes;}
		 		set{_classes = value;}
		}
	}
	
	
	
	
	
	
	
	
	
	/// <summary>
	/// Классификатор
	/// </summary>
	[Serializable]
	public class Classifier : IClassifire
    {
		[NonSerialized]
		StructClass _class;// Текущий класс
		StructClasses _classes;// Классификатор
		[NonSerialized]
		Forel _forel;
		
		/// <summary>
		/// Массив классов
		/// </summary>
		public StructClasses Classes
		{
				get{return _classes;}
		 		set{_classes = value;}
		}
		
		
		/// <summary>
		/// Классификатор
		/// </summary>
		public Classifier()
		{
			_classes = new StructClasses();
		}
		
		
		
		
		/// <summary>
		/// Классификатор
		/// </summary>
		/// <param name="path">Путь до файла</param>
		public Classifier(string path)
		{
			_classes = new StructClasses();
			Open(path);
		}
		
		
		
		/// <summary>
		/// Классивикатор
		/// </summary>
		/// <param name="classifikator"> Коллекция классов</param>
		public Classifier(StructClasses classifikator)
		{
			_classes = classifikator;
		}
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		/// <summary>
		/// Преобразование картинки в вектор
		/// </summary>
		/// <param name="Bmp">Картинка</param>
		/// <param name="flag">Реаальный ли размер использовать?</param>
		/// <returns></returns>
       static public Vector GeneratVector(Bitmap Bmp, bool flag)
        {
       	
       	 	int W = 60;
            int H = 60;
            int N = 3600;
            Bitmap fotoBmp = new Bitmap(Bmp, 60, 60);
            
       	if(flag){
       		 fotoBmp = new Bitmap(Bmp);
          	 W = fotoBmp.Width;
             H = fotoBmp.Height;
             N = W*H;
       	}
       	
       	
       	
       	
            double[] val = new double[N];

            Double[] mas = new double[N];

            double sum = 0, m = 0; // сумма и мат. ожидание

            for (int j = 0, k = 0; j < H; j++)
            {
                for (int i = 0; i < W; i++)
                {
                    val[k] = (fotoBmp.GetPixel(i, j).R + fotoBmp.GetPixel(i, j).G + fotoBmp.GetPixel(i, j).B) / 3.0;
                    sum += val[k++];
                }
            }


            // к общ виду
            for (int i = 0; i < N; i++)
            {
                m = sum / N;
                mas[i] = (val[i] - m) / 200.0;
            }


            return new Vector(mas);
        }
        
        
        
        /// <summary>
        /// Преобразование вектора в картинку
        /// </summary>
        /// <param name="vect">Вектор</param>
        /// <returns></returns>
     static public  Bitmap GeneratBmp(Vector vect)
        {
            Bitmap fotoBmp = new Bitmap( 60, 60); 

		const int W = 60;
		const int H = 60;
         
            int color;
            
            for (int j = 0, k = 0; j < H; j++)
            {
                for (int i = 0; i < W; i++)
                {
                	color = (int)Math.Abs(vect.DataInVector[k]*200);
                	
                	fotoBmp.SetPixel(i,j,Color.FromArgb(color,color,color));
                	k++;
                }
            }


            return fotoBmp;
        }
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		/// <summary>
		/// Поиск центра класса
		/// </summary>
		/// <param name="vectors">Точки класса</param>
		/// <returns></returns>
		Vector GetCentr(Vector[] vectors)
		{
			Vector output = new Vector();
			
			int N = vectors.Length;
			output = vectors[0];
				
			for(int i = 1; i<N; i++)
				output += vectors[i];
			
			return output/N;
		}
		
		
		
		
		
		
		
		
		
		/// <summary>
		/// Обучение одного элеменнта класса
		/// </summary>
		/// <param name="tDataset">Выборка</param>
		/// <param name="nameClass">Имя класса</param>
		/// <returns></returns>
		 Vector Teach1(Vector[] tDataset, string nameClass)
		{
		 	_class = new StructClass();
		 	Vector a  = GetCentr(tDataset);
		 	_class._centGiperSfer = a;
		 	_class._strName = nameClass;
		 	return a;
		}
		 
		 
		 
		 
		 /// <summary>
		/// Обучение одного элеменнта класса
		/// </summary>
		/// <param name="tDataset">Выборка</param>
		/// <param name="nameClass">Имя класса</param>
		/// <returns></returns>
		public Vector AddClasses(Vector[] tDataset, string nameClass)
		{
		 	_class = new StructClass();
		 	Vector a  = GetCentr(tDataset);
		 	
		 	Claster[] clasters;
		 	
		 	_forel = new Forel(tDataset);
		 	clasters = _forel.Clasters;
		 	
		 	
		 	for(int i = 0; i<clasters.Length; i++)
		 	{
		 	_class = new StructClass();
		 	_class._centGiperSfer = clasters[i].Centr;
		 	_class._strName = nameClass;
		 	_classes._classes.Add(_class);
		 	}
		 	return a;
		}



        /// <summary>
        /// Добавление класса в классификатор
        /// </summary>
        /// <param name="tDataset">Выборка</param>
        /// <param name="nameClass">Имя класса</param>
        /// <returns></returns>
        public Vector AddClass1(Vector[] tDataset, string nameClass)
        {
            Vector a = Teach1(tDataset, nameClass);
            _classes._classes.Add(_class);
            return a;
        }



        /// <summary>
		/// Добавление класса в классификатор
		/// </summary>
		/// <param name="tDataset">Выборка</param>
		/// <param name="nameClass">Имя класса</param>
		/// <returns></returns>
		public void AddClass(Vector[] tDataset, string nameClass)
        {
            Vector a = Teach1(tDataset, nameClass);
            _classes._classes.Add(_class);
        }














        /// <summary>
        /// Сохранение классификатора
        /// </summary>
        /// <param name="path">Путь</param>
        public void Save(string path)
		{
			try{
			StructClasses clases = new StructClasses();
			clases._classes = _classes._classes;
			 
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Create, FileAccess.Write, FileShare.None))
			{
			 	binFormat.Serialize(fStream, clases);
			}
			  }
			
			catch
			{
				throw new ArgumentException("Ошибка сохранения", "Сохранение");
			}
		}
		
		
		/// <summary>
		/// Загрузка классификатора
		/// </summary>
		/// <param name="path">Путь</param>
		public void Open(string path)
		{
			
			try{
			 	
			 StructClasses clases;
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Open, FileAccess.Read, FileShare.None))
			{
			 	clases =(StructClasses)binFormat.Deserialize(fStream);
			}
			
			 _classes._classes = clases._classes;
			}
			
			catch
			{
				throw new ArgumentException("Ошибка загрузки", "Загрузка");
			}
			
		}
		
		
		
		
		
		
		
		
		
		/// <summary>
		/// Распознавание
		/// </summary>
		/// <param name="inp">Вектор который надо распознать</param>
		public string RecognizeVector(Vector inp)
		{
			
			double _stMin = 1e+300, _st;
			string output = "";
			
			for(int i = 0; i<_classes._classes.Count;  i++)
			{
				_st = Distance.ManhattanDistance(inp, _classes._classes[i]._centGiperSfer); // Вычисление билжайшего центра
				if(_st<_stMin)_stMin = _st;
			}


            for (int i = 0; i < _classes._classes.Count; i++)
			{
				_st = Distance.ManhattanDistance(inp, _classes._classes[i]._centGiperSfer); // Вычисление билжайшего центра
				
				if(_st == _stMin)
				{
					output = _classes._classes[i]._strName;
					break;
				}
				
			}
			
			
			return output;
			}
		
	}
}
