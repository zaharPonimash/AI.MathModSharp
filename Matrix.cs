/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 28.01.2016
 * Time: 13:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.Graphiks;


namespace AI.MathMod
{
	/// <summary>
	/// Класс реализующий матрицы и операции над ними
	/// </summary>
	/// 
	[Serializable]
	public class Matrix : IMathStruct
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

		public double this[int i, int j]
		{
			get{return _matr[i,j];}
			set{_matr[i,j] = value;}
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




        /// <summary>
        ///  сложение 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator + (Matrix A, Matrix B)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			if(A._m == B._m&&A._n==B._n)
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]+B._matr[i,j];
			
			else throw new ArgumentException("Размерности матриц не совпадают", "Сложение матриц");
			
			return C;
		}




        /// <summary>
        ///  сложение 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Matrix operator + (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]+k;
			
			return C;
		}



        ///
        public static Matrix operator + (double k, Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]+k;
			
			return C;
		}


        /// <summary>
        /// вычитание
        /// </summary>
        /// <param name="A"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Matrix operator - (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]-k;
			
			return C;
		}



        /// <summary>
        /// вычитание
        /// </summary>
        /// <param name="k"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix operator - (double k, Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
		
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = k-A._matr[i,j];
			
			return C;
		}




        /// <summary>
        ///  вычитание матриц
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator - (Matrix A, Matrix B)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			if(A._m == B._m&&A._n==B._n)
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]-B._matr[i,j];
			
			else throw new ArgumentException("Размерности матриц не совпадают", "Вычитание матриц");
			
			return C;
		}
		
		
		
		
		///	
				
		public static Matrix operator * (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]*k;
			
			
			return C;
		}
		
		
		
		
		
			
		///		
		public static Matrix operator / (Matrix A, double k)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]/k;
			
			
			return C;
		}



        /// <summary>
        ///  Умножение матрицы на число	
        /// </summary>
        /// <param name="k"></param>
        /// <param name="A"></param>
        /// <returns></returns>

        public static Matrix operator / (double k,Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = k/A._matr[i,j];
			
			
			return C;
		}




        /// <summary>
        /// Умножение вектора на матрицу
        /// </summary>
        /// <param name="k"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix operator * (double k, Matrix A)
		{
			Matrix C = new Matrix(A._m,A._n);
			
			
			for(int i = 0; i < A._m; i++)
				for(int j = 0; j < A._n; j++) C._matr[i,j] = A._matr[i,j]*k;
			
			
			return C;
		}














        /// <summary>
        /// Умножение вектора на матрицу
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator * (Matrix A, Vector B)
		{
			return A*B.ToMatrix();
		}


        /// <summary>
        /// Умножение вектора на матрицу
        /// </summary>
        /// <param name="B"></param>
        /// <param name="A"></param>
        /// <returns></returns>
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
		
		/// <summary>
        /// Умножение матриц
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
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
		/// Визуализация матриц
		/// </summary>
		public void Visual()
		{
			MatrixVisual mv = new MatrixVisual(this);
			mv.Show();
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
				
				for(int i = 0; i<_n; i++)
				 for(int j = 0; j<_m; j++)
					{
					vect.Vecktor[index] = Matr[j,i];
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
			
			for(int i = 0; i<_n; i++)
			{
				matr += "\n";
					for(int j = 0; j<_m; j++)
					{
						matr += "" + _matr[j,i];
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

		
		
		
		/// <summary>
		/// Разложение матрицы на столбцы
		/// </summary>
		/// <param name="matr">Матрица</param>
		/// <returns>Массив векторов</returns>
		public static Vector[] GetColumns(Matrix matr)
		{
			Vector[] columns = new Vector[matr.N];
			
			for (int i = 0; i < columns.Length; i++)
			{
				columns[i] = new Vector(matr.M);
				for (int j = 0; j <matr.M; j++)
					columns[i][j] = matr[j,i];
			}
			
			return columns;
		}
		
		
		public Matrix Round(int n)
		{
			Matrix matr = new Matrix(_m,_n);
			
			for (int i = 0; i < _m; i++) {
				for (int j = 0; j < _n; j++) {
					matr[i,j] = Math.Round(_matr[i,j], n);
				}
			}
			
			return matr;
		}
		
		
		
//-----------	Возможны ошибки ------------------------------------------------------	
		
		
		
		/// <summary>
		/// QR Разложение матрицы
		/// </summary>
		/// <param name="input">Входная матрица</param>
		/// <returns>Матрица Q и R (первая — Q, вторая R)</returns>
		public Matrix[] QRDecomposition(Matrix input)
		{
			Vector[] av = GetColumns(input);
			
			
			List<Vector> u = new List<Vector>();
			u.Add(av[0]);
			List<Vector> e = new List<Vector>();
			e.Add(u[0]/ GeomFunc.NormVect(u[0]));
			
			int len = av.Length;
			
			for (int i = 1; i < len; i++) {

				double[] projAcc = new double[len];
				for (int j = 0; j < projAcc.Length; j++) {
					projAcc[j] = 0;
				}
				for (int j = 0; j < i; j++) {
					Vector proj = GeomFunc.ProectionAtoB (av[i], e[j]);
					for (int k = 0; k < projAcc.Length; k++) {
						projAcc[k] += proj[k];
					}
				}

				Vector ui = new Vector(len);
				for (int j = 0; j < ui.N; j++) 
				{
					ui[j] = input[j,i] - projAcc[j];
				}

				u.Add(ui);
				e.Add(u[i]/GeomFunc.NormVect(u[i]));
			}
			
			
			Matrix q = new Matrix(len, len);
			for (int i = 0; i < len; i++) {
				for (int j = 0; j < len; j++) {
					q[i,j] = e[j][i];
				}
			}


			Matrix r = new Matrix(len, len);
			for (int i = 0; i < len; i++) {
				for (int j = 0; j < e.Count; j++) {
					if (i >= j) {
						r[i,j] = GeomFunc.ScalarProduct(e[j], av[i]);
					} else {
						r[i,j] = 0;
					}
				}
			}

			
			r = r.Tr();
			
			Matrix[] outpMatrixs = new Matrix[2];
			
			outpMatrixs[0] = q;
			outpMatrixs[1] = r;
			
			return outpMatrixs;
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
		/// Альтернативная матрица
		/// </summary>
		/// <param name="functions">Функции</param>
		/// <param name="values">Значения</param>
		/// <returns>Возвращает альтернативную матрицу</returns>
		public static Matrix AlternativMatrix(Func<double, double>[] functions, Vector values)
		{
			Matrix matr = new Matrix(values.N, functions.Length);
			
			for (int i = 0; i < values.N; i++)
			{
				for (int j = 0; j < functions.Length; j++) 
				{
					matr[i,j] = functions[j](values[i]);
				}	
			}
			
			return matr;
		}
		
		
		
		/// <summary>
		/// Ортогональная матрица
		/// </summary>
		/// <param name="functions">Порождающая функция</param>
		/// <param name="values">Значения</param>
		/// <returns>Возвращает альтернативную матрицу</returns>
		public static Matrix OrtogonalMatrix(Func<int, double, double> functions, Vector values, int count)
		{
			Matrix matr = new Matrix(values.N, count);
			
			for (int i = 0; i < values.N; i++)
			{
				for (int j = 0; j < count; j++) 
				{
					matr[i,j] = functions(j, values[i]);
				}	
			}
			
			return matr;
		}
		
		public double Determinant()
        {
            double result = 1.0;
            Matrix matrix = Copy();
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < M; j++)
                {
                    if (matrix[j, i] != 0)
                    {
                        matrix.Swap(i, j, 1);
                        break;
                    }
                    else if(j + 1 == M)
                    {
                        i++;
                        goto go;
                    }
                }
                Vector a = matrix.GetVector(i, 1);
                a *= 1.0 / a[i];
                for (int j = i + 1; j < M; j++)
                {
                    double c = matrix[j, i];
                    for (int k = i; k < N; k++)
                        matrix[j, k] -= a[k] * c;
                }
            go: { }
            }
            for (int i = 0; i < M; i++)
                result *= matrix[i, i];
                return result;
        }

		
		public Vector GetVector(int index, int dimension)
        {
            Vector result;
            switch (dimension)
            {
                case 0:
                    result = new Vector(M);
                    for (int i = 0; i < M; i++)
                        result[i] = Matr[i, index];
                    return result;
                case 1:
                    result = new Vector(N);
                    for (int i = 0; i < N; i++)
                        result[i] = Matr[index, i];
                    return result;
            }
            return null;
        }
		


 /// <summary>
        /// Заменяет строки/столбцы
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="dimension"></param>
        public void Swap(int i, int j, int dimension)
        {
            if (i != j)
            {
                double c;
                switch (dimension)
                {
                    case 0:
                        for (int k = 0; k < M; k++)
                        {
                            c = Matr[k, i];
                            Matr[k, i] = Matr[k, j];
                            Matr[k, j] = c;
                        }
                        break;
                    case 1:
                        for (int k = 0; k < N; k++)
                        {
                            c = Matr[i, k];
                            Matr[i, k] = Matr[j, k];
                            Matr[j, k] = c;
                        }
                        break;
                }
            }
        }
		
		public void MatrixShow()
		{
			MatrixOut mOut = new MatrixOut(this);
			mOut.Show();
		}
		
		
		/// <summary>
		/// Метод создает матрицу с коэфициентами попарной корреляции векторов
		/// </summary>
		/// <param name="vectors">Вектора</param>
		/// <returns>Корреляционная матрица</returns>
		public static Matrix CorrelationMatrixNorm(Vector[] vectors)
		{
			Matrix corelationMatrix = new Matrix(vectors.Length, vectors.Length);
			
			
			for (int i = 0; i < vectors.Length; i++) {
				for (int j = 0; j < vectors.Length; j++) {
					if(i == j) corelationMatrix[i,j] = 1;
					else
						corelationMatrix[i,j] = Statistic.CorrelationCoefficient(vectors[i], vectors[j]);
				}
			}
			
			return corelationMatrix;
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