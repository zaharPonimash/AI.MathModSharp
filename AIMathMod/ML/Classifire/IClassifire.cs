namespace AI.MathMod.ML.Classifire
{
    /// <summary>
    /// Интерфейс для работы классификаторов
    /// </summary>
    public interface IClassifire
    {
        /// <summary>
        /// Добавление класса
        /// </summary>
        /// <param name="tDataset">Выборка</param>
        /// <param name="nameClass">Имя класса</param>
        void AddClass(Vector[] tDataset, string nameClass);

        /// <summary>
        /// Сохранение классификатора
        /// </summary>
        /// <param name="path">Путь</param>
        void Save(string path);

        /// <summary>
		/// Загрузка классификатора
		/// </summary>
		/// <param name="path">Путь</param>
		void Open(string path);


        /// <summary>
        /// Распознавание
        /// </summary>
        /// <param name="inp">Вектор который надо распознать</param>
        string RecognizeVector(Vector inp);
    }
}
