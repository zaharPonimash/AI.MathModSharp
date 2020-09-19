/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 30.07.2018
 * Время: 15:08
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using AI.MathMod.ML.Datasets;
using System;

namespace AI.MathMod.ML.NeuronNetwork
{
    /// <summary>
    /// Description of MenegerNNW.
    /// </summary>
    public class MenegerNNW
    {
        /// <summary>
        /// Нейросеть
        /// </summary>
        public Net _net;
        private readonly VectorIntDataset _vid;

        /// <summary>
        /// Нейросетевой менеджер
        /// </summary>
        /// <param name="net">Нейросеть</param>
        /// <param name="vid">Датасет</param>
        public MenegerNNW(Net net, VectorIntDataset vid)
        {
            _net = net;
            _vid = vid;
        }



        /// <summary>
        /// Создание нейронной сети
        /// </summary>
        /// <param name="inpDim">Количество входов</param>
        /// <param name="classCount">Количествой классов</param>
        /// <param name="hDim">Кол-во нейронов на скрытом слое</param>
        /// <param name="learningRate">Скорость обучения</param>
        /// <returns>Нейросеть</returns>
        public static Net GetNet(int inpDim, int classCount, int hDim, double learningRate)
        {
            Net net = new Net(); // Создание нейросети
            net.Add(new FullBipolyareSigmoid(inpDim, hDim)); // добавление сигмоидального скрытого слоя с 10 нейронами
            net.Add(new Softmax(classCount)); // Выходной софтмакс слой
            Console.WriteLine("Learning Rate: {0}", net.LerningRate); // Вывод рассчитанной скорости обучение
            net.LerningRate = learningRate; // Установка своей
            net.Moment = 0.3; // Установка момента
            Console.WriteLine("Learning Rate: {0}", net.LerningRate); // Вывод новой скорости
            return net;
        }


        /// <summary>
        /// Создание нейронной сети
        /// </summary>
        /// <param name="inpDim">Количество входов</param>
        /// <param name="classCount">Количествой классов</param>
        /// <param name="hDim">Кол-во нейронов на скрытом слое</param>
        /// <returns>Нейросеть</returns>
        public static Net GetNet(int inpDim, int classCount, int hDim)
        {
            Net net = new Net(); // Создание нейросети
            net.Add(new FullBipolyareSigmoid(inpDim, hDim)); // добавление сигмоидального скрытого слоя с 10 нейронами
            net.Add(new Softmax(classCount)); // Выходной софтмакс слой
            Console.WriteLine("Learning Rate: {0}", net.LerningRate); // Вывод рассчитанной скорости обучени
            net.Moment = 0.3; // Установка момента
            return net;
        }

        /// <summary>
        /// Обучение
        /// </summary>
        /// <param name="epoch">количество эпох</param>
        public void Train(int epoch)
        {
            for (int i = 0; i < epoch; i++)
            {
                for (int j = 0; j < _vid.Count; j++)
                {
                    VectorClass vC = _vid.GetRandomData();
                    _net.TrainClassifier(vC.InpVector, vC.ClassMark);
                }
            }
        }

        /// <summary>
        /// Тестирование
        /// </summary>
        /// <param name="vidTest">Датасет</param>
        /// <returns>Вероятность верного ответа</returns>
        public double Test(VectorIntDataset vidTest)
        {
            double corr = 0;
            for (int i = 0; i < vidTest.Count; i++)
            {
                if (vidTest[i].ClassMark == GetClass(vidTest[i].InpVector))
                {
                    corr++;
                }
            }

            return corr / vidTest.Count;
        }



        /// <summary>
        /// Тестирование
        /// </summary>
        public string TestStr(VectorIntDataset vidTest, int n)
        {
            double[] corr = new double[n];
            double[] N = new double[n];
            int index;
            string str = string.Empty;

            for (int i = 0; i < vidTest.Count; i++)
            {
                index = vidTest[i].ClassMark;
                if (index == GetClass(vidTest[i].InpVector))
                {
                    corr[index]++;
                }

                N[index]++;

            }

            for (int i = 0; i < n; i++)
            {
                str += "Preсision " + i + ": " + (corr[i] / N[i] * 100) + "%\n";
            }


            return str;
        }

        /// <summary>
        /// Выход сети
        /// </summary>
        /// <param name="inp">Вход</param>
        /// <returns>Метка класса</returns>
        public int Output(Vector inp)
        {
            return GetClass(inp);
        }



        /// <summary>
        /// Получение метки класса в результате работы нейронки
        /// </summary>
        /// <param name="sig">Сигнал</param>
        /// <returns>Метка класса</returns>
        private int GetClass(Vector sig)
        {
            Vector outp = _net.Output(sig); // Получаем выходной вектор с нейросети
            double max = Statistic.MaximalValue(outp); // ищем максимальную активацию

            return (int)outp.IndexValue(max); // Ищем индекс (это и есть наш класс)
        }
    }
}
