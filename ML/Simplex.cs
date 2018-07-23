/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 05.07.2018
 * Время: 17:12
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using AI.MathMod.AdditionalFunctions;

namespace AI.MathMod.ML
{
	/// <summary>
	/// Симплекс-метод
	/// </summary>
	public class Simplex
    {
		
        Matrix table; //симплекс таблица
        
        /// <summary>
        /// Итоговая матрица
        /// </summary>
        public Matrix CalculatedTable
        {
        	get{return table;}
		}
        
        /// <summary>
        /// Выигрыш от стратегии
        /// </summary>
        public double Cost{get; protected set;}
        
        int m, n, N, M, MN;
        Vector grad, resultVector;
        List<int> basis; //список базисных переменных
 		
        
        public Simplex(Matrix source)
        {
            m =source.M;
            n = source.N;
            N = n-1;
            M = m-1;
            MN = n+m-1;
            table = new Matrix(m, MN);
            basis = new List<int>();
            grad = new Vector(N);
            
            
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < table.N; j++)
                {
                    if (j < n)
                        table[i, j] = source[i, j];
                    else
                        table[i, j] = 0;
                }
                //выставляем коэффициент 1 перед базисной переменной в строке
                if ((n + i) < table.N)
                {
                    table[i, n + i] = 1;
                    basis.Add(n + i);
                }
            }
 			
            
            GetVectorGrad();
            n = table.N;
        }
 
        /// <summary>
        /// Запуск метода
        /// </summary>
        /// <returns></returns>
        public Vector Run()
        {
            int mainCol, mainRow; //ведущие столбец и строка
            Vector result = new Vector(N);
 				
            
            while (!IsEnd())
            {
                mainCol = SerchMainCol();
                mainRow = SerchMainRow(mainCol);
                basis[mainRow] = mainCol;
 
                Matrix newTable = new Matrix(m, n);
 
                for (int j = 0; j < n; j++)
                    newTable[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];
 
                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;
 
                    for (int j = 0; j < n; j++)
                        newTable[i, j] = table[i, j] - table[i, mainCol] * newTable[mainRow, j];
                }
                table = newTable;
                
            }
 
            //Запись вектора опт. параметров
            for (int i = 0; i < N; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = table[k, 0];
                else
                    result[i] = 0;
            }
 			
            resultVector = result;
            Cost = GetCost();
            
            return resultVector;
        }
 		
        // Вектор градиента
        void GetVectorGrad()
        {
        	for (int i = 0; i < grad.N; i++)
        	{
        		grad[i] = -table[M,i+1];
        	}
        }
        
        // Проверка оптимальности решения
        bool IsEnd()
        {
            bool flag = true;
 
            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }
 
            return flag;
        }
 
        // Поиск опорного(ведущего столбца)
        int SerchMainCol()
        {
            int mainCol = 1;
 
            for (int j = 2; j < n; j++)
                if (table[m - 1, j] < table[m - 1, mainCol])
                    mainCol = j;
 
            return mainCol;
        }
 		// Поиск ведущей строки
        int SerchMainRow(int mainCol)
        {
            int mainRow = 0;
 
            for (int i = 0; i < m - 1; i++)
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }
 
            for (int i = mainRow + 1; i < m - 1; i++)
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                    mainRow = i;
 
            return mainRow;
        }
 
        double GetCost()
        {
        	return GeomFunc.ScalarProduct(resultVector, grad);
        }
        
        string toStr(int num)
        {
        	string output = "Итоговая симплекс-таблица:" + table.Round(num).ToString("\t")+"\n\n" +
				"///-----------------------------------///\n\nОптимальные значения:" +"\n";
			
			for (int i = 0; i < resultVector.N; i++)
            {
				output+= String.Format("\nX[{0}] = {1}", i+1, Math.Round(resultVector[i], num));
            }
			
			output+= "\n\n///-------------------------------------///\n\nВыгода от стратегии: " + Math.Round(Cost,num) + " единиц";
			return output;
        }
        
        
		public override string ToString()
		{
			try
			{
				return toStr(3);
			}
			catch
			{
				Run();
				return toStr(3);
			}
		}
		
		public string ToString(int num)
		{
			try
			{
				return toStr(num);
			}
			catch
			{
				Run();
				return toStr(num);
			}
		}
		
    }
}
