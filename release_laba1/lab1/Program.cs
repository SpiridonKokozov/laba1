using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.Title = "Кокозов С.И. гр.ИУ5-33Б";
            if (args.Length != 3)
            {
                System.Console.WriteLine("Необходимо ввести 3 параметра");
                Console.ReadKey();
                return 0;
            }
            double A, B, C,D;
            while (!double.TryParse(args[0], out A) || A==0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Ошибка ввода А");
                Console.ResetColor();
                System.Console.Write("Введите А:");
                args[0] =Console.ReadLine();
            }
            while (!double.TryParse(args[1], out B))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Ошибка ввода B");
                Console.ResetColor();
                System.Console.Write("Введите B:");
                args[1] = Console.ReadLine();
            }
            while (!double.TryParse(args[2], out C))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Ошибка ввода C");
                Console.ResetColor();
                System.Console.Write("Введите C:");
                args[2] = Console.ReadLine();
            }
             
            D = B * B - 4 * A * C;
            if (D < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Уравнение не имеет корней");
            }
            else if (D == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                double X=-B/(2*A);
                System.Console.WriteLine("Уравнение имеет один корень X={0}",X);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                double X1=(-B+Math.Sqrt(D))/(2*A);
                double X2=(-B-Math.Sqrt(D))/(2*A);
                System.Console.WriteLine("Уравнение имеет два корня: X1={0},  X2={1}",X1,X2);
            }            
            Console.ResetColor();
            Console.ReadKey();
            return 0;
        }
    }
}
