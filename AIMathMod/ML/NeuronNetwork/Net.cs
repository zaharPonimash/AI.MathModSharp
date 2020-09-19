/*
 * Создано в SharpDevelop.
 * Пользователь: admin
 * Дата: 16.11.2017
 * Время: 1:47
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AI.MathMod.ML.NeuronNetwork
{
    /// <summary>
    /// Нейронная сеть
    /// </summary>
    [Serializable]
    public class Net
    {
        /// <summary>
        /// Список слоев НС
        /// </summary>
        public List<ILayer> _layers = new List<ILayer>();
        private int countNeuronsForLastLayer;
        private readonly Random _rnd = new Random();


        /// <summary>
        /// Скорость обучения (исправить)
        /// </summary>
        public double LerningRate
        {
            get => (_layers[0] as FullConLayerBase).norm;
            set
            {
                for (int i = 0; i < _layers.Count; i++)
                {
                    (_layers[i] as FullConLayerBase).norm = value;
                }
            }
        }

        /// <summary>
        /// Инерционность обучения
        /// </summary>
        public double Moment
        {
            get => (_layers[0] as FullConLayerBase).moment;
            set
            {
                for (int i = 0; i < _layers.Count; i++)
                {
                    (_layers[i] as FullConLayerBase).moment = value;
                }
            }
        }


        /// <summary>
        /// Создание сети
        /// </summary>
        public Net()
        {

        }


        /// <summary>
        /// Создание сети
        /// </summary>
        /// <param name="rnd">Датчик случ. чисел</param>
        public Net(Random rnd)
        {
            _rnd = rnd;
        }

        /// <summary>
        /// Создание сети
        /// </summary>
        /// <param name="path">Путь до сохраненной НС</param>
        public Net(string path)
        {
            Open(path);
        }

        /// <summary>
        /// Добавление слоя
        /// </summary>
        /// <param name="layer">Слой</param>
        public void Add(ILayer layer)
        {
            if (_layers.Count == 0 || layer is CapsuleLinearLayer)
            {
                _layers.Add(layer);
                _layers[0].WGenerate(_rnd);
            }
            else
            {
                int inp = _layers[_layers.Count - 1].SizeOut;
                layer.SetParam(inp, layer.SizeOut);
                _layers.Add(layer);
                _layers[_layers.Count - 1].WGenerate(_rnd);
            }


            countNeuronsForLastLayer = layer.SizeOut;
        }


        /// <summary>
        /// Выход сети
        /// </summary>
        /// <param name="input">Вход</param>
        public Vector Output(Vector input)
        {
            Vector outp = _layers[0].Output(input);

            for (int i = 1; i < _layers.Count; i++)
                outp = _layers[i].Output(outp);

            return outp;
        }


        /// <summary>
        /// Выход сети (Класс)
        /// </summary>
        /// <param name="input">Вход</param>
        public int OutputClass(Vector input)
        {
            Vector outp = Output(input);
            return (int)outp.IndexValue(outp.Max());
        }

        /// <summary>
        /// Обучение классификатора
        /// </summary>
        /// <param name="inp">Вектор входа</param>
        /// <param name="outp">Метка класса</param>
        /// <returns>Ошибка на примере</returns>
        public double TrainClassifier(Vector inp, int outp)
        {
            Vector output = new Vector(countNeuronsForLastLayer);
            output[outp] = 1;
            Output(inp);
            _layers[_layers.Count - 1].Delt(output);


            for (int i = _layers.Count - 2; i >= 0; i--)
                _layers[i].DeltH(_layers[i + 1]);


            for (int i = 0; i < _layers.Count; i++)
                _layers[i].Train();

            return _layers[_layers.Count - 1].Eps;
        }


        /// <summary>
        /// Обучение сети
        /// </summary>
        /// <param name="inp">Вектор входа</param>
        /// <param name="output">Вектор выхода</param>
        /// <returns>Ошибка на примере</returns>
        public double Train(Vector inp, Vector output)
        {
            Output(inp);
            _layers[_layers.Count - 1].Delt(output);


            for (int i = _layers.Count - 2; i >= 0; i--)
                _layers[i].DeltH(_layers[i + 1]);


            for (int i = 0; i < _layers.Count; i++)
                _layers[i].Train();

            return _layers[_layers.Count - 1].Eps;
        }

        /// <summary>
        /// Сохранение нейронной сети
        /// </summary>
        /// <param name="path">Путь до файла</param>
        public void Save(string path)
        {
            try
            {

                BinaryFormatter binFormat = new BinaryFormatter();

                using (Stream fStream = new FileStream(path,
                  FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, this);
                }
            }

            catch
            {
                throw new ArgumentException("Ошибка сохранения", "Сохранение");
            }

        }



        /// <summary>
        /// Загрузка матрицы
        /// </summary>
        /// <param name="path">Путь до файла</param>		
        public void Open(string path)
        {

            try
            {
                Net net;
                BinaryFormatter binFormat = new BinaryFormatter();

                using (Stream fStream = new FileStream(path,
                  FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    net = (Net)binFormat.Deserialize(fStream);
                }

                _layers = net._layers;

                countNeuronsForLastLayer = net.countNeuronsForLastLayer;

            }

            catch
            {
                throw new ArgumentException("Ошибка загрузки");
            }

        }

    }
}
