/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 28.01.2016
 * Time: 13:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace AI.MathMod
{
	/// <summary>
	/// Класс реализующий матрицы и операции над ними
	/// </summary>
	/// 
	[Serializable]
	public class Matrix
	{
		#region Поля
		double[,] _matr; // Матрица
		[NonSerialized]
		int _m = 3; // колво строк
		[NonSerialized]
		int _n = 3; // колво столбцов
		#endregion
		#region Свойства	
			
		// Свойства	
		/// <summary>
		/// Массив значений в матрице
		/// </summary>
		public Double[ , ] Matr
		{
			get{return _matr;}
			set{ _matr = value; GetMN();}
		}
		
		/// <summary>
		/// Количество строк
		/// </summary>
		public Int32 M
		{
			get{return _m;}
		}
		
		/// <summary>
		/// Количество столбцов
		/// </summary>
		public Int32 N
		{
			get{return _n;}
		}		
			
#endregion
		#region Конструкторы
	
			
	 
		/// <summary>
		/// Создает матрицу со всеми нулями размерности 3х3
		/// </summary>
		public Matrix()
		{
			_matr = new double[3,3];
		}
		
		
		
		
		/// <summary>
		/// Создает матрицу на основе двумерного массива
		/// </summary>
		public Matrix(double[,] matr)
		{
			_matr = matr;
			GetMN();
		}
		
		
		
		
		/// <summary>
		/// Создает матрицу со всеми нулями размерности MxN
		/// </summary>
		public Matrix(int m, int n)
		{
			_m =  m;
			_n =  n;
			_matr = new double[m,n];
		}
		
		
#endregion		
		#region ОПЕРАЦИИ 
		
		
		
		
		// сложение матриц
		public static Matrix operator + (Matrix A, Matrix B)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			if(A._m == B._m&&A._n==B._n)
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]+B._matr[i,j];
			
			else throw new ArgumentException("Размерности матриц не совпадают", "Сложение матриц");
			
			return C;
		}
		
		
		
			
		// сложение 
		public static Matrix operator + (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]+k;
			
			return C;
		}
		
		
		
		// сложение 
		public static Matrix operator + (double k, Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]+k;
			
			return C;
		}
		
		
		// вычитание
		public static Matrix operator - (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]-k;
			
			return C;
		}
		
		
		
		// вычитание
		public static Matrix operator - (double k, Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = k-A._matr[i,j];
			
			return C;
		}
		
		
		
			
		// вычитание матриц
		public static Matrix operator - (Matrix A, Matrix B)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			if(A._m == B._m&&A._n==B._n)
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]-B._matr[i,j];
			
			else throw new ArgumentException("Размерности матриц не совпадают", "Вычитание матриц");
			
			return C;
		}
		
		
		
		
		// Умножение матрицы на число	
				
		public static Matrix operator * (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]*k;
			
			
			return C;
		}
		
		
		
		
		// Умножение матрицы на число
				
		public static Matrix operator * (double k, Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]*k;
			
			
			return C;
		}
		
		
		
		
		
		
		
		// Деление матрицы на число	
				
		public static Matrix operator / (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]/k;
			
			
			return C;
		}
		
		
		
		
		
		// Деление матрицы на число
				
		public static Matrix operator / (double k, Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]/k;
			
			
			return C;
		}
		
		
		// Умножение матрицы на вектор
		public static Matrix operator * (Matrix A, Vector B)
		{
			return A*B.ToMatrix();
		}
			
		
		//Умножение вектора на матрицу
		public static Vector operator *(Vector B, Matrix A)
		{
			return (B.ToMatrix()*A).ToVector();
		}
		
		
		
		
		
			// Умножение матрицы на вектор
	/*	public static Matrix operator * (Matrix A, ComplexVector B)
		{
			return A*B.ToMatrix();
		}
			
		
		//Умножение вектора на матрицу
		public static Vector operator *(ComplexVector B, Matrix A)
		{
			return (B.ToMatrix()*A).ToVector();
		}
		*/
		
		// Умножение матриц
		public static Matrix operator * (Matrix A, Matrix B)
		{
			Matrix C = new Matrix(A._m,B._n);
			
			int n = A._n;
			
			
			double[] umn = new double[n];
			
			if(A._n == B._m)
			for(int i = 0; i < A._m; i++)
					for(int j = 0; j < B._n; j++){	
				for(int k = 0; k<n;k++) C._matr[i,j] += A._matr[i,k]*B._matr[k,j];
			}
					
			
			else throw new ArgumentException("Матрицы не возможно умножить", "Умножение матриц");
			
			return C;
		}
		
		
		
#endregion				
		#region Определитель
		
		
		
		double[,] GetMinor(double[,] matrix, int n)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < n; j++)
                    result[i - 1, j] = matrix[i, j];
                for (int j = n + 1; j < matrix.GetLength(0); j++)
                    result[i - 1, j - 1] = matrix[i, j];
            }
            return result;
        }
		
		
		
		
		// рассчет определителя
		 double DetermN(double[,] matrix)
        {
            if (matrix.Length == 4)
            {
                return matrix[0, 0]*matrix[1, 1] - matrix[0, 1]*matrix[1, 0];
            }
            
            
            
            double sign = 1, result = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                double[,] minor = GetMinor(matrix, i);
                result += sign*matrix[0, i]*DetermN(minor);
                sign = -sign;
            }
            return result;
        }
		
		
		
		
		/// <summary>
		/// Вычисляет определитель матрицы
		/// </summary>
		public double Determ()
		{
			if(_matr.GetLength(0) != _matr.GetLength(1))
				throw new InvalidOperationException("Матрица не квадратная");
			
			return DetermN(_matr);
		}
		
		
		
		#endregion		
		#region Функции
		
		
		// получение данных о размерности матрицы
		void GetMN()
		{
			_m = _matr.GetLength(0);
			_n = _matr.GetLength(1);
		}
		
		/// <summary>
		///  Преобразование матрицы в вектор
		/// </summary>
		public Vector ToVector()
		{
			if(_m != 1)throw new ArgumentException("Невозможно преобразовать матрицу в вектор", "Преобразование");
			double[] vector = new double[_n];
			
			for(int i = 0; i<_n; i++) vector[i] = _matr[0,i];
			
			return new Vector(vector);
		}
		
		
		/// <summary>
		/// Транспонирование матрицы
		/// </summary>
		/// <returns>Возвращает транспонированную матрицу</returns>
		public Matrix Tr()
		{
			
			double[,] T = new double[_n,_m];
			
			for(int i = 0; i<_m; i++)
					for(int j = 0; j<_n; j++)
					{
						T[j,i]= _matr[i,j];
					}
					
			
			
			return new Matrix(T);
		}
		
		
		
		
		
		
		/// <summary>
		/// Возведение матрицы в степень 
		/// путем матричного умножения на саму себя
		/// </summary>
		/// <param name="A">Входная матрица</param>
		/// <param name="stepen">Степень</param>
			static	public Matrix Pow(Matrix A, int stepen)
				{
					Matrix B = A.Copy();
					
					for(int i = 1; i<stepen; i++)B *= A;
					
					return B;
				}
		
		
		
			/// <summary>
			/// Копирование матрицы
			/// </summary>
			/// <returns>Возвращает копию</returns>
			public Matrix Copy()
			{
				Matrix B = new Matrix(_m,_n);
				for(int i = 0; i<_m; i++)
					for(int j = 0; j<_n; j++)
					{
						B.Matr[i,j]= _matr[i,j];
					}
				return B;
			}
		
		
		
		
		
		
		
		
		
		
		
		
		
		/// <summary>
		/// Вытягивает матрицу в вектор
		/// </summary>
			public Vector Spagetiz()
			{
				Vector vect = new Vector(_m*_n);
				int index = 0;
				
				for(int i = 0; i<_m; i++)
				 for(int j = 0; j<_n; j++)
					{
					vect.Vecktor[index] = Matr[i,j];
					index++;
				    }
				
				return vect;
			}
		
		
		
		/// <summary>
		/// Выводит значение элементов матрицы в виде текста
		/// </summary>
		public override string ToString()
		{
			string matr = string.Empty;
			
			for(int i = 0; i<_m; i++)
			{
				matr += "\n\n";
					for(int j = 0; j<_n; j++)
					{
						matr += "\t" + _matr[i,j];
					}
					
			}
		
			
			return matr;
		}
		
		
		/// <summary>
		/// Выводит значение элементов матрицы в виде текста
		/// </summary>
		/// <param name="sep">Разделитель горизонтальный</param>
		/// <returns></returns>
		public string ToString(string sep)
		{
			string matr = string.Empty;
			
			for(int i = 0; i<_m; i++)
			{
				matr += "\n";
					for(int j = 0; j<_n; j++)
					{
						matr += sep + _matr[i,j];
					}
					
			}
		
			
			return matr;
		}

		
// -----------------------------------------------------------------

#endregion
        #region Сериализация

		/// <summary>
		/// Сохранение матрицы
		/// </summary>
		/// <param name="path">Путь до файла</param>
		public void Save(string path)
		{
			try{
			MatrixSerrial matrix = new MatrixSerrial();
			matrix._matr = _matr;
			 
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Create, FileAccess.Write, FileShare.None))
			{
			 	binFormat.Serialize(fStream, matrix);
			}
			  }
			
			catch
			{
				throw new ArgumentException("Ошибка сохранения", "Сохранение");
			}
				
		}
				
		
		
		
		
		
		/// <summary>
		/// Загрузка матрицы
		/// </summary>
		/// <param name="path">Путь до файла</param>		
		public void Open(string path)
		{
			
			try{
			 	
			 MatrixSerrial matrix;
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Open, FileAccess.Read, FileShare.None))
			{
			 	matrix =(MatrixSerrial)binFormat.Deserialize(fStream);
			}
			
			 _matr = matrix._matr;
			 
				GetMN();
			}
			
			catch
			{
				throw new ArgumentException("Ошибка загрузки", "Загрузка");
			}
			
		}
		
		
		
		
		
		#endregion
		
	
		
	}

	
	
			// Структура для сериализации матрицы
			[Serializable]
			struct MatrixSerrial
			{
				public double[,] _matr;
			}
	
}