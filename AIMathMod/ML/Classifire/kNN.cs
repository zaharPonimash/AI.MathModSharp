using System;

namespace AI.MathMod.ML.Classifire
{
    /// <summary>
    /// Метод к-ближ. соседей
    /// </summary>
    public class kNN : IClassifire
    {
        /// <summary>
        /// Добавить класс
        /// </summary>
        /// <param name="tDataset">Элементы класса</param>
        /// <param name="nameClass">Имя класса</param>
        public void AddClass(Vector[] tDataset, string nameClass)
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
