/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 10.08.2018
 * Время: 13:35
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.Algebra;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace AI.MathMod.ML.Regression
{
    /// <summary>
    /// Множественная регрессия
    /// </summary>
    [Serializable]
    public class MultipleRegression
    {
        private Vector _param, std, mean;

        [NonSerialized]
        private readonly Vector[] _x;
        [NonSerialized]
        private readonly double[] _y;
        [NonSerialized]
        private Matrix A;
        [NonSerialized]
        private Vector B;
        [NonSerialized]
        private readonly int n;
        [NonSerialized]
        private readonly int m;

        /// <summary>
        /// Параметры модели
        /// </summary>
        public Vector Parammetrs
        {
            get;
            private set;
        }



        /// <summary>
        /// Множественная линейная регрессия
        /// </summary>
        /// <param name="X">Вектора входа</param>
        /// <param name="Y">Выходы</param>
        /// <param name="isScale">Стоит ли применить масштабирование к данным</param>
        public MultipleRegression(Vector[] X, Vector Y, bool isScale = false)
        {
            n = X.Length;
            m = X[0].N + 1;

            if (isScale)
            {
                _x = Vector.ScaleData(X);
                std = MathFunc.sqrt(Statistic.EnsembleDispersion(X));
                mean = Statistic.MeanVector(X);
                std = std.AddOne();
                mean = mean.Shift(1);
            }
            else
            {
                std = new Vector(m) + 1;
                mean = new Vector(m);
                _x = new Vector[n];

                for (int i = 0; i < n; i++)
                {
                    _x[i] = X[i].Copy();
                }
            }

            // Добавление единицы
            for (int i = 0; i < n; i++)
            {
                _x[i] = _x[i].AddOne();
            }

            _y = Y.DataInVector;
            GenA();
            GenB();
            GenParam();
        }


        /// <summary>
        /// Множественная линейная регрессия
        /// </summary>
        /// <param name="path">Путь до модели</param>
        public MultipleRegression(string path)
        {
            LoadModel(path);
        }

        // Составление матрицы
        private void GenA()
        {
            A = new Matrix(m, m);
            double c = 0;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {

                    c = 0;

                    for (int k = 0; k < n; k++)
                    {
                        c += _x[k][i] * _x[k][j];
                    }

                    A[i, j] = c;
                }
            }
        }



        // Вектор ответа
        private void GenB()
        {
            B = new Vector(m);
            double c = 0;


            for (int i = 0; i < m; i++)
            {
                c = 0;

                for (int j = 0; j < n; j++)
                {
                    c += _x[j][i] * _y[j];
                }

                B[i] = c;
            }

        }

        // Генерация параметров системы
        private void GenParam()
        {
            Kramer kram = new Kramer();
            _param = kram.GetAnswer(A, B);
            _param.Visual();
        }

        /// <summary>
        /// Прогноз
        /// </summary>
        /// <param name="vect">Вектор входа</param>
        /// <returns>Выход</returns>
        public double Predict(Vector vect)
        {
            Vector inp = vect.AddOne();
            inp -= mean;
            inp /= std;
            return GeomFunc.ScalarProduct(inp, _param);
        }


        /// <summary>
        /// Прогноз
        /// </summary>
        /// <param name="inp">Вектора входа</param>
        /// <returns>Вектор выхода</returns>
        public Vector Predict(Vector[] inp)
        {
            Vector outp = new Vector(inp.Length);

            for (int i = 0; i < inp.Length; i++)
            {
                outp[i] = Predict(inp[i]);
            }

            return outp;
        }


        /// <summary>
        /// Сохранение модели
        /// </summary>
        /// <param name="path">Путь</param>
        public void SaveModel(string path)
        {
            try
            {
                BinaryFormatter binFormat = new BinaryFormatter();

                using (Stream fStream = new FileStream(path,
                                           FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, this);
                }
            }
            catch
            {
                throw new ArgumentException("Ошибка сохранения");
            }
        }

        /// <summary>
        /// Загрузка модели
        /// </summary>
        /// <param name="path">Путь</param>
        public void LoadModel(string path)
        {
            try
            {
                MultipleRegression mR;
                BinaryFormatter binFormat = new BinaryFormatter();

                using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    mR = (MultipleRegression)binFormat.Deserialize(fStream);
                }

                std = mR.std;
                mean = mR.mean;
                _param = mR._param;
            }

            catch
            {
                throw new ArgumentException("Ошибка загрузки");
            }
        }
    }
}
