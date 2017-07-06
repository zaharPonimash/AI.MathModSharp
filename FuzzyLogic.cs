/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 04.02.2016
 * Time: 23:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AI.MathMod
{
	/// <summary>
	/// Не четкая логическая переменная, Fuzzy Logic Variable(FLV)
	/// </summary>
	public class FLV
	{
		double _flv;
		
		
		/// <summary>
		/// Нечеткая логическая переменная
		/// </summary>
		public double Flv
		{
			get{return _flv;}
			set
			{
				if(value>1) _flv = 1;
				else if(value<0) _flv = 0;
				else _flv = value;
			}
			
		}
		
		
		
		
		
		
		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		
		public FLV()
		{
			_flv = 0;
		}
		
		
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="flv">численное значение нечеткой переменной</param>
		public FLV(double flv)
		{
			_flv = flv;
		}
		
		
		
		
		
		
		
		//Не 
		public static FLV operator ! (FLV a)
		{
				return new FLV(1.0-a._flv);
		}
		
		//И
		public static FLV operator & (FLV a, FLV b)
		{
			return new FLV(a._flv*b._flv);
		}
		
		//Или
		public static FLV operator  | (FLV a, FLV b)
		{
			return new FLV(a._flv + b._flv - a._flv*b._flv);
		}
		
		
		
	}
}
