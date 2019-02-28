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
        /// <summary>
        /// Глубина
        /// </summary>
        public int Depth;
        /// <summary>
        /// Высота
        /// </summary>
        public int Height;
        /// <summary>
        /// Значения
        /// </summary>
        public double[] DataInTensor;
        /// <summary>
        /// Ширина
        /// </summary>
        public int Width;
        Random rnd = new Random();

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
            this.DataInTensor = new double[n];

            // Нормализация веса выполняется для выравнивания дисперсии
            // выхода каждого нейрона, иначе нейроны с большим количеством
            // входов будут иметь выходы большей дисперсии
            var scale = Math.Sqrt(1.0 / (width * height * depth));

            for (var i = 0; i < n; i++)
            {
            	this.DataInTensor[i] = Statistic.Gauss(rnd)*scale;
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
            this.DataInTensor = new double[n];

            if (c != 0)
            {
                for (var i = 0; i < n; i++)
                {
                    this.DataInTensor[i] = c;
                }
            }
        }


        /// <summary>
        /// Инициализация с помощь интерфейса IList
        /// </summary>
        /// <param name="weights">Значения</param>
        public Tensor(IList<double> weights)
        {
            this.Width = 1;
            this.Height = 1;
            this.Depth = weights.Count;

            this.DataInTensor = new double[this.Depth];

            for (var i = 0; i < this.Depth; i++)
            {
                this.DataInTensor[i] = weights[i];
            }
        }

   	/// <summary>
   	/// Копирует значения
   	/// </summary>
        public Tensor Copy()
        {
            var Tensor3 = new Tensor(this.Width, this.Height, this.Depth, 0.0);
            var n = this.DataInTensor.Length;

            for (var i = 0; i < n; i++)
            {
               Tensor3.DataInTensor[i] = this.DataInTensor[i];
            }

            return Tensor3;
        }

       
	/// <summary>
	/// Сложение
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
	/// Сложение
	/// </summary>
	/// <param name="A"></param>
	/// <param name="b"></param>
	/// <returns></returns>
        public static Tensor operator+ (double b, Tensor A)
        {
            Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

            for (int i = 0; i < A.DataInTensor.Length; i++)
            {
                newTensor.DataInTensor[i] = A.DataInTensor[i] + b;
            }

            return newTensor;
        }


	/// <summary>
	/// Умножение
	/// </summary>
	/// <param name="A"></param>
	/// <param name="k"></param>
	/// <returns></returns>
		public static Tensor operator * (Tensor A, double k)
		{
			Tensor C = new Tensor(A.Width, A.Height, A.Depth);
		
			for(int i = 0; i < A.DataInTensor.Length; i++)
				 C.DataInTensor[i] = A.DataInTensor[i]*k;
			
			return C;
		}
        
		/// <summary>
		/// Вычитание
		/// </summary>
		public static Tensor operator - (Tensor A, double k)
		{
			Tensor C = new Tensor(A.Width, A.Height, A.Depth);
		
			for(int i = 0; i < A.DataInTensor.Length; i++)
				 C.DataInTensor[i] = A.DataInTensor[i]-k;
			
			return C;
		}
		
		/// <summary>
		/// Деление
		/// </summary>
		public static Tensor operator / (Tensor A, double k)
		{
			Tensor C = new Tensor(A.Width, A.Height, A.Depth);
		
			for(int i = 0; i < A.DataInTensor.Length; i++)
				 C.DataInTensor[i] = A.DataInTensor[i]/k;
			
			return C;
		}
        
	/// <summary>
	/// Выдает значение с заданной позиции
	/// </summary>
	 public double Get(int x, int y, int d)
        {
            var ix = ((Width * y) + x) * Depth + d;
            return DataInTensor[ix];
        }



	/// <summary>
	/// Устанавливает значение на заданную позицию
	/// </summary>
        public void Set(int x, int y, int d, double v)
        {
            var ix = ((Width * y) + x) *Depth + d;
            DataInTensor[ix] = v;
        }
		
		
		/// <summary>
		/// Нормализация
		/// </summary>
		public Tensor Normalise()
		{
			Vector vec = new Vector(DataInTensor);
			
			Statistic stat = new Statistic(vec);
			
			Tensor Out = (this-stat.MinValue)/(stat.MaxValue-stat.MinValue);
			
			return Out;
		}
		
		/// <summary>
		/// Преобразование в вектор
		/// </summary>
		public Vector ToVector()
		{
			return new Vector(DataInTensor);
		}
		
		/// <summary>
		/// Вычитание
		/// </summary>
       	 	public static Tensor operator- (double b, Tensor A)
        	{
           	 	Tensor newTensor = new Tensor(A.Width, A.Height, A.Depth);

           		 for (int i = 0; i < A.DataInTensor.Length; i++)
            		{
                		newTensor.DataInTensor[i] = b - A.DataInTensor[i];
            		}

            		return newTensor;
        	}

	/// <summary>
	/// Установка константы
	/// </summary>
	/// <param name="c">Константа</param>
        public void SetConst(double c)
        {
            for (var i = 0; i < this.DataInTensor.Length; i++)
            {
            	this.DataInTensor[i] += c;
            }
        }
    }
}