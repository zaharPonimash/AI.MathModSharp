# [Новая версия (Многие функции были улучшены)](https://github.com/AIFramework/AI_Free)

# AI.MathMod
Математическая библиотека для языка C#, которая является основой моего фреймворка для разработки ИИ (AIFrNet). 
Библиотека позволяет работать с векторами матрицами и средствами обработки сигналов и машинного обучения. 
Основная идея этой библиотеки заключается в том что бы максимально упростить работу с математикой на языке C# задать некий единый стандарт всему фреймворку, для этого используются такие объекты как: вектора, матрицы, комплексные вектора тензоры и обощенные математические структуры.




Пространства имен:





AI.MathMod — основное пространство имен.


В нем находятся такие типы, как: вектора(Vector), матрицы(Matrix), комплексные вектора(ComplexVector), тензоры(Tensor). Для всех этих типов поддерживаются стандартные операции(сложение, вычитание, деление, произведение(вектора поэлементно, а матрицы по правилу умножения матриц), возведение в степень), так же присутствуют специальные операции, например для комплексных векторов это расчет вектора модулей или фаз, вывод действительных или мнимых частей. Для матриц это расчет определителя, транспонирование и т.д.. Для векторов имеется визуализация на основе библиотеки ZedGraph. ZedGraph на GitHub: https://github.com/ZedGraph/ZedGraph

Так же присутствует тип нечеткой логической переменной(FuzzyLogic). Если кто интересуется этой темой, то про нечкую логику, множества и отношения можно посмотреть тут: http://edu.sernam.ru/book_mmn.php?id=14

Имеется класс Functions, в котором реализованы операции интегрирование, дифференцирования, вычисления суммы и произведения ряда. 

Класс Convolution реализует прямую и цикличную(круговую) свертку. Про свертки можно почитать на этом сайте: http://ru.bmstu.wiki/%D0%A1%D0%B2%D0%B5%D1%80%D1%82%D0%BA%D0%B0

Класс Furie реализует прямое и обратное ДПФ и БПФ, для таких типов как вектора и комплексные вектора. Так же имеется реализация асинхронного БПФ, но для корректной работы в нее надо добавить события окончания преобразования.
Код БПФ для массива типа Complex[] взят с сайта: https://ru.wikibooks.org/wiki/%D0%A0%D0%B5%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D0%B8_%D0%B0%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC%D0%BE%D0%B2/%D0%91%D1%8B%D1%81%D1%82%D1%80%D0%BE%D0%B5_%D0%BF%D1%80%D0%B5%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5_%D0%A4%D1%83%D1%80%D1%8C%D0%B5
Обратное БПФ писал по аналогии, если в прямом преобразовании exp(-jwt), то в обратом exp(jwt). Подробнее про алгоритм БПФ можно посмотреть здесь: https://youtu.be/bqB40VIY17E

Класс Correlation реализует методы быстрой и обычной взаимо- и авто- корреляции, так же методы поиска паттернов в сигнале. Подробнее про корреляцию: http://edu.alnam.ru/book_v_tau2.php?id=52

Класс Statistic позволяет выделять стат. параметры вектора.










AI.MathMod.ML — пространство имен для работы с машинным обучением.


Класс GradientDecent — градиентный спуск с постоянным шагом. Литература: http://www.machinelearning.ru/wiki/index.php?title=%D0%9C%D0%B5%D1%82%D0%BE%D0%B4_%D0%B3%D1%80%D0%B0%D0%B4%D0%B8%D0%B5%D0%BD%D1%82%D0%BD%D0%BE%D0%B3%D0%BE_%D1%81%D0%BF%D1%83%D1%81%D0%BA%D0%B0


AI.MathMod.ML.Regression

Класс LinearRegression — линейная регрессия. Литература: https://ru.wikipedia.org/wiki/%D0%9B%D0%B8%D0%BD%D0%B5%D0%B9%D0%BD%D0%B0%D1%8F_%D1%80%D0%B5%D0%B3%D1%80%D0%B5%D1%81%D1%81%D0%B8%D1%8F

Класс SinApproximation — нелинейная регрессия(синусы).

Класс RBFGauss — нелинейная регрессия, функциями гаусса. 






AI.MathMod.Signals — пространство имен для работы с сигналами.


Класс DCT реализует прямое и обратное дискретно-косинусное преобразование. Литература: http://alnam.ru/book_ach.php?id=64

Класс Wavelet реализует прямое и обратное быстрое вейвлет-преобразование для действительных векторов.
Прямое и обратное быстрое вельвет преобразование взято от сюда: https://ru.wikipedia.org/wiki/%D0%94%D0%B8%D1%81%D0%BA%D1%80%D0%B5%D1%82%D0%BD%D0%BE%D0%B5_%D0%B2%D0%B5%D0%B9%D0%B2%D0%BB%D0%B5%D1%82-%D0%BF%D1%80%D0%B5%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5

Класс FastHilbert реализует быстрое преобразование Гильберта.

Класс Kepstr кепстральное преобразование.

Signal класс для генерирования основных типов сигналов.

Filters класс реализующий основные типы фильтров.
