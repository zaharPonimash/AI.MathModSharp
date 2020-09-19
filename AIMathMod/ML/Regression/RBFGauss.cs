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
    public class RBFGauss
    {
        private Matrix A;
        /// <summary>
        /// Параметры
        /// </summary>
        public Vector param;
        private readonly int len, len05, n;
        private readonly Vector newX, newY;
        private readonly double sigma, max, min;

        /// <summary>
        /// Регрессия
        /// </summary>
        /// <param name="X">Вектор входа</param>
        /// <param name="Y">Вектор выхода</param>
        /// <param name="nRBF">Кол-во ядер</param>
        public RBFGauss(Vector X, Vector Y, int nRBF)
        {

            n = nRBF;
            len = X.N / n;
            len05 = len / 2;
            max = Statistic.MaximalValue(X);
            min = Statistic.MinimalValue(X);

            sigma = (max - min) / (nRBF);

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
            {
                vect[i] = ExtensionOfFeatureSpace.GaussRBF(newX[i], newX, sigma);
            }

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
            Vector X = ExtensionOfFeatureSpace.GaussRBF(inp, newX, sigma);
            return GeomFunc.ScalarProduct(X, param);
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

