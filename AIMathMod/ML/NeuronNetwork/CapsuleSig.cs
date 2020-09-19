/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 17.01.2018
 * Время: 18:32
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.AdditionalFunctions;
using System;

namespace AI.MathMod.ML.NeuronNetwork
{
    /// <summary>
    /// Сигмоидальный капсульный слой
    /// </summary>
    [Serializable]
    public class CapsuleSig : CapsuleLinearLayer
    {
        /// <summary>
        /// Капсульный сигмоидальный слой
        /// </summary>
        public CapsuleSig(Capsule[] caps)
        {
            Init(caps);
        }

        /// <summary>
        /// Ф-я активации
        /// </summary>
        public override Vector FActivation(Vector inp)
        {
            return NeuroFunc.Sigmoid(inp, 1);
        }

        /// <summary>
        /// Производная ф-ии акт
        /// </summary>
        public override Vector DfDy()
        {
            return OutputLayer * (1 - OutputLayer);
        }
    }
}
