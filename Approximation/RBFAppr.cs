/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 13.09.2018
 * Время: 13:08
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.Algebra;
using AI.MathMod.ML.Datasets;

namespace AI.MathMod.Approximation
{
	/// <summary>
	/// Description of RBFAppr.
	/// </summary>
	public class RBFAppr
	{
		
		public Vector newX;
		double min, max, sig;
		Vector Y, X, param;
		
		public RBFAppr(Vector x, Vector y)
		{
			min = x[0];
			max = x[x.N-1];
			X = x.Copy();
			Y = y.Copy();
			sig = (max-min)/x.N;
			Param();
		}
		
		
		public double Predict(double inp)
		{
			Vector data = ExtensionOfFeatureSpace.GaussRBF(inp, X, sig);
			double outp = GeomFunc.ScalarProduct(data, param);
			return outp;
		}
		
		
		public Vector Prediction(double step)
		{
			newX = MathFunc.GenerateTheSequence(min, step, max);
			Vector y = new Vector(newX.N);
			
			for (int i = 0; i < newX.N; i++)
			{
				y[i] = Predict(newX[i]);
			}
			
			return y;
		}
		
		
		
		void Param()
		{
			var A = new Matrix(X.N, X.N);
			
			Vector[] vect = new Vector[X.N];
			for (int i = 0; i < X.N; i++) 
				vect[i] = ExtensionOfFeatureSpace.GaussRBF(X[i], X, sig);
			
			for (int i = 0; i < X.N; i++) {
				for (int j = 0; j < X.N; j++) {
					A[i,j] = vect[i][j];
				}
			}
			
			Kramer kram = new Kramer();
			
			param = kram.GetAnswer(A, Y);
		}
		
	}
}
