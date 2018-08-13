/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 10.08.2018
 * Время: 14:07
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.ML.Datasets;


namespace AI.MathMod.ML.Regression
{
	/// <summary>
	/// Description of PolynomialRegression.
	/// </summary>
	public class PolynomialRegression
	{
		
		MultipleRegression mR;
		int _nPoly;
		
		public PolynomialRegression(Vector inp, Vector outp, int nPoly = 3)
		{
			_nPoly = nPoly;
			Vector[] vects = new Vector[inp.N];
			
			for (int i = 0; i < inp.N; i++) 
				vects[i] = ExtensionOfFeatureSpace.Cos(inp[i], nPoly);
			
			mR = new MultipleRegression(vects, outp.Vecktor);
		}
		
		
		
		public double Predict(double inp)
		{
			Vector X = ExtensionOfFeatureSpace.Cos(inp, _nPoly);
			return mR.Predict(X);
		}
		
		
		public Vector Predict(Vector vect)
		{
			Vector outp = new Vector(vect.N);
			
			for (int i = 0; i < vect.N; i++) outp[i] = Predict(vect[i]);
			
			return outp;
		}
		
	}
}
