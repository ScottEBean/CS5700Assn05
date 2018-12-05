using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class Cell
  {
    public int Row { get; private set; }
    public int Col { get; private set; }
    public char Value { get; set; }
    public List<char> ValidOptions { get; private set; }

    public Cell(Cell cell )
    {
      Row = cell.Row;
      Col = cell.Col;
      Value = cell.Value;
      ValidOptions = new List<char>(cell.ValidOptions);
    }

    public Cell( int row, int col, char value, List<char> set )
    {
      if(value != '-' && !set.Contains(value))
      {
        throw new Exception("Invalid character");
      }

      Row = row;
      Col = col;
      Value = value;
      if(Value == '-')
      {
        ValidOptions = new List<char>(set);
      }
      else
      {
        ValidOptions = new List<char> ();
      }
    }

    public override bool Equals( object obj )
    {
      var comparitor = obj as Cell;
      return Row == comparitor.Row && Col == comparitor.Col && Value == comparitor.Value;
    }
  }
}
