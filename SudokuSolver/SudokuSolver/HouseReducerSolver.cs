using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class HouseReducerSolver : PuzzleSolver
  {
    public HouseReducerSolver( Puzzle puzzle ) : base(puzzle) { Name = "House Reduction"; }

    public override void SolvePoint( int row, int col )
    {
      SolveTimer.Start();

      for (int i = row; i < row + Sudoku.SuperCellSize; i++)
      {
        for (int j = col; j < col + Sudoku.SuperCellSize; j++)
        {
          if(i == row && j == col) { continue; }
          if (Sudoku.Grid[row, col].ValidOptions.Contains(Sudoku.Grid[i,j].Value))
          {
            Sudoku.Grid[row, col].ValidOptions.Remove(Sudoku.Grid[i, j].Value);
          }
        }
      }

      SolveTimer.Stop();
    }
  }
}
