using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class LastOptionSolver : PuzzleSolver
  {
    public LastOptionSolver(Puzzle puzzle) : base(puzzle){ Name = "Last Option Solver";  }

    public override void SolvePoint( int row, int col )
    {      
      if (Sudoku.Grid[row, col].ValidOptions.Count == 1)
      {
        SolveTimer.Start();
        Count++;
        Sudoku.Grid[row, col].Value = Sudoku.Grid[row, col].ValidOptions[0];
        Sudoku.Grid[row, col].ValidOptions.Remove(Sudoku.Grid[row, col].ValidOptions[0]);
        Sudoku.SolvedCellCount++;
        SolveTimer.Stop();
      }      
    }

  }
}
