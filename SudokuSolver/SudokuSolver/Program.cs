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
      string inputFilePath = null;
      string outputFilePath = null;
      string inputDirectory = null;
      string outputDirectory = null;
      List<Puzzle> puzzles = new List<Puzzle>();
      Puzzle puzzle = null;

      if (args.Length == 0)
      {
        Console.WriteLine("Please provide an input file path or directory.");
        Console.WriteLine("Additionally, an output path/directory may be specified as an argument after the input path/directory.");
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
        if (Directory.Exists(args[0]))
        {
          inputDirectory = args[0];
          outputDirectory = args[1];
        }
        else
        {
          inputFilePath = args[0];
          outputFilePath = args[1];
        }
      }
      else
      {
        if (Directory.Exists(args[0]))
        {
          inputDirectory = args[0];
        }
        else
        {
          inputFilePath = args[0];
        }
      }


      if (inputDirectory.Length > 1)
      {
        var inputFiles = Directory.EnumerateFiles(inputDirectory, "*.txt");
        foreach (var file in inputFiles)
        {
          puzzles.Add(new PuzzleCreator(inputFilePath).CreatePuzzle());
        }
      }
      else
      {
        puzzle = new PuzzleCreator(inputFilePath).CreatePuzzle();
      }

      //do stuff on puzzles or puzzle here


      Console.WriteLine("\n\nPress Any Key to Exit");
      Console.ReadLine();
    }
  }
}
