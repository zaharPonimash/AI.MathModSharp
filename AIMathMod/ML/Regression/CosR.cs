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
	public class CosRegression
	{
		
		MultipleRegression mR;
		int _cos;
		
		/// <summary>
		/// Регрессия по косинусам
		/// </summary>
		/// <param name="inp">Вектор входа</param>
		/// <param name="outp">Вектор входа</param>
		/// <param name="cos"></param>
		public CosRegression(Vector inp, Vector outp, int cos = 3)
		{
			_cos = cos;
			Vector[] vects = new Vector[inp.N];
			
			for (int i = 0; i < inp.N; i++)
			{
				vects[i] = ExtensionOfFeatureSpace.SinCos(inp[i], cos);
				vects[i] = vects[i].AddOne();
			}
			
			mR = new MultipleRegression(vects, outp);
		}
		
		
		/// <summary>
		/// Прогноз
		/// </summary>
		/// <param name="inp">Значение незав. переменной</param>
		public double Predict(double inp)
		{
			Vector X = ExtensionOfFeatureSpace.SinCos(inp, _cos);
			return mR.Predict(X.AddOne());
		}
		
		/// <summary>
		/// Прогноз
		/// </summary>
		/// <param name="vect">Значения незав. переменных</param>
		public Vector Predict(Vector vect)
		{
			Vector outp = new Vector(vect.N);
			
			for (int i = 0; i < vect.N; i++) outp[i] = Predict(vect[i]);
			
			return outp;
		}
		
	}
}

