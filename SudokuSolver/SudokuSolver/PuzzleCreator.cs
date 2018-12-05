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
    public List<char> SymbolSet { get; private set; }
    public FileStream InputFileStream { get; private set; }
    public StreamReader InputFileReader { get; private set; }


    public PuzzleCreator( string inputPath )
    {
      if (!File.Exists(inputPath))
      {
        throw new Exception($"The path {inputPath} does not exist!");
      }

      InputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      InputFileReader = new StreamReader(InputFileStream);


      SetPuzzleSize();
      SetSymbols();
      SetGrid();

      InputFileStream.Close();
      InputFileReader.Close();
    }

    private void SetPuzzleSize( )
    {
      PuzzleSize = Convert.ToInt32(InputFileReader.ReadLine().Replace("\r\n", ""));

      var allowableSizes = new List<int> { 4, 9, 16, 25, 36 };
      if (!allowableSizes.Contains(PuzzleSize))
      {
        throw new Exception("Invalid puzzle size");
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

        for (int j = 0; j < PuzzleSize; j++)
        {
          var value = line[j];
          if(value != '-') { SolvedCellCount++; }
          Grid[i, j] = new Cell(i, j, value, SymbolSet);
        }
      }
    }

    public Puzzle CreatePuzzle( )
    {
      return new Puzzle(PuzzleSize, Grid, SymbolSet, SolvedCellCount);
    }
  }
}
