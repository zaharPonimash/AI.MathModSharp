/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 05.03.2017
 * Время: 18:11
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace AI.MathMod.ML.Regression
{
    /// <summary>
    /// Аппроксимация данных синусоидами
    /// </summary>
    [Serializable]
    public class SinApproximation
    {
        [NonSerialized]
        private Thread th;
        [NonSerialized]
        private readonly GradientDecentDataset _gdd = new GradientDecentDataset();
        /// <summary>
        /// Параметры
        /// </summary>
        public Vector _param;
        private readonly int _n;
        [NonSerialized]
        private readonly GradientDecent _gD;

        /// <summary>
        /// Аппроксимация степенным рядом
        /// </summary>
        /// <param name="X">Вектор Х</param>
        /// <param name="Y">Вектор Y</param>
        /// <param name="n">количество членов ряда(гармоник)</param>
        public SinApproximation(Vector X, Vector Y, int n)
        {
            _n = n + 1;
            _param = new Vector(_n);
            _param.DataInVector[0] = 0.1;

            for (int i = 0; i < X.N; i++)
            {
                _gdd.Add(X.DataInVector[i], Y.DataInVector[i]);
            }


            _gD = new GradientDecent(_param, GetError, _gdd)
            {
                Norm = 1,
                Step = 1e-9
            };
        }


        /// <summary>
        /// Рассчет по модели
        /// </summary>
        /// <param name="x">входное значение</param>
        /// <param name="param">вектор парамметров</param>
        /// <returns></returns>
        private double Work(double x, Vector param)
        {
            double outp = 0;

            for (int i = 1; i < _n; i++)
            {
                outp += param.DataInVector[i] * Math.Sin(x * i / param.DataInVector[0]);
            }

            return outp;
        }



        /// <summary>
        /// Расчет вектора значений
        /// </summary>
        /// <param name="X"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private Vector Work(Vector X, Vector param)
        {
            Vector Y = new Vector(X.N);

            for (int i = 0; i < X.N; i++)
            {
                Y.DataInVector[i] = Work(X.DataInVector[i], param);
            }

            return Y;
        }



        /// <summary>
        /// Работа системы(рассчет вектора значений)
        /// </summary>
        /// <param name="X">Вектор входа</param>
        /// <returns></returns>
        public Vector Work(Vector X)
        {
            return Work(X, _param);
        }




        /// <summary>
        /// Целевая ф-я
        /// </summary>
        /// <param name="param"></param>
        /// <param name="inp"></param>
        /// <param name="ideal"></param>
        /// <returns></returns>
        private double GetError(Vector param, List<Vector> inp, List<Vector> ideal)
        {
            Vector X = new Vector(inp.Count);
            Vector Y = new Vector(X.N);
            Vector E;

            for (int i = 0; i < X.N; i++)
            {
                X.DataInVector[i] = inp[i].DataInVector[0];
                Y.DataInVector[i] = ideal[i].DataInVector[0];
            }

            E = (Y - Work(X, param)) ^ 2;
            return Functions.Summ(E) / E.N;
        }





        /// <summary>
        /// Синхронное обучение модели, 30 иттераций
        /// </summary>
        public void Teach()
        {
            _gD.Decent();
            _param = _gD.Parammetrs;
        }

        /// <summary>
        /// Синхронное обучение модели, любое кол-во иттераций
        /// </summary>
        /// <param name="n">Кол-во иттераций</param>
        public void Teach(int n)
        {
            _gD.Itterations = n;
            _gD.Decent();
            _param = _gD.Parammetrs;
        }


        /// <summary>
        /// Асинхронное обучение модели, 30 иттераций
        /// </summary>
        public void AsyncTeach()
        {
            th = new Thread(Teach);
            th.Start();
        }

        /// <summary>
        /// Асинхронное обучение модели, любое кол-во иттераций
        /// </summary>
        /// <param name="n">Кол-во иттераций</param>
        public void AsyncTeach(int n)
        {
            _gD.Itterations = n;
            th = new Thread(Teach);
            th.Start();
        }

    }
}
