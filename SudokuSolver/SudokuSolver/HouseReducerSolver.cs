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
      
      if (Sudoku.Grid[row, col].Value != '-') { SolveTimer.Stop(); return; }
      Count++;
      var house = Sudoku.GetHouse(row, col);

      for (int i = 0; i < Sudoku.Size; i++)
      {
        if (row == house[i].Row && col == house[i].Col) { continue; }

        if (Sudoku.Grid[row, col].ValidOptions.Contains(house[i].Value))
        {
          Sudoku.Grid[row, col].ValidOptions.Remove(house[i].Value);
        }
      }

      SolveTimer.Stop();
    }
  }
}
