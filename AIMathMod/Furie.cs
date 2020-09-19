
using AI.MathMod.AdditionalFunctions;
using System;
using System.Collections.Generic;
using System.Numerics;


namespace AI.MathMod
{



    /// <summary>
    /// Преобразование Фурье
    /// </summary>
    public class Furie
    {
        /// <summary>
        /// Вектор поворота
        /// </summary>
        public ComplexVector rotateCoef { get; set; }

        private readonly Dictionary<double, Complex> dic = new Dictionary<double, Complex>();
        private Complex rot;
        /// <summary>
        /// Кол-во
        /// </summary>
        public int _n;


        /// <summary>
        /// Фурье
        /// </summary>
        public Furie(int n)
        {
            _n = Functions.NextPow2(n);
        }


        /// <summary>
        /// Быстрое Фурье
        /// </summary>
        /// <param name="inp"></param>
        /// <returns></returns>
        public ComplexVector FFT(Vector inp)
        {
            Complex[] compInp = new Complex[_n];

            for (int i = 0; i < inp.N; i++)
            {
                compInp[i] = new Complex(inp[i], 0);
            }

            return new ComplexVector(FFT(compInp));
        }

        /// <summary>
        /// Реальная часть ОБПФ
        /// </summary>
        /// <param name="cInp">Комплексный вектор</param>
        public Vector RealIFFT(ComplexVector cInp)
        {
            return IFFT(cInp).RealToVector() / _n;
        }

        /// <summary>
        /// Реальная часть БПФ(не нормировано на кол-во)
        /// </summary>
        /// <param name="cInp">Комплексный вектор</param>
        public Vector RealIFFT2(ComplexVector cInp)
        {
            return IFFT(cInp).RealToVector();
        }

        /// <summary>
        /// Выдает спектр сигнала от 0 до fd/2
        /// </summary>
        /// <param name="input">Вектор входа</param>
        /// <param name="window">Оконная ф-я</param>
        /// <returns>Спектр сигнала</returns>
        public Vector GetSpectr(Vector input, Func<int, Vector> window)
        {
            Vector vect = input * window(input.N); // Применение оконной функции
            vect = FFT(vect).MagnitudeToVector() / (_n / 2.0); // Амплитуды
            return vect.CutAndZero(_n / 2); // Половина вектора
        }

        private Complex[] FFT(Complex[] inp)
        {
            Complex[] X;
            int N = inp.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = inp[0] + inp[1];
                X[1] = inp[0] - inp[1];
            }

            else
            {


                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = inp[2 * i];
                    x_odd[i] = inp[2 * i + 1];
                }
                Complex[] X_even = FFT(x_even);
                Complex[] X_odd = FFT(x_odd);
                X = new Complex[N];

                for (int i = 0; i < N / 2; i++)
                {
                    rot = Rotate(i, N);
                    X[i] = X_even[i] + rot * X_odd[i];
                    X[i + N / 2] = X_even[i] - rot * X_odd[i];
                }
            }

            return X;
        }

        /// <summary>
        /// ОБПФ
        /// </summary>
        /// <param name="inp">Вход</param>
        public ComplexVector IFFT(ComplexVector inp)
        {
            ComplexVector cV = !inp.CutAndZero(_n); // Комплексно-сопряженный вектор
            return new ComplexVector(FFT(cV.DataInVector));
        }

        #region БПФ

        /// <summary>
        /// Вычисление поворачивающего модуля e^(-i*2*PI*k/N)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Complex Rotate(int k, int N)
        {

            if (k % N == 0)
            {
                return 1;
            }

            double arg = -2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }





        /// <summary>
        /// Возвращает спектр сигнала
        /// </summary>
        /// <param name="x">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static Complex[] fft(Complex[] x)
        {
            Complex[] X;
            int N = x.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = x[0] + x[1];
                X[1] = x[0] - x[1];
            }
            else
            {
                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = x[2 * i];
                    x_odd[i] = x[2 * i + 1];
                }
                Complex[] X_even = fft(x_even);
                Complex[] X_odd = fft(x_odd);
                X = new Complex[N];
                for (int i = 0; i < N / 2; i++)
                {
                    X[i] = X_even[i] + Rotate(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - Rotate(i, N) * X_odd[i];
                }
            }
            return X;
        }



        /// <summary>
        /// Возвращает спектр сигнала
        /// </summary>
        /// <param name="inp">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static Complex[] ifft(Complex[] inp)
        {
            ComplexVector cV = !new ComplexVector(inp); // Комплексно-сопряженный вектор
            return fft(cV.DataInVector);
        }



        /// <summary>
        /// Возвращает комплексный вектор спектра сигнала
        /// </summary>
        /// <param name="inp">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static ComplexVector fft(ComplexVector inp)
        {
            ComplexVector inpV = inp.CutAndZero(Functions.NextPow2(inp.N));
            return new ComplexVector(fft(inpV.DataInVector));
        }


        /// <summary>
        /// Быстрое преобразование Фурье(БПФ)
        /// </summary>
        /// <param name="inp">Входной вектор</param>
        public static ComplexVector fft(Vector inp)
        {
            ComplexVector cv = new ComplexVector(inp);
            return fft(cv);
        }











        /// <summary>
        /// Возвращает комплексный вектор спектра сигнала
        /// </summary>
        /// <param name="inp">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        private static ComplexVector ifft1(ComplexVector inp)
        {
            ComplexVector inpV = inp.CutAndZero(Functions.NextPow2(inp.N));
            return new ComplexVector(ifft(inpV.DataInVector));
        }

        private static ComplexVector ifft1(Vector inp)
        {
            ComplexVector cv = new ComplexVector(inp);

            return ifft(cv);
        }

        #endregion


        #region ОБПФ
        /// <summary>
        /// ОБПФ
        /// </summary>
        /// <param name="A">Входной вектор</param>
        public static ComplexVector ifft(ComplexVector A)
        {
            ComplexVector C = ifft1(A);

            return C / C.N;
        }


        /// <summary>
        /// ОБПФ
        /// </summary>
        /// <param name="A">Входной вектор</param>
        public static ComplexVector ifft(Vector A)
        {
            ComplexVector C = ifft1(A);

            return C / C.N;
        }
        #endregion





        /// <summary>
        /// Дискретное преобразование Фурье
        /// </summary>
        /// <param name="x">Входной действительный вектор</param>
        public static ComplexVector DPF(Vector x)
        {
            int N = x.N;
            ComplexVector x1 = new ComplexVector(N);
            ComplexVector Out = new ComplexVector(N);

            Complex i = new Complex(0, 1);


            for (int k = 0; k < N; k++)
            {

                for (int n = 0; n < N; n++)
                {
                    x1.DataInVector[n] = x.DataInVector[n] * Complex.Exp((-2 * Math.PI * i * k * n) / N);
                }

                Out.DataInVector[k] = Functions.Summ(x1);
            }

            return Out;
        }


        /// <summary>
        /// Дискретное преобразование Фурье
        /// </summary>
        /// <param name="x">Входной комплесный вектор</param>
        public static ComplexVector DPF(ComplexVector x)
        {
            int N = x.N;
            ComplexVector x1 = new ComplexVector(N);
            ComplexVector Out = new ComplexVector(N);

            Complex i = new Complex(0, 1);


            for (int k = 0; k < N; k++)
            {
                for (int n = 0; n < N; n++)
                {
                    x1.DataInVector[n] = x.DataInVector[n] * Complex.Exp((-2 * Math.PI * i * k * n) / N);
                }

                Out.DataInVector[k] = Functions.Summ(x1);
            }

            return Out;
        }

        /// <summary>
        /// Обратное дискретное преобразование Фурье
        /// </summary>
        /// <param name="x">Входной действительный вектор</param>
        public static ComplexVector ODPF(Vector x)
        {
            int N = x.N;
            ComplexVector x1 = new ComplexVector(N);
            ComplexVector Out = new ComplexVector(N);

            Complex i = new Complex(0, 1);


            for (int k = 0; k < N; k++)
            {

                for (int n = 0; n < N; n++)
                {
                    x1.DataInVector[n] = x.DataInVector[n] * Complex.Exp((2 * Math.PI * i * k * n) / N);
                }

                Out.DataInVector[k] = Functions.Summ(x1);
            }

            return Out / N;
        }


        /// <summary>
        /// Обратное дискретное преобразование Фурье
        /// </summary>
        /// <param name="x">Входной действительный вектор</param>
        public static ComplexVector ODPF(ComplexVector x)
        {
            int N = x.N;
            ComplexVector x1 = new ComplexVector(N);
            ComplexVector Out = new ComplexVector(N);

            Complex i = new Complex(0, 1);


            for (int k = 0; k < N; k++)
            {
                for (int n = 0; n < N; n++)
                {
                    x1.DataInVector[n] = x.DataInVector[n] * Complex.Exp((2 * Math.PI * i * k * n) / N);
                }

                Out.DataInVector[k] = Functions.Summ(x1);
            }

            return Out / N;
        }

        /// <summary>
        /// Чачтотно-временное преобразование
        /// </summary>
        /// <param name="vect">Вектор</param>
        /// <param name="lenFr">Размер фрейма</param>
        public static Matrix TimeFrTransform(Vector vect, int lenFr = 1000)
        {
            int lenTime = vect.N / lenFr;
            Vector[] vects = new Vector[lenTime];
            double[,] matr = new double[lenFr, lenTime];

            for (int i = 0; i < lenTime; i++)
            {
                vects[i] = vect.GetInterval(i * lenFr, (i + 1) * lenFr);
            }

            for (int i = 0; i < lenTime; i++)
            {
                vects[i] = Furie.fft(vects[i]).MagnitudeToVector() / lenFr;

                for (int j = 0; j < lenFr; j++)
                {
                    matr[j, i] = vects[i][j];
                }
            }

            return new Matrix(matr);
        }


    }

    /// <summary>
    /// Оконные ф-ии БПФ
    /// </summary>
    public static class WindowForFFT
    {

        /// <summary>
        /// Окно ханна дает уровень боковых лепестков -31.5 db
        /// </summary>
        /// <param name="windowSize">Размер окна</param>
        public static Vector HannWindow(int windowSize)
        {
            Vector n = Vector.Seq0(1, windowSize);
            Vector w = 0.5 * (1 - MathFunc.cos(Math.PI * 2.0 * n / (windowSize - 1)));
            return w;
        }

        /// <summary>
        /// Окно Хэмминга дает уровень боковых лепестков -42 db
        /// </summary>
        /// <param name="windowSize">Размер окна</param>
        public static Vector HammingWindow(int windowSize)
        {
            Vector n = Vector.Seq0(1, windowSize);
            Vector w = 0.53836 - 0.46164 * MathFunc.cos(Math.PI * 2.0 * n / (windowSize - 1));
            return w;
        }

        /// <summary>
        /// Прямоугольное окно дает уровень боковых лепестков -13 db
        /// </summary>
        /// <param name="windowSize">Размер окна</param>
        public static Vector RectWindow(int windowSize)
        {
            return new Vector(windowSize) + 1;
        }

        /// <summary>
        /// Окно Блэкмана дает уровень боковых лепестков -58 db
        /// </summary>
        /// <param name="windowSize">Размер окна</param>
        public static Vector BlackmanWindow(int windowSize)
        {
            Vector n = Vector.Seq0(1, windowSize);
            const double a = 0.16, a1 = 0.5;
            double a0 = (1 - a) / 2.0, a2 = a / 2.0;

            Vector cos1 = MathFunc.cos(Math.PI * 2.0 * n / (windowSize - 1)), cos2 = MathFunc.cos(Math.PI * 4.0 * n / (windowSize - 1));
            Vector w = a0 - a1 * cos1 + a2 * cos2;
            return w;
        }

        /// <summary>
        /// Окно Кайзера дает уровень боковых лепестков -90 db (Не реализован)
        /// </summary>
        /// <param name="windowSize">Размер окна</param>
        public static Vector KaiserWindow(int windowSize)
        {
            Vector n = Vector.Seq0(1, windowSize);

            return n;
        }
    }
}

