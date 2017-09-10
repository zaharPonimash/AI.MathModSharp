using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MathMod.ML.Classifire
{
    /// <summary>
    /// Машина опорных векторов
    /// </summary>
    public class SVM : IClassifire
    {
        /// <summary>
        /// Добавить класс
        /// </summary>
        /// <param name="tViborka">Элементы класса</param>
        /// <param name="nameClass">Имя класса</param>
        public void AddClass(Vector[] tViborka, string nameClass)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Открыть к-тор
        /// </summary>
        /// <param name="path">Путь</param>
        public void Open(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Распознавание вектора
        /// </summary>
        /// <param name="inp">Вектор</param>
        /// <returns>Результат</returns>
        public string RecognizeVector(Vector inp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сохранение к-тора
        /// </summary>
        /// <param name="path">Путь</param>
        public void Save(string path)
        {
            throw new NotImplementedException();
        }
    }
}
