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
using System.Collections.Generic;


namespace AI.MathMod
{
	
	
	
	/// <summary>
	/// Преобразование Фурье
	/// </summary>
    public class Furie
    {
    	/// <summary>
    	/// Вектор поворота
    	/// </summary>
    	public ComplexVector rotateCoef{get;set;}
    	Dictionary<double, Complex> dic = new Dictionary<double, Complex>();
    	
    	Complex rot;
    	
    	public int _n;
    	int _l = 0;
    	int[] deepC;
    	double log2;
    	int pow2;
    	
    	
    	
    	public Furie(int n)
    	{
    		_n = Functions.NextPow2(n);
//    		rotateCoef = new ComplexVector(_n*2);
//    		log2 = Math.Log(2);
//    		pow2 = (int)(Math.Log(_n)/log2);
//    		int newn = _n;
//    		deepC = new int[pow2+2];
//    		deepC[0] = _n/2;
//    		
//    		while(newn != 0)
//    		{
//    			
//    			newn /= 2;
//    			
//    			for(int i = 0; i<newn; i++)
//    			{
//    				double key = Math.Round((float)(i)/(float)newn,3);
//    				if(!dic.ContainsKey(key))	dic.Add(key, Rotate(i,newn));
//    				
//    			}
//    		}
//    		
    		//rotateCoef.RealToVector().Visual();
    		
    	}
    	
    	
    	/// <summary>
    	/// Быстрое Фурье
    	/// </summary>
    	/// <param name="inp"></param>
    	/// <returns></returns>
    	public ComplexVector FFT(Vector inp)
    	{
    		var compInp = new Complex[_n];
    		
    		for (int i = 0; i < inp.N; i++)
    		{
    			compInp[i] = new Complex(inp[i],0);
    		}
    		
    		return new ComplexVector(FFT(compInp));
    	}
    	
    	public Vector RealIFFT(ComplexVector cInp)
    	{
    		return IFFT(cInp).RealToVector()/_n;
    	}
    	
    	public Vector RealIFFT2(ComplexVector cInp)
    	{
    		return IFFT(cInp).RealToVector();
    	}
    	
    	Complex GetRot(int l, int i)
    	{
    		return rotateCoef[deepC[l]+i];
    	}
    	
    	
    	Complex[] FFT(Complex[] inp)
    	{
    		Complex[] X;
            int N = inp.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = inp[0] + inp[1];
                X[1] = inp[0] - inp[1];
            }
            
            else
            {
            	
            	
                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = inp[2 * i];
                    x_odd[i] = inp[2 * i + 1];
                }
                Complex[] X_even = FFT(x_even);
                Complex[] X_odd = FFT(x_odd);
                X = new Complex[N];
                
                for (int i = 0; i < N / 2; i++)
                {
                	rot = Rotate(i, N);
                	X[i] = X_even[i] +  rot* X_odd[i];
                    X[i + N / 2] = X_even[i] - rot* X_odd[i];
                }
            }
//            dic[Math.Round((float)(i)/(float)N,3)]
            
            return X;
    	}
    	
    	public ComplexVector IFFT(ComplexVector inp)
        {
        	ComplexVector cV = !inp.CutAndZero(_n); // Комплексно-сопряженный вектор
        	return new ComplexVector(FFT(cV.Vecktor));
        }
    	
    #region БПФ
    
        /// <summary>
        /// Вычисление поворачивающего модуля e^(-i*2*PI*k/N)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Complex Rotate(int k, int N)
        {
        
            if (k % N == 0) return 1;
            double arg = -2 * Math.PI * k / N;
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
                    X[i] = X_even[i] + Rotate(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - Rotate(i, N) * X_odd[i];
                }
            }
            return X;
        }
        
        
        
        /// <summary>
        /// Возвращает спектр сигнала
        /// </summary>
        /// <param name="inp">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static Complex[] ifft(Complex[] inp)
        {
        	ComplexVector cV = !new ComplexVector(inp); // Комплексно-сопряженный вектор
        	return fft(cV.Vecktor);
        }
//        /// <summary>
//        /// Центровка массива значений полученных в fft (спектральная составляющая при нулевой частоте будет в центре массива)
//        /// </summary>
//        /// <param name="X">Массив значений полученный в fft</param>
//        /// <returns></returns>
//        public static Complex[] nfft(Complex[] X)
//        {
//            int N = X.Length;
//            Complex[] X_n = new Complex[N];
//            for (int i = 0; i < N / 2; i++)
//            {
//                X_n[i] = X[N / 2 + i];
//                X_n[N / 2 + i] = X[i];
//            }
//            return X_n;
//        }
//        
        
        
        
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
	
	
	public static Matrix TimeFrTransform(Vector vect, int lenFr = 1000)
	{
		int lenTime = vect.N/lenFr;
		Vector[] vects = new Vector[lenTime];
		double[,] matr = new double[lenFr,lenTime];
		
		for (int i = 0; i < lenTime; i++)
		{
			vects[i] = vect.GetInterval(i*lenFr, (i+1)*lenFr);
		}
		
		for (int i = 0; i < lenTime; i++)
		{
			vects[i] = Furie.fft(vects[i]).MagnitudeToVector()/lenFr;
			
			for (int j = 0; j < lenFr; j++)
			{
				matr[j,i] = vects[i][j];
			}
		}
		
		return new Matrix(matr);
	}
	
	
    }
}

