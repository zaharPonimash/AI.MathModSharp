/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 28.01.2016
 * Time: 17:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Numerics;


namespace AI.MathMod
{
	
	/// <summary>
	/// Лицензия
	/// </summary>
	public static class Лицензия
	{
		public static string Автор = 
			"Автор: Понимаш Захар Алексеевич, \n" +
			"библиотека создана 28.01.16 в 13:57 \n" +
			"находится на стадии бетта-тестирования \n" +
			"страница в вк: https://vk.com/zzakhar2013";
		
		public static string лицензия = 
			"Разрешено использование в любых НЕКОММЕРЧЕСКИХ проектах," +
			" дизассемлерирование и декомпилирование, " +
			"а так же использование в коммерческих проектах, без моего писменного согласия, запрещено!";
	}
	
	
	/// <summary>
	/// Математические фукции
	/// </summary>
	public static class Functions
	{
		/// <summary>
		/// Следующая степень числа 2
		/// </summary>
		/// <param name="n">входное число</param>
		/// <returns></returns>
		public static int NextPow2(int n)
		{
			int pow = 0;
			
			for (int i = 1; i < 40; i++)
			{
				pow = (int)Math.Pow(2,i);
				if (n<=pow) return pow;
			}
			
			return -1;
		}
		
		
		
		
	#region Сумма
	/// <summary>
	/// Суммирование всех элементов массива типа double
	/// </summary>
	public static double Summ(double[] mass)
	{
		double summ = 0;
		
		for(int i = 0; i<mass.Length; i++)
			summ += mass[i];
		
		return summ;
	}


		
	/// <summary>
	/// Суммирование всех элементов действительного вектора
	/// </summary>
	public static double Summ(Vector vect)
	{
		int n = vect.N;
		double summ = 0;
		
		
		for(int i = 0; i<n; i++)
			summ += vect.Vecktor[i];
		
		return summ;
	}
	
	/// <summary>
	/// Суммирование всех элементов комплексного вектора
	/// </summary>
	public static Complex Summ(ComplexVector vect)
	{
		int n = vect.N;
		Complex summ = 0;
		
		
		for(int i = 0; i<n; i++)
			summ += vect.Vecktor[i];
		
		return summ;
	}
	
	
	/// <summary>
	/// Суммирование всех элементов массива типа int
	/// </summary>
	public static int Summ(int[] mass)
	{
		int summ = 0;
		
		for(int i = 0; i<mass.Length; i++)
			summ += mass[i];
		
		return summ;
	}
	
	#endregion
	
	
	
	#region Интеграл
	/// <summary>
	/// Вычисляет интегральную функцию действительный вектор
	/// Входной вектор апроксиммирован полиномом 0-го порядка
	/// с коэфициентом 2
	/// </summary>
	/// <returns></returns>
	public static Vector IntegralInterp(Vector A)
	{
		
		int kRasshR = 2;
		Vector B = new Vector(A.N*kRasshR), C, D = A.InterpolayrZero(kRasshR);
		
		for(int i = 0; i<B.N; i++)
		{
			C = D.CutAndZero(i+1);
			B.Vecktor[i] = Summ(C/kRasshR);
		}
			
		return B.Decim(kRasshR);
	}
	
	

	/// <summary>
	/// Вычисляет интегральную функцию действительный вектор
	/// </summary>
	/// <returns></returns>
	public static Vector Integral(Vector A)
	{
		
		
		Vector B = new Vector(A.N), C;
		
		for(int i = 0; i<B.N; i++)
		{
			C = A.CutAndZero(i+1);
			B.Vecktor[i] = Summ(C);
		}
			
		return B;
	}
	
	
	
	/// <summary>
	/// Вычисляет определенный интеграл
	/// </summary>
	/// <param name="A">Входной действительный вектор</param>
	/// <param name="a">Нижний предел</param>
	/// <param name="b">Верхний предел</param>
	public static double Integral(Vector A, double a, double b)
	{
		
		
		Vector B = new Vector(A.N), C;
		int beg = 0;
		
		
		for(int i = 0; i<B.N; i++,beg++)
		{
			if(A.Vecktor[i] >= a) break;
		}
		
		
		for(int i = beg; i<B.N; i++)
		{
			C = A.CutAndZero(i+1);
			B.Vecktor[i] = Summ(C);
			if(A.Vecktor[i] >= b) break;
		}
			
		return B.Vecktor[B.N-1]-B.Vecktor[0];
	}
	
	
	
	/// <summary>
	/// Вычисляет определенный интеграл
	/// </summary>
	/// <param name="A">Входной действительный вектор</param>
	/// <param name="b">Верхний предел(Нижний предел равен первому значению)</param>
	public static double Integral(Vector A, double b)
	{
		Vector B = new Vector(A.N), C;

		for(int i = 0; i<B.N; i++)
		{
			C = A.CutAndZero(i+1);
			B.Vecktor[i] = Summ(C);
			if(A.Vecktor[i] >= b) break;
		}
			
		return B.Vecktor[B.N-1]-B.Vecktor[0];
	}
	
	
	
	
	
	/// <summary>
	/// Вычисляет диференциальную функцию действительный вектор
	/// </summary>
	/// <param name="A"> Входной вектор</param>
	public static Vector Diff(Vector A)
	{
		Vector B = new Vector(A.N);
		
		B.Vecktor[0] = A.Vecktor[0];
		
		for(int i = 1; i<B.N; i++)
		{
			B.Vecktor[i] = A.Vecktor[i] - A.Vecktor[i-1];
		}
			
		return B;
	}
	
	
	/// <summary>
	/// Вычисляет диференциальную функцию комплексный вектор
	/// </summary>
	/// <param name="A">Входной вектор</param>
	public static ComplexVector Diff(ComplexVector A)
	{
		ComplexVector B = new ComplexVector(A.N);
		
		B.Vecktor[0] = A.Vecktor[0];
		
		for(int i = 1; i<B.N; i++)
		{
			B.Vecktor[i] = A.Vecktor[i] - A.Vecktor[i-1];
		}
			
		return B;
	}
	
	
	
	/// <summary>
	/// Вычисляет кратный интеграл по dx
	/// </summary>
	/// <param name="A">Входной вектор</param>
	/// <param name="k">Кратность 1, 2, 3 ....</param>
	/// <returns>Действительный вектор</returns>
	public static Vector Integral(Vector A, int k)
	{
			Vector B = A.Copy();
			for(int i = 0; i<k; i++)
			B = Integral(B);
			return B;
	}
	
	
	
	/// <summary>
	/// Вычисляет кратный интеграл по dx
	/// </summary>
	/// <param name="A">Входной вектор</param>
	/// <param name="k">Кратность 1, 2, 3 ....</param>
	/// <returns>Комплексный вектор</returns>
	public static ComplexVector Integral(ComplexVector A, int k)
	{
			ComplexVector B = A.Copy();
			for(int i = 0; i<k; i++)
			B = Integral(B);
			return B;
	}
	
	
	
	/// <summary>
	/// Вычисляет i-ю производную по dx
	/// </summary>
	/// <param name="A">Входной вектор</param>
	/// <param name="i">Порядок производной 1, 2, 3 ....</param>
	/// <returns>Комплексный вектор</returns>
	public static ComplexVector Diff(ComplexVector A, int i)
	{
			ComplexVector B = A.Copy();
			for(int j = 0; j<i; j++)
			B = Diff(B);
			return B;
	}
	
	
	/// <summary>
	/// Вычисляет i-ю производную по dx
	/// </summary>
	/// <param name="A">Входной вектор</param>
	/// <param name="i">Порядок производной 1, 2, 3 ....</param>
	/// <returns>Действительный вектор</returns>
	public static Vector Diff(Vector A, int i)
	{
			Vector B = A.Copy();
			for(int j = 0; j<i; j++)
			B = Diff(B);
			return B;
	}
	
	
	
	
	
	
	/// <summary>
	/// Вычисляет интегральную функцию комплексный вектор
	/// </summary>
	/// <returns></returns>
	public static ComplexVector Integral(ComplexVector A)
	{
		int kRasshR = 2;
		ComplexVector B = new ComplexVector(A.N), C, D = A.InterpolayrZero(kRasshR);
		
		for(int i = 0; i<A.N; i++)
		{
			C = A.CutAndZero(i);
			B.Vecktor[i] = Summ(C);
		}
			
		return B;
	}
	
	
	#endregion
	
	
	
	
	
	#region Перемножение
	/// <summary>
	/// Перемножение всех элементов массива типа double
	/// </summary>
	public static double Multiplication(double[] mass)
	{
		double multipl = 1;
		
		for(int i = 0; i<mass.Length; i++)
			multipl *= mass[i];
		
		return multipl;
	}


		
	/// <summary>
	/// Перемножение всех элементов действительного вектора
	/// </summary>
	public static double Multiplication(Vector vect)
	{
		int n = vect.N;
		double multipl = 1;
		
		
		for(int i = 0; i<n; i++)
			multipl *= vect.Vecktor[i];
		
		return multipl;
	}
	
	
	/// <summary>
	/// Перемножение всех элементов массива типа int
	/// </summary>
	public static int Multiplication(int[] mass)
	{
		int multipl = 1;
		
		for(int i = 0; i<mass.Length; i++)
			multipl *= mass[i];
		
		return multipl;
	}
	
	#endregion
	
	
	
	
	
	
	
	}
}
