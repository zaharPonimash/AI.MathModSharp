/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.11.2017
 * Время: 2:17
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ML.NeuronNetwork
{
	[Serializable]
	public class FullBipolyareSigmoid : FullConLayerBase
	{
		public FullBipolyareSigmoid(int inp, int outp)
		{
			SetParam(inp, outp);
		}
		
		public FullBipolyareSigmoid(int neuronCount)
		{
			OutputLayer = new Vector(neuronCount);
			SizeOut = neuronCount;
		}
		
		public override Vector FActivation(Vector inp)
		{
			return 1.7159*NeuroFunc.SigmoidBiplyar(inp, 2.0/3.0);
		}
		
		
		public override Vector DfDy()
		{
			Vector A = OutputLayer;
			return (1+A)*(1-A);
		}
		
	}
}
