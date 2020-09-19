/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 28.07.2018
 * Время: 13:41
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.ML.NeuronNetwork
{
    /// <summary>
    /// Полносвязный слой
    /// </summary>
    [Serializable]
    public class FullConLayerBase : ILayer
    {
        /// <summary>
        /// Размерность выхода
        /// </summary>
        public int SizeOut
        {
            get;
            set;
        }

        /// <summary>
        /// Матрица весов
        /// </summary>
        public Matrix W { set; get; }
        /// <summary>
        /// Вектор входа
        /// </summary>
        protected Vector Inp;
        /// <summary>
        /// Дельты
        /// </summary>
        public virtual Vector Delts { get; set; }
        /// <summary>
        /// Выход слоя
        /// </summary>
        public Vector OutputLayer { get; protected set; }
        /// <summary>
        /// Скорость обучения
        /// </summary>
        public double norm;
        /// <summary>
        /// Ошибка
        /// </summary>
        public double Eps { set; get; }
        /// <summary>
        /// Момент
        /// </summary>
        public double moment;
        /// <summary>
        /// Матрица весов на прошлой иттерации обучения
        /// </summary>
        protected Matrix Last;

        /// <summary>
        /// Полносвязный слой
        /// </summary>
        public FullConLayerBase()
        {
        }

        /// <summary>
        /// Полносвязный слой
        /// </summary>
        public FullConLayerBase(int inp, int outp)
        {
            SetParam(inp, outp);
        }

        /// <summary>
        /// Установка параметров
        /// </summary>
        public virtual void SetParam(int inp, int outp)
        {
            Inp = new Vector(inp);
            OutputLayer = new Vector(outp);
            SizeOut = outp;
            norm = 0.5 / (inp * outp);
            moment = 0;
            W = 0.003 * Statistic.randNorm(inp, outp) / inp;
            Last = new Matrix(inp, outp);
        }

        #region ILayer implementation

        /// <summary>
        /// Прямой проход сети
        /// </summary>
        /// <param name="input">Вход</param>
        public virtual Vector Output(Vector input)
        {
            Inp = new Vector(input.N);

            for (int i = 0; i < input.N; i++)
            {
                Inp[i] = input[i];
            }

            OutputLayer = FActivation(Inp * W);
            return OutputLayer;
        }

        /// <summary>
        /// Обратный проход сети
        /// </summary>
        public virtual Vector Backwards()
        {
            return Delts * W.Tr();
        }

        /// <summary>
        /// Ф-я активации
        /// </summary>
        /// <param name="inp">Вход</param>
        public virtual Vector FActivation(Vector inp)
        {
            return inp;
        }



        /// <summary>
        /// Производная ф-ии активации
        /// </summary>
        public virtual Vector DfDy()
        {
            return new Vector(OutputLayer.N) + 1;
        }

        /// <summary>
        /// Ошибка на скрытом слое
        /// </summary>
        /// <param name="layer">Следующий слой</param>
        public void DeltH(ILayer layer)
        {
            Delts = layer.Backwards();
        }

        /// <summary>
        /// Обучение НС
        /// </summary>
        public virtual void Train()
        {
            for (int i = 0; i < OutputLayer.N; i++)
            {
                for (int j = 0; j < Inp.N; j++)
                {
                    double c = moment * Last.Matr[j, i] + norm * Inp[j] * Delts[i];
                    W.Matr[j, i] -= c;
                    Last.Matr[j, i] = c;
                }
            }
        }




        /// <summary>
        /// Перегенерирование весов
        /// </summary>
        /// <param name="rnd">Случайные числа</param>
        public void WGenerate(Random rnd)
        {
            W = 0.003 * Statistic.randNorm(W.M, W.N, rnd) / W.N;
        }

        /// <summary>
        /// Расчет дельт
        /// </summary>
        /// <param name="ideal">Идеальный выход</param>
        public virtual void Delt(Vector ideal)
        {
            Delts = DfDy() * (OutputLayer - ideal);//ideal.N;

            Eps = 0.5 * Functions.Summ((OutputLayer - ideal) * (OutputLayer - ideal)) / ideal.N;
        }

        #endregion
    }
}
