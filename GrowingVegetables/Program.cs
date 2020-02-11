using System;
using System.Collections.Generic;
using System.Threading;

namespace GrowingVegetables
{
    enum CellState
    {
        Empty,
        Planted,
        Green,
        Yellow,
        Red,
        Overgrow
    }

    class Cell
    {
        public CellState state = CellState.Empty;

        public void Plant()
        {
            state = CellState.Planted;
        }

        public void Harvest()
        {
            state = CellState.Empty;
        }

        public void NextState()
        {
            if ((state != CellState.Empty) && (state != CellState.Overgrow))
                state++;
        }

        public override string ToString()
        {
            switch (state)
            {
                case CellState.Planted:
                    return ".";
                case CellState.Green:
                    return "o";
                case CellState.Yellow:
                    return "i";
                case CellState.Red:
                    return "Y";
                case CellState.Overgrow:
                    return "@";
            }

            return " ";
        }
    }

    internal class Program
    {
        private const int fieldSize = 10;
        Cell[] field = new Cell[fieldSize];

        private void run()
        {
            for (int i = 0; i < fieldSize; i++)
                field[i] = new Cell();
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            char c = '-';
            while (c != 'q')
            {
                c = Console.ReadKey().KeyChar;
                if ((c >= '0') && (c <= '9'))
                {
                    int cellIndex = int.Parse(c.ToString());
                    if (field[cellIndex].state == CellState.Empty)
                        field[cellIndex].Plant();
                    else
                        field[cellIndex].Harvest();
                }
            }
        }

        private void PrintField()
        {
            Console.WriteLine();
            for (int i = 0; i < fieldSize; i++)
                Console.Write(field[i].ToString() + " ");
        }

        private void TimerCallback(Object o)
        {
            for (int i = 0; i < fieldSize; i++)
                field[i].NextState();
            PrintField();
            GC.Collect();
        }

        public static void Main(string[] args)
        {
            new Program().run();
        }
    }
}