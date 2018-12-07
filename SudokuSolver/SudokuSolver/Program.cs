using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
  public class Program
  {


    static void Main( string[] args )
    {
      string inputFilePath = null;
      string outputFilePath = null;
      string inputDirectory = null;
      string outputDirectory = null;
      List<Puzzle> puzzles = new List<Puzzle>();
      List<PuzzleSolver> templates = new List<PuzzleSolver>();

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

      if (args.Length > 1)
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


      if (!String.IsNullOrEmpty(inputDirectory))
      {
        var inputFiles = Directory.EnumerateFiles(inputDirectory, "*.txt");
        foreach (var file in inputFiles)
        {
          try
          {
            inputFilePath = file;
            puzzles.Add(new PuzzleCreator(inputFilePath).CreatePuzzle());
          }
          catch (Exception e)
          {
            Console.Write(e.Message);
            Console.WriteLine(" - " + file.ToString());
          }
        }
      }
      else
      {
        puzzles.Add(new PuzzleCreator(inputFilePath).CreatePuzzle());
      }

      foreach (var puzzle in puzzles)
      {
        templates.Clear();
        templates.Add(new RowReducerSolver(puzzle));
        templates.Add(new ColReducerSolver(puzzle));
        templates.Add(new HouseReducerSolver(puzzle));
        templates.Add(new LastOptionSolver(puzzle));
        templates.Add(new OneOptionSolver(puzzle));

        Solve(puzzle, templates);

        if (String.IsNullOrEmpty(outputDirectory) && String.IsNullOrEmpty(outputFilePath))
        {
          puzzle.ConsolePrint(templates);
        }

        if (!String.IsNullOrEmpty(outputDirectory))
        {
          puzzle.FilePrint(outputDirectory, templates);
        }

        if (!String.IsNullOrEmpty(outputFilePath))
        {
          puzzle.FilePrint(outputFilePath, templates);
        }
      }      
    }

    public static void Solve( Puzzle puzzle, List<PuzzleSolver> templateList )
    {
      var solvedCount = puzzle.SolvedCellCount;
      var prevSolvedCount = 0;
      while (solvedCount > prevSolvedCount && !puzzle.IsSolved())
      {
        prevSolvedCount = puzzle.SolvedCellCount;

        for (int i = 0; i < puzzle.Size; i++)
        {
          for (int j = 0; j < puzzle.Size; j++)
          {
            templateList[0].SolvePoint(i, j);
            templateList[1].SolvePoint(i, j);
            templateList[2].SolvePoint(i, j);
            templateList[3].SolvePoint(i, j);
            templateList[4].SolvePoint(i, j);

            if (puzzle.IsSolved())
            {
              break;
            }
          }

          if (puzzle.IsSolved())
          {
            break;
          }
        }

        solvedCount = puzzle.SolvedCellCount;
      }
    }
  }
}
