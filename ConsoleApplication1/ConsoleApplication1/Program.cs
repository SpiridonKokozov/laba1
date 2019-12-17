using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    abstract class Figure
    {
        public abstract double Perimeter();
        public abstract double Area();
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
            return "Прямоугольник: " + Convert.ToString(Width) + "x" + Convert.ToString(Height) + "\nПериметр: " + Convert.ToString(Perimeter()) + "\nПлощадь: " + Convert.ToString(Area());
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
            return "Квадрат " + Convert.ToString(Width) + "x" + Convert.ToString(Height) + "\nПериметр: " + Convert.ToString(Perimeter()) + "\nПлощадь: " + Convert.ToString(Area());
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
            return "Радиус: " + Convert.ToString(Radius) + "\nПериметр: " + Convert.ToString(Perimeter()) + "\nПлощадь: " + Convert.ToString(Area());
        }
        public void Print()
        {
            Console.WriteLine("=== Круг ===");
            Console.WriteLine(ToString());
        }
    }





    class Program
    {
        static void Main(string[] args)
        {
            Rectangle a = new Rectangle(10, 20);
            a.Print();
            Console.WriteLine();
            Square b = new Square(10);
            b.Print();
            Console.WriteLine();
            Circle c=new Circle(10);
            c.Print();
            Console.ReadKey();
        }
    }
}
