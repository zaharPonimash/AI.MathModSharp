using AI.MathMod.AdditionalFunctions;
using System.Collections.Generic;

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
        /// Контрастирование
        /// </summary>
        /// <param name="img"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Matrix ContrastFilter(Matrix img, int x, int y)
        {
            int H = img.M - x, W = img.N - y;
            Matrix newMatr = new Matrix(img.M, img.N);

            for (int i = 0; i < H; i += x)
            {
                for (int j = 0; j < W; j += y)
                {
                    for (int k = 0; k < x; k++)
                    {
                        for (int z = 0; z < y; z++)
                        {
                            newMatr.Matr[z + i, k + j] = FilterContrast(img, x, y, j, i)[z, k];
                        }
                    }
                }
            }

            return newMatr;

        }

        /// <summary>
        /// Алгоритм локального контрастирования
        /// </summary>
        /// <param name="img">Изображение</param>
        /// <returns></returns>
        public static Matrix FC(Matrix img)
        {
            Matrix newMatr = new Matrix(img.M, img.N);

            for (int i = 1; i < 5; i++)
            {
                newMatr += ContrastFilter(img, 4 * (i + 6), 4 * (i + 6));
            }

            newMatr += FilterContrast(img, img.N, img.M, 0, 0);

            return newMatr / 5;
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

            newMatr = MathFunc.abs((newMatr * coef + dx / 255.0) - Statistic.ExpectedValue(newMatr.Spagetiz()));

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
        private static double Filter(Matrix img, Matrix filter, int dx, int dy)
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
        private static double FilterMedian(Matrix img, Matrix filter, int dx, int dy)
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
        private static double FilterSCO(Matrix img, Matrix filter, int dx, int dy)
        {

            Vector vect = new Vector(filter.N * filter.M);


            for (int i = 0, k = 0; i < filter.M; i++)
            {
                for (int j = 0; j < filter.N; j++)
                {
                    vect.DataInVector[k++] = img.Matr[dy + i, dx + j] * filter.Matr[i, j];
                }
            }

            return Statistic.Std(vect);
        }

        //Элемент контрасного фильтра
        private static Matrix FilterContrast(Matrix img, int x, int y, int dx, int dy)
        {

            Vector vect;
            Matrix matr = new Matrix(y, x);

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    matr[i, j] = img.Matr[dy + i, dx + j];
                }
            }


            matr = MathFunc.lg(matr * 255 + 1);
            vect = matr.Spagetiz();
            double cko = 3 * Statistic.Std(vect), m = Statistic.ExpectedValue(vect);

            return NeuroFunc.Sigmoid((matr - m) / (cko + 0.01), 3);
        }






    }
}
