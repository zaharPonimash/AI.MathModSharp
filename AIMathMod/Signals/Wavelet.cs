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
using System.ComponentModel;
using AI.MathMod;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Signals
{
	/// <summary>
	/// Вейвлеты
	/// </summary>
	public class Wavelet
	{
		
		PerentWavelet _pw;
		
		/// <summary>
		/// Непрерывное вейвлет преобразование
		/// </summary>
		/// <param name="pw">Порождение вейвлетов</param>
		public Wavelet(PerentWavelet pw)
		{
			_pw = pw;
		}
		
		
		
		/// <summary>
		/// Поиск патернов в сигнале
		/// </summary>
		/// <param name="sig">Сигнал</param>
		/// <returns>Максимумы патернов</returns>
		public Vector SerchPatern(Vector sig)
		{
			ComplexVector spectr = _pw.fur.FFT(sig-Statistic.ExpectedValue(sig));
			Vector[] output = new Vector[_pw.waveletSpectrs.Length];
			double std = Statistic.Std(sig);
			
			
			for (int i = 0; i < output.Length; i++) 
			{
				output[i] = _pw.fur.RealIFFT(_pw.waveletSpectrs[i]*spectr);
				output[i] /= std*_pw.std[i];
				output[i] *= 6*_pw.scals[i];
			}
			
			Vector res = Statistic.MaxEns(output);
			
			return res;
		}
		
		
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
			sourceData.AddRange(inpNew.DataInVector);
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
			sourceData.AddRange(inpNew.DataInVector);
			double[] outp = InverseTransform(sourceData).ToArray();
			return new Vector(outp);
		}
		
		
	}
	
	/// <summary>
	/// Ф-я порождения вейвлетов
	/// </summary>
	public class PerentWavelet
	{
		/// <summary>
		/// Спектры ф-й
		/// </summary>
		public ComplexVector[] waveletSpectrs;
		/// <summary>
		/// Фурье
		/// </summary>
		public Furie fur;
		/// <summary>
		/// Вектор СКО
		/// </summary>
		public Vector std;
		/// <summary>
		/// Масштабы
		/// </summary>
		public Vector scals;
		
		/// <summary>
		/// Порождения вейвлетов
		/// </summary>
		/// <param name="wavelet">Порождающая функция</param>
		/// <param name="scales">Масштабы</param>
		/// <param name="n">Размерность входа</param>
		public PerentWavelet(Func<double, Vector> wavelet, Vector scales, int n)
		{
			fur = new Furie(n);
			scals = scales.Copy();
			
			waveletSpectrs = new ComplexVector[scales.N];
			Vector wavReal;
			std = new Vector(scales.N);
			
			for (int i = 0; i < waveletSpectrs.Length; i++) 
			{
				wavReal = wavelet(scales[i]).Revers();
				wavReal -= Statistic.ExpectedValue(wavReal);
				//wavReal *= scales[i];
				waveletSpectrs[i] = fur.FFT(wavReal);
				waveletSpectrs[i] /= wavReal.N;
				std[i] = Statistic.Std(wavReal);
			}
			
		}
		
		
		
	}
}
