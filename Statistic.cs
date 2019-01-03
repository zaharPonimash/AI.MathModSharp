/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 03.02.2016
 * Time: 22:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AI.MathMod.AdditionalFunctions;


namespace AI.MathMod
{
	/// <summary>
	/// Класс содержит методы для статистического анализа.
	/// А так же генераторы псевдо случайных чисел
	/// </summary>
	public class Statistic
	{
		
		
		Vector _vector;
		double _expectedValue,
				_dispers,
				_std, 
				_max,
				_min;
		int _n;
		
		
		static Random _rand = new Random();
		
				
				/// <summary>
				/// Среднеквадратичное отклонение
				/// </summary>
				public double SCO
				{
					get{return _std;}
				}
				
				/// <summary>
				/// Минимальное значение в массиве
				/// </summary>
				public double MinValue
				{
					get{return _min;}
				}
				
				/// <summary>
				/// Максимальное значение в массиве
				/// </summary>
				public double MaxValue
				{
					get{return _max;}
				}
		
				
				/// <summary>
				/// Дисперсия
				/// </summary>
				public double Dispersia
				{
					get{return _dispers;}
				}
				
				
				/// <summary>
				/// Математическое ожидание
				/// </summary>
				public double Expected
				{
					get{return _expectedValue;}
				}
		
		
				
		// Конструктор
		/// <summary>
		/// Создает объек класса Statistic, принимает вектор входных значений случайной переменной
		/// </summary>
		/// <param name="A">Вектор значений</param>
		public Statistic(Vector A)
		{
			_vector = A;
			_n = _vector.N;
			ExpectedValue();
			Dispers();
			Std();
			MaxMinValue();
		}
		
		
		
		
		/// <summary>
		/// Математическое ожидание
		/// </summary>
		/// <returns></returns>
		void ExpectedValue()
		{
			_expectedValue = Functions.Summ(_vector)/_n;
		}
		
		
	
		
		/// <summary>
		/// Дисперсия
		/// </summary>
		void Dispers()
		{
			_dispers = Functions.Summ((_vector - _expectedValue)^2)/(_n-1);
		}
		
		
		/// <summary>
		/// СКО
		/// </summary>
		void Std()
		{
			_std = Math.Sqrt(_dispers);
		}
		
		/// <summary>
		/// Дисперсия вектора
		/// </summary>
		/// <param name="vector">Входной вектор</param>
		/// <returns></returns>
		public static double Dispers(Vector vector)
		{
				double dispers, eV = ExpectedValue(vector);
				dispers = Functions.Summ((vector-eV)^2)/(vector.N - 1);
				return dispers;
		}
		
		
		/// <summary>
		/// Среднеквадратичное отклонение
		/// </summary>
		/// <param name="vector">Входной вектор</param>
		/// <returns></returns>
		public static double Std(Vector vector)
		{
			return Math.Sqrt(Dispers(vector));
		}
		
		
		
		/// <summary>
		/// Характеристическая функция
		/// </summary>
		/// <returns>Возвращает вектор отсчетов</returns>
		public Vector CharacteristicFunction()
		{
			double[] img = new double[_n];
			
			ComplexVector fnc = new ComplexVector(_vector.Vecktor, img);
			 
			return Furie.fft(fnc).MagnitudeToVector();
		}
		
		
		/// <summary>
		/// Характеристическая функция
		/// </summary>
		/// <returns>Возвращает вектор отсчетов</returns>
		public Statistic CharacteristicFunc()
		{
			double[] img = new double[_n];
			
			ComplexVector fnc = new ComplexVector(_vector.Vecktor, img);
			 
			return new Statistic(Furie.fft(fnc).MagnitudeToVector());
		}
		
		
		
		/// <summary>
		/// Генератор случайных чисел с равномерным распределением
		/// </summary>
		/// <param name="n">Длинна вектора</param>
		/// <returns>Возвращает вектор случайных чисел</returns>
		public static Vector rand(int n)
		{
			Random A = new Random();
			Vector vect = new Vector(n);
			for(int i = 0; i<n; i++) vect.Vecktor[i] = A.NextDouble();
			return vect;
		}
		
		
		
		
		
		/// <summary>
		/// Гауссовское распределение
		/// </summary>
		/// <returns>Возвращает норм. распред величину СКО = 1, M = 0</returns>
		static public double  Gauss(Random A)
		{
			double a =	2*A.NextDouble()-1,
			b = 2*A.NextDouble()-1,
			s = a*a+b*b;
			
			if(a==0&&b==0)
			{
				a = 0.000001;			
				s = a*a+b*b;
			}
			
			return b*Math.Sqrt(Math.Abs(-2*Math.Log(s)/s));
		}
		
		
		
		
		/// <summary>
		/// Генератор случайных чисел с нормальным распределением
		/// </summary>
		/// <param name="n">Длинна вектора</param>
		/// <returns>Возвращает вектор случайных чисел</returns>
		public static Vector randNorm(int n)
		{
			Random A = new Random();
			Vector vect = new Vector(n);
			for(int i = 0; i<n; i++) vect.Vecktor[i] = Gauss(A);
			return vect;
		}
		
		
		
		/// <summary>
		/// Генератор случайных чисел с нормальным распределением
		/// </summary>
		/// <param name="n">Длинна вектора</param>
		/// <param name="rnd">Генератор случайных чисел</param>
		/// <returns>Возвращает вектор случайных чисел</returns>
		public static Vector randNorm(int n, Random rnd)
		{
			Vector vect = new Vector(n);
			for(int i = 0; i<n; i++) vect.Vecktor[i] = Gauss(rnd);
			return vect;
		}
		
		
		
		
		// Минимальное и макимальное значения
		void MaxMinValue()
		{
			_max = _vector.Vecktor[0];
			_min = _vector.Vecktor[0];
			for(int i = 1; i<_n; i++)
			{
				if(_vector.Vecktor[i]> _max) _max = _vector.Vecktor[i];
				if(_vector.Vecktor[i]< _min) _min = _vector.Vecktor[i];
			}
			
			
		}
		
		
		
		
		/// <summary>
		/// Cоздает матрицу с равномерно распределенными значениями
		/// размерности m на n
		/// </summary>
		/// <param name="m">Количество строк</param>
		/// <param name="n">Количество столбцов</param>
		public static Matrix rand(int m, int n)
		{
			Random rn = new Random();
			Matrix C = new Matrix(m,n);
			
			for(int i = 0; i < m; i++)
				for(int j = 0; j < n; j++) C.Matr[i,j] = rn.NextDouble();
	
			return C;
		}
		
		
	
		/// <summary>
		/// Максимальное значение вектора
		/// </summary>
		/// <param name="vect">Вектор</param>
		public static double MaximalValue(Vector vect)
		{
			double max = vect.Vecktor[0];
			
			for(int i = 1; i<vect.N; i++)
			{
				if(vect.Vecktor[i]> max) max = vect.Vecktor[i];
			}
			
			return max;
		}
		
		
		/// <summary>
		/// Минимальное значение вектора
		/// </summary>
		/// <param name="vect">Вектор</param>
		public static double MinimalValue(Vector vect)
		{
			
			double min = vect.Vecktor[0];
			
			for(int i = 1; i<vect.N; i++)
			{
				if(vect.Vecktor[i]< min) min = vect.Vecktor[i];
			}
			
			return min;
		}
		
		
		
		
		
		/// <summary>
		/// Cоздает матрицу с равномерно распределенными значениями
		/// размерности n на n
		/// </summary>
		public static Matrix rand(short n)
		{
			Random rn = new Random();
			Matrix C = new Matrix(n,n);
			
			for(int i = 0; i < n; i++)
				for(int j = 0; j < n; j++) C.Matr[i,j] = rn.NextDouble();
	
			return C;
		}
		
			/// <summary>
		/// Математическое ожидание
		/// </summary>
		/// <param name="vector">вектор, содержащий отсчеты случайной величины</param>
		/// <returns></returns>
		public static double ExpectedValue(Vector vector)
		{
				return Functions.Summ(vector)/vector.N;
		}
		
		
		/// <summary>
		/// Cоздает матрицу с нормально распределенными значениями
		/// размерности m на n
		/// </summary>
		/// <param name="m">Количество строк</param>
		/// <param name="n">Количество столбцов</param>
		public static Matrix randNorm(int m, int n)
		{
			Random rn = new Random();
			Matrix C = new Matrix(m,n);
			
			for(int i = 0; i < m; i++)
				for(int j = 0; j < n; j++) C.Matr[i,j] = Gauss(rn);
	
			return C;
		}
		
		
		/// <summary>
		/// Cоздает матрицу с нормально распределенными значениями
		/// размерности m на n
		/// </summary>
		/// <param name="m">Количество строк</param>
		/// <param name="n">Количество столбцов</param>
		/// <param name="rnd">Генератор случ чисел</param>
		public static Matrix randNorm(int m, int n, Random rnd)
		{
			Matrix C = new Matrix(m,n);
			
			for(int i = 0; i < m; i++)
				for(int j = 0; j < n; j++) C.Matr[i,j] = Gauss(rnd);
	
			return C;
		}
		
		
		
		
		/// <summary>
		/// Cоздает матрицу с нормально распределенными значениями
		/// размерности n на n
		/// </summary>
		public static Matrix randNorm(short n)
		{
			Random rn = new Random();
			Matrix C = new Matrix(n,n);
			
			for(int i = 0; i < n; i++)
				for(int j = 0; j < n; j++) C.Matr[i,j] = rn.NextDouble();
	
			return C;
		}
		
		/// <summary>
		/// Генератор случайных чисел с нормальным распределением
		/// </summary>
		/// <returns>Возвращает случайные числа</returns>
		public double randNorm()
		{
			return Gauss(_rand);
		}
		
		
		
		
		/// <summary>
		/// Строит гистограмму
		/// </summary>
		/// <param name="nRazr">Количество разрядов гистограммы</param>
		/// <returns>возращает вектор длинной nRazr, содержащий отсчеты для построения гистограммы</returns>
		public Histogramma Histogramm(int nRazr)
		{
			double shag = (_max-_min)/nRazr;
			Vector histogr = new Vector(nRazr), x = new Vector(nRazr);
			
			int ht = 0; // высота столба
			
			for(int i = 0; i< nRazr; i++){
				ht = 0;
				foreach(double element in _vector.Vecktor)
				if( element >= (_min + i*shag) && element < _min + (i+1)*shag)// попадение в интервал					
				ht++;
				x.Vecktor[i]=((_min + i*shag)+_min + (i+1)*shag)/2.0;
				histogr.Vecktor[i] = ht;
			}
			
			Histogramma A = new Histogramma();
			A.X = x;
			A.Y = histogr/_vector.N;
			return A;
		}
		
		
		
		
		
		/// <summary>
		/// Начальный момент
		/// </summary>
		/// <param name="n">порядок момента 1,2,3...</param>
		/// <returns>Возвращает число типа Double</returns>
		public double InitialMoment(int n)
		{
			return ExpectedValue(_vector^n);
		}
		
		
		
		/// <summary>
		/// Центральный момент
		/// </summary>
		/// <param name="n">порядок момента 1,2,3...</param>
		/// <returns>Возвращает число типа Double</returns>
		public double CentrMoment(int n)
		{
			return ExpectedValue((_vector-_expectedValue)^n);
		}
		
		/// <summary>
		/// Асимметрия распределения
		/// </summary>
		/// <returns>Возвращает коэффициент асимметрии, число типа Double</returns>
		public double Asymmetry()
		{
			return CentrMoment(3)/(_std*_std*_std);
		}
		
		/// <summary>
		/// Эксцесс, "крутость" распределения
		/// </summary>
		/// <returns>Возвращает коэффициент эксцесса, число типа Double</returns>
		public double Excess()
		{
			return CentrMoment(4)/(_std*_std*_std*_std)-3;
		}
		
		
		/// <summary>
		/// Ковариация(корреляционный момент, линейная зависимость) двух векторов,
		/// длины векторов должны быть равны
		/// </summary>
		/// <param name="X">первый вектор</param>
		/// <param name="Y">второй вектор</param>
		/// <returns>Возвращает число типа Double</returns>
		public static double Cov(Vector X, Vector Y)
		{
			int n1 = X.N;
			int n2 = Y.N;
			string exceptionStr = string.Format("Невозможно выполнить ковариацию, длинна одного вектора {0}, а второго {1}", n1, n2);
			if(n1!=n2)throw new ArgumentException(exceptionStr, "Ковариация");
			double Mx = 0, My = 0, cov = 0;
			
			for(int i = 0; i < X.N; i++){
				Mx += X[i];
				My += Y[i];
			}
			
			Mx /= n1;
			My /= n1;
			
			for(int i = 0; i < X.N; i++)
				cov += (X[i]-Mx)*(Y[i]-My);
			
			cov /= n1-1;
			
			return cov;
		}
		
		
		
	
		
		
		
		
		
		
		/// <summary>
		/// Коэфициент корреляции
		/// </summary>
		/// <param name="X">Вектор X</param>
		/// <param name="Y">Вектор Y</param>
		/// <returns>Возвращает коэф. кор.</returns>
		public static double CorrelationCoefficient(Vector X, Vector Y)
		{
			int n = X.N;
			
			double Mx = 0, My = 0, cor = 0, Dx = 0, Dy =0, dx, dy;
			
			for(int i = 0; i < X.N; i++){
				Mx += X[i];
				My += Y[i];
			}
			
			Mx /= n;
			My /= n;
			
			
			for(int i = 0; i < X.N; i++){
				cor += (X[i]-Mx)*(Y[i]-My);
				dx = X[i]-Mx;
				dy = Y[i]-My;
				Dx += dx*dx;
				Dy += dy*dy;
			}
			
			cor = cor/Math.Sqrt(Dx*Dy);
			
			return cor;
		}
		
		
		/// <summary>
		/// Усреднение по выборке(ансамблю)
		/// </summary>
		/// <param name="vectors">Выборка</param>
		/// <returns>Средний вектор</returns>
		public static Vector MeanVector(Vector[] vectors)
		{
			Vector output = Functions.Summ(vectors);
			output /= vectors.Length;
			return output;
		}
		
		/// <summary>
		/// Среднее геометрическое 
		/// </summary>
		public static double MeanGeom(Vector vect)
		{
			int numMinus = 0;
			
			for (int i = 0; i < vect.N; i++)
			{
				if(vect[i] < 0) numMinus++;
			}
			
			Vector res = MathFunc.ln(MathFunc.abs(vect));
			double summ = Functions.Summ(res);
			summ /= vect.N;
			
			switch (numMinus % 2) {
				case 1:
					return -Math.Exp(summ);
				default:
					return Math.Exp(summ);
			}
			
		}
		
		
		/// <summary>
		/// Среднее гармоническое
		/// </summary>
		/// <returns></returns>
		public static double MeanGarmonic(Vector vect)
		{
			Vector res = 1/vect;
			double summ = Functions.Summ(res);
			return vect.N/summ;
		}
		
		
		/// <summary>
		/// Среднeквадратичное значение
		/// </summary>
		/// <returns></returns>
		public static double RMS(Vector vect)
		{
			Vector res = vect^2;
			double summ = Functions.Summ(res);
			return Math.Sqrt(summ/vect.N);
		}
		
		
		/// <summary>
		/// Дисперсия по ансамлю
		/// </summary>
		/// <param name="ensemble">Ансамбль векторов</param>
		public static Vector EnsembleDispersion(Vector[] ensemble)
		{
			Vector res = new Vector(ensemble[0].N);
			Vector mean = MeanVector(ensemble);
			
			for (int i = 0; i < ensemble.Length; i++) 
			{
				res += (ensemble[i]-mean)^2;
			}
			
			res /= ensemble.Length-1;
			
			return res;
		}
		
		
		
		/// <summary>
		/// Максимум по ансамлю
		/// </summary>
		/// <param name="ensemble">Ансамбль векторов</param>
		public static Vector MaxEns(Vector[] ensemble)
		{
			Vector res = new Vector(ensemble[0].N);
			
			for (int i = 0; i < ensemble[0].N; i++) 
			{
				res[i] = ensemble[0][i];
				
				for (int j = 1; j < ensemble.Length; j++) 
				{
					if (ensemble[j][i] > res[i]) {
						res[i] = ensemble[j][i];
					}	
				}	
			}
			
			return res;
		}
		
		/// <summary>
		/// Возвращает вектор с максимальной энергией
		/// </summary>
		/// <param name="ens">Ансамбль векторов</param>
		/// <returns>Вектор с максимальной энергией</returns>
		public static Vector MaxEnergeVector(Vector[] ens)
		{
			Vector res = new Vector(ens.Length);
			
			for (int i = 0; i < res.N; i++) 
			{
				res[i] = GeomFunc.NormVect(ens[i]);
			}
			
			double max = MaximalValue(res);
			int ind = (int)res.IndexValue(max);
			
			return ens[ind].Copy();
		}
		
		/// <summary>
		/// Средняя частота (не нормированная, зависит от кол-ва точек)
		/// </summary>
		/// <param name="signal">Сигнал</param>
		/// <param name="oldPart">Сглаживание</param>
		/// <returns></returns>
		public static double SimpleMeanFreq(Vector signal, double oldPart = 0.001)
		{

			Vector signal2 = Signals.Filters.ExpAv(signal, oldPart);
			signal2-=Statistic.ExpectedValue(signal2);
			Vector signalQR = signal2^2;
			Vector signalDQR = Functions.Diff(signal2)^2;
			
			double mq1 = 0, mq2 = 0;
			
			for(int i = 0; i < signalQR.N; i++)
			{
				mq1 += signalQR[i];
				mq2 += signalDQR[i];
			}
			
			return Math.Sqrt(mq2/mq1);
			
		}
		
		/// <summary>
		/// Средняя частота сигнала
		/// </summary>
		/// <param name="signal">Сигнал</param>
		/// <param name="fd">Частота дискретизации</param>
		/// <param name="oldPart">Коэффициент сглаживания</param>
		/// <returns>Средняя частота [Гц]</returns>
		public static double MeanFreq(Vector signal, double fd, double oldPart = 0.001)
		{

			double k = fd/(2*Math.PI);
			double W = SimpleMeanFreq(signal, oldPart);
			
			return Math.Round(k*W,3);
			
		}
		
		/// <summary>
		/// Изменение частоты
		/// </summary>
		/// <param name="signal">Сигнал</param>
		/// <returns>Дивиация средней частоты</returns>
		public static double DivFreq(Vector signal)
		{
			Vector dif = Functions.Diff(signal);
			
			return SimpleMeanFreq(dif)/SimpleMeanFreq(signal);
		}
		
		
		
		
		
		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	/// <summary>
	/// Структура гистограммы
	/// </summary>
	[Serializable]
	public class Histogramma
	{
		Vector _x;
		Vector _y;
		string _name = "Гистограмма";
		string _opis = "нет";
		string _xlable = "x";
		string _ylable = "P(x)";
		
		
		/// <summary>
		/// Значения столбцов
		/// </summary>
		public Vector X
		{
			get{return _x;}
			set{_x = value;}
		}
		
		/// <summary>
		/// Высоты столбцов
		/// </summary>
		public Vector Y
		{
			get{return _y;}
			set{_y = value;}
		}
		
		/// <summary>
		/// Название гистограммы
		/// </summary>
		public string Name
		{
			get{return _name;}
			set{_name = value;}
		}
		
		
		/// <summary>
		/// Описание гистограммы
		/// </summary>
		public string Info
		{
			get{return _opis;}
			set{_opis = value;}
		}
		
		
		/// <summary>
		/// Название оси "Х" гистограммы
		/// </summary>
		public string XLable
		{
			get{return _xlable;}
			set{_xlable = value;}
		}
		
		
		/// <summary>
		/// Название оси "У" гистограммы
		/// </summary>
		public string YLables
		{
			get{return _ylable;}
			set{_ylable = value;}
		}
		
		
		/// <summary>
		/// Сохранение гистограммы
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
		/// Загрузка гистограммы
		/// </summary>
		/// <param name="path">Путь до файла</param>		
		public void Open(string path)
		{
			
			try{
			 	
				Histogramma hist = new Histogramma();
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Open, FileAccess.Read, FileShare.None))
			{
			 	hist =(Histogramma)binFormat.Deserialize(fStream);
			}
			
			 _ylable = hist._ylable;
			 _xlable = hist._xlable;
			 _opis = hist._opis;
			 _name = hist._name;
			 _x = hist._x;
			 _y = hist._y;
			
			}
			
			catch
			{
				throw new ArgumentException("Ошибка загрузки", "Загрузка");
			}
			
		}
		
		
	}
}
