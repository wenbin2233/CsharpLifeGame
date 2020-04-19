using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLifeGame
{
    class Program
    {

        struct Cell
        {
            public int X;
            public int Y;
            public bool NowLifeStatus;
            public bool NextLifeStatus;
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
                        X = i,
                        Y = j,
                        NowLifeStatus = false,
                        NextLifeStatus = false
                    });
                }
            }

            return cellBoard;
        }

        static List<Cell> RandomActivateCell(List<Cell> cells, int count,int rowCount)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                var randomX = random.Next(0,rowCount);
                var randomY = random.Next(0,rowCount);
                var index = cells.FindIndex(c => c.X == randomX && c.Y == randomY);
                var cell = cells.Find(c => c.X == randomX && c.Y == randomY);
                cells.Remove(cell);
                cell.NowLifeStatus = true;
                cells.Insert(index, cell);
            }
            return cells;
        }

        static List<Cell> StaticActivateCell(List<Cell> cells,int startX,int startY, int rowCount,int colCount)
        {
            for (int i = startX; i < startX + rowCount; i++)
            {
                for (int j = startY; j < startY + colCount; j++)
                {
                    var index = cells.FindIndex(c => c.X == i && c.Y == j);
                    var cell = cells.Find(c => c.X == i && c.Y == j);
                    cells.Remove(cell);
                    cell.NowLifeStatus = true;
                    cells.Insert(index, cell);
                }
            }
            return cells;
        }

        static List<Cell> EvolutionCell(List<Cell> cells, int row, int col)
        {
            var evolutionCell = new List<Cell>();
            var refreshCell = new List<Cell>();
            foreach (var cell in cells)
            {
                var aliveNeighbor = 0;
                var newCell = cell;
                for (int i = cell.X - 1; i <= cell.X + 1; i++)
                {
                    if (i < 0 || i >= row) continue;

                    for (int j = cell.Y - 1; j <= cell.Y + 1; j++)
                    {
                        if (j < 0 || j >= col) continue;

                        if (i == cell.X && j == cell.Y) continue;

                        if (cells.FirstOrDefault(c => c.X == i && c.Y == j).NowLifeStatus)
                        {
                            aliveNeighbor++;
                        }
                    }
                }

                if (cell.NowLifeStatus)
                {
                    if (aliveNeighbor < 2)
                    {
                        newCell.NextLifeStatus = false;
                    }
                    if (aliveNeighbor == 2 || aliveNeighbor == 3)
                    {
                        newCell.NextLifeStatus = true;
                    }

                    if (aliveNeighbor > 3)
                    {
                        newCell.NextLifeStatus = false;
                    }
                }

                if (aliveNeighbor == 3 && !cell.NowLifeStatus)
                {
                    newCell.NextLifeStatus = true;
                }

                evolutionCell.Add(newCell);
            }

            foreach (var evoCell in evolutionCell)
            {
                var cell = evoCell;
                cell.NowLifeStatus = cell.NextLifeStatus;
                refreshCell.Add(cell);
            }

            return refreshCell;
        }

        static string StatusStringConverter(bool status)
        {
            return status ? " 1 " : "   ";
        }

        static void Main(string[] args)
        {
            var count = 30;
            var board = GetCellsBoard(count, count);
            //var aliveCell = RandomActivateCell(board, 9, count);
            var aliveCell = StaticActivateCell(board, 15, 15,3,3);
            var age = 0;
            while (aliveCell.Count(c => c.NowLifeStatus) != 0)
            {
                Console.WriteLine($"第{age}代，{aliveCell.Count(c => c.NowLifeStatus)}个细胞存活");
                for (var i = 0; i < count; i++)
                {
                    for (var j = 0; j < count; j++)
                    {
                        Console.Write(StatusStringConverter(aliveCell.FirstOrDefault(c => c.X==i&&c.Y==j).NowLifeStatus));
                    }

                    Console.WriteLine();
                }

                aliveCell = EvolutionCell(aliveCell, count, count);
                age++;
                Console.Read();
            }
            
        }
    }
}
