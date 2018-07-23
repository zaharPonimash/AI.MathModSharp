
using System;
using AI.MathMod;
using System.Drawing;

namespace AI.MathMod
{
	/// <summary>
	/// Информационный блок, основной тип данных МАС
	/// </summary>
	public class InformationBlock
	{
		/// <summary>
		/// Текстовая информация
		/// </summary>
		public string[] TextInform{get; set;}
		/// <summary>
		/// Графическая информация
		/// </summary>
		public Bitmap[] ImageInform{get; set;}
		/// <summary>
		/// Информация в векторном виде
		/// </summary>
		public Vector[] SingnalInform{get; set;}
		/// <summary>
		/// Информация в матричном виде
		/// </summary>
		public Matrix[] MatrixInform{get; set;}
		/// <summary>
		/// Информация в тензорном виде
		/// </summary>
		public Tensor[] TensorInform{get; set;}
	}
}
