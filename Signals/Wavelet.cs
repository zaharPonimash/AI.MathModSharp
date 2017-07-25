/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 06.06.2017
 * Время: 17:56
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using AI.MathMod;

namespace AI.MathMod.Signals
{
	/// <summary>
	/// Description of Wavelet.
	/// </summary>
	public static class Wavelet
	{
		static List<Double> DirectTransform(List<Double> SourceList)
        {
            if (SourceList.Count == 1)
                return SourceList;

            List<Double> RetVal = new List<Double>();
            List<Double> TmpArr = new List<Double>();

            for (int j = 0; j < SourceList.Count - 1; j+=2)
            {
                RetVal.Add((SourceList[j] - SourceList[j + 1]) / 2.0);
                TmpArr.Add((SourceList[j] + SourceList[j + 1]) / 2.0);
            }

            RetVal.AddRange(DirectTransform(TmpArr));

            return RetVal;
        }
		
		
		static List<Double> InverseTransform(List<Double> SourceList)
        {
            if (SourceList.Count == 1)
                return SourceList;

            List<Double> RetVal = new List<Double>();
            List<Double> TmpPart = new List<Double>();

            for (int i = SourceList.Count / 2; i < SourceList.Count; i++)
                TmpPart.Add(SourceList[i]);

            List<Double> SecondPart = InverseTransform(TmpPart);
            
            for (int i = 0; i < SourceList.Count / 2; i++)
            {
                RetVal.Add(SecondPart[i] + SourceList[i]);
                RetVal.Add(SecondPart[i] - SourceList[i]);
            }

            return RetVal;
        }
		
		
		
		
		/// <summary>
		/// Быстрое вельвет преобразование
		/// </summary>
		/// <param name="inp">Входной вектор</param>
		/// <returns>Результат</returns>
		public static Vector FWT(Vector inp)
		{
			Vector inpNew = inp.CutAndZero(Functions.NextPow2(inp.N));
			List<double> sourceData = new List<double>();
			sourceData.AddRange(inpNew.Vecktor);
			double[] outp = DirectTransform(sourceData).ToArray();
			return new Vector(outp);
		}
		
		/// <summary>
		/// Обратное быстрое вельвет преобразование
		/// </summary>
		/// <param name="inp">Входной вектор</param>
		/// <returns>Результат</returns>
		public static Vector IFWT(Vector inp)
		{
			Vector inpNew = inp.CutAndZero(Functions.NextPow2(inp.N));
			List<double> sourceData = new List<double>();
			sourceData.AddRange(inpNew.Vecktor);
			double[] outp = InverseTransform(sourceData).ToArray();
			return new Vector(outp);
		}
		
		
	}
}
