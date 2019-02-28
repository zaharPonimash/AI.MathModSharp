/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 29.01.2016
 * Time: 18:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Numerics;


namespace AI.MathMod
{
	/// <summary>
	/// Класс реализующий свертку последовательностей
	/// </summary>
	public static class Convolution
	{
		
		
		
	/// <summary>
	/// Прямая свертка 
	/// </summary>
		static public Vector DirectConvolution(Vector A, Vector B)
		{
			int nMax = A.N+B.N-1;
			Vector st, ht;
			double[] convolut = new double[nMax];
			
			
			for(int n = 0; n<nMax; n++){
			st = A.CutAndZero(n);
			ht = B.CutAndZero(n);
			ht = ht.Revers();
			convolut[n] = Functions.Summ(st*ht);
			}
			return new Vector(convolut);
		}
		
		
		
		
	/// <summary>
	/// Прямая свертка комплексный вектор
	/// </summary>
		static public ComplexVector DirectConvolution(ComplexVector A, ComplexVector B)
		{
			int nMax = A.N+B.N-1;
			ComplexVector st, ht;
			Complex[] convolut = new Complex[nMax];
			
			
			for(int n = 0; n<nMax; n++){
			st = A.CutAndZero(n);
			ht = B.CutAndZero(n);
			ht = ht.Revers();
			convolut[n] = Functions.Summ(st*ht);
			}
			return new ComplexVector(convolut);
		}
		
		
		/// <summary>
	/// Прямая свертка комплексный и реальный вектор
	/// </summary>
		static public ComplexVector DirectConvolution(ComplexVector A, Vector B)
		{
			int nMax = A.N+B.N-1;
			ComplexVector st;
			Vector ht;
			Complex[] convolut = new Complex[nMax];
			
			
			for(int n = 0; n<nMax; n++){
			st = A.CutAndZero(n);
			ht = B.CutAndZero(n);
			ht = ht.Revers();
			convolut[n] = Functions.Summ(st*ht);
			}
			return new ComplexVector(convolut);
		}
		
		
		
		/// <summary>
	/// Круговая свертка 
	/// </summary>
		static public Vector СircularConvolution(Vector A, Vector B)
		{
			if(A.N == B.N){
			int nMax = A.N;
			Vector st, ht;
			double[] convolut = new double[nMax];
			
			
			for(int n = 0; n<nMax; n++){
			st = A.CutAndZero(n);
			ht = B.CutAndZero(n);
			ht = ht.Revers();
			convolut[n] = Functions.Summ(st*ht);
			}
			return new Vector(convolut);
			}
			
			else{return DirectConvolution(A,B);}
		}
		
		
		
		
	/// <summary>
	/// Круговая свертка комплексный вектор
	/// </summary>
		static public ComplexVector СircularConvolution(ComplexVector A, ComplexVector B)
		{
			if(A.N == B.N){
			int nMax = A.N;
			ComplexVector st, ht;
			Complex[] convolut = new Complex[nMax];
			
			
			for(int n = 0; n<nMax; n++){
			st = A.CutAndZero(n);
			ht = B.CutAndZero(n);
			ht = ht.Revers();
			convolut[n] = Functions.Summ(st*ht);
			}
			return new ComplexVector(convolut);
			}
			
			else{return DirectConvolution(A,B);}
		}
		
		
		
		
		
		
		
	}
}
