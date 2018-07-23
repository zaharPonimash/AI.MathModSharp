/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 17.01.2018
 * Время: 18:32
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Сигмоидальный капсульный слой
	/// </summary>
	[Serializable]
	public class CapsuleSig: CapsuleLinearLayer
	{
		public CapsuleSig(Capsule[] caps)
		{
			Init(caps);
		}
		
		
		public override Vector FActivation(Vector inp)
		{
			return NeuroFunc.Sigmoid(inp, 1);
		}
		
		
		public override Vector DfDy()
		{
			return OutputLayer*(1-OutputLayer);
		}
	}
}
