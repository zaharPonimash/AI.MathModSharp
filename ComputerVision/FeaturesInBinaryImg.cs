/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 24.07.2018
 * Время: 1:47
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Numerics;
using System.Collections.Generic;

namespace AI.MathMod.ComputerVision
{
	/// <summary>
	/// Description of FeaturesInBinaryImg.
	/// </summary>
	public class FeaturesInBinaryImg
	{
		ComplexVector points;
		
		bool _isRot, _isScale, _isMove;
		int n = 30;
		
		/// <summary>
		/// Фичи из матрицы изобр
		/// </summary>
		/// <param name="isRot">Сохранить оригенальный поворот</param>
		/// <param name="isScale">Сохранить оригенальный масштаб</param>
		/// <param name="isMove">Сохранить оригенальное смещение</param>
		/// <param name="nGarm">Количество гармоник, кол-во точек в 2 раза больше</param>
		public FeaturesInBinaryImg(bool isRot = false, bool isScale = false, bool isMove = false, int nGarm = 30)
		{
			_isRot = isRot;
			_isScale = isScale;
			_isMove = isMove;
			n = nGarm;
		}
		
		
		public Vector MatrixFeatures(Matrix img)
		{
			GenVectorPoint(img);
			return GenFeature();
		}
		
		// Генерация вектора фич
		Vector GenFeature()
		{
			ComplexVector cV = Furie.fft(points);
			double k, cP1, cP2;
			Complex kR;
			
			if(!_isScale)
			{
				cP1 = cV[0].Magnitude;
				cP2 = cV[cV.N-1].Magnitude;
				k = Math.Sqrt(cP1*cP1 + cP2*cP2);
				cV /= k;
			}
			
			
			if(!_isRot)
			{
				cP1 = cV[0].Phase;
				cP2 = cV[cV.N-1].Phase;
				kR = Complex.Exp(new Complex(0,1)*(cP2-cP1)/2.0);
				cV *= kR;
			}
			
			if (!_isMove)
			{
				cV[0] = 0;
			}
			
			cV = cV.CutAndZero(n);
			
			Vector modules = cV.MagnitudeToVector();
			Vector phases = cV.PhaseToVector();
			
			return Vector.Concatinate(new Vector[]{modules, phases});
			
		}
		
		// Получение точек в виде комплексных чисел
		void GenVectorPoint(Matrix img)
		{
			List<Complex> pointList = new List<Complex>();
			
			for (int i = 0; i < img.M; i++) {
				for (int j = 0; j < img.N; j++) {
					
					if(img[i,j] < 0.5)
						pointList.Add(new Complex(j, i));
				}
			}
			
			points = new ComplexVector(pointList.Count);
			
			for (int i = 0; i < points.N; i++)
			{
				points[i] = pointList[i];
			}
		
		}
	}
}
