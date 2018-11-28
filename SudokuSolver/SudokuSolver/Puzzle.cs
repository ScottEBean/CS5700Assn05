using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class Puzzle
  {
    public int Size { get; private set; }

    public Cell[,] OriginalGrid { get; private set; }
    public Cell[,] Grid { get; private set; }
    public List<char> SymbolSet { get; private set; }

    public Puzzle( int size, Cell[,] grid, List<char> symbolSet )
    {
      Size = size;
      OriginalGrid = new Cell[Size, Size];
      Grid = new Cell[Size, Size];
      GridDeepCopy(grid);
      SymbolSet = new List<char>(symbolSet);
    }

    private void GridDeepCopy( Cell[,] grid )
    {
      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
        {
          OriginalGrid[i, j] = new Cell(grid[i, j]);
          Grid[i, j] = new Cell(grid[i, j]);
        }
      }
    }

    public bool IsSolved( )
    {
      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
        {
          if (Grid[i, j].Value == '-')
          {
            return false;
          }
        }
      }
      return true;
    }

    public Cell[] GetSuperCell( int row, int col )
    {
      if (row < 0 || row > Size - 1 || col < 0 || col > Size - 1)
      {
        throw new Exception("Invalid bounds");
      }

      var superCellSize = (Int32)Math.Sqrt(Size);
      var cellArr = new List<Cell>(Size);

      GetTopLeft(row, col, out int tlRow, out int tlCol, superCellSize);
      

      for (int i = tlRow; i < tlRow + superCellSize; i++)
      {
        for (int j = tlCol; j < tlCol + superCellSize; j++)
        {
          cellArr.Add(Grid[i, j]);
        }
      }

      return cellArr.ToArray();
    }

    private void GetTopLeft( int row, int col, out int tlRow, out int tlCol, int size )
    {
      tlRow = -1;
      tlCol = -1;
      for (int i = size; i > 0; i--)
      {
        if (row >= i * size - size) { tlRow = i * size - size; }
        if (col >= i * size - size) { tlCol = i * size - size; }
      }
    }

    public Cell[] GetSuperCol( int col )
    {
      if (col < 0 || col > Size - 1)
      {
        throw new Exception("Invalid bounds");
      }

      var cellArr = new List<Cell>(Size);

      for (int i = 0; i < Size; i++)
      {
        cellArr.Add(Grid[i, col]);
      }

      return cellArr.ToArray();
    }

    public Cell[] GetSuperRow( int row )
    {
      if (row < 0 || row > Size - 1)
      {
        throw new Exception("Invalid bounds");
      }

      var cellArr = new List<Cell>(Size);

      for (int j = row; j < Size; j++)
      {
        cellArr.Add(Grid[row, j]);
      }

      return cellArr.ToArray();
    }

    public void ConsolePrint( )
    {
      Console.WriteLine(Size);
      for (int i = 0; i < Size; i++)
      {
        Console.Write($"{SymbolSet[i]} ");
      }

      Console.WriteLine();

      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
        {
          Console.Write($"{OriginalGrid[i, j].Value} ");
        }
        Console.WriteLine();
      }

      Console.WriteLine("Solution:");

      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
        {
          Console.Write($"{Grid[i, j].Value} ");
        }
        Console.WriteLine();
      }
    }

    public void FilePrint( string outputFilePath )
    {
      FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write);
      StreamWriter fileWriter = new StreamWriter(fileStream);

      fileWriter.WriteLine(Size);

      for (int i = 0; i < Size; i++)
      {
        fileWriter.Write($"{SymbolSet[i]} ");
      }

      fileWriter.WriteLine();

      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
        {
          fileWriter.Write($"{OriginalGrid[i, j].Value} ");
        }
        fileWriter.WriteLine();
      }

      fileWriter.WriteLine("Solution:");

      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
        {
          fileWriter.Write($"{Grid[i, j].Value} ");
        }
        fileWriter.WriteLine();
      }

      fileWriter.Close();
      fileStream.Close();
    }
  }
}
