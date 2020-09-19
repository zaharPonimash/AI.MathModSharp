/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 08.04.2015
 * Time: 22:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;


namespace AI.MathMod.ML
{




    namespace Clasterisators
    {





        /// <summary>
        /// Кластеризатор - Форель с корреляционной метрикой
        /// </summary>
        public class CorrForel
        {
            private readonly Vector[] _dataset, _datasetNotClasteris, _nowDataset; // Выборка
            private readonly List<Claster> _clasters = new List<Claster>(); // Кластеры в выборке
            private readonly Claster _claster = new Claster();
            private readonly double R0 = 0, Rn = 0;
            private readonly Vector _mainCentr;
            private readonly Random rng = new Random();


            /// <summary>
            /// Кластеры
            /// </summary>
            public Claster[] Clasters => _clasters.ToArray();







            /// <summary>
            /// Корреляционный форель
            /// </summary>
            public CorrForel(Vector[] dataset)
            {
                Vector _old = new Vector(), _new = new Vector(); // Центры гиперсфер

                _datasetNotClasteris = _dataset = dataset; // Загрузка выборки
                _old = _mainCentr = GetCentr(_dataset); // Получение центра
                Rn = R0 = Max(_dataset, _mainCentr);// Начальный радиус гиперсферы




                // Кластеризация
                while (_datasetNotClasteris.Length != 0)
                {
                    Rn = 0.9 * R0; // Уменьшение радиуса гиперсферы
                    _nowDataset = GetGipersfer(Rn, _datasetNotClasteris[rng.Next(_datasetNotClasteris.Length)], _datasetNotClasteris); // обводка гиперсферой
                    _new = GetCentr(_nowDataset);// новый центр

                    //Центр кластера
                    while (_old != _new)
                    {
                        Rn *= 0.9; //Уменьшение радиуса гиперсферы
                        _old = _new; // сохранение старого радиуса
                        _nowDataset = GetGipersfer(Rn, _old, _datasetNotClasteris); // обводка гиперсферой	
                        try
                        {
                            _new = GetCentr(_nowDataset);// новый центр
                        }
                        catch { break; }
                    }

                    _claster = new Claster
                    {
                        Centr = _new,// Добавление центра
                        Dataset = _nowDataset// выборка
                    };// Новый кластер
                    _clasters.Add(_claster);// Добавление кластера в коллекцию
                    _datasetNotClasteris = AWithOutB(_datasetNotClasteris, _nowDataset); // Удаление кластеризированных данных

                }



            }




            /// <summary>
            /// Корреляционный форель
            /// </summary>
            public CorrForel(Vector[] dataset, int minR)
            {
                Vector _old = new Vector(), _new = new Vector(); // Центры гиперсфер

                _datasetNotClasteris = _dataset = dataset; // Загрузка выборки
                _old = _mainCentr = GetCentr(_dataset); // Получение центра
                Rn = R0 = Max(_dataset, _mainCentr);// Начальный радиус гиперсферы




                // Кластеризация
                while (_datasetNotClasteris.Length != 0)
                {
                    Rn = 0.9 * R0; // Уменьшение радиуса гиперсферы
                    _nowDataset = GetGipersfer(Rn, _datasetNotClasteris[0], _datasetNotClasteris); // обводка гиперсферой
                    _new = GetCentr(_nowDataset);// новый центр

                    //Центр кластера
                    while ((_old != _new) && (Rn >= minR))
                    {
                        Rn *= 0.9; //Уменьшение радиуса гиперсферы
                        _old = _new; // сохранение старого радиуса
                        _nowDataset = GetGipersfer(Rn, _old, _datasetNotClasteris); // обводка гиперсферой					
                        _new = GetCentr(_nowDataset);// новый центр
                    }

                    _claster = new Claster
                    {
                        Centr = _new,// Добавление центра
                        Dataset = _nowDataset// выборка
                    };// Новый кластер
                    _clasters.Add(_claster);// Добавление кластера в коллекцию
                    _datasetNotClasteris = AWithOutB(_datasetNotClasteris, _nowDataset); // Удаление кластеризированных данных

                }



            }




            /// <summary>
            /// Проводит гиперсферу нужного радиуса из конкретной точки и на заданном множестве
            /// </summary>
            /// <param name="R">Радиус</param>
            /// <param name="m">Центр масс</param>
            /// <param name="mass">Множество точек</param>
            /// <returns></returns>
            private Vector[] GetGipersfer(double R, Vector m, Vector[] mass)
            {
                List<Vector> OUT = new List<Vector>();

                foreach (Vector i in mass)
                {
                    if (Distance.CorrDist(m, i) <= R)
                    {
                        OUT.Add(i); // проведение окружности
                    }
                }

                return OUT.ToArray();
            }



            /// <summary>
            /// Максимальная дистанция
            /// </summary>
            /// <returns></returns>
            private double Max(Vector[] mass, Vector m)
            {
                double max = Distance.CorrDist(mass[0], m), d;
                for (int i = 1; i < mass.Length; i++)
                {
                    d = Distance.CorrDist(mass[i], m);

                    //d = Distance.ManhattanDistance(mass[i],m);
                    if (max < d)
                    {
                        max = d;
                    }
                }
                return max;
            }




            /// <summary>
            /// Множество А\В
            /// </summary>
            /// <param name="A">Множество А</param>
            /// <param name="B">Множество В</param>
            /// <returns>А\B</returns>
            private Vector[] AWithOutB(Vector[] A, Vector[] B)
            {
                List<Vector> C = new List<Vector>();
                bool flag = true; // флаг принадлежности


                for (int i = 0; i < A.Length; i++)
                {
                    flag = true;

                    for (int j = 0; j < B.Length; j++)
                    {
                        if (A[i] == B[j])
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        C.Add(A[i]);
                    }
                }

                return C.ToArray();
            }





            /// <summary>
            /// Поиск центра класса
            /// </summary>
            /// <param name="vectors">Точки класса</param>
            /// <returns></returns>
            private Vector GetCentr(Vector[] vectors)
            {
                Vector output = new Vector();
                int N = vectors.Length;
                output = vectors[0];
                for (int i = 1; i < N; i++)
                {
                    output += vectors[i];
                }

                return output / N;
            }






        }


    }




}
