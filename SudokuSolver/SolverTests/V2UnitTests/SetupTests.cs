using SudokuSolverV2.Solver;

namespace SolverTests.V2UnitTests;

[TestClass]
public class SetupTests
{
    [TestMethod]
    public void TestCreatingSolverObjectAssignsSeedValuesAsPuzzleValues()
    {
        // Arrange
        int[,] seedValues =
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
        Solver solver = Solver.Create(seedValues);

        // Assert 
        Assert.AreEqual(4, solver.PuzzleValues[1, 7]);
        Assert.AreEqual(1, solver.PuzzleValues[8, 4]);
        Assert.AreEqual(7, solver.PuzzleValues[4, 4]);
        Assert.AreEqual(0, solver.PuzzleValues[9, 9]);
        Assert.AreEqual(3, solver.PuzzleValues[3, 2]);
    }
    [TestMethod]
    public void TestUpdatingPuzzleValuesToExistingSolverIsSuccessful()
    {
        // Arrange
        int[,] seedValues =
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
        Solver solver = Solver.CreateWithoutValues();
        Assert.AreEqual(0, solver.PuzzleValues[1, 7]);
        Assert.AreEqual(0, solver.PuzzleValues[5, 6]);


        // Act
        //solver.

        // Assert 
        Assert.AreEqual(4, solver.PuzzleValues[1, 7]);
        Assert.AreEqual(1, solver.PuzzleValues[8, 4]);
        Assert.AreEqual(7, solver.PuzzleValues[4, 4]);
        Assert.AreEqual(0, solver.PuzzleValues[9, 9]);
        Assert.AreEqual(3, solver.PuzzleValues[3, 2]);
    }
    /*
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

        // Act
        int[,] seedMatrix = SudokuSolverV2.Solver.CreateMatrixBySuperimposition(intMatrix, superImposeMatrix);
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(seedMatrix);

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

        // Act
        int[,] seedMatrix = MatrixFactory.CreateMatrixBySuperimposition(intMatrix, superImposeMatrix);
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(seedMatrix);

        // Assert
        Assert.AreEqual(5, puzzle.Matrix[0, 2].Values[0]);  // given (control)
        Assert.AreEqual(1, puzzle.Matrix[0, 0].Values[0]);  // superimposed (from template)
        Assert.AreEqual(2, puzzle.Matrix[2, 4].Values[0]);  // superimposed (from template)
        Assert.AreEqual(8, puzzle.Matrix[8, 8].Values[0]);  // superimposed (from template)
        Assert.AreEqual(1, puzzle.Matrix[7, 4].Values[0]);  // superimposed (from template)
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
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(intMatrix);

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

        // Act
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(intMatrix);

        // Assert
        Assert.AreEqual(5, puzzle.Cells[4].Id);
        Assert.AreEqual(28, puzzle.Cells[27].Id);
        Assert.AreEqual(4, puzzle.Cells[3].Id);
        Assert.AreEqual(10, puzzle.Cells[9].Id);
        Assert.AreEqual(81, puzzle.Cells[80].Id);

        Assert.AreEqual(1, puzzle.Matrix[0, 0].Id);
        Assert.AreEqual(23, puzzle.Matrix[2, 4].Id);
        Assert.AreEqual(81, puzzle.Matrix[8, 8].Id);
    }
    */
}




