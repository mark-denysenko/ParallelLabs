# Parallel Labs
univesity program (variant 5)

# Lab 1
Створити вектор з N>=1000 елементами з випадкових чисел. Знайти евклідову норму вектора 

# Lab 2
Программа моделирует обслуживание двух потоков процессов с разными параметрами двумя центральными процессорами компьютера с одной очередью. Если сгенерирован процесс второго потока и второй процессор занят, процесс поступает в очередь. Если сгенерирован процесс первого потока, то, если первый процессор занят обработкой первого потока,  то процесс поступает на обработку на второй процессор. Если и второй процессор занят, то процесс уничтожается. Если в момент генерации процесса первого потока на первом процессоре обрабатывается процесс второго потока, то процесс прерывается и возвращается в очередь. Определите максимальную длину очереди, процент уничтоженных процессов для первого потока и процент прерванных запросов для второго потока. 

# Lab 3
1) Розробити програму, яка за допомогою AtomicInteger  і метода compareAndSet:
виконує наступні операції для одновимірного массиву. Для потоків використовувати  ExecutorService або parallelStream.
2) Створити паралельні фкнкції для знаходження:
- кількості елементів за умовою;
- мінімального та максимального елементів з індексами;
- контрольної суми із використанням XOR.

# Lab 4
Написати програму у якій реалізовано всі 4 класичних задачі паралельного програмування:
1.	Виробники — споживачі. (ReentrantLock)
2.	Письменники — читачі з блокуванням нових читачів, коли письменник чекає на вхід. (Semaphore)
3.	Обідаючі філософи. (ReentrantLock)
4.	Сплячий перукар. (Object, synchronized, wait/ notify.)

# Lab 5
Создать 2 массива (или коллекции) со случайными числами. В первом массиве —
оставить элементы которые больше среднего значения массива, во втором —
меньше. Отсортировать массивы и слить в один отсортированный массив те
элементы, которые есть в одном массиве и нет в другом.

# Lab 6
Для вариантов пераой лабораторной работы сделать программы используя OpenMP (или аналоги) + MPI для выбранного языка разработки.

# Lab 7
Написать клиент-серверное приложение, используя Java RMI, Sockets или аналогичную технологию.
Чат. Сервер рассылает всем клиентам информацию о клиентах, вошедших в чат и покинувших его

# Lab 8
1.	Создать файл XML и соответствующую ему схему XSD. 
2.	При разработке XSD использовать простые и комплексные типы, перечисления, шаблоны и предельные значения, обязательно использование атрибутов и типа ID.
3.	Сгенерировать или создать класс(ы) (Java), соответствующий данному описанию. 
4.	Создать приложение для разбора XML-документа и инициализации коллекции объектов информацией из XML-файла. Для разбора использовать  DOM-парсер, XPath, JAXB. Для сортировки объектов использовать интерфейс Comparator.
5.	Произвести проверку XML-документа с привлечением XSD. 
6.	Выполнить преобразование объектов классов (JAXB) в JSON.

Компьютерные комплектующие имеют следующие характеристики:
•	Name – название комплектующего.
•	Origin – страна производства.
•	Price – цена (0 – n рублей).
•	Type (должно быть несколько) – периферийное либо нет, энергопотребление (ватт), наличие кулера (есть либо нет), группа комплектующих (устройства ввода-вывода, мультимедийные), порты (COM, USB, LPT).
•	Critical – критично ли наличие комплектующего для работы компьютера.
Корневой элемент назвать Device.

# Lab 9
Предметную область взять из задания по XML.
Веб-приложение разбить на 2 части: 
- первая принимает запрос от клиента, валидирует и отправляет через систему обмена сообщения второй части;
- вторая часть — забирает сообщение из очереди и сохраняет в БД.
Система управления очередями, например RabbitMQ.

