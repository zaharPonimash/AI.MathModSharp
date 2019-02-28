
using System;
using AI.MathMod;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Description of Sofmax.
	/// </summary>
	[Serializable]
	public class Softmax : FullConLayerBase
	{
		/// <summary>
		/// Софтмакс
		/// </summary>
		public Softmax(int inp, int outp)
		{
			SetParam(inp, outp);
		}
		/// <summary>
		/// Софтмакс
		/// </summary>
		public Softmax(int neuronCount)
		{
			OutputLayer = new Vector(neuronCount);
			SizeOut = neuronCount;
		}
		
		/// <summary>
		/// Ф-я активации
		/// </summary>
		public override Vector FActivation(Vector inp)
		{
			Vector oupt = MathFunc.exp(inp);
			oupt /= Functions.Summ(oupt);
			return oupt;
		}
	}
}
