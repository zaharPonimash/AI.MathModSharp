/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.11.2017
 * Время: 2:17
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.AdditionalFunctions;
using System;

namespace AI.MathMod.ML.NeuronNetwork
{

    /// <summary>
    /// Гиперболический тангенс
    /// </summary>
    [Serializable]
    public class FullBipolyareSigmoid : FullConLayerBase
    {
        /// <summary>
        /// Гиперболический тангенс
        /// </summary>
        public FullBipolyareSigmoid(int inp, int outp)
        {
            SetParam(inp, outp);
        }
        /// <summary>
        /// Гиперболический тангенс
        /// </summary>
        public FullBipolyareSigmoid(int neuronCount)
        {
            OutputLayer = new Vector(neuronCount);
            SizeOut = neuronCount;
        }

        /// <summary>
        /// Ф-я активации
        /// </summary>
        /// <param name="inp">Вход</param>
        public override Vector FActivation(Vector inp)
        {
            return 1.7159 * NeuroFunc.SigmoidBiplyar(inp, 2.0 / 3.0);
        }

        /// <summary>
        /// Производная ф-ии активации
        /// </summary>
        public override Vector DfDy()
        {
            Vector A = OutputLayer;
            return (1 + A) * (1 - A);
        }

    }
}
