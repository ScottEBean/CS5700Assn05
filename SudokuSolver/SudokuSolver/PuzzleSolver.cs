using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public abstract class PuzzleSolver
  {
    public Stopwatch SolveTimer { get; protected set; }
    public Puzzle Sudoku { get; protected set; }

    public PuzzleSolver(Puzzle puzzle )
    {
      Sudoku = puzzle;
    }

    public bool IsSolution()
    {
      if (!Sudoku.IsSolved()) { return false; }
      return true;
    }

    public bool SolvePuzzle()
    {
      bool notFinished = true;
      while (notFinished)
      {
        notFinished = false;
        for (int i = 0; i < Sudoku.Size; i++)
        {
          for (int j = 0; j < Sudoku.Size; j++)
          {
            if(Sudoku.Grid[i,j].Value == '-')
            {
              notFinished = SolvePoint(i, j) ? true : false;
            }
          }
        }
      }

      return Sudoku.IsSolved();      
    }    

    protected bool HasCompleteSet( Cell[] superCell)
    {
       
      var currentValues = new List<char> { };
      for (int i = 0; i < Sudoku.Size; i++)
      {
        currentValues.Add(superCell[i].Value);
      }

      var listEquality = currentValues.All(Sudoku.SymbolSet.Contains) && currentValues.Count == Sudoku.SymbolSet.Count;

      if (listEquality)
      {
        return true;
      }

      return false;
    }

    public abstract bool SolvePoint(int row, int col );
    
    public string PrintTime( )
    {
      var output = $"{this.GetType().Name}: {SolveTimer.Elapsed}";
      return output;
    }
  }
}
