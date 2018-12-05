using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Tests
{
  [TestClass()]
  public class PuzzleTests
  {
    readonly string invalidInputPath0 = "InvalidInputPath.txt";
    readonly string invalidPuzzlePath0 = "C:/OOProjects/Assn05/SudokuSolver/SudokuSolver/17x17invalid.txt";
    readonly string testInputPath0 = "C:/OOProjects/Assn05/SudokuSolver/SudokuSolver/16X16.txt";
    readonly string testInputPath1 = "C:/OOProjects/Assn05/SudokuSolver/SudokuSolver/Solved4x4.txt";
    readonly string testOutputPath = "C:/OOProjects/Assn05/SudokuSolver/SudokuSolver/bin/Debug/testOutput.txt";
    List<char> testSymbolSet0 = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
    List<char> testSymbolSet1 = new List<char> { '1', '2', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G' };


    [TestMethod()]
    public void CellWithValueConstructorTest( )
    {
      var testCell = new Cell(0, 0, '4', new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G' });
      var expectedValidOptions = new List<char> { '4' };
      var listEquality = testCell.ValidOptions.All(expectedValidOptions.Contains) && testCell.ValidOptions.Count == 1;

      Assert.AreEqual(0, testCell.Row);
      Assert.AreEqual(0, testCell.Col);
      Assert.AreEqual('4', testCell.Value);
      Assert.IsTrue(listEquality);
    }

    [TestMethod()]
    public void CellWithDashConstructorTest( )
    {
      var testCell = new Cell(23, 0, '-', new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G' });

      Assert.AreEqual(23, testCell.Row);
      Assert.AreEqual(0, testCell.Col);
      Assert.AreEqual('-', testCell.Value);      
      var listEquality = testCell.ValidOptions.All(testSymbolSet0.Contains) && testCell.ValidOptions.Count == testSymbolSet0.Count;
      Assert.IsTrue(listEquality);
    }

    [TestMethod()]
    public void InvalidCellConstructorTest( )
    {
      try
      {
        var testCell = new Cell(23, 0, '$', new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G' });
      }
      catch(Exception e)
      {
        Assert.AreEqual(e.Message, "Invalid character");
      }      
    }
    
    [TestMethod()]
    public void RemoveCellOptionTest( )
    {
      var testCell = new Cell(23, 0, '-', new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G' });

      testCell.ValidOptions.Remove('3');
      var listEquality = testCell.ValidOptions.All(testSymbolSet1.Contains) && testCell.ValidOptions.Count == testSymbolSet1.Count;
      Assert.IsTrue(listEquality);
    }

    [TestMethod()]
    public void PuzzleCreatorConstructorTest( )
    {
      var testCreator = new PuzzleCreator(testInputPath0);
      var expectedSymbolSet = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
      var listEquality = testCreator.SymbolSet.All(expectedSymbolSet.Contains) && testCreator.SymbolSet.Count == expectedSymbolSet.Count;
      Assert.AreEqual(testCreator.PuzzleSize, 16);
      Assert.IsTrue(listEquality);
      Assert.AreEqual(testCreator.Grid[0, 0].Value, '4');
      Assert.AreEqual(testCreator.Grid[0, 1].Value, '9');
      Assert.AreEqual(testCreator.Grid[0, 2].Value, '-');

      var expectedOptions = new List<char> { '4' };
      listEquality = testCreator.Grid[0, 0].ValidOptions.All(expectedOptions.Contains) && testCreator.Grid[0, 0].ValidOptions.Count == expectedOptions.Count;
      Assert.IsFalse(listEquality);


      listEquality = testCreator.Grid[0, 2].ValidOptions.All(testSymbolSet0.Contains) && testCreator.Grid[0, 2].ValidOptions.Count == testSymbolSet0.Count;
      Assert.IsTrue(listEquality);
    }

    [TestMethod()]
    public void InvalidCreatorConstructorTest( )
    {
      try
      {
        var testCreator = new PuzzleCreator(invalidInputPath0);
      }
      catch(Exception e)
      {
        Assert.AreEqual(e.Message, $"The path {invalidInputPath0} does not exist!");
      }

      try
      {
        var testCreator = new PuzzleCreator(invalidPuzzlePath0);
      }
      catch (Exception e)
      {
        Assert.AreEqual(e.Message, "Invalid puzzle size");
      }

    }
       
    [TestMethod()]
    public void ValidPuzzleConstructorTest( )
    {
      var testPuzzle = new PuzzleCreator(testInputPath0);
      var listEquality = testPuzzle.SymbolSet.All(testSymbolSet0.Contains) && testPuzzle.SymbolSet.Count == testSymbolSet0.Count;
      Assert.AreEqual(testPuzzle.PuzzleSize, 16);
      Assert.IsTrue(listEquality);
      Assert.AreEqual(testPuzzle.Grid[0, 0].Value, '4');
      Assert.AreEqual(testPuzzle.Grid[0, 1].Value, '9');
      Assert.AreEqual(testPuzzle.Grid[0, 2].Value, '-');

      var expectedOptions = new List<char> { '4' };
      listEquality = testPuzzle.Grid[0, 0].ValidOptions.All(expectedOptions.Contains) && testPuzzle.Grid[0, 0].ValidOptions.Count == expectedOptions.Count;
      Assert.IsFalse(listEquality);


      listEquality = testPuzzle.Grid[0, 2].ValidOptions.All(testSymbolSet0.Contains) && testPuzzle.Grid[0, 2].ValidOptions.Count == testSymbolSet0.Count;
      Assert.IsTrue(listEquality);
    }

    [TestMethod()]
    public void PuzzleNotSolvedTest( )
    {
      var testPuzzle = new PuzzleCreator(testInputPath0).CreatePuzzle();
      Assert.IsFalse(testPuzzle.IsSolved());
    }

    [TestMethod()]
    public void PuzzleIsSolvedTest( )
    {
      var testPuzzle = new PuzzleCreator(testInputPath1).CreatePuzzle();
      Assert.IsTrue(testPuzzle.IsSolved());
    }

    [TestMethod()]
    public void TestConsolePuzzleprint( )
    {
      var testPuzzle = new PuzzleCreator(testInputPath0).CreatePuzzle();
      Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

      testPuzzle.ConsolePrint();
      // Please see the Test Output Pane for results after selecting this test in the test explorer (Visual Studio)

    }

    [TestMethod()]
    public void TestFilePuzzlePrint( )
    {
      var testPuzzle = new PuzzleCreator(testInputPath0).CreatePuzzle();

      testPuzzle.FilePrint(testOutputPath);

      Assert.IsTrue(File.Exists(testOutputPath));
      // Please see the file located at C:/OOProjects/Assn05/SudokuSolver/SudokuSolver/bin/Debug/testOuput.txt
      // for results
    }

  }
}