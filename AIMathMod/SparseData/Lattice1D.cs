/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 03.09.2018
 * Время: 0:14
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System.Collections.Generic;

namespace AI.MathMod.SparseData
{
    /// <summary>
    /// Description of Lattice1D.
    /// </summary>
    public class Lattice1D<T> : ILattice<T>, IMathStruct
    {
        /// <summary>
        /// Ячейки
        /// </summary>
        public List<Cell1D<T>> Cells { get; set; }
        /// <summary>
        /// Одномерная решетка
        /// </summary>
        public Lattice1D() { }

        /// <summary>
        /// Одномерная решетка
        /// </summary>
        public Lattice1D(Cell1D<T>[] cells)
        {
            Cells = new List<Cell1D<T>>();
            Cells.AddRange(cells);
        }


        /// <summary>
        /// Умножение решетки матрицу 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Vector operator *(Lattice1D<T> A, Matrix B)
        {
            Vector output = new Vector(B.N);


            for (int j = 0; j < B.N; j++)
            {

                for (int i = 0; i < A.Cells.Count; i++)
                {
                    output[j] += (B[A.Cells[i].coordinate, j] as dynamic) * A.Cells[i].Value;
                }

            }

            return output;
        }



        /// <summary>
        /// Одномерная решетка в строку
        /// </summary>
        public override string ToString()
        {
            Cells.Sort((a, b) => a.coordinate.CompareTo(b.coordinate) * (-1)); // Сортировка по позициям
            string str = string.Empty;
            int i = 0;

            for (int j = 1; j < Cells.Count + 1; j++)
            {
                for (; i < Cells[Cells.Count - j].coordinate; i++)
                {
                    str += 0 + " ";
                }

                i++;

                str += Cells[Cells.Count - j].Value.ToString() + " ";
            }
            return str.Trim();
        }



    }
}
