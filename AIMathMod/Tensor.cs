using System;
using System.Collections.Generic;

namespace AI.MathMod
{
    /// <summary>
    ///     ������������ ������ 3-�� �����, ��� ������� ���� ��������� ����
    /// </summary>
    [Serializable]
    public class Tensor
    {
        /// <summary>
        /// �������
        /// </summary>
        public int Depth;
        /// <summary>
        /// ������
        /// </summary>
        public int Height;
        /// <summary>
        /// ��������
        /// </summary>
        public double[] DataInTensor;
        /// <summary>
        /// ������
        /// </summary>
        public int Width;
        private readonly Random rnd = new Random();

        /// <summary>
        ///     ���������� ������� ���������� �������
        /// </summary>
        /// <param name="width">������</param>
        /// <param name="height">������</param>
        /// <param name="depth">�������</param>
        public Tensor(int width, int height, int depth)
        {

            Width = width;
            Height = height;
            Depth = depth;

            //���������� ��������� � �������
            int n = width * height * depth;
            DataInTensor = new double[n];

            // ������������ ���� ����������� ��� ������������ ���������
            // ������ ������� �������, ����� ������� � ������� �����������
            // ������ ����� ����� ������ ������� ���������
            double scale = Math.Sqrt(1.0 / (width * height * depth));

            for (int i = 0; i < n; i++)
            {
                DataInTensor[i] = Statistic.Gauss(rnd) * scale;
            }
        }

        /// <summary>
        /// ������ 3-�� �����
        /// </summary>
        /// <param name="width">������</param>
        /// <param name="height">������</param>
        /// <param name="depth">�������</param>
        /// <param name="c">�������� ������� ���������������� ������</param>
        public Tensor(int width, int height, int depth, double c)
        {

            Width = width;
            Height = height;
            Depth = depth;

            int n = width * height * depth;
            DataInTensor = new double[n];

            if (c != 0)
            {
                for (int i = 0; i < n; i++)
                {
                    DataInTensor[i] = c;
                }
            }
        }


        /// <summary>
        /// ������������� � ������ ���������� IList
        /// </summary>
        /// <param name="weights">��������</param>
        public Tensor(IList<double> weights)
        {
            Width = 1;
            Height = 1;
            Depth = weights.Count;

            DataInTensor = new double[Depth];

            for (int i = 0; i < Depth; i++)
            {
                DataInTensor[i] = weights[i];
            }
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        public Tensor Copy()
        {
            Tensor Tensor3 = new Tensor(Width, Height, Depth, 0.0);
            int n = DataInTensor.Length;

            for (int i = 0; i < n; i++)
            {
                Tensor3.DataInTensor[i] = DataInTensor[i];
            }

            return Tensor3;
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Tensor operator +(Tensor A, double b)
        {
            Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.DataInTensor.Length; i++)
            {
                newTensor.DataInTensor[i] = A.DataInTensor[i] + b;
            }

            return newTensor;
        }




        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Tensor operator +(double b, Tensor A)
        {
            Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.DataInTensor.Length; i++)
            {
                newTensor.DataInTensor[i] = A.DataInTensor[i] + b;
            }

            return newTensor;
        }


        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="A"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Tensor operator *(Tensor A, double k)
        {
            Tensor C = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.DataInTensor.Length; i++)
                C.DataInTensor[i] = A.DataInTensor[i] * k;

            return C;
        }

        /// <summary>
        /// ���������
        /// </summary>
        public static Tensor operator -(Tensor A, double k)
        {
            Tensor C = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.DataInTensor.Length; i++)
                C.DataInTensor[i] = A.DataInTensor[i] - k;

            return C;
        }

        /// <summary>
        /// �������
        /// </summary>
        public static Tensor operator /(Tensor A, double k)
        {
            Tensor C = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.DataInTensor.Length; i++)
                C.DataInTensor[i] = A.DataInTensor[i] / k;

            return C;
        }

        /// <summary>
        /// ������ �������� � �������� �������
        /// </summary>
        public double Get(int x, int y, int d)
        {
            int ix = ((Width * y) + x) * Depth + d;
            return DataInTensor[ix];
        }



        /// <summary>
        /// ������������� �������� �� �������� �������
        /// </summary>
        public void Set(int x, int y, int d, double v)
        {
            int ix = ((Width * y) + x) * Depth + d;
            DataInTensor[ix] = v;
        }


        /// <summary>
        /// ������������
        /// </summary>
        public Tensor Normalise()
        {
            Vector vec = new Vector(DataInTensor);

            Statistic stat = new Statistic(vec);

            Tensor Out = (this - stat.MinValue) / (stat.MaxValue - stat.MinValue);

            return Out;
        }

        /// <summary>
        /// �������������� � ������
        /// </summary>
        public Vector ToVector()
        {
            return new Vector(DataInTensor);
        }

        /// <summary>
        /// ���������
        /// </summary>
        public static Tensor operator -(double b, Tensor A)
        {
            Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.DataInTensor.Length; i++)
            {
                newTensor.DataInTensor[i] = b - A.DataInTensor[i];
            }

            return newTensor;
        }

        /// <summary>
        /// ��������� ���������
        /// </summary>
        /// <param name="c">���������</param>
        public void SetConst(double c)
        {
            for (int i = 0; i < DataInTensor.Length; i++)
            {
                DataInTensor[i] += c;
            }
        }
    }
}