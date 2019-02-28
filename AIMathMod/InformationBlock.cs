using System;
using System.Drawing;
using System.Collections.Generic;


namespace AI.MathMod
{
	/// <summary>
	/// Информационный блок, основной тип данных МАС
	/// </summary>
	public class InformationBlock
	{
		/// <summary>
		/// Текстовая информация
		/// </summary>
		public string[] TextInform{get; set;}
		/// <summary>
		/// Графическая информация
		/// </summary>
		public Bitmap[] ImageInform{get; set;}
		/// <summary>
		/// Информация в векторном виде
		/// </summary>
		public Vector[] SingnalInform{get; set;}
		/// <summary>
		/// Информация в матричном виде
		/// </summary>
		public Matrix[] MatrixInform{get; set;}
		/// <summary>
		/// Информация в тензорном виде
		/// </summary>
		public Tensor[] TensorInform{get; set;}
		/// <summary>
		/// Найден ли ответ
		/// </summary>
		public bool Flag
		{
			get{
				try
				{
					return namedVar[mainVar] != null;
				}
				catch
				{
					return false;
				}
			}
		}
		/// <summary>
		/// Именованные переменные
		/// </summary>
		public Dictionary<string, Var> namedVar = new Dictionary<string, Var>();
		/// <summary>
		/// Основная переменная
		/// </summary>
		public string mainVar;		
		
		/// <summary>
		/// Вывод в строку
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return mainVar +" = "+namedVar[mainVar].Doub;
		}
	}
	
	/// <summary>
	/// Переменная инф. блока
	/// </summary>
	public class Var
	{
		/// <summary>
		/// Строка
		/// </summary>
		public string Str;
		/// <summary>
		/// Целочисленное
		/// </summary>
		public int Int;
		/// <summary>
		/// С плав. запятой
		/// </summary>
		public double Doub;
	}
}
