/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 28.06.2017
 * Время: 12:50
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;

namespace AI.MathMod
{
	/// <summary>
	/// Класс реализует работу с комплексными векторами
	/// </summary>
	[Serializable]
	public class ComplexVector : IMathStruct
	{
		Complex[] _vector;
		int _n;
		
		
		
		#region Свойства
		
		/// <summary>
		/// Массив типа Complex содержащий отсчеты вектора
		/// </summary>
		public  Complex[] Vecktor
		{
			get{return _vector;}
			set{ _vector = value; _n = _vector.Length;}
		}
		
		
		/// <summary>
		/// Размерность комплексного вектора
		/// </summary>
		public Int32 N
		{
			get{return _n;}
		}

        /// <summary>
        /// Доступ по индексу
        /// </summary>
        /// <param name="i">Индекс</param>
        /// <returns>Значение вектора</returns>
        public Complex this[int i]
        {
            get { return _vector[i]; }
            set { _vector[i] = value; }
        }
		#endregion
		
    	#region Конструкторы
        /// <summary>
        /// Создает вектор с нулями (0+0j) размерности 3
        /// </summary>
        public ComplexVector()
		{
			_vector = new Complex[3];
		}
		
		/// <summary>
		/// Создает вектор с нулями (0+0j) размерности n
		/// </summary>
		public ComplexVector(int n)
		{
			_vector = new Complex[n];
			_n = n;
		}
		
		
		/// <summary>
		/// Создает вектор на основе массива
		/// </summary>
		public ComplexVector(Complex[] vector)
		{
			_vector = vector;
			_n = _vector.Length;
		}
		
		/// <summary>
		/// Создает вектор на основе массивов действительной и мнимой части
		/// </summary>
		/// <param name="vectorReal">Действительная часть</param>
		/// <param name="vectorImg">Мнимая часть</param>
		public ComplexVector(double[] vectorReal, double[] vectorImg)
		{
			Init(vectorReal, vectorImg);
		}
		
		/// <summary>
		/// Создает вектор на основе векторов действительной и мнимой части
		/// </summary>
		/// <param name="vectorReal">Действительная часть</param>
		/// <param name="vectorImg">Мнимая часть</param>
		public ComplexVector(Vector vectorReal, Vector vectorImg)
		{
			Init(vectorReal.Vecktor, vectorImg.Vecktor);
		}
		
		/// <summary>
		/// Создает вектор на основе векторов действительной части мнимая заполнена нулями
		/// </summary>
		/// <param name="vectorReal">Реальная часть</param>
		public ComplexVector(Vector vectorReal)
		{
			Vector vectorImg = new Vector(vectorReal.N);
			Init(vectorReal.Vecktor, vectorImg.Vecktor);
		}
		
		#endregion	
		
		#region Операции

			
		void Init(double[] vectorReal, double[] vectorImg)
		{
		int n1 = vectorImg.Length;
			int n2 = vectorReal.Length;
			
			string exceptionStr = string.Format("Длины массивов не совпадают, n1 = {0}, n2 = {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Остаток от деления");
			_vector = new Complex[n1];
			
			
			for(int i = 0; i<n1; i++){
				_vector[i] = new Complex(vectorReal[i],vectorImg[i]);
			}
			
			_n = _vector.Length;
		
		}
		
		
		 /// <summary>
		/// Дополнение нулями или обрезание до нужного размера 
		/// вектора.
		/// </summary>
		/// <param name="n">Новый размер</param>
		public ComplexVector CutAndZero(int n)
		{
			Complex[] newVect = new Complex[n];
			
			if(n>_n){
				for(int i = 0; i < _n; i++) newVect[i] = _vector[i];
				for(int i = _n; i < n; i++) newVect[i] = new Complex(0,0);
			}
			else
			{
				for(int i = 0; i < n; i++) newVect[i] = _vector[i];
			}
			
			return new ComplexVector(newVect);
		}







	
		/// <summary>
		/// Поэлементное умножение
		/// </summary>
		/// <param name="A">Первый вектор</param>
		/// <param name="B">Второй</param>
		/// <returns>Результат</returns>
		public static ComplexVector operator * (ComplexVector A, ComplexVector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно перемножить вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Перемножение");
			ComplexVector C = new ComplexVector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]*B._vector[i];
			
			return C;
		}
		
		/// <summary>
		/// Поэлементное сложение
		/// </summary>
		/// <param name="A">Первый вектор</param>
		/// <param name="B">Второй</param>
		/// <returns>Результат</returns>
		public static	ComplexVector operator + (ComplexVector A, ComplexVector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно сложить вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Сложение");
			ComplexVector C = new ComplexVector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]+B._vector[i];
			return C;
		}
		
		
		
		/// <summary>
		/// Поэлементное деление
		/// </summary>
		/// <param name="A">Первый вектор</param>
		/// <param name="B">Второй</param>
		/// <returns>Результат</returns>
		public static ComplexVector operator / (ComplexVector A, ComplexVector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно деление векторов, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Деление");
			ComplexVector C = new ComplexVector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]/B._vector[i];
			return C;
		}
		
		/// <summary>
		/// Поэлементное вычитание
		/// </summary>
		/// <param name="A">Первый вектор</param>
		/// <param name="B">Второй</param>
		/// <returns>Результат</returns>
		public static ComplexVector operator - (ComplexVector A, ComplexVector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно вычесть вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Вычитание");
			ComplexVector C = new ComplexVector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]-B._vector[i];
			return C;
		}
		
		
		
		
		/// <summary>
		/// Поэлементное умножение на реальный вектор
		/// </summary>
		/// <param name="A">Первый вектор</param>
		/// <param name="B">Второй</param>
		/// <returns>Результат</returns>
		public static ComplexVector operator * (ComplexVector A, Vector B)
		{
			int n1 = A._n;
			int n2 = B.N;
			string exceptionStr = string.Format("Невозможно вычесть вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Вычитание");
			ComplexVector C = new ComplexVector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]*B.Vecktor[i];
			return C;
		}
		
		
		/// <summary>
		/// Поэлементное умножение на реальный вектор
		/// </summary>
		/// <param name="A">Первый вектор</param>
		/// <param name="B">Второй</param>
		/// <returns>Результат</returns>
		public static ComplexVector operator * (Vector B, ComplexVector A)
		{
			int n1 = A._n;
			int n2 = B.N;
			string exceptionStr = string.Format("Невозможно вычесть вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Вычитание");
			ComplexVector C = new ComplexVector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]*B.Vecktor[i];
			return C;
		}


        /// <summary>
        /// Умножение на число
        /// </summary>
        /// <param name="k">комплексное число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator * (Complex k, ComplexVector A)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k*A._vector[i];
			return C;
		}


        /// <summary>
        /// Вычитание из числа
        /// </summary>
        /// <param name="k">комплексное число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator - (Complex k, ComplexVector A)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k-A._vector[i];
			return C;
		}

        /// <summary>
        /// Вычитание числа
        /// </summary>
        /// <param name="k">комплексное число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator - (ComplexVector A,Complex k)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = A._vector[i]-k;
			return C;
		}


        /// <summary>
        /// Вычитание из числа
        /// </summary>
        /// <param name="k">реальное число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator - (double k, ComplexVector A)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k-A._vector[i];
			return C;
		}

        /// <summary>
        /// Вычитание числа
        /// </summary>
        /// <param name="k"> число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator - (ComplexVector A,double k)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = A._vector[i]-k;
			return C;
		}

        /// <summary>
        /// Сложение
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator + (Complex k, ComplexVector A)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k+A._vector[i];
			return C;
		}

        /// <summary>
        /// Сложение
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator + (ComplexVector A,Complex k)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = A._vector[i]+k;
			return C;
		}


        /// <summary>
        /// Сложение
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator + (double k, ComplexVector A)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k+A._vector[i];
			return C;
		}

        /// <summary>
        /// Сложение
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator + (ComplexVector A,double k)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = A._vector[i]-k;
			return C;
		}


        /// <summary>
        /// Отрицание
        /// </summary>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator - (ComplexVector A)
			{
				return 0.0-A;
			}


        /// <summary>
        /// Умножение
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator * (ComplexVector A, Complex k)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k*A._vector[i];
			return C;
		}



        /// <summary>
        /// Деление
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator / (Complex k, ComplexVector A)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k/A._vector[i];
			return C;
		}

        /// <summary>
        /// Деление
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator / (ComplexVector A, Complex k)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = A._vector[i]/k;
			return C;
		}





        /// <summary>
        /// Возведение в степень
        /// </summary>
        /// <param name="k">Число</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator ^ ( ComplexVector A, Complex k)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = Complex.Pow(A._vector[i], k);
			return C;
		}



        /// <summary>
        /// Комплексно сопряженный вектор
        /// </summary>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator ! ( ComplexVector A)
		{
			int n = A._n;
			ComplexVector C = new ComplexVector(n);
			for(int i = 0; i<n; i++) C._vector[i] = Complex.Conjugate(A._vector[i]);
			return C;
		}



        /// <summary>
        /// Поэлементное возведение
        /// </summary>
        /// <param name="B">Вектор степеней</param>
        /// <param name="A">Комплексный вектор</param>
        /// <returns>Комплексный вектор</returns>
        public static ComplexVector operator ^ (ComplexVector A, ComplexVector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно выполнить операцию A^B, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Степень");
			ComplexVector C = new ComplexVector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = Complex.Pow(A._vector[i],B._vector[i]);
			return C;
		}
		
		
		
		
		
				
		
		
		/*
		// Свертка
		public static Vector operator & (Vector A, Vector B)
		{
			return Convolution.DirectConvolution(A,B);
		}
		
		// Корреляция
		public static Vector operator | (Vector A, Vector B)
		{
			return Correlation.CrossCorrelation(A,B);
		}
			
		// Автокорреляция
		public static Vector operator ! (Vector A)
		{
			return Correlation.AutoCorrelation(A);
		}
		*/
		
	#endregion
		
		#region Функции
	
	
		/// <summary>
		/// Копирование вектора
		/// </summary>
		/// <returns>Возвращает копию</returns>
		public ComplexVector Copy()
		{
			ComplexVector A = new ComplexVector(_n);
			
			for(int i = 0; i<_n; i++) A._vector[i] = _vector[i];
			return A;
		}
		
		
		
		/// <summary>
		///Реверс вектора
		/// </summary>
		public ComplexVector Revers()
		{
			Complex[] newVect = new Complex[_n];
			for(int i = 0; i < _n; i++) newVect[i] = _vector[_n-i-1];
			return new ComplexVector(newVect);
		}
		
		
		
		
		
		
		/// <summary>
		/// Сдвиг последовательности на определенное число
		/// Пример: последовательность 1 2 3 сдвинута на 2
		/// это 0 0 1 2 3, на 4 это 0 0 0 0 1 2 3
		/// </summary>
		/// <param name="valueShift"> На сколько сдвинуть</param>
		/// <returns>возвращает вектор длинны N+valueShift</returns>
		public ComplexVector Shift(int valueShift)
		{
			int N = _n+valueShift;
			Complex[] newVect = new Complex[N];
			
			for(int i = 0; i<valueShift; i++) newVect[i] = new Complex(0,0);
			for(int i = valueShift; i<N; i++) newVect[i] = _vector[i-valueShift];
			
			
			return new ComplexVector(newVect);
		}
		
		
		 /// <summary>
        /// Центровка массива значений полученных при преобразовании Фурье
        /// </summary>
        public ComplexVector FurCentr()
        {
            Complex[] centr = new Complex[N];
            for (int i = 0; i < N / 2; i++)
            {
                centr[i] = this[N / 2 + i];
                centr[N / 2 + i] = this[i];
            }
            return new ComplexVector(centr);
        }
		
		
		/// <summary>
		/// Возвращает вектор мнимой части комплексного вектора
		/// </summary>
		/// <returns></returns>
		public Vector ImgToVector()
		{
			Vector A = new Vector(_n);
			
			for(int i = 0; i<_n; i++) A.Vecktor[i] = _vector[i].Imaginary;
			
			return A;
		}
		
		
		/// <summary>
		/// Возвращает вектор действительной части комплексного вектора
		/// </summary>
		/// <returns></returns>
		public Vector RealToVector()
		{
			Vector A = new Vector(_n);
			
			for(int i = 0; i<_n; i++) A.Vecktor[i] = _vector[i].Real;
			
			return A;
		}
		
		
		
		
		/// <summary>
		/// Возвращает вектор фаз комплексного вектора
		/// </summary>
		/// <returns></returns>
		public Vector PhaseToVector()
		{
			Vector A = new Vector(_n);
			
			for(int i = 0; i<_n; i++) A[i] = _vector[i].Phase;
			
			return A;
		}
		
		
		
		/// <summary>
		/// Возвращает вектор модулей комплексного вектора
		/// </summary>
		/// <returns></returns>
		public Vector MagnitudeToVector()
		{
			Vector A = new Vector(_n);
			
			for(int i = 0; i<_n; i++) A.Vecktor[i] = _vector[i].Magnitude;
			
			return A;
		}
		
		
		
		
		/// <summary>
		/// Децимация(прореживание) вектора
		/// </summary>
		/// <param name="kDecim">Коэффициент децимации</param>
		/// <returns></returns>
		public ComplexVector Decim(int kDecim)
		{
			ComplexVector C;
			
			if(_n%kDecim == 0) C = new ComplexVector(_n/kDecim);
			else C = new ComplexVector(_n/kDecim+1);
			
			int k = 0;
			
			for(int i = 0; i<_n; i+=kDecim)
			{
				C._vector[k] = _vector[i];
					k++;
			}
			
			return C;
			
		}
		
		
		
		
		/*
		
		// Сохранение вектора
		public void Save(string path)
		{
			Matrix A = this.ToMatrix();
			A.Save(path);
		}
		
		//Загрузка вектора
		public void Open(string path)
		{
			Matrix A = new Matrix();
			A.Open(path);
			this._vector = A.ToVector()._vector;
		}
		*/
		
		
		
		
		/// <summary>
		/// Интерполяция поленомом нулевого порядка
		/// </summary>
		/// <param name="kInterp">коэффициент интерполяции</param>
		/// <returns></returns>
		public ComplexVector InterpolayrZero(int kInterp)
		{
			ComplexVector C = new ComplexVector(_n*kInterp);
		
			for(int i = 0; i<C._n; i++)
			{
				C._vector[i] = _vector[i/kInterp];
			}
					
			
			return C;
		}
		
		
		
		
		
		/// <summary>
		/// Преобразование вектора в структуру типа List"Complex" 
		/// </summary>
		public List<Complex> ToList()
		{
			List<Complex> d = new List<Complex>();
			d.AddRange(_vector);
			return d;
		}
		
		
		
		
		/// <summary>
		/// Сохранение комплексного вектора
		/// </summary>
		/// <param name="path">Путь до файла</param>
		
		public void Save(string path)
		{
			try{
			
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Create, FileAccess.Write, FileShare.None))
			{
			 	binFormat.Serialize(fStream, this);
			}
			  }
			
			catch
			{
				throw new ArgumentException("Ошибка сохранения", "Сохранение");
			}
				
		}
		
		
		
		
		
		
		
		
		/// <summary>
		/// Загрузка комплексного вектора
		/// </summary>
		/// <param name="path">Путь до файла</param>		
		public void Open(string path)
		{
			
			try{
			 	
			 ComplexVector cVector;
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Open, FileAccess.Read, FileShare.None))
			{
			 	cVector =(ComplexVector)binFormat.Deserialize(fStream);
			}
			
			 _vector = cVector._vector;
			 _n = cVector._n;
			
			}
			
			catch
			{
				throw new ArgumentException("Ошибка загрузки", "Загрузка");
			}
			
		}
		#endregion	
		
	}
	
}
