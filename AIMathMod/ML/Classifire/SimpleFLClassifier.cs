﻿using AI.MathMod.AdditionalFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AI.MathMod.ML.Classifire
{

    /// <summary>
    /// Модель класса
    /// </summary>
    [Serializable]
    public class SModel : List<SModelComponent>
    {
        /// <summary>
        /// Вероятность
        /// </summary>
        public double Probability { get; set; }
        
        /// <summary>
        /// Имя класса
        /// </summary>
        public string NameClass { get; set; }

        /// <summary>
        /// Веса для каждого компонента
        /// </summary>
        public Vector Weights { get; set; }

        /// <summary>
        /// Априорная вероятность
        /// </summary>
        public double ProbabilityApriory { get; set; }

		/// <summary>
		/// Задание модели по умолчанию
		/// </summary>
        public SModel()
        {
        	ProbabilityApriory = 1.0;
        }
        
        
//        
//        internal void CalculateProb()
//        {
//            Probability = 0;
//            for (int i = 0; i < Count; i++)
//                Probability += this[i].pr;
//
//            Probability /= Count;
//        }

        /// <summary>
        /// Рассчет вероятностей с учетом весов
        /// </summary>
        public void CalculateProb()
        {
            Probability = 0;
            for (int i = 0; i < Count; i++)
                Probability += Weights.DataInVector[i] * this[i].pr;

            Probability /= Functions.Summ(Weights);
            
            Probability *= ProbabilityApriory;
        }


    }

    /// <summary>
    /// Модель компоненты вектора
    /// </summary>
    [Serializable]
    public class SModelComponent
    {
    	/// <summary>
    	/// Мат. ожидаине
    	/// </summary>
        public double _e = 0;
        /// <summary>
    	/// СКО
    	/// </summary>
        public double _std = 1;
        /// <summary>
        /// Вероятность
        /// </summary>
        public double pr = 0;

        /// <summary>
        /// Стат. модель компонента модели
        /// </summary>
        public SModelComponent()
        {

        }
        

        /// <summary>
        /// Стат. модель компонента модели
        /// </summary>
        /// <param name="e">Мат. ожидание</param>
        /// <param name="std">Среднеквадратичное отклонение</param>
        public SModelComponent(double e, double std)
        {
            _std = std;
            _e = e;
        }
    }

    /// <summary>
    /// Простой статистический классификатор, 
    /// который предполагает, что у величины 
    /// гауссовский з-н распределения
    /// </summary>
    [Serializable]
    public class SimpleFLClassifier : IClassifire
    {

        List<SModel> models = new List<SModel>();
        /// <summary>
        /// Порог
        /// </summary>
        public Double Threshold { get; set; } 
        

        
        /// <summary>
        /// Простой статистический классификатор, 
        /// который предполагает, что у величины 
        /// гауссовский з-н распределения
        /// </summary>
        public SimpleFLClassifier()
        {
            Threshold = 0.5;
        }

        /// <summary>
        /// Простой статистический классификатор, 
        /// который предполагает, что у величины 
        /// гауссовский з-н распределения
        /// </summary>
        /// <param name="path">Путь</param>
        public SimpleFLClassifier(string path)
        {
            Open(path);
            Threshold = 0.5;
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="inp">Вектор входа</param>
        /// <returns>Максимально похожая модель</returns>
        public SModel Output(Vector inp)
        {
        	
        		
            foreach (var sMod in models)
            {
                GetProbability(inp.DataInVector, sMod);
            }

            models.Sort((a, b) => a.Probability.CompareTo(b.Probability) * -1);
            return models[0];
        }


        /// <summary>
        /// Добавление модели
        /// </summary>
        public void AddModel(Vector[] vec, string name)
        {
        	Vector[] vectors = vec;
        	
            Vector[] components = new Vector[vectors[0].N];
            SModel sMode = new SModel();
            sMode.NameClass = name;

            for(int i = 0; i<components.Length; i++)
            {
                components[i] = new Vector(vectors.Length);

                for (int j = 0; j < vectors.Length; j++)
                {
                    components[i].DataInVector[j] = vectors[j].DataInVector[i];
                }

                sMode.Add(new SModelComponent(Statistic.ExpectedValue(components[i]), Statistic.Std(components[i])));
            }
			
            sMode.Weights = new Vector(sMode.Count)+0.0001;
            models.Add(sMode);
        }
        
        
       /// <summary>
       /// Весовые коэффициенты
       /// </summary>
        public void GetWeights()
        {
        	for (int i = 0; i < models.Count; i++)
        	{
        		for (int j = 0; j < models.Count; j++)
        		{
        			models[i].Weights += GW(models[i], models[j]);
        		}
        		
        		models[i].Weights /= models.Count;
        	}
        }
        	

        Vector GW(SModel model1, SModel model2)
        {
        	Vector w = new Vector(model1.Count);
        	
        	for (int i = 0; i < model1.Count; i++)
        		{
        			w[i] =  Math.Abs(model1[i]._e-model2[i]._e);
        		}
        	
        	return w;
        }
        

        /// <summary>
        /// Вероятности принадлежности к классу
        /// </summary>
        /// <param name="vect"></param>
        /// <param name="sm"></param>
        void GetProbability(double[] vect, SModel sm)
        {
            for (int i = 0; i<vect.Length; i++)
            {
                sm[i].pr = DistributionFunc.GaussNorm1(vect[i], sm[i]._e, sm[i]._std);
            }

            sm.CalculateProb();
        }


        /// <summary>
		/// Сохранение классификатора
		/// </summary>
		/// <param name="path">Путь</param>
		public void Save(string path)
        {
            try
            {
                
                BinaryFormatter binFormat = new BinaryFormatter();

                using (Stream fStream = new FileStream(path,
                  FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, models);
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

            try
            {

                    
                BinaryFormatter binFormat = new BinaryFormatter();

                using (Stream fStream = new FileStream(path,
                  FileMode.Open, FileAccess.Read, FileShare.None))
                {
                   models = (List<SModel>)binFormat.Deserialize(fStream);
                }

              
            }

            catch
            {
                throw new ArgumentException("Ошибка загрузки", "Загрузка");
            }

        }


        /// <summary>
        /// Добавление класса\модели
        /// </summary>
        public void AddClass(Vector[] tDataset, string nameClass)
        {
            AddModel(tDataset, nameClass);
        }


        /// <summary>
        /// Распознавание вектора
        /// </summary>
        /// <param name="inp">Вектор</param>
        /// <returns>Имя класса</returns>
        public string RecognizeVector(Vector inp)
        {
        	Vector input = inp;
        	
            SModel model = Output(input);
            return model.Probability >= Threshold ? model.NameClass : "none";
        }
    }
}
