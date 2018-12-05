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
    public int SuperCellSize { get; private set; }
    public int SolvedCellCount { get; set; }

    public Cell[,] OriginalGrid { get; private set; }
    public Cell[,] Grid { get; set; }
    public List<char> SymbolSet { get; private set; }

    public Puzzle( int size, Cell[,] grid, List<char> symbolSet, int solvedCount )
    {
      Size = size;
      SuperCellSize = Convert.ToInt32(Math.Sqrt(Size));
      OriginalGrid = new Cell[Size, Size];
      Grid = new Cell[Size, Size];
      GridDeepCopy(grid);
      SymbolSet = new List<char>(symbolSet);
      SolvedCellCount = solvedCount;
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
      // Check each cell for '-' character.
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

      // Check rows and cols for complete sets
      for (int i = 0; i < Size; i++)
      {
        if (!IsRowSolved(i)) { return false; }
        if(!IsColSolved(i)) { return false; }
      }

      // Check houses for complete sets
      for (int i = 0; i < Size - SuperCellSize; i += SuperCellSize)
      {
        for (int j = 0; j < Size - SuperCellSize; j += SuperCellSize)
        {
          if(!IsHouseSolved(i, j))
          {
            return false;
          }
        }
      }

      return true;
    }

    private bool IsRowSolved(int row)
    {
      var superCellSymbols = new List<char>(SymbolSet);

      for (int j = 0; j < Size; j++)
      {
        if(superCellSymbols.Contains(Grid[row, j].Value))
        {
          superCellSymbols.Remove(Grid[row, j].Value);
        }
      }

      if(superCellSymbols.Count > 0) { return false; }

      return true;
    }

    private bool IsColSolved( int col )
    {
      var superCellSymbols = new List<char>(SymbolSet);

      for (int i = 0; i < Size; i++)
      {
        if (superCellSymbols.Contains(Grid[i, col].Value))
        {
          superCellSymbols.Remove(Grid[i, col].Value);
        }
      }

      if (superCellSymbols.Count > 0) { return false; }

      return true;
    }

    private bool IsHouseSolved(int row, int col )
    {
      var superCellSymbols = new List<char>(SymbolSet);

      for (int i = row; i < row + SuperCellSize; i++)
      {
        for (int j = col; j < col + SuperCellSize; j++)
        {
          if (superCellSymbols.Contains(Grid[i, j].Value))
          {
            superCellSymbols.Remove(Grid[i, j].Value);
          }
        }
      }
      if (superCellSymbols.Count > 0) { return false; }

      return true;
    }


    public Cell[] GetHouse( int row, int col )
    {
      if (row < 0 || row > Size - 1 || col < 0 || col > Size - 1)
      {
        throw new Exception("Invalid bounds");
      }

      var cellArr = new List<Cell>(Size);

      GetTopRow(row, out int tlRow);
      GetTopCol(col, out int tlCol);
      

      for (int i = tlRow; i < tlRow + SuperCellSize; i++)
      {
        for (int j = tlCol; j < tlCol + SuperCellSize; j++)
        {
          cellArr.Add(Grid[i, j]);
        }
      }

      return cellArr.ToArray();
    }

    private void GetTopRow( int row,  out int tlRow)
    {
      tlRow = -1;      
      for (int i = SuperCellSize; i > 0; i--)
      {
        if (row >= i * SuperCellSize - SuperCellSize)
        {
          tlRow = i * SuperCellSize - SuperCellSize;
          return;
        }        
      }      
    }

    private void GetTopCol( int col, out int tlCol )
    {
      tlCol = -1;
      for (int i = SuperCellSize; i > 0; i--)
      {
        if (col >= i * SuperCellSize - SuperCellSize)
        {
          tlCol = i * SuperCellSize - SuperCellSize;
          return;
        }
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

      for (int j = 0; j < Size; j++)
      {
        cellArr.Add(Grid[row, j]);
      }

      return cellArr.ToArray();
    }

    public void ConsolePrint( List<PuzzleSolver> solverList )
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

      Console.WriteLine();
      var ts = new TimeSpan();
      foreach(var template in solverList)
      {
        ts += template.SolveTimer.Elapsed;
      }

      string elapsedTime = ts.ToString("G");

      Console.WriteLine($"Total Time: {elapsedTime}");
      Console.WriteLine();
      Console.WriteLine("{0,-23} {1,-12} {2,-23}\n",
                        "Strategy", "Uses", "Time");

      foreach (var template in solverList)
      {
        string time = template.SolveTimer.Elapsed.ToString("G");
        Console.WriteLine("{0,-23} {1,-12} {2,-23}",
                        template.Name, template.Count, time);
      }
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

    public void FilePrint( string outputFilePath, List<PuzzleSolver> solverList )
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

      fileWriter.WriteLine();
      var ts = new TimeSpan();
      foreach (var template in solverList)
      {
        ts += template.SolveTimer.Elapsed;
      }

      string elapsedTime = ts.ToString("G");

      fileWriter.WriteLine($"Total Time: {elapsedTime}");
      fileWriter.WriteLine();
      fileWriter.WriteLine("{0,-23} {1,-12} {2,-23}\n",
                        "Strategy", "Uses", "Time");

      foreach (var template in solverList)
      {
        string time = template.SolveTimer.Elapsed.ToString("G");
        fileWriter.WriteLine("{0,-23} {1,-12} {2,-23}",
                        template.Name, template.Count, time);
      }

      fileWriter.Close();
      fileStream.Close();
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
    }
  }
}
