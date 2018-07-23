using System;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Description of CasuleLayer.
	/// </summary>
	[Serializable]
	public class CapsuleLinearLayer:FullConLayerBase
	{
		
		protected Capsule[] _capsules;
		protected double[] norms;
		
		
		public CapsuleLinearLayer(Capsule[] capsules)
		{
			Init(capsules);
		}
		
		public CapsuleLinearLayer(){}
		
		
		public virtual void Init(Capsule[] caps)
		{
			_capsules = caps;
			W = Capsule.GenerateMatrixW(caps);
			Inp = new Vector(W.M);
			OutputLayer = new Vector(W.N);
			SizeOut = W.N;
			norms = Capsule.GetNorms(_capsules);
			Last = new Matrix(W.M, W.N);
			norm = 0.1/(W.M+W.N);
		}
		
		
		public override void Train()
		{
			for (int i = 0; i < OutputLayer.N; i++) 
				for (int j = 0; j < Inp.N; j++)
			{
				double c = (W[j,i] == 0)? 0:moment*Last[j,i] + (1-moment)*norms[i]*Inp[j]*Delts[i];
				W[j,i] -= c;
				Last.Matr[j,i] = c;
			}
		}
	}
	
	/// <summary>
	/// Капсула
	/// </summary>
	[Serializable]
	public class Capsule
	{
		public int inputStartInterval;
		public int inputEndInterval;
		public int neuronCount;
		public double norm;
		
		
		/// <summary>
		/// Капсула 
		/// </summary>
		/// <param name="isi">Индекс начала</param>
		/// <param name="iei">Индекс окончание</param>
		/// <param name="nc">Количество нейронов в капсуле</param>
		public Capsule(int isi, int iei, int nc)
		{
			inputStartInterval = isi;
			inputEndInterval = iei;
			neuronCount = nc;
			norm = .03/(iei-isi);
		}
		
		// Нормы обучения
		public static double[] GetNorms(Capsule[] capsules)
		{
			var matrixNdim = 0;
			for (int i = 0; i <capsules.Length; i++)
				matrixNdim += capsules[i].neuronCount;
			
			var norms = new double[matrixNdim];
			
			for (int i = 0, k = 0, acc = 0; i < matrixNdim; i++)
			{
				if (i < acc+capsules[k].neuronCount)
					norms[i] = capsules[k].norm;
				else
				{
					acc += capsules[k].neuronCount;
					k++;
				}
				
			}
			
			
			return norms;
		}
		
		// Генерарация весов по капсулам
		public static Matrix GenerateMatrixW(Capsule[] capsules)
		{
			var rnd = new Random();
			var matrixNdim = 0;
			var sempl = 0.0;
			var scale = 0.0;
			
			for (int i = 0; i <capsules.Length; i++)
				matrixNdim += capsules[i].neuronCount;
			
			var w = new Matrix
			(capsules[capsules.Length-1].inputEndInterval+1, matrixNdim);
			
			
			// Заполнение весами
			for (int i = 0, k = 0, acc = 0; i < w.M; i++)
				for (int j = 0; j < w.N; j++)
			{
				if (j < acc+capsules[k].neuronCount)
				{
					sempl = 2*rnd.NextDouble()-1;
					scale = 1.0/(capsules[k].inputEndInterval-capsules[k].inputStartInterval);
					
					if(i>=capsules[k].inputStartInterval && i<=capsules[k].inputEndInterval)
						w[i,j] = (sempl != 0)? sempl*scale: rnd.NextDouble()*scale;
					
				}
				else
				{
						acc += capsules[k].neuronCount;
						k++;
						sempl = 2*rnd.NextDouble()-1;
						scale = 1.0/(capsules[k].inputEndInterval-capsules[k].inputStartInterval);
					
						if(i>=capsules[k].inputStartInterval && i<=capsules[k].inputEndInterval)
						w[i,j] = (sempl != 0)? sempl*scale: rnd.NextDouble()*scale;
				}
			}
			
			
			return w;
		}
		
		
		
		
	}
}
