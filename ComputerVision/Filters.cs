using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ComputerVision
{
    /// <summary>
    /// Фильтрация изображений
    /// </summary>
    public class ImgFilters
    {
        /// <summary>
        /// Пространственный фильтр полутонового изображения
        /// </summary>
        /// <param name="img">Матрица изображения</param>
        /// <param name="filter">Матрица фильтра</param>
        /// <param name="coef">Коэффициент контраста</param>
        /// <param name="dx">Яркость</param>
        /// <returns>Возвращает результат фильтрации</returns>
        public static Matrix SpaceFilter(Matrix img, Matrix filter, double coef = 1, double dx = 0)
        {
            int H = img.M - filter.M + 1, W = img.N - filter.N + 1;
            Matrix newMatr = new Matrix(H, W);

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    newMatr.Matr[i, j] = Filter(img, filter, j, i);
                }
            }

            return NeuroFunc.Relu((newMatr * coef + dx / 255.0), 1, 0);

        }

        /// <summary>
        /// Медианный фильтр полутонового изображения
        /// </summary>
        /// <param name="img">Матрица изображения</param>
        /// <param name="filter">Матрица фильтра</param>
        /// <param name="coef">Коэффициент контраста</param>
        /// <param name="dx">Яркость</param>
        /// <returns>Возвращает результат фильтрации</returns>
        public static Matrix MedianFilter(Matrix img, Matrix filter, double coef = 1, double dx = 0)
        {
            int H = img.M - filter.M + 1, W = img.N - filter.N + 1;
            Matrix newMatr = new Matrix(H, W);

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    newMatr.Matr[i, j] = FilterMedian(img, filter, j, i);
                }
            }

            newMatr = MathFunc.abs((newMatr * coef + dx / 255.0)-Statistic.ExpectedValue(newMatr.Spagetiz()));

            return NeuroFunc.Relu(newMatr, 1, 0);

        }

        /// <summary>
        /// Регулировка контраста и яркости
        /// </summary>
        /// <param name="img">Матрица изображения</param>
        /// <param name="coef">Контраст</param>
        /// <param name="dx">Яркость</param>
        /// <returns>Итоговое изображение</returns>
        public static Matrix Contrast(Matrix img, double coef = 1, double dx = 0)
        {
            return NeuroFunc.Relu((img * coef + dx / 255.0), 1, 0);
        }


        /// <summary>
        /// STD фильтр полутонового изображения
        /// </summary>
        /// <param name="img">Матрица изображения</param>
        /// <param name="filter">Матрица фильтра</param>
        /// <param name="coef">Коэффициент контраста</param>
        /// <param name="dx">Яркость</param>
        /// <returns>Возвращает результат фильтрации</returns>
        public static Matrix StdFilter(Matrix img, Matrix filter, double coef = 1, double dx = 0)
        {
            int H = img.M - filter.M + 1, W = img.N - filter.N + 1;
            Matrix newMatr = new Matrix(H, W);

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    newMatr.Matr[i, j] = FilterSCO(img, filter, j, i);
                }
            }

            return NeuroFunc.Relu((newMatr * coef + dx / 255.0), 1, 0);

        }


        // Элемент фильтра
        static double Filter(Matrix img, Matrix filter, int dx, int dy)
        {
            double akkum = 0;

            for (int i = 0; i < filter.M; i++)
            {
                for (int j = 0; j < filter.N; j++)
                {
                    akkum += img.Matr[dy + i, dx + j] * filter.Matr[i, j];
                }
            }

            return akkum / (filter.M * filter.N);
        }

        // Элемент медианного фильтра
        static double FilterMedian(Matrix img, Matrix filter, int dx, int dy)
        {

            List<double> ld = new List<double>();

            for (int i = 0; i < filter.M; i++)
            {
                for (int j = 0; j < filter.N; j++)
                {
                    ld.Add(img.Matr[dy + i, dx + j] * filter.Matr[i, j]);
                }
            }

            ld.Sort();

            return ld[ld.Count / 2];
        }


        // Элемент СКО фильтра
        static double FilterSCO(Matrix img, Matrix filter, int dx, int dy)
        {

            Vector vect = new Vector(filter.N * filter.M);


            for (int i = 0, k =0; i < filter.M; i++)
            {
                for (int j = 0; j < filter.N; j++)
                {
                    vect.Vecktor[k++] = img.Matr[dy + i, dx + j] * filter.Matr[i, j];
                }
            }

            return Statistic.Sco(vect);
        }




    }
}
