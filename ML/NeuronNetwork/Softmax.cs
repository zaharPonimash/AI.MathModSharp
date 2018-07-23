
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
		public Softmax(int inp, int outp)
		{
			SetParam(inp, outp);
		}
		
		public Softmax(int neuronCount)
		{
			OutputLayer = new Vector(neuronCount);
			SizeOut = neuronCount;
		}
		
		
		public override Vector FActivation(Vector inp)
		{
			Vector oupt = MathFunc.exp(inp);
			oupt /= Functions.Summ(oupt);
			return oupt;
		}
	}
}
