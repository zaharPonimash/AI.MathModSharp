/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 07.06.2017
 * Время: 11:46
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Signals
{
    /// <summary>
    ///Кепстральный анализ
    /// </summary>
    public static class Kepstr
    {
        /// <summary>
        /// Быстрое кепстральное преобразование
        /// </summary>
        /// <param name="signal">Сигнал</param>
        /// <returns></returns>
		public static Vector FKT(Vector signal)
        {
            ComplexVector spectr = Furie.fft(signal);
            Vector Aspectr = MathFunc.ln(spectr.MagnitudeToVector().TransformVector(x => x * x));
            return Furie.fft(Aspectr).RealToVector() / Aspectr.N;
        }



        /// <summary>
        /// Быстрое кепстральное преобразование
        /// </summary>
        /// <param name="signal">Сигнал</param>
        /// <returns></returns>
        public static Vector FKT(ComplexVector signal)
        {
            ComplexVector spectr = Furie.fft(signal);
            Vector Aspectr = MathFunc.ln(spectr.MagnitudeToVector().TransformVector(x => x*x));
            return Furie.fft(Aspectr).RealToVector() / Aspectr.N;
        }
    }
}
