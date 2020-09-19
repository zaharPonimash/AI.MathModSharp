/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 10.08.2018
 * Время: 17:20
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.AdditionalFunctions;
using AI.MathMod.Algebra;
using AI.MathMod.ML.Datasets;

namespace AI.MathMod.ML.Regression
{
    /// <summary>
    /// Description of RegressionLagrang.
    /// </summary>
    public class SincRegression
    {
        private Matrix A;
        private Vector param;
        private readonly int len, len05, n;
        private readonly Vector newX, newY;

        /// <summary>
        /// Регрессия на базе синуса котельникова
        /// </summary>
        public SincRegression(Vector X, Vector Y, int nRBF)
        {

            n = nRBF;
            len = X.N / n;
            len05 = len / 2;

            newX = new Vector(n);
            newY = new Vector(n);


            for (int i = len05, k = 0; i < X.N - 1; i += len)
            {
                newX[k] = Statistic.ExpectedValue(X.GetInterval(i - len05, i + len05));
                newY[k] = Statistic.ExpectedValue(Y.GetInterval(i - len05, i + len05));
                k++;
            }

            Param();

        }

        private void Param()
        {
            A = new Matrix(n, n);

            Vector[] vect = new Vector[n];
            for (int i = 0; i < n; i++)
                vect[i] = ExtensionOfFeatureSpace.Sinc(newX[i], newX);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = vect[i][j];
                }
            }

            Kramer kram = new Kramer();

            param = kram.GetAnswer(A, newY);
        }

        /// <summary>
        /// Прогноз
        /// </summary>
        /// <param name="inp">Значение незав. переменной</param>
        public double Predict(double inp)
        {
            Vector X = ExtensionOfFeatureSpace.Sinc(inp, newX);
            return GeomFunc.ScalarProduct(X, param);
        }


        /// <summary>
        /// Прогноз
        /// </summary>
        /// <param name="vect">Значение незав. переменных</param>
        public Vector Predict(Vector vect)
        {
            Vector outp = new Vector(vect.N);

            for (int i = 0; i < vect.N; i++) outp[i] = Predict(vect[i]);

            return outp;
        }
    }
}

