/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.11.2017
 * Время: 1:35
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.ML.NeuronNetwork;

namespace AI.MathMod.ML
{
    /// <summary>
    /// Линейный автокодировщик
    /// </summary>
    public class LinearAutocoder
    {
        private readonly Net net = new Net();
        private readonly LinearLayer ll;
        /// <summary>
        /// Матрица отображения пространств
        /// </summary>
        public Matrix Coder { get; set; }

        /// <summary>
        /// Линейный автокодировщик
        /// </summary>
        /// <param name="inputs">Размерность исходного пространства</param>
        /// <param name="outps">Размерность нового пространства</param>
        public LinearAutocoder(int inputs, int outps)
        {
            ll = new LinearLayer(inputs, outps);
            net.Add(ll);
            net.Add(new FullBipolyareSigmoid(inputs));
        }

        /// <summary>
        /// Обучение
        /// </summary>
        /// <param name="input">Вектор входа/выхода</param>
        /// <returns>Ошибка MSE</returns>
        public double Train(Vector input)
        {
            Vector inp = input / Statistic.MaximalValue(input);
            return net.Train(inp, inp);
        }


        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="input">Вход</param>
        public Vector Output(Vector input)
        {
            Vector outp = input * Coder;
            return outp;
        }

        /// <summary>
        /// Восстановление вектора
        /// </summary>
        /// <param name="vect"></param>
        /// <returns></returns>
        public Vector Reconstruct(Vector vect)
        {
            return vect * Coder.Tr();
        }

        /// <summary>
        /// Генерация матрицы(нормированной)
        /// </summary>
        public void GenerateCoderMatrix()
        {
            Coder = ll.W;
            Vector matrixData = Coder.Spagetiz();
            double en = Statistic.Std(matrixData);
            Coder /= en;
        }


    }







}
