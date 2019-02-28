/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 28.07.2018
 * Время: 13:44
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.ML.NeuronNetwork
{
	/// <summary>
	/// Слой нейронной сети
	/// </summary>
	public interface ILayer
	{ 
		
		Matrix W{set; get;}
		
		/// <summary>
		/// Размерность выхода
		/// </summary>
		int SizeOut{set; get;}
		/// <summary>
		/// Ошибка
		/// </summary>
		double Eps{set; get;}
		
		/// <summary>
		/// Дельты
		/// </summary>
		Vector Delts{set; get;}
		
		/// <summary>
		/// Прямой проход сети
		/// </summary>
		/// <param name="input">Вход</param>
		Vector Output(Vector input);
		
		/// <summary>
		/// Ошибка на выходе
		/// </summary>
		/// <param name="ideal">Идеальный вектор</param>
		void Delt(Vector ideal);
		
		/// <summary>
		/// Ошибка на скрытом слое
		/// </summary>
		/// <param name="layer">Следующий слой</param>
		void DeltH(ILayer layer);
		
		/// <summary>
		///  Обратный проход
		/// </summary>
		Vector Backwards();
		/// <summary>
		/// Обучение НС
		/// </summary>
		void Train();
		/// <summary>
		/// Перегенерация для повторяемости результатов
		/// </summary>
		/// <param name="rnd">ГСПЧ</param>
		void WGenerate(Random rnd);
		
		/// <summary>
		/// Установка(расчет) параметров НС
		/// </summary>
		/// <param name="inp">Размерность входа слоя</param>
		/// <param name="outp">Число нейронов(размерность выхода)</param>
		void SetParam(int inp, int outp);		
	}
}
