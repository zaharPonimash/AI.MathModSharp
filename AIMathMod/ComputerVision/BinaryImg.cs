/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.09.2018
 * Время: 9:33
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.AdditionalFunctions;
using System;
using System.Drawing;

namespace AI.MathMod.ComputerVision
{
    /// <summary>
    /// Description of BinaryImg.
    /// </summary>
    public class BinaryImg
    {
        private bool[,] img;
        /// <summary>
        /// Ширина
        /// </summary>
        public int M { get; set; }
        /// <summary>
        /// Высота
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// Вывод индекса
        /// </summary>
        public bool this[int i, int j]
        {
            set => img[i, j] = value;

            get => img[i, j];
        }

        /// <summary>
        /// Бинарное изображение
        /// </summary>
        /// <param name="matr">Матрица серого</param>
        public BinaryImg(Matrix matr)
        {
            ToBools(matr);
            M = matr.M;
            N = matr.N;
        }

        /// <summary>
        /// Бинарное изображение
        /// </summary>
        /// <param name="bm">Изображение</param>
        public BinaryImg(Bitmap bm)
        {
            Matrix matr = ImgConverter.BmpToMatr(bm);
            matr = NeuroFunc.Threshold(matr, 0.85);
            ToBools(matr);
            M = matr.M;
            N = matr.N;
        }

        private Matrix ToMatrix()
        {
            Matrix matr = new Matrix(M, N);

            for (int i = 0; i < matr.M; i++)
            {
                for (int j = 0; j < matr.N; j++)
                {
                    matr[i, j] = img[i, j] ? 1 : 0;
                }
            }

            return matr;
        }


        /// <summary>
        /// Бинарное в матрицу
        /// </summary>
        public Matrix ToMatrixInvers()
        {
            Matrix matr = new Matrix(M, N);

            for (int i = 0; i < matr.M; i++)
            {
                for (int j = 0; j < matr.N; j++)
                {
                    matr[i, j] = img[i, j] ? -1 : 0;
                }
            }

            return matr;
        }

        /// <summary>
        /// Бинарное в Bitmap
        /// </summary>
        public Bitmap ToBmp()
        {
            return ImgConverter.MatrixToBitmap(ToMatrix());
        }

        private void ToBools(Matrix matr)
        {
            img = new bool[matr.M, matr.N];

            for (int i = 0; i < matr.M; i++)
            {
                for (int j = 0; j < matr.N; j++)
                {
                    img[i, j] = Math.Abs(matr[i, j] - 1) < 0.1;
                }
            }
        }
    }
}
