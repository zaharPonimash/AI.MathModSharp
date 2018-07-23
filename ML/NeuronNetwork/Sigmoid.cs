/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 17.01.2018
 * Время: 17:06
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Description of Sigmoid.
	/// </summary>
	[Serializable]
	public class Sigmoid:FullConLayerBase
	{
		public Sigmoid(int inp, int outp)
		{
			SetParam(inp, outp);
		}
		
		public Sigmoid(int neuronCount)
		{
			OutputLayer = new Vector(neuronCount);
			SizeOut = neuronCount;
		}
		
		public override Vector FActivation(Vector inp)
		{
			return NeuroFunc.Sigmoid(inp);
		}
		
		
		public override Vector DfDy()
		{
			Vector A = FActivation(OutputLayer);
			return OutputLayer*(1-OutputLayer);
		}
		
	}
}
