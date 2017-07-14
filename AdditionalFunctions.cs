/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 07.02.2016
 * Time: 18:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Numerics;

namespace AI.MathMod.AdditionalFunctions
{
 
	
		
	
	/// <summary>
	/// Математические ф-и для векторов и матриц
	/// </summary>
	public static class MathFunc
	{
		
		
		/// <summary>
		/// Генерирование последовательности
		/// </summary>
		/// <param name="began">Начальное значение</param>
		/// <param name="step">Шаг</param>
		/// <param name="end">Конечное значение</param>
		/// <returns>Возвращает последовательность</returns>
		public static Vector GenerateTheSequence(double began, double step, double end)
		{
			double n = end - began;
			int N = (n%step == 0)? (int)(n/step):(int)(n/step)+1;
			
			Vector sequen = new Vector(N);
			
			for(int i = 0; i<N; i++)
				sequen.Vecktor[i] = began + i*step;
			
			return sequen;
		}
		
		
		
		
		
		
		
		
		
		/// <summary>
		/// Генерирование последовательности
		/// </summary>
		/// <param name="began">Начальное значение</param>
		/// <param name="end">Конечное значение</param>
		/// <returns>Возвращает послеовательность с шагом 1</returns>
		public static Vector GenerateTheSequence(double began, double end)
		{
			double n = end - began;
			int N = (n%1 == 0)? (int)(n):(int)(n+1);
			
			Vector sequen = new Vector(N);
			
			for(int i = 0; i<N; i++)
				sequen.Vecktor[i] = began + i;
			
			return sequen;
		}
		
		
		
		/// <summary>
		/// Перевод градусов в радианы
		/// </summary>
		/// <param name="grad">значение в градусах</param>
		public static double GradToRad(double grad)
		{
			return grad*Math.PI/180.0;
		}
		
		
		/// <summary>
		/// Перевод градусов в радианы
		/// </summary>
		/// <param name="Inp">значения в градусах</param>
		public static Vector GradToRad(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = GradToRad(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		
			/// <summary>
		/// Перевод радиан в градусы
		/// </summary>
		/// <param name="rad">значение в радианах</param>
		public static double RadToGrad(double rad)
		{
			return rad*180.0/Math.PI;
		}
		
		
		/// <summary>
		/// Перевод радиан в градусы
		/// </summary>
		/// <param name="Inp">значение в радианах</param>
		public static Vector RadToGrad(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = RadToGrad(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		
		
		
		/// <summary>
		/// Вычисление факториала числа
		/// </summary>
		/// <param name="x">Число</param>
		/// <returns>Факториал</returns>
		public static double factorial(double x)
		{
			double outp = 0;
			
			if(x == 0) outp = 1;
			else
			{
				Vector vector = GenerateTheSequence(1,x+1);
				outp = Functions.Multiplication(vector);
			}
			
			return outp;
		}
		
		
		
		
		
		
		/// <summary>
		/// Вычисление факториала векторов поэлементно
		/// </summary>
		/// <param name="Inp">Входной вектор</param>
		/// <returns>Факториал</returns>
		public static Vector factorial(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = factorial(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		
		
		
		/// <summary>
		/// Вычисление синусов
		/// </summary>
		/// <param name="Inp">Вектор углов(в радианах)</param>
		/// <returns>Вектор синусов</returns>
		public static Vector sin(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Sin(Inp.Vecktor[i]);
			return A;
		}
		
		
		/// <summary>
		/// Вычисление косинусов
		/// </summary>
		/// <param name="Inp">Вектор углов(в радианах)</param>
		/// <returns>Вектор косинусов</returns>
		public static Vector cos(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Cos(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		/// <summary>
		/// Вычисление тангенсов
		/// </summary>
		/// <param name="Inp">Вектор углов(в радианах)</param>
		/// <returns>Вектор тангенсов</returns>
		public static Vector tg(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Tan(Inp.Vecktor[i]);
			return A;
		}
		
		
		/// <summary>
		/// Вычисление котангенсов
		/// </summary>
		/// <param name="Inp">Вектор углов(в радианах)</param>
		/// <returns>Вектор котангенсов</returns>
		public static Vector ctg(Vector Inp)
		{
			return 1.0/tg(Inp);
		}
		
		
		/// <summary>
		/// Вычисление арксинусов
		/// </summary>
		/// <param name="Inp">Вектор синусов</param>
		/// <returns>Вектор углов(в радианах)</returns>
		public static Vector arcsin(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Asin(Inp.Vecktor[i]);
			return A;
		}
		
		/// <summary>
		/// Вычисление арккосинусов
		/// </summary>
		/// <param name="Inp">Вектор косинусов</param>
		/// <returns>Вектор углов(в радианах)</returns>
		public static Vector arccos(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Acos(Inp.Vecktor[i]);
			return A;
		}
		
		/// <summary>
		/// Вычисление арктангенсов
		/// </summary>
		/// <param name="Inp">Вектор тангенсов</param>
		/// <returns>Вектор углов(в радианах)</returns>
		public static Vector arctg(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Atan(Inp.Vecktor[i]);
			return A;
		}
		
		/// <summary>
		/// Дсятичный логарифм
		/// </summary>
		/// <param name="Inp">Подлогарифмическое число</param>
		public static Vector lg(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Log10(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		/// <summary>
		/// Логарифм по основанию "e"
		/// </summary>
		/// <param name="Inp">Подлогарифмическое число</param>
		public static Vector ln(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Log(Inp.Vecktor[i]);
			return A;
		}
		
		/// <summary>
		/// Секанс угла
		/// </summary>
		/// <param name="Inp">углы</param>
		public static Vector sec(Vector Inp)
		{
			return 1/cos(Inp);
		}
		
		
		/// <summary>
		/// Косеканс угла
		/// </summary>
		/// <param name="Inp">углы</param>
		public static Vector cosec(Vector Inp)
		{
			return 1/sin(Inp);
		}
		
		/// <summary>
		/// Экспонента e^x
		/// </summary>
		/// <param name="Inp">показатели степени</param>
		/// <returns>e^Inp - поэлементно</returns>
			public static Vector exp(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Exp(Inp.Vecktor[i]);
			return A;
		}
			
			
			/// <summary>
			/// Гиперболический тангенс
			/// </summary>
			/// <param name="Inp">углы</param>

			public static Vector tanh(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Tanh(Inp.Vecktor[i]);
			return A;
		}
			
			
	
		/// <summary>
		/// Квадратный корень
		/// </summary>
		/// <param name="Inp">числа</param>		
		public static Vector sqrt(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Sqrt(Inp.Vecktor[i]);
			return A;
		}
		
			
			
		public static Matrix sin(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Sin(Inp.Matr[i,j]);
			return A;
		}
		
		
		public static Matrix exp(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Exp(Inp.Matr[i,j]);
			return A;
		}
		
		
		
		public static Matrix tanh(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Tanh(Inp.Matr[i,j]);
			return A;
		}
		
		
		
		
		
		public static Matrix cos(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Cos(Inp.Matr[i,j]);
			return A;
		}
		
		public static Matrix tg(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Tan(Inp.Matr[i,j]);
			return A;
		}
		
		
		public static Matrix ctg(Matrix Inp)
		{
			return 1.0/tg(Inp);
		}
		
		
		public static Matrix arcsin(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Asin(Inp.Matr[i,j]);
			return A;
		}
		
		
		public static Matrix arccos(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Acos(Inp.Matr[i,j]);
			return A;
		}
		
		
		
		public static Matrix arctg(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Atan(Inp.Matr[i,j]);
			return A;
		}
		
		
		public static Matrix abs(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Abs(Inp.Matr[i,j]);
			return A;
		}
		
		
		public static Matrix sqrt(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Sqrt(Inp.Matr[i,j]);
			return A;
		}
		
		
		
		public static Matrix lg(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Log10(Inp.Matr[i,j]);
			return A;
		}
		
		
		
		public static Matrix ln(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Log(Inp.Matr[i,j]);
			return A;
		}
		
		public static Matrix sec(Matrix Inp)
		{
			return 1.0/cos(Inp);
		}
		
		public static Matrix cosec(Matrix Inp)
		{
			return 1.0/sin(Inp);
		}
		
		
		
		public static ComplexVector sin(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Sin(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		public static ComplexVector exp(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Exp(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector tanh(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Tanh(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		public static ComplexVector sqrt(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Sqrt(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector cos(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Cos(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector lg(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Log10(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector ln(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Log(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector tg(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Tan(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector ctg(ComplexVector Inp)
		{
			return 1.0/tg(Inp);
		}
		
		
		
		public static ComplexVector arcsin(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Asin(Inp.Vecktor[i]);
			return A;
		}
		
		public static ComplexVector arccos(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Acos(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector arctg(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Atan(Inp.Vecktor[i]);
			return A;
		}
		
		
		public static ComplexVector sec(ComplexVector Inp)
		{
			return 1/cos(Inp);
		}
		
		
		public static ComplexVector cosec(ComplexVector Inp)
		{
			return 1/sin(Inp);
		}
		
		
		public static Vector abs(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Math.Abs(Inp.Vecktor[i]);
			return A;
		}
		
		
		
		public static Vector abs(ComplexVector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.Vecktor[i] = Complex.Abs(Inp.Vecktor[i]);
			return A;
		}

		
	}
	
	
	
	
	/// <summary>
	/// Функции активации нейронов
	/// </summary>
	public static class NeuroFunc
	{
		/// <summary>
		/// Сигмоидальная однополярная активационная ф-я
		/// </summary>
		/// <param name="Inp">Входной вектор</param>
		/// <param name="betta">Угол наклона</param>
		public static Vector Sigmoid(Vector Inp, double betta =1 )
		{
			return 1.0/(1+MathFunc.exp(Inp*(-betta)));
		}
		
		
			
		/// <summary>
		/// Сигмоидальная однополярная активационная ф-я
		/// </summary>
		/// <param name="x">Входное значение</param>
		/// <param name="betta">Угол наклона</param>
		public static double Sigmoid(double x, double betta =1)
		{
			return 1.0/(1+Math.Exp(x*(-betta)));
		}
		
		
		/// <summary>
		/// Сигмоидальная однополярная активационная ф-я
		/// </summary>
		/// <param name="x">Входное значение</param>
		/// <param name="betta">Угол наклона</param>
		public static double InverseSigmoid(double x, double betta = 1)
		{
			return -Math.Log(1/x-1)/betta;
		}
		
		
		/// <summary>
		/// Сигмоидальная биполярная активационная ф-я
		/// </summary>
		/// <param name="Inp">Входной вектор</param>
		/// <param name="betta">Угол наклона</param>
		public static Vector SigmoidBiplyar(Vector Inp, double betta =1)
		{
			return MathFunc.tanh(Inp*betta);
		}
		
		/// <summary>
		/// Пороговая активационная ф-я
		/// </summary>
		/// <param name="Inp">Входной вектор</param>
		/// <param name="porog">Порог</param>
		public static Vector Porog(Vector Inp, double porog=0)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++)
				if(Inp.Vecktor[i] >= porog)A.Vecktor[i] = 1;
				else A.Vecktor[i] = 0;
			return A;
		}
		
		
		
		
		public static Matrix Sigmoid(Matrix Inp, double betta =1)
		{
			return 1.0/(1+MathFunc.exp(Inp*(-betta)));
		}
		
		
		public static Matrix SigmoidBiplyar(Matrix Inp, double betta =1)
		{
			return MathFunc.tanh(Inp*betta);
		}
		
		
		public static Matrix Porog(Matrix Inp, double porog = 0)
		{
			Matrix A = new Matrix(Inp.M,Inp.N);
			
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
			{
				if(Inp.Matr[i,j] >= porog)A.Matr[i,j] = 1;
				else A.Matr[i,j] = 0;
			}
			
			return A;
		}
		
	}
	
	
	
	
	
	
	
	/// <summary>
	/// Функции распределения случайной величины
	/// </summary>
	public static class DistributionFunc
	{
	
		
		/// <summary>
		/// Функция распределения по нормальному закону 
		/// </summary>
		/// <param name="Inp">Входной вектор</param>
		/// <param name="m">Мат. ожидание</param>
		/// <param name="sko">СКО</param>
		public static Vector Gauss(Vector Inp, double m, double sko)
		{
			return (1.0/(sko*Math.Sqrt(2*Math.PI)))*MathFunc.exp(((Inp-m)^2)/(-2*sko*sko));
		}
		
		
		/// <summary>
		/// Функция распределения Пуасона 
		/// </summary>
		/// <param name="Inp">Входной вектор</param>
		/// <param name="m">Мат. ожидание от 0 до +inf</param>
		public static Vector Puasson(Vector Inp, double m)
		{
			return ((m^Inp)/MathFunc.factorial(Inp))*Math.Exp(-m);
		}
		
	}
	
	
	
	/// <summary>
	/// Аналитическая геометрия
	/// </summary>
	public static class GeomFunc
	{
	
		/// <summary>
		/// Вычисляет эвклидовую норму вектора 
		/// </summary>
		/// <param name="vector">Входной вектор</param>
		static public double NormVect(Vector vector)
		{
			return Math.Sqrt(Functions.Summ((vector^2)));
		}
	
		
		/// <summary>
		/// Скалярное произведение 2-х векторов
		/// </summary>
		/// <param name="vector">Первый вектор</param>
		/// <param name="vector2">Второй вектор</param>
		/// <returns>Возвращает скалярное произведение</returns>
		static public double ScalarProduct(Vector vector, Vector vector2)
		{
			if(vector.N != vector2.N)throw new ArgumentException("Размерности векторов не совпадают", "Умножение векторов");
			return Functions.Summ(vector*vector2);
		}
		
		
		/// <summary>
		/// Угол между векторами
		/// </summary>
		/// <param name="vector">Первый вектор</param>
		/// <param name="vector2">Второй вектор</param>
		/// <returns>Возвращает угол в радианах</returns>
		static public double AngleVect(Vector vector, Vector vector2)
		{
			double a = ScalarProduct(vector, vector2), b = NormVect(vector)*NormVect(vector2);
			return Math.Acos(a/b);
		}
		
		
		/// <summary>
		/// Вычисляет вектор соединяющий точку А с точкой Б
		/// </summary>
		/// <param name="pointA">координаты точки А</param>
		/// <param name="pointB">координаты точки Б</param>
		/// <returns>Возвращает компаненты вектора</returns>
		static public Vector VectorFromAToB(Vector pointA, Vector pointB)
		{
			if(pointA.N != pointB.N)throw new ArgumentException("Размерности точек не совпадают", "");
			return pointB - pointA;
		}
		
		
		
		
		/// <summary>
		/// Вычисляет растояние от точки А до Б
		/// </summary>
		/// <param name="pointA">координаты точки А</param>
		/// <param name="pointB">координаты точки Б</param>
		/// <returns>Возвращает растояние</returns>
		static public double DistanceFromAToB(Vector pointA, Vector pointB)
		{
			return NormVect(VectorFromAToB(pointA, pointB));
		}
		
	}
	
	
	}

