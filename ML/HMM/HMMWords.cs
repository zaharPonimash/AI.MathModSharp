/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 13.04.2018
 * Время: 22:13
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;

namespace HiMaMo
{
	/// <summary>
	/// Скрытая марковская модель
	/// </summary>
	public class HMM
	{
		
		public double[,] stateMatrix, stateAlter;
		string[] stateNames;
		double len;
		
		public HMM()
		{
			
		}
		
		/// <summary>
		/// Обучение
		/// </summary>
		/// <param name="TrainText">Тренировочный текст</param>
		public void Train(string TrainText)
		{
			
			
			string[] trainText = TrainText.ToLower().Split();
			stateNames = GetWords(trainText);
			
			
			stateMatrix = new double[stateNames.Length,stateNames.Length];
			stateAlter = new double[stateNames.Length,stateNames.Length];
			len = stateNames.Length*stateNames.Length;
			
			
			for (int i = 0; i < trainText.Length-1; i++)
			{
				for (int j = 0; j < stateNames.Length; j++)
					for (int k = 0; k < stateNames.Length; k++)
					{
						if (trainText[i] == stateNames[j]
					    &&trainText[i+1] == stateNames[k])
						{
							stateMatrix[j, k]++;
							stateAlter[j, k] ++;
											break;
						}
				}
			}
			
			
			double max = GetMax(stateAlter);
			
			for (int j = 0; j < stateNames.Length; j++)
					for (int k = 0; k < stateNames.Length; k++)
			{
				stateMatrix[j, k] /= trainText.Length;
				stateAlter[j, k] /= max;
				stateAlter[j, k] = (1 - stateAlter[j, k])*0.9999;
			}
			
		}
		
		
		
		
		/// <summary>
		/// Максимальное значение в матрице
		/// </summary>
		/// <param name="matrix">Матрица</param>
		/// <returns>Максимальное значение</returns>
		double GetMax(double[,] matrix)
		{
			double max = matrix[0,0];
			
			for (int j = 0; j < stateNames.Length; j++)
					for (int k = 0; k < stateNames.Length; k++)
			{
				if( matrix[j, k]>max) max = matrix[j, k];
			}
			
			return max;
		}
		
		
		/// <summary>
		/// Генерация текста
		/// </summary>
		/// <param name="num">Сколько слов</param>
		/// <param name="begin">Первое слово</param>
		/// <returns>Сгенерированный текст</returns>
		public string Generate(int num, string begin)
		{
			Random rnd = new Random();
			String[] chs = new string[num];
			int ch;
			chs[0] = begin;
			string outp = begin+" ";
			
			
			for (int i = 1; i < num; i++) {
				
				while(true)
				{
					ch = rnd.Next(stateNames.Length);
					
					 if (rnd.NextDouble() > stateAlter[GetInd(chs[i-1]), ch])
					{
						chs[i] = stateNames[ch];
						outp += chs[i]+" ";
						break;
					}
					
				}
			}
			
			
			return outp;
		}
		
		/// <summary>
		/// Поиск индекса
		/// </summary>
		/// <param name="stateName">Имя состояния</param>
		/// <returns></returns>
		int GetInd(string stateName)
		{
			int ind = 0;
			
			for (int i = 0; i < stateNames.Length; i++)
			{
				if(stateName == stateNames[i])
				{
					ind = i;
					break;
				}
			}
			
			return ind;
		}
		
		
		
		/// <summary>
		///  Получение слов
		/// </summary>
		/// <param name="strs">Строки входа</param>
		/// <returns></returns>
		string[] GetWords(string[] strs)
		{
			List<string> words = new List<string>();
			words.Add(strs[0]);
			bool flag = true;
			
			for (int i = 0; i < strs.Length; i++) {
				flag = true;
				for (int j = 0; j < words.Count; j++) {
					if(strs[i] == words[j]) 
					{
						flag = false;
						break;
					}
					
				
				}
				
					if(flag) words.Add(strs[i]);
			}
			
			return words.ToArray();
			
		}
		
	}
}
