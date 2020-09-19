/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 12.08.2018
 * Время: 1:09
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.ML.Datasets;

namespace AI.MathMod.ML.Regression
{
    /// <summary>
    /// Description of PolinomialRegressionNNW.
    /// </summary>
    public class PolinomialRegressionNNW

    {
        private readonly MultipleRegressionNNW mR;
        private readonly int _nPoly;
        /// <summary>
        /// Полиноминаяльная регрессия нейросеть
        /// </summary>
        public PolinomialRegressionNNW(Vector inp, Vector outp, int nPoly = 3)
        {
            _nPoly = nPoly;
            Vector[] vects = new Vector[inp.N];

            for (int i = 0; i < inp.N; i++)
            {
                vects[i] = ExtensionOfFeatureSpace.Polinomial(inp[i], nPoly);
            }

            mR = new MultipleRegressionNNW(vects, outp);
        }

        /// <summary>
        /// Обучение
        /// </summary>
        public void Train(double ep = 1)
        {
            mR.Train(ep);
        }

        /// <summary>
        /// Прогноз
        /// </summary>
        /// <param name="inp">Значение незав. переменной</param>
        public double Predict(double inp)
        {
            Vector X = ExtensionOfFeatureSpace.Polinomial(inp, _nPoly);
            return mR.Predict(X);
        }

        /// <summary>
        /// Прогноз
        /// </summary>
        /// <param name="vect">Значения незав. переменных</param>
        public Vector Predict(Vector vect)
        {
            Vector outp = new Vector(vect.N);

            for (int i = 0; i < vect.N; i++)
            {
                outp[i] = Predict(vect[i]);
            }

            return outp;
        }

    }

}
