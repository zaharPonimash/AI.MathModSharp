/*
 * Created by SharpDevelop.
 * User: 01
 * Date: 08.04.2015
 * Time: 22:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using AI.MathMod;
using AI.MathMod.AdditionalFunctions;
using System.Numerics;
using System.Collections.Generic;


namespace AI.MathMod.ML
{
	
	
	
	/// <summary>
	/// Пространство имен кластеризатора
	/// </summary>
	namespace Clasterisators
	{
		
		

		
		
		/// <summary>
		/// Кластеризатор - Форель с корреляционной метрикой
		/// </summary>
		public class CorrForel
		{
			
			Vector[] _viborca, _vibNeClaster, _nowVib; // Выборка
			List<Claster> _clasters = new List<Claster>(); // Кластеры в выборке
			Claster _claster = new Claster(); 
			double R0 = 0,Rn = 0;
			Vector _mainCentr;
			bool flag = true;
			Random rng = new Random();
			
			
			/// <summary>
            /// Кластеры
            /// </summary>
			public Claster[] Clasters
			{
				get{return _clasters.ToArray();}
			}
			
			
			
			
			
			

			/// <summary>
			/// Конструктор класса
			/// </summary>
			/// <param name="viborca"></param>
			public CorrForel(Vector[] viborca)
			{
				Vector _old = new Vector(), _new= new Vector(); // Центры гиперсфер
					
				_vibNeClaster = _viborca = viborca; // Загрузка выборки
				_old =  _mainCentr = GetCentr(_viborca); // Получение центра
				Rn = R0 = Max(_viborca, _mainCentr);// Начальный радиус гиперсферы
				
				
				
				
				// Кластеризация
				while(_vibNeClaster.Length != 0)
				{
					Rn = 0.9*R0; // Уменьшение радиуса гиперсферы
					_nowVib = GetGipersfer(Rn,_vibNeClaster[rng.Next(_vibNeClaster.Length)],_vibNeClaster); // обводка гиперсферой
					_new = GetCentr(_nowVib);// новый центр
					
					//Центр кластера
					while(_old != _new)
					{
						Rn *= 0.9; //Уменьшение радиуса гиперсферы
						_old = _new; // сохранение старого радиуса
						_nowVib = GetGipersfer(Rn,_old,_vibNeClaster);	// обводка гиперсферой	
						try{
						_new = GetCentr(_nowVib);// новый центр
						}
						catch{break;}
					}
					
					_claster = new Claster();// Новый кластер
					_claster.Centr = _new;// Добавление центра
					_claster.Viborka = _nowVib;// выборка
					_clasters.Add(_claster);// Добавление кластера в коллекцию
					_vibNeClaster = AWithOutB(_vibNeClaster, _nowVib); // Удаление кластеризированных данных
					
				}
				
				
				
			}

			
			
			
			/// <summary>
			/// Конструктор класса
			/// </summary>
			/// <param name="viborca"></param>
			public CorrForel(Vector[] viborca, int minR)
			{
				Vector _old = new Vector(), _new= new Vector(); // Центры гиперсфер
					
				_vibNeClaster = _viborca = viborca; // Загрузка выборки
				_old =  _mainCentr = GetCentr(_viborca); // Получение центра
				Rn = R0 = Max(_viborca, _mainCentr);// Начальный радиус гиперсферы
				
				
				
				
				// Кластеризация
				while(_vibNeClaster.Length != 0)
				{
					Rn = 0.9*R0; // Уменьшение радиуса гиперсферы
					_nowVib = GetGipersfer(Rn,_vibNeClaster[0],_vibNeClaster); // обводка гиперсферой
					_new = GetCentr(_nowVib);// новый центр
					
					//Центр кластера
					while((_old != _new)&&(Rn>=minR))
					{
						Rn *= 0.9; //Уменьшение радиуса гиперсферы
						_old = _new; // сохранение старого радиуса
						_nowVib = GetGipersfer(Rn,_old,_vibNeClaster);	// обводка гиперсферой					
						_new = GetCentr(_nowVib);// новый центр
					}
					
					_claster = new Claster();// Новый кластер
					_claster.Centr = _new;// Добавление центра
					_claster.Viborka = _nowVib;// выборка
					_clasters.Add(_claster);// Добавление кластера в коллекцию
					_vibNeClaster = AWithOutB(_vibNeClaster, _nowVib); // Удаление кластеризированных данных
					
				}
				
				
				
			}




			/// <summary>
			/// Проводит гиперсферу нужного радиуса из конкретной точки и на заданном множестве
			/// </summary>
			/// <param name="R">Радиус</param>
			/// <param name="m">Центр масс</param>
			/// <param name="mass">Множество точек</param>
			/// <returns></returns>
			Vector[] GetGipersfer(double R, Vector m, Vector[] mass)
			 {
				List<Vector> OUT = new List<Vector>();
				
				foreach(Vector i in mass)
					if(Distance.CorrDist(m,i) <= R) OUT.Add(i); // проведение окружности
				
				return OUT.ToArray();
			 }

			
			
			/// <summary>
			/// Максимальная дистанция
			/// </summary>
			/// <returns></returns>
			double Max(Vector[] mass, Vector m)
			{
				double max = Distance.CorrDist(mass[0],m),d;
				for(int i = 1; i<mass.Length; i++)  
				{
					d = Distance.CorrDist(mass[i],m);
					
					//d = Distance.ManhattanDistance(mass[i],m);
					if(max < d) max = d;
				}
				return max;
			}


			
			
			/// <summary>
			/// Множество А\В
			/// </summary>
			/// <param name="A">Множество А</param>
			/// <param name="B">Множество В</param>
			/// <returns>А\B</returns>
			Vector[] AWithOutB(Vector[] A, Vector[] B)
			{
				List<Vector> C = new List<Vector>();
				bool flag = true; // флаг принадлежности
				
				
				for(int i = 0; i<A.Length ; i++){
					flag = true;
					
					for(int j = 0; j<B.Length ; j++)
					{
						if(A[i] == B[j])
						{
							flag = false;
							break;
						}
					}
						
					if(flag) C.Add(A[i]);
				}
				
				return C.ToArray();
			}





		/// <summary>
		/// Поиск центра класса
		/// </summary>
		/// <param name="vectors">Точки класса</param>
		/// <returns></returns>
		Vector GetCentr(Vector[] vectors)
		{
			Vector output = new Vector();
			int N = vectors.Length;
			output = vectors[0];
			for(int i = 1; i<N; i++)
				output += vectors[i];
			return output/N;
		}




			
			
		}
		
		
	}
	
	

	
}
