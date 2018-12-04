using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class OneOptionSolver : PuzzleSolver
  {

    public OneOptionSolver( Puzzle puzzle ) : base(puzzle) { Name = "One Option Solver"; }

    public override void SolvePoint( int row, int col )
    {
      SolveTimer.Start();


      LastOption(row, col, Sudoku.GetHouse(row, col));
      LastOption(row, col, Sudoku.GetSuperRow(row));
      LastOption(row, col, Sudoku.GetSuperCol(col));

      SolveTimer.Stop();

    }

    private void LastOption( int row, int col, Cell[] superCell )
    {

      var dashCount = superCell.Where(cell => cell.Value == '-').Count();

      if (dashCount == 1)
      {
        for (int i = 0; i < Sudoku.Size; i++)
        {
          Sudoku.Grid[row, col].ValidOptions.Remove(superCell[i].Value);
        }

        Sudoku.Grid[row, col].Value = Sudoku.Grid[row, col].ValidOptions[0];
        
      }
    }
  }
}
