﻿/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 18.03.2017
 * Время: 10:22
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
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
	public class StructClassCorr
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
		 /// Вероятность принадлежности
		 /// </summary>
		 public double Probability{get;set;}


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
	public class StructClassesCorr
	{
		public List<StructClassCorr> _classes = new List<StructClassCorr>(); // набор классов
		
        /// <summary>
        /// Список классов
        /// </summary>
		public List<StructClassCorr> Classes
		{
				 get{return _classes;}
		 		set{_classes = value;}
		}
	}
	
	
	
	
	
	
	
	
	
	/// <summary>
	/// Классификатор
	/// </summary>
	[Serializable]
	public class CorrelationClassifier : IClassifire 
    {
        StructClassesCorr _classes;// Классификатор
        [NonSerialized]
		StructClassCorr _class;// Текущий класс
		[NonSerialized]
		Forel _forel;
		
		
		/// <summary>
		/// Классы
		/// </summary>
		public StructClassesCorr Classes
		{
				get{return _classes;}
		 		set{_classes = value;}
		}
		
		
		/// <summary>
        /// Корреляционный классификатор
        /// </summary>
		public CorrelationClassifier()
		{
			_classes = new StructClassesCorr();
		}
		
		
		
		
		/// <summary>
		/// Классификатор
		/// </summary>
		/// <param name="path">Путь до файла</param>
		public CorrelationClassifier(string path)
		{
			_classes = new StructClassesCorr();
			Open(path);
		}
		
		
		
		/// <summary>
		/// Классификатор
		/// </summary>
		/// <param name="classifikator"> Коллекция классов</param>
		public CorrelationClassifier(StructClassesCorr classifikator)
		{
			_classes = classifikator;
		}
		
		
		
		
		
		
		
		
		/// <summary>
		/// Корреляционная метрика
		/// </summary>
		/// <param name="A">Первый вектор</param>
		/// <param name="B">Второй вектор</param>
		/// <returns>Коэффициент корреляции нормированный [0;1]</returns>
		public static double CorrelationMetric(Vector A, Vector B)
		{
			double r = Statistic.CorrelationCoefficient(A,B);
			r = (r>0)?r:0;
			return r;
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
		/// <param name="tViborka">Выборка</param>
		/// <param name="nameClass">Имя класса</param>
		/// <returns></returns>
		 Vector Teach1(Vector[] tViborka, string nameClass)
		{
		 	_class = new StructClassCorr();
		 	Vector a  = GetCentr(tViborka);
		 	_class._centGiperSfer = a;
		 	_class._strName = nameClass;
		 	return a;
		}
		 
		 
		 
		 
		 /// <summary>
		/// Обучение одного элеменнта класса
		/// </summary>
		/// <param name="tViborka">Выборка</param>
		/// <param name="nameClass">Имя класса</param>
		/// <returns></returns>
		public Vector AddClasses(Vector[] tViborka, string nameClass)
		{
		 	_class = new StructClassCorr();
		 	Vector a  = GetCentr(tViborka);
		 	
		 	Claster[] clasters;
		 	
		 	_forel = new Forel(tViborka);
		 	clasters = _forel.Clasters;
		 	
		 	
		 	for(int i = 0; i<clasters.Length; i++)
		 	{
		 	_class = new StructClassCorr();
		 	_class._centGiperSfer = clasters[i].Centr;
		 	_class._strName = nameClass;
		 	_classes._classes.Add(_class);
		 	}
		 	return a;
		}



        /// <summary>
        /// Добавление класса в классификатор
        /// </summary>
        /// <param name="tViborka">Выборка</param>
        /// <param name="nameClass">Имя класса</param>
        /// <returns></returns>
        public Vector AddClass1(Vector[] tViborka, string nameClass)
        {
            Vector a = Teach1(tViborka, nameClass);
            _classes._classes.Add(_class);
            return a;
        }


        /// <summary>
		/// Добавление класса в классификатор
		/// </summary>
		/// <param name="tViborka">Выборка</param>
		/// <param name="nameClass">Имя класса</param>
		public void AddClass(Vector[] tViborka, string nameClass)
        {
            Vector a = Teach1(tViborka, nameClass);
            _classes._classes.Add(_class);
        }














        /// <summary>
        /// Сохранение классификатора
        /// </summary>
        /// <param name="path">Путь</param>
        public void Save(string path)
		{
			try{
			StructClassesCorr clases = new StructClassesCorr();
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
			 	
			 StructClassesCorr clases;
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Open, FileAccess.Read, FileShare.None))
			{
			 	clases =(StructClassesCorr)binFormat.Deserialize(fStream);
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
			
			for(int i = 0; i<_classes._classes.Count;  i++)
				_classes._classes[i].Probability = CorrelationMetric(inp, _classes._classes[i]._centGiperSfer); // Вычисление билжайшего центра	
			_classes._classes.Sort((a, b) => a.Probability.CompareTo(b.Probability)*-1);	
			return _classes._classes[0].StrName;	
		}
		
		
		/// <summary>
		/// Распознавание
		/// </summary>
		/// <param name="inp">Вектор который надо распознать</param>
		public StructClassCorr RecognizeVectorStruct(Vector inp)
		{
			
			for(int i = 0; i<_classes._classes.Count;  i++)
				_classes._classes[i].Probability = CorrelationMetric(inp, _classes._classes[i]._centGiperSfer); // Вычисление билжайшего центра	
			_classes._classes.Sort((a, b) => a.Probability.CompareTo(b.Probability)*-1);	
			return _classes._classes[0];	
		}
		
		
		
		
		
		
		
	}
}
