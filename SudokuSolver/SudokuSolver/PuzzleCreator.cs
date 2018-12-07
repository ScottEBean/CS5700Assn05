using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class PuzzleCreator
  {
    public Cell[,] Grid { get; private set; }
    public int PuzzleSize { get; private set; }
    public int SolvedCellCount { get; private set; }
    private readonly int LineCount;
    public string Name { get; private set; }
    public List<char> SymbolSet { get; private set; }
    public FileStream InputFileStream { get; private set; }
    public StreamReader InputFileReader { get; private set; }


    public PuzzleCreator( string inputPath )
    {
      if (!File.Exists(inputPath))
      {
        throw new Exception($"The path {inputPath} does not exist!");
      }

      Name = inputPath.Split('/').Last().Replace(".txt", "");
      LineCount = File.ReadLines(inputPath).Count();
      InputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      InputFileReader = new StreamReader(InputFileStream);


      SetPuzzleSize();
      SetSymbols();
      SetGrid();

      InputFileStream.Close();
      InputFileReader.Close();

      if (!HasDistinctCells())
      {
        throw new Exception("Invalid puzzle: Duplicate characters in row, col, or house");
      }
    }

    private void SetPuzzleSize( )
    {
      PuzzleSize = Convert.ToInt32(InputFileReader.ReadLine().Replace("\r\n", ""));
      var allowableSizes = new List<int> { 4, 9, 16, 25, 36 };
      if (!allowableSizes.Contains(PuzzleSize))
      {
        throw new Exception("Invalid puzzle: Invalid puzzle size");
      }

      if (LineCount < PuzzleSize + 2 || LineCount >= PuzzleSize + 3)
      {
        throw new Exception("Invalid puzzle: Puzzle is too small or too large");
      }
    }

    private void SetSymbols( )
    {
      SymbolSet = new List<char> { };
      var symbolList = InputFileReader.ReadLine().Replace("\r\n", "").Replace(" ", "");

      foreach (var symbol in symbolList)
      {
        SymbolSet.Add(symbol);
      }
    }

    private void SetGrid( )
    {
      Grid = new Cell[PuzzleSize, PuzzleSize];

      for (int i = 0; i < PuzzleSize; i++)
      {
        var line = InputFileReader.ReadLine().Replace("\r\n", "").Replace(" ", "");

        if (line.Length != PuzzleSize)
        {
          throw new Exception("Invalid puzzle: Too few or too many grid cells");
        }

        for (int j = 0; j < PuzzleSize; j++)
        {
          var value = line[j];
          if (value != '-') { SolvedCellCount++; }
          Grid[i, j] = new Cell(i, j, value, SymbolSet);
        }
      }
    }

    private bool HasDistinctCells( )
    {
      string vals = "";

      // check rows for duplicates
      for (int i = 0; i < PuzzleSize; i++)
      {
        for (int j = 0; j < PuzzleSize; j++)
        {
          if (Grid[i, j].Value != '-')
          {
            vals += Grid[i, j].Value;
          }
        }

        if (vals.Distinct().Count() != vals.Length)
        {
          return false;
        }

        vals = "";
      }

      

      // Check columns for duplicates
      for (int j = 0; j < PuzzleSize; j++)
      {
        for (int i = 0; i < PuzzleSize; i++)
        {
          if (Grid[i, j].Value != '-')
          {
            vals += Grid[i, j].Value;
          }
        }

        if (vals.Distinct().Count() != vals.Length)
        {
          return false;
        }
        vals = "";
      }

      var houseSize = Convert.ToInt32(Math.Sqrt(PuzzleSize));

      // Check houses for duplicates
      for (int i = 0; i < PuzzleSize; i+= houseSize)
      {
        for (int j = 0; j < PuzzleSize; j += houseSize)
        {
          for (int k = i; k < i + houseSize; k++)
          {
            for (int l = j; l < j + houseSize; l++)
            {
              if (Grid[k, l].Value != '-')
              {
                vals += Grid[k, l].Value;
              }
            }
          }
          if (vals.Distinct().Count() != vals.Length)
          {
            return false;
          }
          vals = "";
        }
      }

      return true;
    }

    public Puzzle CreatePuzzle( )
    {
      return new Puzzle(PuzzleSize, Grid, SymbolSet, SolvedCellCount, Name);
    }
  }
}
