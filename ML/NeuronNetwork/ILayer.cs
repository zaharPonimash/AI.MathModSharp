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
		int SizeOut{set; get;}
		
		double Eps{set; get;}
		
		//Дельты
		Vector Delts{set; get;}
		
		// Прямой проход сети
		Vector Output(Vector input);
		
		// Ошибка на выходе
		void Delt(Vector ideal);
		
		void DeltH(ILayer layer);
		
		// Обратный проход
		Vector Backwards();
		
		void Train();
		
		void WGenerate(Random rnd);
		
		void SetParam(int inp, int outp);		
	}
}
