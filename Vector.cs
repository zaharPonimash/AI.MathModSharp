/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 28.06.2017
 * Время: 12:46
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.IO;
using AI.MathMod.Graphiks;

namespace AI.MathMod
{
	/// <summary>
	/// Класс реализующий вектора и операции над ними
	/// </summary>
	[Serializable]
	public class Vector
	{
		
		
		#region поля и свойства
		double[] _vector;
		int _n;
		
		
		/// <summary>
		/// Массив типа double содержащий отсчеты вектора
		/// </summary>
		public Double[] Vecktor
		{
			get{return _vector;}
			set{ _vector = value; _n = _vector.Length;}
		}
		
		
		/// <summary>
		/// Размерность вектора
		/// </summary>
		public Int32 N
		{
			get{return _n;}
		}
		
		
		#endregion
		
		
		
		#region Конструкторы
		/// <summary>
		/// Создает вектор с нулями размерности 3
		/// </summary>
		public Vector()
		{
			_vector = new double[3];
			_n = 3;
		}
		
		
		/// <summary>
		/// Создает вектор размерности 1, со значением a
		/// </summary>
		/// <param name="a">Значение нулевой ячейки</param>
		public Vector(double a)
		{
			_vector = new double[1];
			_vector[0] = a;
			_n = 1;
		}
		
		
		
		/// <summary>
		/// Создает вектор с нулями размерности n
		/// </summary>
		public Vector(int n)
		{
			_vector = new double[n];
			_n = n;
		}
		
		
		
		/// <summary>
		/// Создает вектор на основе массива
		/// </summary>
		public Vector(double[] vector)
		{
			_vector = vector;
			_n = _vector.Length;
		}
		
		
		/// <summary>
		/// Создает вектор на основе строк, где каждая строка представляет число double
		/// </summary>
		/// <param name="strVector"></param>
		public Vector(string[] strVector)
		{
			StrInit(strVector);
		}
		
		
		/// <summary>
		/// Создает вектор на основе текстового файла, где лежат числа double
		/// </summary>
		/// <param name="textPath">Путь до файла</param>
		/// <param name="separator">разделитель, 1 заменяется на \n</param>
		public Vector(string textPath, char separator)
		{
			
			string[] strs;
			
			if (separator != '1')
			{
				strs = File.ReadAllText(textPath).Split(separator);
			}
			
			else strs = File.ReadAllLines(textPath);
			
			StrInit(strs);
		}
		
		
		
		
		
		/// <summary>
		/// Создает вектор на основе текстового файла, каждая строка представляет число double
		/// </summary>
		/// <param name="textPath">Путь до файла</param>
		public Vector(string textPath)
		{
			string[] strs;
			strs = File.ReadAllLines(textPath);
			StrInit(strs);
		}
		
		
		#endregion
		
		
		
		
		
		
		
		
	#region Операции
		
		// Поэлементное умножение
		public static Vector operator * (Vector A, Vector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно перемножить вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Перемножение");
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]*B._vector[i];
			
			return C;
		}
		
			// Поэлементное сложение
		public static	Vector operator + (Vector A, Vector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно сложить вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Сложение");
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]+B._vector[i];
			return C;
		}
		
		
		
			// Cложение c числом
		public static	Vector operator + (Vector A, double k)
		{
			int n1 = A._n;
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]+k;
			return C;
		}
		
		
			// Cложение c числом
		public static	Vector operator + (double k, Vector A)
		{
			int n1 = A._n;
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]+k;
			return C;
		}
		
		
		
	
		
		
			// вычитание из числа
		public static	Vector operator - (double k, Vector A)
		{
			int n1 = A._n;
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = k - A._vector[i];
			return C;
		}
		
		
		
			// вычитание числа
		public static	Vector operator - ( Vector A, double k)
		{
			int n1 = A._n;
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i] - k;
			return C;
		}
		
		
		//отрицательный вектор
		public static Vector operator - (Vector A)
		{
			return 0.0-A;
		}
		
		
		// Поэлементное деление
		public static Vector operator / (Vector A, Vector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно деление векторов, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Деление");
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]/B._vector[i];
			return C;
		}
		
		// Поэлементное вычитание
		public static Vector operator - (Vector A, Vector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно вычесть вектора, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Вычитание");
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]-B._vector[i];
			return C;
		}
		
		
		// Умножение на число
			public static Vector operator * (double k, Vector A)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k*A._vector[i];
			return C;
		}
			
		// Умножение на число
			public static Vector operator * (Vector A, double k)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k*A._vector[i];
			return C;
		}
			
		
			
		// Деление на число
			public static Vector operator / (double k, Vector A)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k/A._vector[i];
			return C;
		}
			
		// Деление на число
			public static Vector operator / (Vector A, double k)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = A._vector[i]/k;
			return C;
		}
				
			
			
			
			
			// Возведение вектора в степень
			public static Vector operator ^ ( Vector A, double k)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = Math.Pow(A._vector[i], k);
			return C;
		}
			
			
			
			// Возведение вектора в степень
			public static Vector operator ^ ( double k,  Vector A)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = Math.Pow(k, A._vector[i]);
			return C;
		}
			
			
			
		// Поэлементное возведение
		public static Vector operator ^ (Vector A, Vector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Невозможно выполнить операцию A^B, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Степень");
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = Math.Pow(A._vector[i],B._vector[i]);
			return C;
		}
		
		
		
			// Остаток от деления
			public static Vector operator % (Vector A, double k)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = A._vector[i]%k;
			return C;
		}
			
				// Остаток от деления
			public static Vector operator % (double k,Vector A)
		{
			int n = A._n;
			Vector C = new Vector(n);
			for(int i = 0; i<n; i++) C._vector[i] = k%A._vector[i];
			return C;
		}
		
			
			
		// Поэлементно остаток от деления
		public static Vector operator % (Vector A, Vector B)
		{
			int n1 = A._n;
			int n2 = B._n;
			string exceptionStr = string.Format("Длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Остаток от деления");
			Vector C = new Vector(n1);
			for(int i = 0; i<n1; i++) C._vector[i] = A._vector[i]%B._vector[i];
			return C;
		}	
		
		
		
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
		
		// равно ли
		public static bool operator == (Vector A, Vector B)
		{
			if(A._n != B._n) return false;
			
			bool flag = true;
			
			for(int i = 0; i<A._n; i++)
			{
				if(A._vector[i] != B._vector[i]){flag = false; break;}
			}
			
			return flag;
		}
		
		/// <summary>
		/// Проверка равенства
		/// </summary>
		/// <param name="A">Вектор 1</param>
		/// <param name="B">Вектор 2</param>
		/// <returns>Равно ли</returns>
		public static bool operator != (Vector A, Vector B)
		{
			return !(A==B);
		}
		
	#endregion
		
		
	
	/// <summary>
	/// Удаление по индексу
	/// </summary>
	/// <param name="elements">Индекс</param>
	public Vector IndexDel(int index)
	{
		List<double> lD = new List<double>();
		lD.AddRange(Copy()._vector);
		lD.RemoveAt(index);
		return ListToVector(lD);
	}
	
	/// <summary>
	/// Удаление выбранного элемента
	/// </summary>
	/// <param name="element">Элементы</param>
	public Vector ElementDel(double element)
	{
		List<double> lD = new List<double>();
		lD.AddRange(Copy()._vector);
		lD.Remove(element);
		return ListToVector(lD);
	}
	
	/// <summary>
	/// Удаление выбранных элементов
	/// </summary>
	/// <param name="elements">Элементы</param>
	public Vector ElementsDel(Vector elements)
	{
		List<double> lD = new List<double>();
		lD.AddRange(Copy()._vector);
		
		foreach (var element in elements.Vecktor) 
		{
			lD.Remove(element);	
		}
		
		return ListToVector(lD);
	}
	
	
	/// <summary>
	/// Удаление выбранных элементов
	/// </summary>
	/// <param name="elements">Элементы</param>
	public Vector ElementsDel(double[] elements)
	{
		List<double> lD = new List<double>();
		lD.AddRange(Copy()._vector);
		
		foreach (var element in elements) 
		{
			lD.Remove(element);	
		}
		
		return ListToVector(lD);
	}
	
	/// <summary>
	/// Удаление выбранных элементов
	/// </summary>
	/// <param name="elements">Элементы</param>
	public Vector ElementsDel(List<double> elements)
	{
		List<double> lD = new List<double>();
		lD.AddRange(Copy()._vector);
		
		foreach (var element in elements) 
		{
			lD.Remove(element);	
		}
		
		return ListToVector(lD);
	}
	
	
	
		void StrInit(string[] strVector)
		{
			_n = strVector.Length;
			_vector = new double[_n];
			
			for (int i = 0; i < _n; i++)
			{
				_vector[i] = Convert.ToDouble(strVector[i]);
			}
			
		}
	
		/// <summary>
		/// Сохраняет вектор как текстовый файл
		/// </summary>
		/// <param name="path">Путь до файла</param>
		public void SaveAsText(string path)
		{
			string text = ToString().Trim(' ');
			File.WriteAllText(path, text);
		}
		
		
		
		/// <summary>
		/// Вставляет значение в начало
		/// </summary>
		/// <param name="data"></param>
		/// <param name="nach"></param>
		/// <returns></returns>
		public Vector BiganData(double data, Vector nach)
		{
			int N = nach._n;
			Vector C = nach.Copy();
			C = C.Shift(1).CutAndZero(N);
			C.Vecktor[0] = data;
			return C;
		}
	
	
		/// <summary>
		/// Возвращает вектор в интервале [a;b]
		/// </summary>
		/// <param name="a">a - нижняя граница</param>
		/// <param name="b">b - верхняя граница</param>
		/// <returns>Вектор</returns>
		public Vector GetInterval(int a, int b)
		{
			return Revers().CutAndZero(_n-a).Revers().CutAndZero(b-a); 
		}
		
		
		/// <summary>
		/// Копирование вектора
		/// </summary>
		/// <returns>Возвращает копию</returns>
		public Vector Copy()
		{
			Vector A = new Vector(_n);
			
			for(int i = 0; i<_n; i++) A._vector[i] = _vector[i];
			return A;
		}
	
		/// <summary>
		/// Визуализация вектора
		/// </summary>
		public void Visual()
		{
			GraphicsView.Plot(this);
		}
		
		
		/// <summary>
		/// Визуализация вектора
		/// </summary>
		public void Visual(Vector xVector)
		{
			GraphicsView.Plot(this, xVector);
		}
		
		/// <summary>
		/// Реверс вектора
		/// </summary>
		public Vector Revers()
		{
			double[] newVect = new double[_n];
			for(int i = 0; i < _n; i++) newVect[i] = _vector[_n-i-1];
			return new Vector(newVect);
		}
		
		
		
		
		
		
		
		/// <summary>
		/// Дополнение нулями или обрезание до нужного размера 
		/// вектора.
		/// </summary>
		/// <param name="n">Новый размер</param>
		public Vector CutAndZero(int n)
		{
			double[] newVect = new double[n];
			
			if(n>_n){
				for(int i = 0; i < _n; i++) newVect[i] = _vector[i];
				for(int i = _n; i < n; i++) newVect[i] = 0;
			}
			else
			{
				for(int i = 0; i < n; i++) newVect[i] = _vector[i];
			}
			
			return new Vector(newVect);
		}
		
		
		
		
		
		/// <summary>
		/// Сдвиг последовательности на определенное число
		/// Пример: последовательность 1 2 3 сдвинута на 2
		/// это 0 0 1 2 3, на 4 это 0 0 0 0 1 2 3
		/// </summary>
		/// <param name="valueShift"> На сколько сдвинуть</param>
		/// <returns>возвращает вектор длинны N+valueShift</returns>
		public Vector Shift(int valueShift)
		{
			int N = _n+valueShift;
			double[] newVect = new double[N];
			
			for(int i = 0; i<valueShift; i++) newVect[i] = 0.0;
			for(int i = valueShift; i<N; i++) newVect[i] = _vector[i-valueShift];
			
			
			return new Vector(newVect);
		}
		
		
		
		
		
		/// <summary>
		/// Преобразование вектора в матрицу
		/// </summary>
		public Matrix ToMatrix()
		{
			//if(_n>=32760)throw new ArgumentException("размерность не позволяет преобразовать вектор в матрицу", "Преобразование");
			double[,] matrix =  new double[1,_n];
			for(int i = 0; i < _n; i++) matrix[0,i] = _vector[i];
			return new Matrix(matrix);
		}
		
		
		
		
		/// <summary>
		/// Децимация(прореживание) вектора
		/// </summary>
		/// <param name="kDecim">Коэффициент децимации</param>
		/// <returns></returns>
		public Vector Decim(int kDecim)
		{
			Vector C;
			
			if(_n%kDecim == 0) C = new Vector(_n/kDecim);
			else C = new Vector(_n/kDecim+1);
			
			int k = 0;
			
			for(int i = 0; i<_n; i+=kDecim)
			{
				C._vector[k] = _vector[i];
					k++;
			}
			
			return C;
			
		}
		
		
		
		
		
		/// <summary>
		/// Интерполяция поленомом нулевого порядка
		/// </summary>
		/// <param name="kInterp">коэффициент интерполяции</param>
		/// <returns></returns>
		public Vector InterpolayrZero(int kInterp)
		{
			Vector C = new Vector(_n*kInterp);
		
			for(int i = 0; i<C._n; i++)
			{
				C._vector[i] = _vector[i/kInterp];
			}
					
			
			return C;
		}
		
		
		
		
		
		
		
		/// <summary>
		/// Сохранение вектора
		/// </summary>
		/// <param name="path">Путь</param>
		public void Save(string path)
		{
			Matrix A = this.ToMatrix();
			A.Save(path);
		}
		
		/// <summary>
		/// Загрузка вектора
		/// </summary>
		/// <param name="path">Путь</param>
		public void Open(string path)
		{
			Matrix A = new Matrix();
			A.Open(path);
			this._vector = A.ToVector()._vector;
		}
		
		
		
		
		/// <summary>
		/// Преобразование вектора в структуру типа List"double" 
		/// </summary>
		public List<double> ToList()
		{
			List<double> d = new List<double>();
			d.AddRange(_vector);
			return d;
		}
		
		
		/// <summary>
		/// Преобразование List в вектор
		/// </summary>
		/// <returns></returns>
		public static Vector ListToVector(List<double> list)
		{
			return new Vector(list.ToArray());
		}
		
		/// <summary>
		/// Вставляет отсчеты второго вектора, после отсчетов первого
		/// </summary>
		/// <param name="nach">куда вставлять</param>
		/// <param name="dop">что вставлять</param>
		/// <returns>вектор размерности nach.N+dop.N</returns>
		public static Vector AddVector(Vector nach, Vector dop)
		{
			List<double> list = nach.ToList();
			list.AddRange(dop.Vecktor);
				
			return new Vector(list.ToArray());
		}
		
		
	  	/// <summary>
		/// Выводит значение элементов вектора в виде строки
		/// </summary>
		public override string ToString()
		{
			string str = "";
			foreach(double i in _vector) str += " " + i;
			return str;
		}
		
	}
	
}
