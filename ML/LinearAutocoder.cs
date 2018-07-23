/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.11.2017
 * Время: 1:35
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.ML.NeuronNetwork;

namespace AI.MathMod.ML
{
	/// <summary>
	/// Линейный автокодировщик
	/// </summary>
	public class LinearAutocoder
	{
		Net net = new Net();
		LinearLayer ll;
		public Matrix Coder{get; set;}
		
		public LinearAutocoder(int inputs, int outps)
		{
			ll = new LinearLayer(inputs, outps);
			net.Add(ll);
			net.Add(new FullBipolyareSigmoid(inputs));
		}
		
		public double Train(Vector input)
		{
			Vector inp = input/Statistic.MaximalValue(input);
			return	net.Train(inp, inp);
		}
		
		
		
		public Vector Output(Vector input)
		{
			Vector outp = input*Coder;
			return outp;
		}
		
		
		public Vector Reconstruct(Vector vect)
		{
			return vect*Coder.Tr();
		}
		
		
		public void GenerateCoderMatrix()
		{
			Coder = ll.W;
			Vector matrixData = Coder.Spagetiz();
			double en = Statistic.Dispers(matrixData);
			Coder /= en;
		}
		
		
	}
	
	
	

	
	
	
}
