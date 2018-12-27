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
	/// Description of Wavelet.
	/// </summary>
	public class Wavelet
	{
		
		PerentWavelet _pw;
		
		public Wavelet(PerentWavelet pw)
		{
			_pw = pw;
		}
		
		
		
		/// <summary>
		/// Поиск патернов в сигнале
		/// </summary>
		/// <param name="sig">Сигнал</param>
		/// <returns>Максимумы патернов</returns>
		public Vector SerchPatern(Vector sig, int v = 0)
		{
			ComplexVector spectr = _pw.fur.FFT(sig-Statistic.ExpectedValue(sig));
			Vector[] output = new Vector[_pw.waveletSpectrs.Length];
			double sco = Statistic.Sco(sig);
			
			
			for (int i = 0; i < output.Length; i++) 
			{
				output[i] = _pw.fur.RealIFFT(_pw.waveletSpectrs[i]*spectr);
				output[i] /= sco*_pw.sco[i];
				output[i] *= 6*_pw.scals[i];
			}
			
			Vector res = Statistic.MaxEns(output);
			double mean = 1;//Statistic.MaximalValue(res);
			
			return res;//NeuroFunc.Porog(NeuroFunc.Sigmoid(8*(res-0.2))^2,0.7);
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
	
	
	public class PerentWavelet
	{
		
		public ComplexVector[] waveletSpectrs;
		public Furie fur;
		public Vector sco;
		public Vector scals;
		
		
		public PerentWavelet(Func<double, Vector> wavelet, Vector scales, int n)
		{
			fur = new Furie(n);
			scals = scales.Copy();
			
			waveletSpectrs = new ComplexVector[scales.N];
			Vector wavReal;
			sco = new Vector(scales.N);
			
			for (int i = 0; i < waveletSpectrs.Length; i++) 
			{
				wavReal = wavelet(scales[i]).Revers();
				wavReal -= Statistic.ExpectedValue(wavReal);
				//wavReal *= scales[i];
				waveletSpectrs[i] = fur.FFT(wavReal);
				waveletSpectrs[i] /= wavReal.N;
				sco[i] = Statistic.Sco(wavReal);
			}
			
		}
		
		
		
	}
}
