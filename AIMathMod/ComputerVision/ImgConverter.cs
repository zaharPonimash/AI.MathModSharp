using AI.MathMod.AdditionalFunctions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace AI.MathMod.ComputerVision
{
    /// <summary>
    /// Конвертирование изображений
    /// в разные математические типы
    /// и обратно
    /// </summary>
    public static class ImgConverter
    {

        /// <summary>
        /// Загрузка картинки
        /// </summary>
        /// <param name="fileName">Имя</param>
        /// <returns>изображение</returns>
        public static Bitmap LoadBitmap(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                return new Bitmap(fs);
        }

        private static unsafe byte[,,] BaseTransformBmp(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            byte[,,] res = new byte[3, height, width];
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            try
            {
                byte* curpos;
                fixed (byte* _res = res)
                {
                    byte* _r = _res, _g = _res + width * height, _b = _res + 2 * width * height;
                    for (int h = 0; h < height; h++)
                    {
                        curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                        for (int w = 0; w < width; w++)
                        {
                            *_b = *(curpos++); ++_b;
                            *_g = *(curpos++); ++_g;
                            *_r = *(curpos++); ++_r;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bd);
            }
            return res;
        }

        /// <summary>
        /// Преобразование изображения в тензор 3-го ранга(нормировка на 1)
        /// </summary>
        /// <param name="Bmp">Изображение</param>
        public static Tensor BmpToTensor(Bitmap Bmp)
        {
            Tensor Out = new Tensor(Bmp.Width, Bmp.Height, 3);

            byte[,,] b = BaseTransformBmp(Bmp);

            for (int i = 0; i < Bmp.Width; i++)
                for (int j = 0; j < Bmp.Height; j++)
                {
                    Out.Set(i, j, 0, b[0, j, i] / 255.0);
                    Out.Set(i, j, 1, b[1, j, i] / 255.0);
                    Out.Set(i, j, 2, b[2, j, i] / 255.0);
                }


            return Out;

        }

        /// <summary>
        /// Изображение в полутоновую матрицу
        /// </summary>
        /// <param name="Bmp">Изображение</param>
        public static Matrix BmpToMatr(Bitmap Bmp)
        {

            int W = Bmp.Width;
            int H = Bmp.Height; ;
            Matrix Out = new Matrix(W, H);

            byte[,,] b = BaseTransformBmp(Bmp);

            for (int i = 0; i < W; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    Out.Matr[i, j] = (b[0, j, i] + b[1, j, i] + b[2, j, i]) / 3.0;
                }
            }

            return Out / 255.0;

        }



        /// <summary>
        /// Преобразование картинки в матрицу H компонент
        /// H принадлежит интервалу [0,1]
        /// </summary>
        /// <param name="Bmp">Картинка</param>
        /// <returns></returns>
        public static Matrix BmpToHMatr(Bitmap Bmp)
        {
            int W = Bmp.Width;
            int H = Bmp.Height; ;
            Matrix Out = new Matrix(W, H);

            Tensor tensor = BmpToTensor(Bmp);

            for (int i = 0; i < W; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    Out[i, j] = HComponent(new int[]
                        {
                            (int)(tensor.Get(i,j,0)*255.0),
                            (int)(tensor.Get(i,j,1)*255.0),
                            (int)(tensor.Get(i,j,2)*255.0)
                        });
                }
            }


            return Out;
        }










        // Вычисление H
        private static double HComponent(int[] rgb)
        {
            int max = rgb.Max();
            int min = rgb.Min();
            int indexMax = -1;
            double H = 0, d = max - min;

            for (int i = 0; i < 3; i++)
            {
                if (rgb[i] == max)
                {
                    indexMax = i;
                    break;
                }
            }


            if (d == 0) H = 0;



            else if (indexMax == 0)
            {
                d = 60.0 / d;
                if (rgb[1] >= rgb[2])
                    H = d * (rgb[1] - rgb[2]);
                else
                    H = d * (rgb[1] - rgb[2]) + 360;
            }

            else if (indexMax == 1)
            {
                d = 60.0 / d;
                H = d * (rgb[2] - rgb[0]) + 120;
            }

            else
            {
                d = 60.0 / d;
                H = d * (rgb[0] - rgb[1]) + 240;
            }

            return H / 360.0;
        }

        private static int BiueInt(double intensiv)
        {
            return 120 / ((int)intensiv + 1);
        }

        private static int RedInt(double intensiv)
        {
            try
            {
                return (int)(intensiv) / 220;
            }
            catch { return 0; }
        }

        /// <summary>
        /// Визуализация матрицы
        /// </summary>
        public static Bitmap Visualiz(Matrix matr)
        {
            Bitmap bmp = new Bitmap(matr.M, matr.N);
            Color color;
            Vector a = matr.Spagetiz();
            double max = new Statistic(MathFunc.abs(a)).MaxValue;
            double k = 250.0 / max;
            double intensiv;


            for (int i = 0; i < matr.M; i++)
            {
                for (int j = 0; j < matr.N; j++)
                {
                    intensiv = Math.Abs(k * matr.Matr[i, j]);
                    try
                    {
                        color = Color.FromArgb((int)(RedInt(intensiv) * intensiv), (int)(0.2 * intensiv), (int)(BiueInt(intensiv) * intensiv));
                    }
                    catch { color = Color.Coral; }
                    bmp.SetPixel(i, j, color);
                }
            }


            return bmp;
        }



        /// <summary>
        /// Перевод матрицы в полутоновое изображение
        /// </summary>
        public static Bitmap MatrixToBitmap(Matrix matr)
        {
            Bitmap bmp = new Bitmap(matr.M, matr.N);
            Color color;
            int intensiv;


            for (int i = 0; i < matr.M; i++)
            {
                for (int j = 0; j < matr.N; j++)
                {

                    try
                    {
                        intensiv = (int)Math.Abs(255 * matr.Matr[i, j]);
                        color = Color.FromArgb(intensiv, intensiv, intensiv);
                    }
                    catch { color = Color.Coral; }
                    bmp.SetPixel(i, j, color);
                }
            }


            return bmp;
        }

        /// <summary>
        /// Тензор в картинку
        /// </summary>
        /// <param name="tensor">Тензор</param>
        /// <returns>Bitmap</returns>
        public static Bitmap TensorToBitmap(Tensor tensor)
        {
            Bitmap bmp = new Bitmap(tensor.Width, tensor.Height);
            Color color;


            for (int i = 0; i < tensor.Width; i++)
            {
                for (int j = 0; j < tensor.Height; j++)
                {

                    try
                    {
                        color = Color.FromArgb((int)(255 * tensor.Get(i, j, 0)), (int)(255 * tensor.Get(i, j, 1)), (int)(255 * tensor.Get(i, j, 2)));
                    }
                    catch { color = Color.Coral; }
                    bmp.SetPixel(i, j, color);
                }
            }


            return bmp;
        }



    }
}
