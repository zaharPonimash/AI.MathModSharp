/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 03.06.2017
 * Время: 16:10
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System.Numerics;

namespace AI.MathMod.Signals
{
    /// <summary>
    /// Description of Hilbert.
    /// </summary>
    public static class Hilbert
    {
        /// <summary>
        /// Сигнал сопряженный по Гильберту
        /// </summary>
        /// <param name="st">Исходный сигнал</param>
        public static Vector ConjugateToTheHilbert(Vector st)
        {
            ComplexVector cv = Furie.DPF(st);
            Complex j = new Complex(0, 1);
            int n1 = st.N / 2, n2 = st.N;

            //cv.RealToVector().Visual();
            for (int i = 0; i < n1; i++)
            {
                cv.DataInVector[i] = cv.DataInVector[i] * (-j);
            }


            for (int i = n1; i < n2; i++)
            {
                cv.DataInVector[i] = cv.DataInVector[i] * j;
            }

            cv = Furie.ODPF(cv);
            return cv.RealToVector();
        }


        /// <summary>
        /// Аналитический сигнал
        /// </summary>
        /// <param name="st">Входной сигнал</param>
        public static ComplexVector GetAnalSig(Vector st)
        {
            ComplexVector cv = new ComplexVector(st.N);
            Vector stH = ConjugateToTheHilbert(st);

            for (int i = 0; i < st.N; i++)
            {
                cv.DataInVector[i] = new Complex(st.DataInVector[i], stH.DataInVector[i]);
            }

            return cv;
        }


        /// <summary>
        /// Огибающая
        /// </summary>
        /// <param name="st">Входной сигнал</param>
        public static Vector Ogib(Vector st)
        {
            return GetAnalSig(st).MagnitudeToVector();
        }

        /// <summary>
        /// Мгновенная фаза
        /// </summary>
        /// <param name="st">Входной сигнал</param>
        public static Vector Phase(Vector st)
        {
            return GetAnalSig(st).PhaseToVector();
        }

        /// <summary>
        /// Мгновенная частота
        /// </summary>
        /// <param name="st">Входной сигнал</param>
        public static Vector Frequency(Vector st)
        {
            return Functions.Diff(GetAnalSig(st).PhaseToVector());
        }

    }
}
