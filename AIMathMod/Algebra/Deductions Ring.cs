/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 16.04.2016
 * Time: 18:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AI.MathMod.Algebra
{
    /// <summary>
    /// Реализует кольцо вычетов Z/mZ и операции над ним
    /// </summary>
    public class DeductionsRing
    {
        private int _x;

        /// <summary>
        /// Модуль кольца
        /// </summary>
        public int M { get; }

        /// <summary>
        /// число пренадлежащее кольцу
        /// </summary>
        public int X
        {
            get => _x;
            set => _x = value % M;
        }

        /// <summary>
        /// Является ли кольцо полем
        /// </summary>
        public bool IsField { get; }


        /// <summary>
        /// Реализует кольцо вычетов Z/mZ и операции над ним
        /// </summary>
        /// <param name="m"> Модуль кольца</param>
        public DeductionsRing(int m)
        {
            M = m;
            IsField = IsSimple(M);
        }



        /// <summary>
        /// Проверка числа на простоту
        /// </summary>
        /// <param name="n">проверяемое число</param>
        public static bool IsSimple(int n)
        {
            bool isSimp = true;

            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    isSimp = false;
                    break;
                }
            }

            return isSimp;
        }



        #region Действия
        /// <summary>
        /// Сложение двух колец вычетов
        /// </summary>
        /// <param name="A">Первое</param>
        /// <param name="B">Второе</param>
        /// <returns>Сумма</returns>
        public static DeductionsRing operator +(DeductionsRing A, DeductionsRing B)
        {
            if (A.M != B.M)
            {
                throw new ArgumentException("Модули колец не совпадают", "Сложение");
            }

            DeductionsRing C = new DeductionsRing(A.M)
            {
                _x = (A._x + B._x) % A.M
            };
            return C;
        }

        /// <summary>
        /// Разность двух колец вычетов
        /// </summary>
        /// <param name="A">Первое</param>
        /// <param name="B">Второе</param>
        /// <returns>Разность</returns>
        public static DeductionsRing operator -(DeductionsRing A, DeductionsRing B)
        {
            if (A.M != B.M)
            {
                throw new ArgumentException("Модули колец не совпадают", "Вычитание");
            }

            DeductionsRing C = new DeductionsRing(A.M)
            {
                _x = (A._x - B._x) % A.M
            };
            return C;
        }

        /// <summary>
        /// Произведение двух колец вычетов
        /// </summary>
        /// <param name="A">Первое</param>
        /// <param name="B">Второе</param>
        /// <returns>Произведение</returns>
        public static DeductionsRing operator *(DeductionsRing A, DeductionsRing B)
        {
            if (A.M != B.M)
            {
                throw new ArgumentException("Модули колец не совпадают", "Умножение");
            }

            DeductionsRing C = new DeductionsRing(A.M)
            {
                _x = (A._x * B._x) % A.M
            };
            return C;
        }

        #endregion
    }
}
