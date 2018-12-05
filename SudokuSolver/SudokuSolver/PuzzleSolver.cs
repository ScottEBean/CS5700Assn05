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
    public int Count { get; protected set; }
    public string Name { get; protected set; }
    public Stopwatch SolveTimer { get; protected set; }
    public Puzzle Sudoku { get; protected set; }

    public PuzzleSolver(Puzzle puzzle )
    {
      Sudoku = puzzle;
      SolveTimer = new Stopwatch();
      Count = 0;
    }

    

    public abstract void SolvePoint(int row, int col );
    
  }
}
