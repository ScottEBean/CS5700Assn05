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
      
      if (Sudoku.Grid[row, col].Value != '-') { SolveTimer.Stop(); return; }
      Count++;
      OneOption(row, col, Sudoku.GetHouse(row, col));
      OneOption(row, col, Sudoku.GetSuperRow(row));
      OneOption(row, col, Sudoku.GetSuperCol(col));

      SolveTimer.Stop();

    }

    private void OneOption( int row, int col, Cell[] superCell )
    {
      var dashCount = superCell.Where(cell => cell.Value == '-').Count();
      
      if (dashCount == 1 && Sudoku.Grid[row, col].Value == '-')
      {       
        var options = new List<char>(Sudoku.SymbolSet);
        
        for (int i = 0; i < Sudoku.Size; i++)
        {
          options.Remove(superCell[i].Value);
        }

        Sudoku.Grid[row, col].Value = Sudoku.Grid[row, col].ValidOptions[0];
        Sudoku.SolvedCellCount++;        
      }
    }
  }
}
