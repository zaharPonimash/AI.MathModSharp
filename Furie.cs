/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 02.02.2016
 * Time: 2:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Numerics;
using System.Threading;


namespace AI.MathMod
{
	
	
	
	/// <summary>
	/// Преобразование Фурье
	/// </summary>
    public class Furie
    {
    	/// <summary>
    	/// Буфер для асинхронного Фурье
    	/// </summary>
    	public ComplexVector buffer{get;set;}
    	
    	
    #region БПФ
    
        /// <summary>
        /// Вычисление поворачивающего модуля e^(-i*2*PI*k/N)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Complex w(int k, int N)
        {
        
            if (k % N == 0) return 1;
            double arg = -2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }
        
        
        
         /// <summary>
        /// Вычисление поворачивающего модуля e^(-i*2*PI*k/N)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Complex w1(int k, int N)
        {
        
            if (k % N == 0) return 1;
            double arg = 2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }
        
        
        /// <summary>
        /// Возвращает спектр сигнала
        /// </summary>
        /// <param name="x">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static Complex[] fft(Complex[] x)
        {
            Complex[] X;
            int N = x.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = x[0] + x[1];
                X[1] = x[0] - x[1];
            }
            else
            {
                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = x[2 * i];
                    x_odd[i] = x[2 * i + 1];
                }
                Complex[] X_even = fft(x_even);
                Complex[] X_odd = fft(x_odd);
                X = new Complex[N];
                for (int i = 0; i < N / 2; i++)
                {
                    X[i] = X_even[i] + w(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - w(i, N) * X_odd[i];
                }
            }
            return X;
        }
        
        
        
        /// <summary>
        /// Возвращает спектр сигнала
        /// </summary>
        /// <param name="x">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static Complex[] ifft(Complex[] x)
        {
            Complex[] X;
            int N = x.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = x[0] + x[1];
                X[1] = x[0] - x[1];
            }
            else
            {
                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = x[2 * i];
                    x_odd[i] = x[2 * i + 1];
                }
                Complex[] X_even = ifft(x_even);
                Complex[] X_odd = ifft(x_odd);
                X = new Complex[N];
                for (int i = 0; i < N / 2; i++)
                {
                    X[i] = X_even[i] + w1(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - w1(i, N) * X_odd[i];
                }
            }
            return X;
        }
        /// <summary>
        /// Центровка массива значений полученных в fft (спектральная составляющая при нулевой частоте будет в центре массива)
        /// </summary>
        /// <param name="X">Массив значений полученный в fft</param>
        /// <returns></returns>
        public static Complex[] nfft(Complex[] X)
        {
            int N = X.Length;
            Complex[] X_n = new Complex[N];
            for (int i = 0; i < N / 2; i++)
            {
                X_n[i] = X[N / 2 + i];
                X_n[N / 2 + i] = X[i];
            }
            return X_n;
        }
        
        
        
        
        /// <summary>
        /// Возвращает комплексный вектор спектра сигнала
        /// </summary>
        /// <param name="inp">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static ComplexVector fft(ComplexVector inp)
        {
        	ComplexVector inpV = inp.CutAndZero(Functions.NextPow2(inp.N));
        	return new ComplexVector(fft(inpV.Vecktor));
        }
        
        
        /// <summary>
        /// Быстрое преобразование Фурье(БПФ)
        /// </summary>
        /// <param name="inp">Входной вектор</param>
        public static ComplexVector fft(Vector inp)
        {
        	ComplexVector cv = new ComplexVector(inp);	
        	return fft(cv);
        }
        
        
        void fftThC()
        {
        	buffer = fft(buffer);
        }
        
        
        
        /// <summary>
        /// Асинхронное БПФ
        /// </summary>
        /// <param name="inp">Входной вектор</param>
        public void fftAs(ComplexVector inp)
        {
        	buffer = inp.Copy();
        	Thread th = new Thread(fftThC);
        	th.Start();
        }
        
        /// <summary>
        /// Асинхронное БПФ
        /// </summary>
        /// <param name="inp">Входной вектор</param>
         public void fftAs(Vector inp)
        {
         	buffer = new ComplexVector(inp.Copy());
        	Thread th = new Thread(fftThC);
        	th.Start();
        }
        
        
          /// <summary>
        /// Возвращает комплексный вектор спектра сигнала
        /// </summary>
        /// <param name="inp">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        static ComplexVector ifft1(ComplexVector inp)
        {
        	ComplexVector inpV = inp.CutAndZero(Functions.NextPow2(inp.N));
        	return new ComplexVector(ifft(inpV.Vecktor));
        }
        
        
        
        static ComplexVector ifft1(Vector inp)
        {
        	ComplexVector cv = new ComplexVector(inp);
        	
        	return ifft(cv);
        }
        
        #endregion
        
        
    #region ОБПФ
   		 /// <summary>
        /// ОБПФ
        /// </summary>
        /// <param name="A">Входной вектор</param>
    public static ComplexVector ifft(ComplexVector A)
    {
    	ComplexVector C = ifft1(A);
    	
    	return C/C.N;
    }
    
    
    	 /// <summary>
        /// ОБПФ
        /// </summary>
        /// <param name="A">Входной вектор</param>
    public static ComplexVector ifft(Vector A)
    {
    	ComplexVector C = ifft1(A);
    	
    	return C/C.N;
    }
	#endregion    
	
	

	
	
	/// <summary>
	/// Дискретное преобразование Фурье
	/// </summary>
	/// <param name="x">Входной действительный вектор</param>
	public static ComplexVector DPF(Vector x)
	{
		int N = x.N;
		ComplexVector x1 = new ComplexVector(N);
		ComplexVector Out = new ComplexVector(N);
		
		Complex i = new Complex(0,1);
		
		
		for(int k =0; k<N; k++){
			
			for(int n =0; n<N; n++)
			x1.Vecktor[n] = x.Vecktor[n]*Complex.Exp((-2*Math.PI*i*k*n)/N);
			
			
			Out.Vecktor[k] = Functions.Summ(x1);
		}
		
		return Out;
	}

	
	/// <summary>
	/// Дискретное преобразование Фурье
	/// </summary>
	/// <param name="x">Входной комплесный вектор</param>
	public static ComplexVector DPF(ComplexVector x)
	{
		int N = x.N;
		ComplexVector x1 = new ComplexVector(N);
		ComplexVector Out = new ComplexVector(N);
		
		Complex i = new Complex(0,1);
		
		
		for(int k =0; k<N; k++){
			for(int n =0; n<N; n++){
			x1.Vecktor[n] = x.Vecktor[n]*Complex.Exp((-2*Math.PI*i*k*n)/N);
			}
			
			Out.Vecktor[k] = Functions.Summ(x1);
		}
		
		return Out;
	}
	
	/// <summary>
	/// Обратное дискретное преобразование Фурье
	/// </summary>
	/// <param name="x">Входной действительный вектор</param>
	public static ComplexVector ODPF(Vector x)
	{
		int N = x.N;
		ComplexVector x1 = new ComplexVector(N);
		ComplexVector Out = new ComplexVector(N);
		
		Complex i = new Complex(0,1);
		
		
		for(int k =0; k<N; k++){
			
			for(int n =0; n<N; n++)
			x1.Vecktor[n] = x.Vecktor[n]*Complex.Exp((2*Math.PI*i*k*n)/N);
			
			
			Out.Vecktor[k] = Functions.Summ(x1);
		}
		
		return Out/N;
	}
	
	
	/// <summary>
	/// Обратное дискретное преобразование Фурье
	/// </summary>
	/// <param name="x">Входной действительный вектор</param>
	public static ComplexVector ODPF(ComplexVector x)
	{
		int N = x.N;
		ComplexVector x1 = new ComplexVector(N);
		ComplexVector Out = new ComplexVector(N);
		
		Complex i = new Complex(0,1);
		
		
		for(int k =0; k<N; k++){
			for(int n =0; n<N; n++){
			x1.Vecktor[n] = x.Vecktor[n]*Complex.Exp((2*Math.PI*i*k*n)/N);
			}
			
			Out.Vecktor[k] = Functions.Summ(x1);
		}
		
		return Out/N;
	}
	
    }
}

