/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 30.03.2018
 * Время: 21:20
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Description of CapsulePRelu.
	/// </summary>
	[Serializable]
	public class CapsuleRelu : CapsuleLinearLayer
	{
		/// <summary>
		/// Капсульный релу слой
		/// </summary>
		/// <param name="caps"></param>
		public CapsuleRelu(Capsule[] caps)
		{
			
			Init(caps);
		}
		
		/// <summary>
		/// Ф-я активации
		/// </summary>
		/// <param name="inp">Вход</param>
		/// <returns></returns>
		public override Vector FActivation(Vector inp)
		{
			return NeuroFunc.Relu(inp, 0.0);
		}
		
		/// <summary>
		/// Производная ф-ии активации
		/// </summary>
		public override Vector DfDy()
		{
			return NeuroFunc.Relu(OutputLayer, 0.0);
		}
	}
}
