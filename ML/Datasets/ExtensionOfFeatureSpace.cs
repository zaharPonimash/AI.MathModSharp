/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 09.08.2018
 * Время: 2:01
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.ML.Regression;

namespace AI.MathMod.ML.Datasets
{
	/// <summary>
	/// Расширение пространства признаков
	/// </summary>
	public static class ExtensionOfFeatureSpace
	{
		public static Vector Polinomial(double x, int n = 2)
		{
			Vector outp = new Vector(n+1);
			
			outp[0] = 1;
			
			if(n >= 1)
			{
				outp[1] = x;
			
				for (int i = 2; i <= n; i++)
					outp[i] = Math.Pow(x, i);
			
			}
			
			return outp;
		}
		
		
		public static Vector Polinomial(Vector inp, int n = 2)
		{
			Vector[] vectors = new Vector[n+1];
		
			vectors[0] = new Vector(1.0);
			
			for (int i = 1; i <= n; i++)
			{
				vectors[i] = inp^i;
			}
			
			
			return Vector.Concatinate(vectors);
		}
		
		
		public static Vector Cos(double x, int n = 2)
		{
			Vector outp = new Vector(n+1);
			
				for (int i = 0; i <= n; i++)
					outp[i] = Math.Cos(x*i);
			
			return outp;
		}
		
		
		public static Vector Sin(double x, int n = 2)
		{
			Vector outp = new Vector(n);
			
				for (int i = 0; i <= n; i++)
					outp[i] = Math.Sin(x*i);
			
			return outp;
		}
		
		
		public static Vector SinCos(double x, int n = 2)
		{
			return Vector.Concatinate(new Vector[]{Sin(x, n), Cos(x,n), new Vector(new double[]{x})});
		}
		
		
		public static Vector Cos(Vector inp, int n = 2)
		{
			Vector[] vectors = new Vector[n+1];
		
			vectors[0] = new Vector(1.0);
			
			for (int i = 1; i <= n; i++)
			{
				vectors[i] = MathFunc.cos(inp*i);
			}
			
			
			return Vector.Concatinate(vectors);
		}
		
		
		
		public static Vector BigDim(double x, int nPolinom = 3, int nCos = 3)
		{
			Vector vect = Cos(x, nCos);
			return Polinomial(vect, nPolinom);
		}
		
		public static Vector BigDim(Vector x, int nPolinom = 3, int nCos = 3)
		{
			Vector vect = Cos(x, nCos);
			return Polinomial(vect, nPolinom);
		}
		
		public static Vector PoliCos(Vector x, int nPolinom = 3, int nCos = 3)
		{
			Vector[] vects = new Vector[2];
			vects[0] = Polinomial(x, nPolinom);
			vects[1] = Cos(x, nCos);
			
			return Vector.Concatinate(vects);
		}
		
		public static Vector PoliCos(double x, int nPolinom = 3, int nCos = 3)
		{
			Vector[] vects = new Vector[2];
			vects[0] = Polinomial(x, nPolinom);
			vects[1] = Cos(x, nCos);
			
			return Vector.Concatinate(vects);
		}
		
		
		public static Vector GaussRBF(double x, Vector centers, double sigm = 1)
		{
			Vector outp = new Vector(centers.N);
			double r = 0;
			
			for (int i = 0; i < centers.N; i++)
			{
				r = Math.Pow((centers[i]-x), 2)/(2*sigm*sigm);
				outp[i] = Math.Exp(-r);
			}
			
			return outp;
		}
		
		
		
		
		public static Vector Sinc(double x, Vector centers)
		{
			Vector outp = new Vector(centers.N);
			double r = 0;
			
			for (int i = 0; i < centers.N; i++)
			{
				r = (x-centers[i]);
				outp[i] = Math.Sin(r)/r;
				outp[i] = r < 1e-3? 1: outp[i];
			}
			
			return outp;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataset"></param>
		/// <returns></returns>
		public static Vector ImportanceSign(Vector[] dataset)
		{
			Vector dispers = Statistic.EnsembleDispersion(dataset);
			double m = Statistic.ExpectedValue(dispers);
			double std = Statistic.Sco(dispers);
			Vector Y = DistributionFunc.GaussNorm1(dispers, m, std);
			Vector X = MathFunc.GenerateTheSequence(0, 1, Y.N);
			var regr = new RBFGauss(X, Y, 25);
			return  regr.Predict(X);
		}
		
	}
	
}
