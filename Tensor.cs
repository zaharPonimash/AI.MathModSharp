using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AI.MathMod
{
    /// <summary>
    ///     Представляет тензор 3-го ранга, как базовый клас нейронной сети
    /// </summary>
    [Serializable]
    public class Tensor
    {
        
        public int Depth;
        public int Height;
        public double[] WeightGradients;
        public double[] Weights;
        public int Width;

        /// <summary>
        ///     Заполнение тензора случайными числами
        /// </summary>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <param name="depth">глубина</param>
        public Tensor(int width, int height, int depth)
        {
            
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            
            //Количество элементов в тензоре
            var n = width * height * depth;
            this.Weights = new double[n];
            this.WeightGradients = new double[n];

            // Нормализация веса выполняется для выравнивания дисперсии
            // выхода каждого нейрона, иначе нейроны с большим количеством
            // входов будут иметь выходы большей дисперсии
            var scale = Math.Sqrt(1.0 / (width * height * depth));

            for (var i = 0; i < n; i++)
            {
                this.Weights[i] = RandomUtilities.Randn(0.0, scale);
            }
        }

        /// <summary>
        /// Тензор 3-го ранга
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="depth">Глубина</param>
        /// <param name="c">Величина которой инициализируется тензор</param>
        public Tensor(int width, int height, int depth, double c)
        {
            
            this.Width = width;
            this.Height = height;
            this.Depth = depth;

            var n = width * height * depth;
            this.Weights = new double[n];
            this.WeightGradients = new double[n];

            if (c != 0)
            {
                for (var i = 0; i < n; i++)
                {
                    this.Weights[i] = c;
                }
            }
        }


        /// <summary>
        /// Инициализация с помощь интерфейса IList<double>
        /// </summary>
        /// <param name="weights">Значения</param>
        public Tensor(IList<double> weights)
        {
            this.Width = 1;
            this.Height = 1;
            this.Depth = weights.Count;

            this.Weights = new double[this.Depth];
            this.WeightGradients = new double[this.Depth];

            for (var i = 0; i < this.Depth; i++)
            {
                this.Weights[i] = weights[i];
            }
        }


        public double Get(int x, int y, int d)
        {
            var ix = ((this.Width * y) + x) * this.Depth + d;
            return this.Weights[ix];
        }




        public void Set(int x, int y, int d, double v)
        {
            var ix = ((this.Width * y) + x) * this.Depth + d;
            this.Weights[ix] = v;
        }

        public void Add(int x, int y, int d, double v)
        {
            var ix = ((this.Width * y) + x) * this.Depth + d;
            this.Weights[ix] += v;
        }

        public double GetGradient(int x, int y, int d)
        {
            var ix = ((this.Width * y) + x) * this.Depth + d;
            return this.WeightGradients[ix];
        }

        public void SetGradient(int x, int y, int d, double v)
        {
            var ix = ((this.Width * y) + x) * this.Depth + d;
            this.WeightGradients[ix] = v;
        }

        public void AddGradient(int x, int y, int d, double v)
        {
            var ix = ((this.Width * y) + x) * this.Depth + d;
            this.WeightGradients[ix] += v;
        }

        public Tensor CloneAndZero()
        {
            return new Tensor(this.Width, this.Height, this.Depth, 0.0);
        }

        public Tensor Clone()
        {
            var Tensor3 = new Tensor(this.Width, this.Height, this.Depth, 0.0);
            var n = this.Weights.Length;

            for (var i = 0; i < n; i++)
            {
               Tensor3.Weights[i] = this.Weights[i];
            }

            return Tensor3;
        }

        public void AddFrom(Tensor Tensor3)
        {
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += Tensor3.Weights[i];
            }
        }

        public void AddGradientFrom(Tensor Tensor3)
        {
            for (var i = 0; i < this.WeightGradients.Length; i++)
            {
                this.WeightGradients[i] += Tensor3.WeightGradients[i];
            }
        }

        public void AddFromScaled(Tensor Tensor3, double a)
        {
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += a * Tensor3.Weights[i];
            }
        }

        public static Tensor operator +(Tensor A, double b)
        {
            Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.Weights.Length; i++)
            {
                newTensor.Weights[i] = A.Weights[i] + b;
            }

            return newTensor;
        }

        public static Tensor operator+ (double b, Tensor A)
        {
            Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.Weights.Length; i++)
            {
                newTensor.Weights[i] = A.Weights[i] + b;
            }

            return newTensor;
        }


		
		public static Tensor operator * (Tensor A, double k)
		{
			Tensor C = new Tensor(A.Width, A.Height, A.Depth);
		
			for(int i = 0; i < A.Weights.Length; i++)
				 C.Weights[i] = A.Weights[i]*k;
			
			return C;
		}
        
		
		public static Tensor operator - (Tensor A, double k)
		{
			Tensor C = new Tensor(A.Width, A.Height, A.Depth);
		
			for(int i = 0; i < A.Weights.Length; i++)
				 C.Weights[i] = A.Weights[i]-k;
			
			return C;
		}
		
		
		public static Tensor operator / (Tensor A, double k)
		{
			Tensor C = new Tensor(A.Width, A.Height, A.Depth);
		
			for(int i = 0; i < A.Weights.Length; i++)
				 C.Weights[i] = A.Weights[i]/k;
			
			return C;
		}
        
		
		public Vector ToVector()
		{
			return new Vector(Weights);
		}
		
		
        public static Tensor operator- (double b, Tensor A)
        {
            Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.Weights.Length; i++)
            {
                newTensor.Weights[i] = b - A.Weights[i];
            }

            return newTensor;
        }


        public void SetConst(double c)
        {
            for (var i = 0; i < this.Weights.Length; i++)
            {
            	this.Weights[i] += c;
            }
        }
    }
}