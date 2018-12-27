/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 22.11.2018
 * Время: 22:38
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.ML.Regression;



namespace AI.MathMod.ML.Classifire
{
	/// <summary>
	/// Description of LogisticRegression.
	/// </summary>
	public class LogisticRegression
	{
		MultipleRegression _lr;
		public Vector t;
		
		public LogisticRegression(Vector x, bool[] y)
		{
			t = new Vector(y.Length);
			Vector[] vecs = new Vector[x.N];
			
			for (int i = 0; i < t.N; i++)
			{
				t[i] = y[i]? 4.6: -4.6;
				
				vecs[i] = new Vector
					(
						new double[]
						{
							1, x[i]
						}
					);
			}
			t*=30;
			
			_lr = new MultipleRegression(vecs,t.Vecktor);
			
			
			
		}
		
		
		
		public LogisticRegression(Vector[] x, bool[] y)
		{
			t = new Vector(y.Length);
			Vector[] vecs = new Vector[x.Length];
			
			for (int i = 0; i < t.N; i++)
			{
				t[i] = y[i]? 8: -8;
				
				vecs[i] = x[i].AddOne();
			}
			
			t*=3000;
			
			_lr = new MultipleRegression(vecs,t.Vecktor);	
		}
		
		
		
		public double Recognition(Vector x)
		{	
			double outp = _lr.Predict(x.AddOne());
			return NeuroFunc.Sigmoid(outp);
		}
		
		
		
		public Vector RecognitionAll(Vector x)
		{
			
			Vector[] vecs = new Vector[x.N];
			
			for (int i = 0; i < x.N; i++)
			{
					vecs[i] = new Vector
					(
						new double[]
						{
							1, x[i]
						}
					);
			}
			
			
			Vector outp = _lr.Predict(vecs);
			
			return NeuroFunc.Sigmoid(outp);
		}
		
	}
}
