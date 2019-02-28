using AI.MathMod.AdditionalFunctions;
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
        public Vector ProbabilityApriory { get; set; }


        internal void CalculateProb()
        {
            Probability = 0;
            for (int i = 0; i < Count; i++)
                Probability += this[i].pr;

            Probability /= Count;
        }

        /// <summary>
        /// Рассчет вероятностей с учетом весов
        /// </summary>
        public void CalculateProbW()
        {
            Probability = 0;
            for (int i = 0; i < Count; i++)
                Probability += Weights.Vecktor[i] * this[i].pr;

            Probability /= Functions.Summ(Weights);
        }


    }

    /// <summary>
    /// Модель компоненты вектора
    /// </summary>
    [Serializable]
    public class SModelComponent
    {
        public double _e = 0;
        public double _sco = 1;
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
        /// <param name="sco">Среднеквадратичное отклонение</param>
        public SModelComponent(double e, double sco)
        {
            _sco = sco;
            _e = e;
        }
    }

    /// <summary>
    /// Простой статистический классификатор, 
    /// который предполагает, что у величины 
    /// гауссовский з-н распределения
    /// </summary>
    [Serializable]
    public class SimpleStatisticClassifire : IClassifire
    {

        List<SModel> models = new List<SModel>();
        public Double Porog { get; set; } 

        /// <summary>
        /// Простой статистический классификатор, 
        /// который предполагает, что у величины 
        /// гауссовский з-н распределения
        /// </summary>
        public SimpleStatisticClassifire()
        {
            Porog = 0.5;
        }

        /// <summary>
        /// Простой статистический классификатор, 
        /// который предполагает, что у величины 
        /// гауссовский з-н распределения
        /// </summary>
        /// <param name="path">Путь</param>
        public SimpleStatisticClassifire(string path)
        {
            Open(path);
            Porog = 0.5;
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="input">Вектор входа</param>
        /// <returns>Максимально похожая модель</returns>
        public SModel Output(Vector input)
        {
            foreach (var sMod in models)
            {
                GetProbability(input.Vecktor, sMod);
            }

            models.Sort((a, b) => a.Probability.CompareTo(b.Probability) * -1);
            return models[0];
        }


        /// <summary>
        /// Добавление модели
        /// </summary>
        public void AddModel(Vector[] vectors, string name)
        {
            Vector[] components = new Vector[vectors[0].N];
            SModel sMode = new SModel();
            sMode.NameClass = name;

            for(int i = 0; i<components.Length; i++)
            {
                components[i] = new Vector(vectors.Length);

                for (int j = 0; j < vectors.Length; j++)
                {
                    components[i].Vecktor[j] = vectors[j].Vecktor[i];
                }

                sMode.Add(new SModelComponent(Statistic.ExpectedValue(components[i]), Statistic.Sco(components[i])));
            }

            models.Add(sMode);
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
                sm[i].pr = DistributionFunc.GaussNorm1(vect[i], sm[i]._e, sm[i]._sco);
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
        public void AddClass(Vector[] tViborka, string nameClass)
        {
            AddModel(tViborka, nameClass);
        }


        /// <summary>
        /// Распознавание вектора
        /// </summary>
        /// <param name="inp">Вектор</param>
        /// <returns>Имя класса</returns>
        public string RecognizeVector(Vector inp)
        {
            SModel model = Output(inp);
            return model.Probability >= Porog ? model.NameClass : "none";
        }
    }
}
