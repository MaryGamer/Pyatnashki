using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pyatnashki
{
    class Game
    {
        int[,] field;
        int size;
        Point[] points;

        public int Length { get { return size; } }
        
        public Game(params int[] val)
        {
            if (Math.Sqrt(val.Length) != (int)Math.Sqrt(val.Length))
            {
                throw new ArgumentException("Передано число параметров, не являющееся квадратом целого числа");
            }

            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] < 0)
                    throw new ArgumentException("В массиве не может быть отрицательных чисел");
            }

            int[] copy = new int[val.Length];
            for (int i = 0; i < val.Length; i++)
            {
                copy[i] = val[i];
            }
            Array.Sort(copy);
            for (int i = 0; i < val.Length; i++)
            {
                if (copy[i] != i)
                    throw new ArgumentException("Исходный массив содержит повторяющиеся числа");
            }

            size = (int)Math.Sqrt(val.Length);
            field = new int[size, size];
            points = new Point[copy.Length];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = val[i * size + j];
                    field[i, j] = value;
                    points[value] = new Point(j, i);
                }
            }
        }

        public static Game ReadCSV(string filename)
        {
            string[] text = File.ReadAllLines(filename);
            List<int> lst = new List<int>();

            for (int i = 0; i < text.Length; i++)
            {
                var StrMas = text[i].Split(';');
                for (int j = 0; j < StrMas.Length; j++)
                {
                    int val = int.Parse(StrMas[j]);
                    lst.Add(val);
                }
            }
            return new Game(lst.ToArray());
        }

        public int this[int x, int y]
        {
            get
            {
                return field[x, y];
            }
        }

        Point GetLocation(int value)
        {
            try
            {
                return points[value];
            }
            catch
            {
                throw new Exception("Нет такого значения в игре!");
            }
        }

        void swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        void swap(ref Point a, ref Point b)
        {
            Point t = a;
            a = b;
            b = t;
        }

        public void Shift(int value)
        {
            Point locV = GetLocation(value);
            Point loc0 = GetLocation(0);

            if (!(locV.Column == loc0.Column || locV.Row == loc0.Row))
            {
                throw new Exception("Нельзя двигать эту фишку!");
            }

            swap(ref field[locV.Row, locV.Column], ref field[loc0.Row, loc0.Column]);
            swap(ref points[0], ref points[value]);
        }

        public bool EndGame()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] != (i * size + j + 1) && (i != size - 1 || j != size - 1))
                        return false;
                    if (field[i, j] != 0 && i == size - 1 && j == size - 1)
                        return false;
                }
            }
            return true;
        }
    }
}
