/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.11.2017
 * Время: 1:51
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.ML.NeuronNetwork
{
    /// <summary>
    /// Линейный слой
    /// </summary>
    [Serializable]
    public class LinearLayer : FullConLayerBase
    {
        /// <summary>
        /// Линейный слой
        /// </summary>
        public LinearLayer(int neuronCount)
        {
            OutputLayer = new Vector(neuronCount);
            SizeOut = neuronCount;
        }

        /// <summary>
        /// Линейный слой
        /// </summary>
        public LinearLayer(int inp, int outp)
        {
            SetParam(inp, outp);
        }


    }
}
