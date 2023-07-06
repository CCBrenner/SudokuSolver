using SudokuSolver;
using System.Diagnostics.Contracts;

namespace SolverTests;

[TestClass]
public class SetupTests
{
    [TestMethod]
    public void TestCreatingCellMatrixCreatesNineByNineMatrixOfCellsFromIntegerMatrix()
    {
        // Arange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };

        // Act
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);

        // Assert
        Assert.AreEqual(5.ToString(), createdCellMatrix[0, 2].Value);
        Assert.AreEqual(3.ToString(), createdCellMatrix[1, 2].Value);
        Assert.AreEqual(3.ToString(), createdCellMatrix[4, 5].Value);

        Assert.AreEqual(" ", createdCellMatrix[0, 6].Value);
        Assert.AreEqual(" ", createdCellMatrix[4, 2].Value);
        Assert.AreEqual(" ", createdCellMatrix[8, 8].Value);
    }

    [TestMethod]
    public void TestCreatingCellMatrixSetsNonZeroCellsToIsGivenValue()
    {
        // Arange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };

        // Act
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);

        // Assert
        Assert.AreEqual(ValueStatus.Given, createdCellMatrix[0, 2].ValueStatus);
        Assert.AreEqual(ValueStatus.Given, createdCellMatrix[5, 1].ValueStatus);
        Assert.AreEqual(ValueStatus.Given, createdCellMatrix[8, 6].ValueStatus);
    }
    [TestMethod]
    public void TestCreatingCellMatrixLeavesAllExpectedAndonfirmedCellFlagsSetToFalse()
    {
        // Arange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };

        // Act
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);

        // Assert
        Assert.AreNotEqual(ValueStatus.Expected, createdCellMatrix[0, 2].ValueStatus);  // given value
        Assert.AreNotEqual(ValueStatus.Expected, createdCellMatrix[8, 8].ValueStatus);  // nongiven value
        Assert.AreNotEqual(ValueStatus.Expected, createdCellMatrix[0, 0].ValueStatus);  // nongiven value

        Assert.AreNotEqual(ValueStatus.Confirmed, createdCellMatrix[0, 2].ValueStatus);  // given value
        Assert.AreNotEqual(ValueStatus.Confirmed, createdCellMatrix[8, 8].ValueStatus);  // nongiven value
        Assert.AreNotEqual(ValueStatus.Confirmed, createdCellMatrix[0, 0].ValueStatus);  // nongiven value
    }

    [TestMethod]
    public void TestCreatingPuzzleFromFactoryMethodSetsIsGivenValueToTrueOnlyForGivenValues()
    {
        // Arrange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };
        int[,] superImposeMatrix =
        {
            { 1, 2, 3,   4, 5, 6,   7, 8, 9 },
            { 4, 5, 6,   7, 8, 9,   1, 2, 3 },
            { 7, 8, 9,   1, 2, 3,   4, 5, 6 },

            { 2, 3, 4,   5, 6, 7,   8, 9, 1 },
            { 5, 6, 7,   8, 9, 1,   2, 3, 4 },
            { 8, 9, 1,   2, 3, 4,   5, 6, 7 },

            { 3, 4, 5,   6, 7, 8,   9, 1, 2 },
            { 6, 7, 8,   9, 1, 2,   3, 4, 5 },
            { 9, 1, 2,   3, 4, 5,   6, 7, 8 },
        };
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);

        // Act
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);

        // Assert
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 2].ValueStatus);
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[5, 1].ValueStatus);
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[8, 6].ValueStatus);
    }
    [TestMethod]
    public void TestCreatingPuzzleFromFactoryMethodSetsNonGivenValuesToCorrespondingValueOfTemplateMatrix()
    {
        // Arrange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };
        int[,] superImposeMatrix =
        {
            { 1, 2, 3,   4, 5, 6,   7, 8, 9 },
            { 4, 5, 6,   7, 8, 9,   1, 2, 3 },
            { 7, 8, 9,   1, 2, 3,   4, 5, 6 },

            { 2, 3, 4,   5, 6, 7,   8, 9, 1 },
            { 5, 6, 7,   8, 9, 1,   2, 3, 4 },
            { 8, 9, 1,   2, 3, 4,   5, 6, 7 },

            { 3, 4, 5,   6, 7, 8,   9, 1, 2 },
            { 6, 7, 8,   9, 1, 2,   3, 4, 5 },
            { 9, 1, 2,   3, 4, 5,   6, 7, 8 },
        };
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);

        // Act
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);

        // Assert
        Assert.AreEqual(5.ToString(), puzzle.Matrix[0, 2].Value);  // given (control)
        Assert.AreEqual(1.ToString(), puzzle.Matrix[0, 0].Value);  // superimposed (from template)
        Assert.AreEqual(2.ToString(), puzzle.Matrix[2, 4].Value);  // superimposed (from template)
        Assert.AreEqual(8.ToString(), puzzle.Matrix[8, 8].Value);  // superimposed (from template)
        Assert.AreEqual(1.ToString(), puzzle.Matrix[7, 4].Value);  // superimposed (from template)
    }
    [TestMethod]
    public void TestCreatingPuzzleFromFactoryMethodSetsIsExpectedValueToTrueForSuperimposedValuesFromTemplateMatrixOnly()
    {
        // Arrange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };
        int[,] superImposeMatrix =
        {
            { 1, 2, 3,   4, 5, 6,   7, 8, 9 },
            { 4, 5, 6,   7, 8, 9,   1, 2, 3 },
            { 7, 8, 9,   1, 2, 3,   4, 5, 6 },

            { 2, 3, 4,   5, 6, 7,   8, 9, 1 },
            { 5, 6, 7,   8, 9, 1,   2, 3, 4 },
            { 8, 9, 1,   2, 3, 4,   5, 6, 7 },

            { 3, 4, 5,   6, 7, 8,   9, 1, 2 },
            { 6, 7, 8,   9, 1, 2,   3, 4, 5 },
            { 9, 1, 2,   3, 4, 5,   6, 7, 8 },
        };
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);

        // Act
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);

        // Assert
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 2].ValueStatus);  // given (control)
        Assert.AreNotEqual(ValueStatus.Expected, puzzle.Matrix[0, 2].ValueStatus);  // given (control)
        Assert.AreEqual(ValueStatus.Expected, puzzle.Matrix[0, 0].ValueStatus);  // superimposed
        Assert.AreEqual(ValueStatus.Expected, puzzle.Matrix[2, 4].ValueStatus);  // superimposed
        Assert.AreEqual(ValueStatus.Expected, puzzle.Matrix[8, 8].ValueStatus);  // superimposed
        Assert.AreEqual(ValueStatus.Expected, puzzle.Matrix[7, 4].ValueStatus);  // superimposed
    }
    [TestMethod]
    public void TestBlocksArrayContainsNineCellReferencesEach()
    {
        // Arrange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);
        Puzzle puzzle = Puzzle.Create(createdCellMatrix);

        // Act
        int[] actual = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        foreach (var block in puzzle.Blocks)
        {
            foreach (var cell in block.Cells)
            {
                actual[cell.BlockId]++;
            }
        }

        // Assert
        int[] expected = new int[10] { 0, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
        Assert.AreEqual(expected[3], actual[3]);
        Assert.AreEqual(expected[4], actual[4]);
        Assert.AreEqual(expected[7], actual[7]);
    }
    [TestMethod]
    public void TestCellIdsAreFromOneToEightyOneInOrderInPuzzleCellList()
    {
        // Arrange
        int[,] intMatrix =
        {
            { 0, 0, 5,   0, 4, 0,   0, 8, 0 },
            { 0, 0, 3,   0, 0, 2,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 9, 1 },

            { 8, 0, 0,   7, 0, 0,   0, 1, 0 },
            { 2, 0, 0,   8, 0, 3,   0, 0, 7 },
            { 0, 6, 0,   0, 0, 4,   0, 0, 9 },

            { 4, 3, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 0,   1, 0, 0 },
            { 0, 8, 0,   0, 5, 0,   6, 0, 0 },
        };
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(intMatrix);

        // Act
        Puzzle puzzle = Puzzle.Create(createdCellMatrix);

        // Assert
        Assert.AreEqual(5, puzzle.Cells[4].Id);
        Assert.AreEqual(28, puzzle.Cells[27].Id);
        Assert.AreEqual(4, puzzle.Cells[3].Id);
        Assert.AreEqual(10, puzzle.Cells[9].Id);
        Assert.AreEqual(81, puzzle.Cells[80].Id);

        Assert.AreEqual(1, puzzle.Matrix[0,0].Id);
        Assert.AreEqual(23, puzzle.Matrix[2,4].Id);
        Assert.AreEqual(81, puzzle.Matrix[8,8].Id);
    }
}
