using System;
using System.Collections.Generic;

namespace CSharpLifeGame
{
    class Program
    {

        struct Cell
        {
            public int x;
            public int y;
            public bool nowLifeStatus;
            public bool nextLifeStatus;
        }

        static List<Cell> GetCellsBoard(int row,int col)
        {
            var cellBoard = new List<Cell>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    cellBoard.Add(new Cell
                    {
                        x = i,
                        y = j,
                        nowLifeStatus = false,
                        nextLifeStatus = false
                    });
                }
            }

            return cellBoard;
        }

        static List<Cell> ActivateCell(List<Cell> cells, int count,int rowCount)
        {
            Random random = new Random();
            return cells;
        }

        static void Main(string[] args)
        {
            
        }
    }
}
