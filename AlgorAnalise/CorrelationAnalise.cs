/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 07.07.2018
 * Время: 11:38
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace AI.MathMod.AlgorAnalise
{
	/// <summary>
	/// Корреляционный анализ (проверка ортогональности)
	/// </summary>
	public class CorrelationAnalise
	{
		public Matrix CorMatrNorm{get; protected set;}
		
		public CorrelationAnalise(Matrix matrix)
		{
			Vector[] vectsCol = Matrix.GetColumns(matrix);
			CorMatrNorm = Matrix.CorrelationMatrixNorm(vectsCol);
		}
		
		
		public double MeanOrtog()
		{
			double mean = 0;
			
			for(int i = 0; i<CorMatrNorm.M; i++)
				for (int j = 0; j < CorMatrNorm.N; j++)
				{
					mean += Math.Abs(CorMatrNorm[i,j]);
				}
			mean = (mean - CorMatrNorm.M)/(CorMatrNorm.M*CorMatrNorm.M-CorMatrNorm.M);
			return 1.0-mean;
		}
	}
}
