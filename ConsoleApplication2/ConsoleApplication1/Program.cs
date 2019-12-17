using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApplication1
{
    abstract class Figure : IComparable
    {
        public string Type
        {
            get
            {
                return this._Type;
            }
            protected set
            {
                this._Type = value;
            }
        }
        string _Type;
        public abstract double Perimeter();
        public abstract double Area();

        public override string ToString()
        {
            return this.Type + " площадью " + this.Area().ToString();
        }
        public int CompareTo(object obj)
        {
            //Приведение параметра к типу "фигура"
            Figure p = (Figure)obj;
            //Сравнение
            if (this.Area() < p.Area()) return -1;
            else if (this.Area() == p.Area()) return 0;
            else return 1; //(this.Area() > p.Area())
        }
    }

    interface IPrint
    {
        void Print();
    }


    class Rectangle : Figure,IPrint
    {
        public double Width;
        public double Height;

        public Rectangle(double width, double height)
        {
            Console.WriteLine("Создан Прямоугольник");
            this.Width = width;
            this.Height = height;
        }
        public override double Perimeter()
        {
            return Width * 2 + Height * 2;
        }
        public override double Area()
        {
            return Width * Height;
        }
        public override string ToString()
        {
            return "Прямоугольник: " + Convert.ToString(Width) + "x" + Convert.ToString(Height) + ", Площадь: " + Convert.ToString(Area());
        }
        public void Print()
        {
            Console.WriteLine("=== Прямоугольник ===");
            Console.WriteLine(ToString());
        }
    }

    class Square : Rectangle
    {
        public Square(double width) :base (width,width)
        {
            Console.WriteLine("Создан Квадрат");
        }
        public override string ToString()
        {
            return "Квадрат " + Convert.ToString(Width) + "x" + Convert.ToString(Height) + ", Площадь: " + Convert.ToString(Area());
        }
        public void Print()
        {
            Console.WriteLine("=== Квадрат ===");
            Console.WriteLine(ToString());
        }
    }

    class Circle : Figure
    {
        public double Radius;

        public Circle( double radius)
        {
            Console.WriteLine("Создан Круг");
            this.Radius = radius;
        }
        public override double Perimeter()
        {
            return 2*3.14*Radius;
        }
        public override double Area()
        {
            return 3.14 * Math.Pow(Radius,2);   
        }
        public override string ToString()
        {
            return "Радиус: " + Convert.ToString(Radius) + ", Площадь: " + Convert.ToString(Area());
        }
        public void Print()
        {
            Console.WriteLine("=== Круг ===");
            Console.WriteLine(ToString());
        }
    }

    public interface IMatrixCheckEmpty<T>
    {
        T getEmptyElement(); /// Возвращает пустой элемент
        bool checkEmptyElement(T element); /// Проверка что элемент является пустым
    }
    public class Matrix<T>
    {
        Dictionary<string, T> _matrix = new Dictionary<string, T>();/// Словарь для хранения значений
        int maxX;/// Количество элементов по горизонтали (максимальноеколичество столбцов)
        int maxY;/// Количество элементов по вертикали (максимальноеколичество строк)
        int maxZ;/// Количество элементов по глубине (максимальноеколичество строк)
        IMatrixCheckEmpty<T> сheckEmpty; /// Реализация интерфейса для проверки пустого элемента
        public Matrix(int px, int py,int pz,IMatrixCheckEmpty<T> сheckEmptyParam)
        {
            this.maxX = px;
            this.maxY = py;
            this.maxZ = pz;
            this.сheckEmpty = сheckEmptyParam;
        }
        public T this[int x, int y,int z] /// Индексатор для доступа к данных
        {
            set
            {
                CheckBounds(x, y,z);
                string key = DictKey(x, y,z);
                this._matrix.Add(key, value);
            }
            get
            {
                CheckBounds(x, y,z);
                string key = DictKey(x, y,z);
                if (this._matrix.ContainsKey(key))
                {
                    return this._matrix[key];
                }
                else
                {
                    return this.сheckEmpty.getEmptyElement();
                }
            }
        }
        void CheckBounds(int x, int y,int z) /// Проверка границ
        {
            if (x < 0 || x >= this.maxX)
            {
                throw new ArgumentOutOfRangeException("x",
                "x=" + x + " выходит за границы");
            }
            if (y < 0 || y >= this.maxY)
            {
                throw new ArgumentOutOfRangeException("y",
                "y=" + y + " выходит за границы");
            }
            if (z < 0 || z >= this.maxZ)
            {
                throw new ArgumentOutOfRangeException("z",
                "z=" + z + " выходит за границы");
            }
        }
        string DictKey(int x, int y,int z) /// Формирование ключа
        {
            return x.ToString() + "_" + y.ToString()+"_"+z.ToString();
        }
        public override string ToString() /// Приведение к строке
        {
            StringBuilder b = new StringBuilder();
            for (int k = 0; k < this.maxZ; k++)
            {
                b.Append(Convert.ToString(k + 1) + " измерение\n");
                b.Append("[");
                for (int j = 0; j < this.maxY; j++)
                {
                    for (int i = 0; i < this.maxX; i++)
                    {
                        //Добавление разделителя-табуляции
                        if (i > 0)
                        {
                            b.Append("\t");
                        }
                        //Если текущий элемент не пустой
                        if (!this.сheckEmpty.checkEmptyElement(this[i, j, k]))
                        {
                            //Добавить приведенный к строке текущий элемент
                            b.Append(this[i, j, k].ToString());
                        }
                        else
                        {
                            //Иначе добавить признак пустого значения
                            b.Append(" - ");
                        }
                    }
                    b.Append("\n");
                }
                b.Append("]\n");
            }
            return b.ToString();
        }
    }
    class FigureMatrixCheckEmpty : IMatrixCheckEmpty<Figure>
    {
        public Figure getEmptyElement()      /// В качестве пустого элемента возвращается null
        {
            return null;
        }
        public bool checkEmptyElement(Figure element) /// Проверка что переданный параметр равен null
        {
            bool Result = false;
            if (element == null)
            {
                Result = true;
            }
            return Result;
        }
    }



    public class SimpleListItem<T>
    {
        public T data { get; set; }// Данные
        public SimpleListItem<T> next { get; set; }  /// Следующий элемент
        public SimpleListItem(T param)///конструктор
        {
            this.data = param;
        }
    }
    public class SimpleList<T> : IEnumerable<T> 
    where T : IComparable
    {
        protected SimpleListItem<T> first = null;/// Первый элемент списка
        protected SimpleListItem<T> last = null;     /// Последний элемент списка
        public int Count       /// Количество элементов
        {
            get { return _count; }
            protected set { _count = value; }
        }
        int _count;
        public void Add(T element)      /// Добавление элемента
        {
            SimpleListItem<T> newItem =
            new SimpleListItem<T>(element);
            this.Count++;
          
            if (last == null)  //Добавление первого элемента
            {
                this.first = newItem;
                this.last = newItem;
            }
            else//Добавление следующих элементов
            {
                this.last.next = newItem;//Присоединение элемента к цепочке
                this.last = newItem;//Присоединенный элемент считается последним
            }
        }
        public SimpleListItem<T> GetItem(int number) /// Чтение контейнера с заданным номером
        {
            if ((number < 0) || (number >= this.Count))
            {
                throw new Exception("Выход за границу индекса");//Можно создать собственный класс исключения
            }
            SimpleListItem<T> current = this.first;
            int i = 0;
            while (i < number)//Пропускаем нужное количество элементов
            {
                current = current.next;//Переход к следующему элементу
                i++;//Увеличение счетчика
            }
            return current;
        }
        public T Get(int number)// Чтение элемента с заданным номером
        {
            return GetItem(number).data;
        }
        public IEnumerator<T> GetEnumerator() /// Для перебора коллекции
        {
            SimpleListItem<T> current = this.first; //Перебор элементов
           
            while (current != null)
            {
                yield return current.data;//Возврат текущего значения
                current = current.next; //Переход к следующему элементу
            }
        }
        //Реализация обобщенного IEnumerator<T> требует реализациинеобобщенного интерфейса
        //Данный метод добавляется автоматически при реализации интерфейса
        System.Collections.IEnumerator
       System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
  
        public void Sort() /// Cортировка
        {
            Sort(0, this.Count - 1);
        }
        private void Sort(int low, int high) /// Алгоритм быстрой сортировки
        {
            int i = low;
            int j = high;
            T x = Get((low + high) / 2);
            do
            {
                while (Get(i).CompareTo(x) < 0) ++i;
                while (Get(j).CompareTo(x) > 0) --j;
                if (i <= j)
                {
                    Swap(i, j);
                    i++; j--;
                }
            } while (i <= j);

            if (low < j) Sort(low, j);
            if (i < high) Sort(i, high);
        }
        private void Swap(int i, int j) /// Вспомогательный метод для обмена элементов присортировке
        {
            SimpleListItem<T> ci = GetItem(i);
            SimpleListItem<T> cj = GetItem(j);
            T temp = ci.data;
            ci.data = cj.data;
            cj.data = temp;
        }
    }


    class SimpleStack<T> : SimpleList<T> where T : IComparable
    {
        public void Push(T element)/// Добавление в стек
        {
            Add(element); //Добавление в конец списка уже реализовано
        }
        public T Pop()// Удаление и чтение из стека
        {
            T Result = default(T);//default(T) - значение для типа T по умолчанию
            if (this.Count == 0) return Result; //Если стек пуст, возвращается значение по умолчанию для типа  
            if (this.Count == 1) //Если элемент единственный
            {
                Result = this.first.data;//то из него читаются данные
                this.first = null;//обнуляются указатели начала и конца списка
                this.last = null;
            }
            //В списке более одного элемента
            else
            {
                //Поиск предпоследнего элемента
                SimpleListItem<T> newLast = this.GetItem(this.Count - 2);
                //Чтение значения из последнего элемента
                Result = newLast.next.data;
                //предпоследний элемент считается последним
                this.last = newLast;
                //последний элемент удаляется из списка
                newLast.next = null;
            }
            //Уменьшение количества элементов в списке
            this.Count--;
            //Возврат результата
            return Result;
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Rectangle a = new Rectangle(10, 20);
            //a.Print();
            //Console.WriteLine();
            Square b = new Square(10);
            //b.Print();
            //Console.WriteLine();
            Circle c=new Circle(10);
            //c.Print();
            Console.WriteLine();

            ArrayList al = new ArrayList();
            al.Add(a);
            al.Add(b);
            al.Add(c);

            Console.WriteLine("ArrayList");
            foreach (Object obj in al)
                Console.WriteLine(obj);
           Console.WriteLine();

           List <Figure> sl = new List<Figure>(){a,b,c};
           sl.Sort();

           Console.WriteLine("List");
           foreach (Object obj in sl)
               Console.WriteLine(obj);
           Console.WriteLine();



           Console.WriteLine("SparseMatrix");
           Matrix<Figure> matrix = new Matrix<Figure>(3, 3, 3, new FigureMatrixCheckEmpty());// { a, b, c };
           matrix[0, 0, 1] = a;
           matrix[1, 1,2] = b;
           matrix[2, 2,0] = c;                                   Console.WriteLine(matrix.ToString());


           Console.WriteLine("SimpleStack");
           SimpleStack<Figure> stack = new SimpleStack<Figure>();
           //добавление данных в стек
           stack.Push(a);
           stack.Push(b);
           stack.Push(c);
           //чтение данных из стека
           while (stack.Count > 0)
           {
               Figure f = stack.Pop();
               Console.WriteLine(f);
           }

            Console.ReadKey();
        }



    }
}
