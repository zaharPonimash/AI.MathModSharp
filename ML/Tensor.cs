/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 04.11.2017
 * Время: 11:28
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod;

namespace Old.AI.MathMod
{
	/// <summary>
	/// Description of Tensor.
	/// </summary>
	public class Tensor
	{
		
		#region Свойства
		
		
		public double[] tensor { get; set; }
		
		public int W{ get; set; }
		
		public int H{ get; set; }
		
		public int D{ get; set; }
		
		public int Len{ get; set; }
		
		public int Rang
		{
			get
			{
				if(W==1) return 0;
				if(H==1) return 1;
				if(D==1) return 2;
				return 3;
			}
		}
		
		public double this[int i] {
			get {
				return tensor[i];
			}
			
			set {
				tensor[i] = value;
			}
		}
		
		public double this[int i, int j, int k] {
			get {
				int index = ((W * j) + i) * D + k;
				return tensor[index];
			}
			
			set {
				int index = ((W * j) + i) * D + k;
				tensor[index] = value;
			}
		}
		
		#endregion
		
		
		#region Конструкторы
		
		public Tensor()
		{
			tensor = new double[1];
		}
		
		
		public Tensor(int w, int h, int d)
		{
			W = w;
			H = h;
			D = d;
			Len = W * H * D;
			tensor = new double[Len];
		}
		
		#endregion
		
		#region Конвертирование типов
		
		public static Tensor MatrixToTensor(Matrix matr)
		{
			Tensor ten = new Tensor(matr.M, matr.N, 1);
			for (int i = 0; i < matr.M; i++)
				for (int j = 0; j < matr.N; j++)
					ten[i, j, 0] = matr[i, j];
			return ten;
		}
		
		public static Matrix TensorToMatrix(Tensor tensor)
		{
			Matrix matr = new Matrix(tensor.W, tensor.H);
			for (int i = 0; i < matr.M; i++)
				for (int j = 0; j < matr.N; j++)
					matr[i, j] = tensor[i, j, 0];
			return matr;
		}
		
		
		public static Tensor VectorToTensor(Vector vect)
		{
			Tensor ten = new Tensor(vect.N, 1, 1);
			for (int i = 0; i < vect.N; i++)
					ten[i, 0, 0] = vect[i];
			return ten;
		}
		
		public static Vector TensorToVector(Tensor ten)
		{
			Vector vect = new Vector(ten.Len);
			
			for (int i = 0; i < vect.N; i++)
					 vect[i] = ten[i];
			return vect;
		}
		
		public Matrix ToMatrix()
		{
			return TensorToMatrix(this);
		}
		
		#endregion
		
		
		public static Tensor GetTensorGaussRandom(int w, int h, int d, double sco, double m)
		{
			Tensor ten = new Tensor(w, h, d);
			Random rnd = new Random();
			for (int i = 0; i < ten.Len; i++) {
				ten[i] = Statistic.Gauss(rnd) * sco + m;
			}
			return ten;
		}
		
		/*
		public static Tensor operator *(Tensor tensor1, Tensor tensor2)
		{
			
		}
		*/
		
		
		public static Tensor operator -(Tensor tensor1, Tensor tensor2)
		{
			Tensor ten = new Tensor(tensor1.W,tensor1.H, tensor1.D);
			
				for (int i = 0; i < tensor1.Len; i++) {
				ten[i] = tensor1[i]-tensor2[i];
				}
			return ten;
		}
		
		
		
		public static Tensor operator +(Tensor tensor1, Tensor tensor2)
		{
			Tensor ten = new Tensor(tensor1.W,tensor1.H, tensor1.D);
			
				for (int i = 0; i < tensor1.Len; i++)
							ten[i] = tensor1[i]+tensor2[i];
				return ten;
		}
		
		
	}
}
