using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  class Program
  {
    static void Main( string[] args )
    {
      string inputFilePath;
      string outputFilePath;     

      if (args.Length == 0)
      {
        Console.WriteLine("Please provide an input file path.");
        Console.WriteLine("Additionally, an output path may be specified as an argument after the input path.");
        Console.WriteLine("\n\nPress Any Key to Exit");
        Console.ReadLine();
        return;
      }

      if (args.Contains("-h"))
      {
        Console.WriteLine("Please enter at least 1 string specifying an input file path.");
        Console.WriteLine("Additionally, an output path may be specified as an argument after the input path.");
        Console.WriteLine("Usage: SudokuSolver <inputPath>");
        Console.WriteLine("or");
        Console.WriteLine("SudokuSolver <inputPath> <outputPath>");
        Console.WriteLine("\n\nPress Any Key to Exit");
        Console.ReadLine();
        return;
      }

      if(args.Length > 1)
      {
        inputFilePath = args[0];
        outputFilePath = args[1];
      }
      else
      {
        inputFilePath = args[0];
      }




      Puzzle sudokuPuzzle = new PuzzleCreator(inputFilePath).CreatePuzzle();

      sudokuPuzzle.ConsolePrint();


      Console.WriteLine("\n\nPress Any Key to Exit");
      Console.ReadLine();
    }
  }
}
