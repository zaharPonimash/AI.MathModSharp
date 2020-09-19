/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 12.08.2018
 * Время: 0:55
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.ML.NeuronNetwork;
using System;

namespace AI.MathMod.ML.Regression
{
    /// <summary>
    /// Множественная регрессия на базе нейронной сети
    /// </summary>
    public class MultipleRegressionNNW
    {
        private readonly Net net = new Net();
        private readonly Vector[] _Xs;
        private readonly Vector _Ys;
        /// <summary>
        /// Множественная регрессия
        /// </summary>
        /// <param name="vectsInp"></param>
        /// <param name="vectOutp"></param>
        public MultipleRegressionNNW(Vector[] vectsInp, Vector vectOutp)
        {
            net.Add(new LinearLayer(vectsInp[0].N, 1));

            net.LerningRate = 0.001;

            _Xs = new Vector[vectOutp.N];

            _Xs = vectsInp;
            _Ys = vectOutp;

        }

        /// <summary>
        /// Обучение
        /// </summary>
        /// <param name="ep"></param>
        public void Train(double ep = 1)
        {
            Random rnd = new Random();

            int countMax = (int)(_Xs.Length * ep);
            int index;

            for (int i = 0; i < countMax; i++)
            {
                index = rnd.Next(_Xs.Length);
                net.Train(_Xs[index], new Vector(_Ys[index]));
            }
        }


        /// <summary>
        /// Прогноз
        /// </summary>
        /// <param name="x">Вектор входа</param>
        /// <returns>выход</returns>
        public double Predict(Vector x)
        {
            return net.Output(x)[0];
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
    }
}
