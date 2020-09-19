/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 13.08.2018
 * Время: 0:13
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */

namespace AI.MathMod.ML.Regression
{
    /// <summary>
    /// Description of ExpMean.
    /// </summary>
    public class ExpMean
    {
        private readonly Vector _inp;
        private double old;
        private readonly double _oldPart;

        /// <summary>
        /// Прогнозирование на основе скользящего среднего
        /// </summary>
        /// <param name="inp">Вход</param>
        /// <param name="oldPart">Старая часть(коэф. сглаживания)</param>
        public ExpMean(Vector inp, double oldPart = 0.9)
        {
            _inp = inp;
            _oldPart = oldPart;
            GetOld();
        }

        /// <summary>
        /// Прогноз
        /// </summary>
        public double Predict(double lastSempl)
        {
            old = old * _oldPart + (1 - _oldPart) * lastSempl;
            return old;
        }

        /// <summary>
        /// Прогноз
        /// </summary>
        public Vector Predict(int n)
        {
            Vector vect = new Vector(n);

            vect[0] = Predict(_inp[_inp.N - 1]);

            for (int i = 1; i < n; i++)
            {
                vect[i] = Predict(vect[i - 1]);
            }

            return vect;
        }

        private void GetOld()
        {
            old = _inp[0];

            for (int i = 1; i < _inp.N; i++)
            {
                old = _oldPart * old + (1 - _oldPart) * _inp[i];
            }
        }

    }
}
