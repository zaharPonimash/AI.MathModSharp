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
    /// Математические функции для векторов и матриц
    /// </summary>
    public static class MathFunc
    {


        /// <summary>
        /// Квадратный корень из ПИ
        /// </summary>
        public static double SqrtPi
        {
            get { return 1.77245385091; }

        }

        /// <summary>
        /// Квадратный корень из 2-х
        /// </summary>
        public static double Sqrt2
        {
            get { return 1.41421356237; }

        }

        /// <summary>
        /// Функция ошибки
        /// </summary>
        /// <param name="x">Аргумент</param>
        public static double erf(double x)
        {
           
            double a = 8 / (3 * Math.PI) * (3 - Math.PI) / (Math.PI - 4),
                exp1 = (-x * x * (4 / Math.PI) + a * x * x) / (1 + a * x);
            return Math.Sign(x)*Math.Sqrt( 1-Math.Exp(exp1));
        }

        /// <summary>
        /// Функция ошибок
        /// </summary>
        /// <param name="Inp">Входной вектор</param>
        /// <returns></returns>
        public static Vector erf(Vector Inp)
        {
            Vector A = new Vector(Inp.N);
            for (int i = 0; i < Inp.N; i++) A.DataInVector[i] = erf(Inp.DataInVector[i]);
            return A;
        }



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
				sequen.DataInVector[i] = began + i*step;
			
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
				sequen.DataInVector[i] = began + i;
			
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = GradToRad(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = RadToGrad(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = factorial(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++)
				A.DataInVector[i] = Math.Sin(Inp.DataInVector[i]);
			return A;
		}
		
		
		
		
		/// <summary>
		/// Округление
		/// </summary>
		/// <param name="Inp">Вектор входа</param>
		/// <param name="digits">до какого знака</param>
		/// <returns>Вектор выхода</returns>
		public static Vector round(Vector Inp, int digits)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Round(Inp.DataInVector[i], digits);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Cos(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Tan(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Asin(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Acos(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Atan(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Дсятичный логарифм
		/// </summary>
		/// <param name="Inp">Подлогарифмическое число</param>
		public static Vector lg(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Log10(Inp.DataInVector[i]);
			return A;
		}
		
		
		
		/// <summary>
		/// Логарифм по основанию "e"
		/// </summary>
		/// <param name="Inp">Подлогарифмическое число</param>
		public static Vector ln(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Log(Inp.DataInVector[i]);
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
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Exp(Inp.DataInVector[i]);
			return A;
		}
			
			
			/// <summary>
			/// Гиперболический тангенс
			/// </summary>
			/// <param name="Inp">углы</param>

			public static Vector tanh(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Tanh(Inp.DataInVector[i]);
			return A;
		}
			
		/// <summary>
		/// Определение знака
		/// </summary>
		/// <param name="Inp">Входной вектор</param>
		/// <returns></returns>
		public static Vector sign(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Sign(Inp.DataInVector[i]);
			return A;
		}
			
			
	
		/// <summary>
		/// Квадратный корень
		/// </summary>
		/// <param name="Inp">числа</param>		
		public static Vector sqrt(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Sqrt(Inp.DataInVector[i]);
			return A;
		}
		
			
		/// <summary>
		/// Вычисление синуса
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix sin(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Sin(Inp.Matr[i,j]);
			return A;
		}
		
		/// <summary>
		/// e^x
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix exp(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Exp(Inp.Matr[i,j]);
			return A;
		}
		
		
		/// <summary>
		/// Гиперболический тангенс
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix tanh(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Tanh(Inp.Matr[i,j]);
			return A;
		}
		
		
		
		
		/// <summary>
		/// Косинус
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix cos(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Cos(Inp.Matr[i,j]);
			return A;
		}
		
		/// <summary>
		/// Тангенс
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix tg(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Tan(Inp.Matr[i,j]);
			return A;
		}
		
		/// <summary>
		/// Котангенс
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix ctg(Matrix Inp)
		{
			return 1.0/tg(Inp);
		}
		
		/// <summary>
		/// Арксинус
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix arcsin(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Asin(Inp.Matr[i,j]);
			return A;
		}
		
		/// <summary>
		/// Арккосинус
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix arccos(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Acos(Inp.Matr[i,j]);
			return A;
		}
		
		
		/// <summary>
		/// Арктангенс
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix arctg(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Atan(Inp.Matr[i,j]);
			return A;
		}
		
		/// <summary>
		/// Модуль
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix abs(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Abs(Inp.Matr[i,j]);
			return A;
		}
		
		/// <summary>
		/// Квадратный корень
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix sqrt(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Sqrt(Inp.Matr[i,j]);
			return A;
		}
		
		
		/// <summary>
		/// Десятичный логарифм
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix lg(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Log10(Inp.Matr[i,j]);
			return A;
		}
		
		
		/// <summary>
		/// Логарифм по основанию E
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix ln(Matrix Inp)
		{
			Matrix A = new Matrix(Inp.M, Inp.N);
			for(int i = 0; i<Inp.M; i++)for(int j = 0; j<Inp.N; j++)
										A.Matr[i,j] = Math.Log(Inp.Matr[i,j]);
			return A;
		}
		
		/// <summary>
		/// Секонс
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix sec(Matrix Inp)
		{
			return 1.0/cos(Inp);
		}
		
		/// <summary>
		/// Косеконс
		/// </summary>
		/// <param name="Inp">Матрица значений для преобразования</param>	
		public static Matrix cosec(Matrix Inp)
		{
			return 1.0/sin(Inp);
		}
		
		
		/// <summary>
		/// Синус
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>	
		public static ComplexVector sin(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Sin(Inp.DataInVector[i]);
			return A;
		}
		
		
		/// <summary>
		/// e^x
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector exp(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Exp(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Гиперболический тангенс
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector tanh(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Tanh(Inp.DataInVector[i]);
			return A;
		}
		
		
		/// <summary>
		/// Квадратный корень
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector sqrt(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Sqrt(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Косинус
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector cos(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Cos(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Логарифм по основанию 10
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector lg(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Log10(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Логарифм по основанию e
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector ln(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Log(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Тангенс
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector tg(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Tan(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Котангенс
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector ctg(ComplexVector Inp)
		{
			return 1.0/tg(Inp);
		}
		
		
		/// <summary>
		/// Арксинус
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector arcsin(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Asin(Inp.DataInVector[i]);
			return A;
		}
		
		
		/// <summary>
		/// Арккосинус
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector arccos(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Acos(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Арктангенс
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector arctg(ComplexVector Inp)
		{
			ComplexVector A = new ComplexVector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Atan(Inp.DataInVector[i]);
			return A;
		}
		
		/// <summary>
		/// Секонс
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector sec(ComplexVector Inp)
		{
			return 1/cos(Inp);
		}
		
		/// <summary>
		/// Косеконс
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static ComplexVector cosec(ComplexVector Inp)
		{
			return 1/sin(Inp);
		}
		
		/// <summary>
		/// Модуль
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static Vector abs(Vector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Math.Abs(Inp.DataInVector[i]);
			return A;
		}
		
		
		/// <summary>
		/// Модуль
		/// </summary>
		/// <param name="Inp">Комплексный вектор значений для преобразования</param>
		public static Vector abs(ComplexVector Inp)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++) A.DataInVector[i] = Complex.Abs(Inp.DataInVector[i]);
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
		/// Сигмоидальная однополярная активационная ф-я
		/// </summary>
		/// <param name="x">Входное значение</param>
		/// <param name="betta">Угол наклона</param>
		public static Vector InverseSigmoid(Vector x, double betta = 1)
		{
			return -MathFunc.ln(1/x-1)/betta;
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
		/// <param name="threshold">Порог</param>
		public static Vector Threshold(Vector Inp, double threshold=0)
		{
			Vector A = new Vector(Inp.N);
			for(int i = 0; i<Inp.N; i++)
				if(Inp.DataInVector[i] >= threshold)A.DataInVector[i] = 1;
				else A.DataInVector[i] = 0;
			return A;
		}


        /// <summary>
        /// Ограничение сверху и снизу
        /// </summary>
        /// <param name="Inp">Входной вектор</param>
        /// <param name="thresholdUp"></param>
        /// <param name="thresholdDoun"></param>
        /// <returns></returns>
        public static Vector Threshold(Vector Inp, double thresholdUp = 1, double thresholdDoun = 0)
        {
            Vector A = new Vector(Inp.N);
            for (int i = 0; i < Inp.N; i++)
                if ((Inp.DataInVector[i] >= thresholdDoun)&& (Inp.DataInVector[i] <= thresholdUp)) A.DataInVector[i] = 1;
                else A.DataInVector[i] = 0;
            return A;
        }

		/// <summary>
		/// Релу
		/// </summary>
		/// <param name="Inp"></param>
		/// <param name="threshold"></param>
		/// <returns></returns>
        public static Vector Relu(Vector Inp, double threshold = 0)
        {
            Vector A = new Vector(Inp.N);
            for (int i = 0; i < Inp.N; i++)
                if (Inp.DataInVector[i] >= threshold) A.DataInVector[i] = Inp.DataInVector[i];
                else A.DataInVector[i] = 0;
            return A;
        }


	/// <summary>
	/// Активация Релу
	/// </summary>
	/// <param name="Inp">Вход</param>
	/// <param name="thresholdUp">Верхний порог</param>
	/// <param name="thresholdDoun">Нижний порог</param>
        public static Vector Relu(Vector Inp, double thresholdUp , double thresholdDoun = 0)
        {
            Vector A = new Vector(Inp.N);
            for (int i = 0; i < Inp.N; i++)
                if ((Inp.DataInVector[i] >= thresholdDoun) && (Inp.DataInVector[i] <= thresholdUp)) A.DataInVector[i] = Inp.DataInVector[i];
                else A.DataInVector[i] = 0;
            return A;
        }

	/// <summary>
	/// Активация Релу
	/// </summary>
	/// <param name="Inp">Вход</param>
	/// <param name="thresholdUp">Верхний порог</param>
	/// <param name="thresholdDoun">Нижний порог</param>
        public static Matrix Relu(Matrix Inp, double thresholdUp, double thresholdDoun = 0)
        {
            Matrix A = new Matrix(Inp.M, Inp.N);

            for (int i = 0; i < Inp.M; i++) for (int j = 0; j < Inp.N; j++)
                {
                    if ((Inp.Matr[i, j] >= thresholdDoun) && (Inp.Matr[i, j] <= thresholdUp)) A.Matr[i, j] = Inp.Matr[i, j];
                    else if (Inp.Matr[i, j] <= thresholdUp) A.Matr[i, j] = 0;
                    else A.Matr[i,j] = 1;
                }

            return A;
        }


        /// <summary>
        /// Сигмоида
        /// </summary>
        /// <param name="Inp"></param>
        /// <param name="betta"></param>
        /// <returns></returns>
        public static Matrix Sigmoid(Matrix Inp, double betta =1)
		{
			return 1.0/(1+MathFunc.exp(Inp*(-betta)));
		}

        /// <summary>
        /// Сигмоида
        /// </summary>
        /// <param name="Inp"></param>
        /// <param name="betta"></param>
        /// <returns></returns>
        public static Matrix SigmoidBiplyar(Matrix Inp, double betta = 1)
        {
            return MathFunc.tanh(Inp * betta);
        }


        /// <summary>
        /// Сигмоида
        /// </summary>
        /// <param name="Inp"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static Matrix Threshold(Matrix Inp, double threshold = 0)
        {
            Matrix A = new Matrix(Inp.M, Inp.N);

            for (int i = 0; i < Inp.M; i++) for (int j = 0; j < Inp.N; j++)
                {
                    if (Inp.Matr[i, j] >= threshold) A.Matr[i, j] = 1;
                    else A.Matr[i, j] = 0;
                }

            return A;
        }


	/// <summary>
	/// Сигмоида
	/// </summary>
	/// <param name="tensor">Тензор входа</param>
	/// <param name="betta">Коэфициент наклона</param>
        public static Tensor Sigmoid(Tensor tensor, double betta = 1)
        {
              Tensor tensorOut = new Tensor(tensor.Width, tensor.Height, tensor.Depth);

            for (int i = 0; i < tensor.DataInTensor.Length; i++)
            {
                tensorOut.DataInTensor[i] = Sigmoid(tensor.DataInTensor[i], betta);
            }

            return tensorOut;
        }
        
        
        /// <summary>
        /// Логарифм по основанию 10
        /// </summary>
        /// <param name="tensor">Тензор входа</param>
         public static Tensor Log10(Tensor tensor)
        {
              Tensor tensorOut = new Tensor(tensor.Width, tensor.Height, tensor.Depth);

            for (int i = 0; i < tensor.DataInTensor.Length; i++)
            {
                tensorOut.DataInTensor[i] = Math.Log10(tensor.DataInTensor[i]);
            }

            return tensorOut;
        }

	/// <summary>
	/// Активация Релу
	/// </summary>
	/// <param name="Inp">Вход</param>
	/// <param name="threshold">Нижний порог</param>
        public static Matrix Relu(Matrix Inp, double threshold = 0)
        {
            Matrix A = new Matrix(Inp.M, Inp.N);

            for (int i = 0; i < Inp.M; i++) for (int j = 0; j < Inp.N; j++)
                {
                    if (Inp.Matr[i, j] >= threshold) A.Matr[i, j] = Inp.Matr[i, j];
                    else A.Matr[i, j] = 0;
                }

            return A;
        }
	
       	/// <summary>
	/// Активация Релу
	/// </summary>
	/// <param name="Inp">Вход</param>
	/// <param name="threshold">Нижний порог</param>
        public static Vector[] Relu(Vector[] Inp, double threshold = 0)
        {
            Vector[] A = new Vector[Inp.Length];

            for (int i = 0; i < A.Length; i++)
            {
                A[i] = new Vector(Inp[i].N);
            }

            for (int i = 0; i < Inp.Length; i++) for (int j = 0; j < Inp[i].N; j++)
                {
                    if (Inp[i][j] >= threshold) A[i][j] = Inp[i][j];
                    else A[i][j] = 0;
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
        /// Функция вероятность принадлежности
        /// </summary>
        /// <param name="Inp">Входное значение</param>
        /// <param name="m">Мат. ожидание</param>
        /// <param name="sko">СКО</param>
        public static double Gauss(double Inp, double m, double sko)
        {
            return (1.0 / (sko * Math.Sqrt(2 * Math.PI))) * Math.Exp(((Inp - m) * (Inp - m)) / (-2 * sko * sko));
        }


	/// <summary>
	/// Функция Гаусса при x=m -> G(x) = 1
	/// </summary>
	/// <param name="inp"></param>
	/// <param name="m"></param>
	/// <param name="sko"></param>
	/// <returns></returns>
        public static Matrix Gauss1(Matrix inp, double m, double sko)
        {
            Matrix matr = new Matrix(inp.M, inp.N);

            for (int i = 0; i < inp.M; i++)
            {
                for (int j = 0; j < inp.N; j++)
                {
                    matr.Matr[i, j] = GaussNorm1(inp.Matr[i, j], m, sko);
                }
            }

            return matr;
        }





        /// <summary>
        /// Функция вероятность принадлежности при inp = m, out = 1
        /// </summary>
        /// <param name="Inp">Входное значение</param>
        /// <param name="m">Мат. ожидание</param>
        /// <param name="std">СКО</param>
        public static double GaussNorm1(double Inp, double m, double std)
        {
            return Math.Exp(((Inp - m) * (Inp - m)) / (-2 * std * std));
        }
        
        /// <summary>
        /// Функция вероятность принадлежности при inp = m, out = 1
        /// </summary>
        /// <param name="Inp">Входной вектор</param>
        /// <param name="m">Мат. ожидание</param>
        /// <param name="std">СКО</param>
        public static Vector GaussNorm1(Vector Inp, double m, double std)
        {
        	Vector vect = new Vector(Inp.N);
        	
        	for (int i = 0; i < vect.N; i++) {
        		vect[i] = GaussNorm1(Inp[i], m, std);
        	}
        	
        	return vect;
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
		/// Проекция вектора А на вектор B
		/// </summary>
		/// <param name="A">Вектор А</param>
		/// <param name="B">Вуктор В</param>
		/// <returns>Результат проецирования</returns>
		static public Vector ProectionAtoB(Vector A, Vector B)
		{
			double k = ScalarProduct(A,B)/ScalarProduct(B,B);
			return k*B;
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
		
		
		/// <summary>
		/// Поворот вектора на заданные углы
		/// </summary>
		static public Vector VectorRotate(Vector inp, double angl, int indAx1, int indAx2)
		{
			Matrix rotateMatr = new Matrix(inp.N, inp.N);
			
			for(int i = 0; i<inp.N; i++)
				rotateMatr[i,i] = 1;
			
			rotateMatr[indAx1, indAx1] = Math.Cos(angl);
			rotateMatr[indAx2, indAx2] = Math.Cos(angl);
			rotateMatr[indAx1, indAx2] = -Math.Sin(angl);
			rotateMatr[indAx1, indAx1] = Math.Sin(angl);
			
			Matrix vectorInp = inp.ToMatrix().Tr();
			
			return (rotateMatr*vectorInp).Tr().ToVector();
				
		}
		
	}
	
	
	}

