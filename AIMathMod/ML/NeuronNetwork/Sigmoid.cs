/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 17.01.2018
 * Время: 17:06
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.AdditionalFunctions;
using System;

namespace AI.MathMod.ML.NeuronNetwork
{
    /// <summary>
    /// Description of Sigmoid.
    /// </summary>
    [Serializable]
    public class Sigmoid : FullConLayerBase
    {
        /// <summary>
        /// Слой с исгмоидальной активацией
        /// </summary>
        /// <param name="inp">Кол-во входов</param>
        /// <param name="outp">Кол-во выходов(нейронов)</param>
        public Sigmoid(int inp, int outp)
        {
            SetParam(inp, outp);
        }

        /// <summary>
        /// Слой с исгмоидальной активацией
        /// </summary>
        /// <param name="neuronCount">Кол-во нейронов</param>
        public Sigmoid(int neuronCount)
        {
            OutputLayer = new Vector(neuronCount);
            SizeOut = neuronCount;
        }

        /// <summary>
        /// Функция активации
        /// </summary>
        /// <param name="inp">Выход линейного слоя</param>
        public override Vector FActivation(Vector inp)
        {
            return NeuroFunc.Sigmoid(inp);
        }

        /// <summary>
        /// Производная функции активации
        /// </summary>
        public override Vector DfDy()
        {
            Vector A = FActivation(OutputLayer);
            return A * (1 - A);
        }

    }
}
