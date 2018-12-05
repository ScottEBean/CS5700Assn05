using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class RowReducerSolver : PuzzleSolver
  {
    public RowReducerSolver( Puzzle puzzle ) : base(puzzle) { Name = "Row Reduction"; }

    public override void SolvePoint( int row, int col )
    {
      SolveTimer.Start();
      
      if(Sudoku.Grid[row,col].Value != '-') { SolveTimer.Stop(); return; }
      Count++;
      for (int j = 0; j < Sudoku.Size; j++)
      {
        if (j == col) { continue; }

        if (Sudoku.Grid[row, col].ValidOptions.Contains(Sudoku.Grid[row, j].Value))
        {
          Sudoku.Grid[row, col].ValidOptions.Remove(Sudoku.Grid[row, j].Value);
        }
      }

      SolveTimer.Stop();
    }
  }
}
