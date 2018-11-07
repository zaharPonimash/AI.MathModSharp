/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 28.07.2018
 * Время: 13:41
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Полносвязный слой
	/// </summary>
	[Serializable]
	public class FullConLayerBase : ILayer
	{
		public int SizeOut {
			get;
			set;
		}

		
		public Matrix W;
		protected Vector Inp; 
		public virtual Vector Delts {get; set;}
		public Vector OutputLayer{get; protected set;}
		public double norm;
		public double Eps{set; get;}
		public double moment;
		protected Matrix Last;
		
		
		public FullConLayerBase()
		{	
		}
		
		public FullConLayerBase(int inp, int outp)
		{
			SetParam(inp, outp);
		}

		
		public virtual void SetParam(int inp, int outp)
		{
			Inp = new Vector(inp);
			OutputLayer = new Vector(outp);
			SizeOut = outp;
			norm = 0.9/(double)(inp);
			moment = 0;
			W = 0.0001*Statistic.randNorm(inp, outp)/inp;
			Last = new Matrix(inp, outp);
		}
		
		#region ILayer implementation

		public virtual Vector Output(Vector input)
		{
			Inp = new Vector(input.N);
			
			for (int i = 0; i < input.N; i++)
				Inp[i] = input[i];
			
			OutputLayer = FActivation(Inp*W);
			return OutputLayer;
		}

		public virtual Vector Backwards()
		{
			return Delts*W.Tr();
		}


		public virtual Vector FActivation(Vector inp)
		{
			return inp;
		}
	
		
		
		
		public virtual Vector DfDy()
		{
			return new Vector(OutputLayer.N) +1;
		}

		public void DeltH(ILayer layer)
		{
			Delts = layer.Backwards();
		}
		
		public virtual void Train()
		{
			for (int i = 0; i < OutputLayer.N; i++) 
				for (int j = 0; j < Inp.N; j++)
			{
				double c = moment*Last.Matr[j,i] + norm*Inp[j]*Delts[i];
				W.Matr[j,i] -= c;
				Last.Matr[j,i] = c;
			}
		}
		
		
		
		
	
		public void WGenerate(Random rnd)
		{
			W = 0.0001*Statistic.randNorm(W.M, W.N, rnd)/W.N;
		}
		
		
		public virtual void Delt(Vector ideal)
		{
			Delts = DfDy()*(OutputLayer-ideal);//ideal.N;
			
			Eps = 0.5*Functions.Summ((OutputLayer-ideal)*(OutputLayer-ideal))/ideal.N;
		}
		
		#endregion
	}
}
