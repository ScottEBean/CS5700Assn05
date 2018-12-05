using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class ColReducerSolver : PuzzleSolver
  {
    public ColReducerSolver( Puzzle puzzle ) : base(puzzle) { Name = "Column Reduction"; }

    public override void SolvePoint( int row, int col )
    {
      SolveTimer.Start();
      
      if (Sudoku.Grid[row, col].Value != '-') { SolveTimer.Stop(); return; }
      Count++;
      for (int i = 0; i < Sudoku.Size; i++)
      {
        if (i == row) { continue; }
        if (Sudoku.Grid[row, col].ValidOptions.Contains(Sudoku.Grid[i, col].Value))
        {
          Sudoku.Grid[row, col].ValidOptions.Remove(Sudoku.Grid[i, col].Value);
        }        
      }
      SolveTimer.Stop();
    }
  }
}
