/*
 * Создано в SharpDevelop.
 * Пользователь: 01
 * Дата: 08.07.2017
 * Время: 15:47
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.Signals
{
	public class Filters
	{
		public static Vector Filter(Vector st, Vector kw)
		{
			Vector newSt = st.CutAndZero(Functions.NextPow2(st.N));
			Vector newKw = kw.CutAndZero(newSt.N);
			ComplexVector Sw = Furie.fft(newSt);
			Sw = Sw*newKw;
			newSt = Furie.ifft(Sw).RealToVector();
			return newSt.CutAndZero(st.N);//newSt.N;
		}
		
		
		public static Vector FilterLow(Vector st, double sr, Vector f)
		{
			double srNew = f.Vecktor[f.N-1]-sr;
			Vector kw = NeuroFunc.Porog(f,srNew).Revers();
			return Filter(st, kw);
		}
		
		
		
		public static Vector FilterBand(Vector st, double sr1, double sr2, Vector f)
		{
			double srNew = f.Vecktor[f.N-1]-sr2;
			Vector kw = NeuroFunc.Porog(f,srNew).Revers();
			Vector kw2 = NeuroFunc.Porog(f,sr1);
			kw *= kw2;
			return Filter(st, kw);
		}
		
		
		public static Vector FilterHigh(Vector st, double sr, Vector f)
		{
			Vector kw = NeuroFunc.Porog(f,sr);
			return Filter(st, kw);
		}
		
		public static Vector FilterRezector(Vector st, double sr1, double sr2, Vector f)
		{
			Vector kw = new Vector(st.N);
			
			kw += 1;
			
			for (int i = 0; i < st.N; i++)
			{
				if((f.Vecktor[i]>=sr1)&&(f.Vecktor[i]<=sr2)) kw.Vecktor[i] = 0;
			}
			
			
			return Filter(st, kw);
		}
		
		
		
		
		public Vector GetAFH(Vector f, double[] param, AFHType afh)
		{
				Vector kw = new Vector(f.N);
				
				if (afh == AFHType.Band)
				{
					
					for (int i = 0; i < f.N; i++)
					{
						if ((f.Vecktor[i] >= param[0]) && (f.Vecktor[i] <= param[1]))
							kw.Vecktor[i] = 1;
					}
					
				}
				
				if (afh == AFHType.High)
				{
					 kw = NeuroFunc.Porog(f,param[0]);
				}
				
				if (afh == AFHType.Low)
				{
				double srNew = f.Vecktor[f.N-1]-param[0];
				kw = NeuroFunc.Porog(f,srNew).Revers();
				}
				
				
				if (afh == AFHType.Rezector)
				{
				kw += 1;
			
				for (int i = 0; i < f.N; i++) {
					if ((f.Vecktor[i] >= param[0]) && (f.Vecktor[i] <= param[1]))
						kw.Vecktor[i] = 0;
				}
				}
				
				return kw;
		}
		
		
	
		public Vector CreationComplexAFH(Vector f, string[] param)
		{
					Vector kw = new Vector(f.N)+1;
					double[] fP;
					
					for (int i = 0; i < param.Length; i++)
					{
							if(param[i].Split(':')[0]=="rezector")
						{
							fP =  new double[2];
							fP[0] = Convert.ToDouble(param[i].Split(':')[1]);
							fP[1] = Convert.ToDouble(param[i].Split(':')[2]);
							kw *= GetAFH(f,fP, AFHType.Rezector);
						}
						
							if(param[i].Split(':')[0]=="low")
						{
							fP =  new double[1];
							fP[0] = Convert.ToDouble(param[i].Split(':')[1]);
							kw *= GetAFH(f,fP, AFHType.Low);
						}
						
							if(param[i].Split(':')[0]=="high")
						{
							fP =  new double[1];
							fP[0] = Convert.ToDouble(param[i].Split(':')[1]);
							kw *= GetAFH(f,fP, AFHType.High);
						}
						
							if(param[i].Split(':')[0]=="band")
						{
							fP =  new double[2];
							fP[0] = Convert.ToDouble(param[i].Split(':')[1]);
							fP[1] = Convert.ToDouble(param[i].Split(':')[2]);
							kw *= GetAFH(f,fP, AFHType.Band);
						}
					}
					
					return kw;
		}
	
	
	
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	public enum AFHType
	{
		Low,
		High,
		Rezector,
		Band
	}
	
	
}
