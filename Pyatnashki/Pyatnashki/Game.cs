﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyatnashki
{
    class Game
    {
        int[,] field;
        int size;

        public int Len { get { return size; } }
        public int Value { get; set; }

        public Game (params int[] val)
        {
            if (Math.Sqrt(val.Length) != (int)Math.Sqrt(val.Length))
            {
                throw new Exception("Передано число параметров, не являющееся квадратом целого числа");
            }

            // Еще проверки?

            size = (int)Math.Sqrt(val.Length);
            field = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    field[i, j] = val[i * size + j];
                }
            }
        }

        public int this[int x, int y] //индексатор
        {
            get
            {
                return field[x, y];
            }
        }

        Point GetLocation(int value)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] == value)
                    {
                        return new Point(j, i);
                    }
                }
            }
            return new Point(-1, -1);
        }
            
        public void Shift(int value)
        {
            Point loc = GetLocation(value);
            if (loc.X == -1 || loc.Y == -1)
            {
                throw new Exception("Нет такого значения в игре!");
            }

            Point[] mas = new Point[4]
            {
                new Point(loc.X, loc.Y > 0 ? loc.Y - 1 : loc.Y),
                new Point(loc.X, loc.Y < size - 1 ? loc.Y + 1 : loc.Y),
                new Point(loc.X > 0 ? loc.X - 1 : loc.X, loc.Y),
                new Point(loc.X < size - 1 ? loc.X + 1 : loc.X, loc.Y)
            };

            for (int i = 0; i < 4; i++)
            {
                if (field[mas[i].Y, mas[i].X] == 0)
                {
                    field[loc.Y, loc.X] = 0;
                    field[mas[i].Y, mas[i].X] = value;
                    return;
                }
            }
            throw new Exception("Нельзя двигать эту фишку!");
        }

        public bool EndGame()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] != (i * size + j + 1)
                        && (field[i, j] != 0 && i == size - 1 && j == size - 1))
                        return false;
                }
            }
            return true;
        }
    }
}
