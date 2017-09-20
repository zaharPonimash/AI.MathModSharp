using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        unsafe static byte[,,] BaseTransformBmp(Bitmap bmp)
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
        public static Tensor3 BmpToTensor(Bitmap Bmp)
        {
            Tensor3 Out = new Tensor3(Bmp.Width, Bmp.Height, 3);

            byte[,,] b = BaseTransformBmp(Bmp);

            for (int i = 0; i < Bmp.Width; i++)
                for (int j = 0; j < Bmp.Height; j++)
                {
                    Out.Set(i, j, 0, b[0, i, j] / 255.0);
                    Out.Set(i, j, 1, b[1, i, j] / 255.0);
                    Out.Set(i, j, 2, b[2, i, j] / 255.0);
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
            int H = Bmp.Height;;
            Matrix Out = new Matrix(W, H);

            byte[,,] b = BaseTransformBmp(Bmp);

            for (int i = 0; i < W; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    Out.Matr[i, j] = (double)(b[0, j, i] + b[1, j, i] + b[2, j, i]) / 3.0;
                }
            }

            return Out / 255.0;

        }

        




    }
}
