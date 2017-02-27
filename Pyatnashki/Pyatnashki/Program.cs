using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyatnashki
{
    class Program
    {
        static public void Print(Game gmb)
        {
            for (int i = 0; i < gmb.Len; i++)
            {
                for (int j = 0; j < gmb.Len; j++)
                {
                    Console.Write(string.Format("{0}\t", gmb[i, j]));
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            try
            {
            Game gmb = new Game(1, 2, 4, 3, 6, 8, 5, 7, 0);
            Print(gmb);

            int n = 0;

                while (!gmb.EndGame())
                {
                    Console.Write("Какое значение двигаем? ");
                    int val = int.Parse(Console.ReadLine());

                    try
                    {
                        gmb.Shift(val);
                        n++;
                    }
                    catch (Exception ex)  // возможные ошибки в ходе игры
                    {
                        Console.WriteLine(string.Format("Неправильный ход! {0}", ex.Message));
                    }
                    Print(gmb);
                }
                Console.WriteLine("Поздравляем! Игра завершена за {0} ходов!", n);
            }
            catch (Exception ex) //возможные ошибки при создании игры
            {
                Console.WriteLine(string.Format("Критическая ошибка! {0}", ex.Message));
            }
            Console.ReadKey();
        }
    }
}

