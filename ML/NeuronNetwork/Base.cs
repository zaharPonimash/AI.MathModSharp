/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.11.2017
 * Время: 1:47
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Слой нейронной сети
	/// </summary>
	public interface ILayer
	{ 
		int SizeOut{set; get;}
		
		double Eps{set; get;}
		
		//Дельты
		Vector Delts{set; get;}
		
		// Прямой проход сети
		Vector Output(Vector input);
		
		// Ошибка на выходе
		void Delt(Vector ideal);
		
		void DeltH(ILayer layer);
		
		// Обратный проход
		Vector Backwards();
		
		void Train();
		
		void SetParam(int inp, int outp);		
	}
	
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
		protected double norm;
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
			W = Statistic.randNorm(inp, outp)/inp;
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
				double c = moment*Last.Matr[j,i] + (1-moment)*norm*Inp[j]*Delts[i];
				W.Matr[j,i] -= c;
				Last.Matr[j,i] = c;
			}
		}
		
		
		
		
	
		
		
		
		public virtual void Delt(Vector ideal)
		{
			Delts = DfDy()*(OutputLayer-ideal);//ideal.N;
			
			Eps = 0.5*Functions.Summ((OutputLayer-ideal)*(OutputLayer-ideal))/ideal.N;
		}
		
		#endregion
	}
	
	/// <summary>
	/// Нейронная сеть
	/// </summary>
	[Serializable]
	public class Net
	{
		public List<ILayer> _layers = new List<ILayer>();
		int countNeuronsForLastLayer;
		
		public Net()
		{
			
		}
		
		public Net(string path)
		{
			Open(path);
		}
		
		
		public void Add(ILayer layer)
		{
			if(_layers.Count == 0||layer is CapsuleLinearLayer) _layers.Add(layer);
			else
			{
				int inp = _layers[_layers.Count-1].SizeOut;
				layer.SetParam(inp, layer.SizeOut);
				_layers.Add(layer);
			}
			
			
				countNeuronsForLastLayer = layer.SizeOut;
		}
		
		
		
		public Vector Output(Vector input)
		{
			Vector outp = _layers[0].Output(input);
			
			for (int i = 1; i < _layers.Count; i++) 
				outp = _layers[i].Output(outp);
			
			return outp;
		}
		
		
		public double TrainClassifier(Vector inp, int outp)
		{
			Vector output = new Vector(countNeuronsForLastLayer);
			output[outp] = 1;
			Output(inp);
			_layers[_layers.Count-1].Delt(output);
			
			
			for (int i = _layers.Count-2; i >= 0; i--)
				_layers[i].DeltH(_layers[i+1]);
			
			
			for (int i = 0; i < _layers.Count; i++) 
				_layers[i].Train();
			
			return _layers[_layers.Count-1].Eps;
		}
		
		
		
		public double Train(Vector inp, Vector output)
		{
			Output(inp);
			_layers[_layers.Count-1].Delt(output);
			
			
			for (int i = _layers.Count-2; i >= 0; i--)
				_layers[i].DeltH(_layers[i+1]);
			
			
			for (int i = 0; i < _layers.Count; i++) 
				_layers[i].Train();
			
			return _layers[_layers.Count-1].Eps;
		}
		
		/// <summary>
		/// Сохранение матрицы
		/// </summary>
		/// <param name="path">Путь до файла</param>
		public void Save(string path)
		{
			try
			{
		
				BinaryFormatter binFormat = new BinaryFormatter();
			
				using(Stream fStream = new FileStream(path,
      			FileMode.Create, FileAccess.Write, FileShare.None))
				{
				 	binFormat.Serialize(fStream, this);
				}
			}
			
			catch
			{
				throw new ArgumentException("Ошибка сохранения", "Сохранение");
			}
				
		}
		
		
		
		/// <summary>
		/// Загрузка матрицы
		/// </summary>
		/// <param name="path">Путь до файла</param>		
		public void Open(string path)
		{
			
			try{
			Net net;
			 BinaryFormatter binFormat = new BinaryFormatter();
			
			using(Stream fStream = new FileStream(path,
      		FileMode.Open, FileAccess.Read, FileShare.None))
			{
			 	net =(Net)binFormat.Deserialize(fStream);
			}
			
			 _layers = net._layers;
			 
			 countNeuronsForLastLayer = net.countNeuronsForLastLayer;
			 
			}
			
			catch
			{
				throw new ArgumentException("Ошибка загрузки", "Загрузка");
			}
			
		}
		
	}
}
