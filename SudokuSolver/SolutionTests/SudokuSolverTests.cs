using SudokuSolver;
using System.Numerics;

namespace SolutionTests;

[TestClass]
public class SudokuSolverTests
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);

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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);

        // Assert
        Assert.IsTrue(createdCellMatrix[0, 2].IsGivenValue);
        Assert.IsTrue(createdCellMatrix[5, 1].IsGivenValue);
        Assert.IsTrue(createdCellMatrix[8, 6].IsGivenValue);
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);

        // Assert
        Assert.IsFalse(createdCellMatrix[0, 2].IsExpectedValue);  // given value
        Assert.IsFalse(createdCellMatrix[8, 8].IsExpectedValue);  // nongiven value
        Assert.IsFalse(createdCellMatrix[0, 0].IsExpectedValue);  // nongiven value

        Assert.IsFalse(createdCellMatrix[0, 2].IsConfirmedValue);  // given value
        Assert.IsFalse(createdCellMatrix[8, 8].IsConfirmedValue);  // nongiven value
        Assert.IsFalse(createdCellMatrix[0, 0].IsConfirmedValue);  // nongiven value
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();

        // Act
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        // Assert
        Assert.IsTrue(puzzle.Matrix[0, 2].IsGivenValue);
        Assert.IsTrue(puzzle.Matrix[5, 1].IsGivenValue);
        Assert.IsTrue(puzzle.Matrix[8, 6].IsGivenValue);
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();

        // Act
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();

        // Act
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        // Assert
        Assert.IsTrue(puzzle.Matrix[0, 2].IsGivenValue);  // given (control)
        Assert.IsFalse(puzzle.Matrix[0, 2].IsExpectedValue);  // given (control)
        Assert.IsTrue(puzzle.Matrix[0, 0].IsExpectedValue);  // superimposed
        Assert.IsTrue(puzzle.Matrix[2, 4].IsExpectedValue);  // superimposed
        Assert.IsTrue(puzzle.Matrix[8, 8].IsExpectedValue);  // superimposed
        Assert.IsTrue(puzzle.Matrix[7, 4].IsExpectedValue);  // superimposed
    }

    [TestMethod]
    public void TestDistinctionRulesEliminatePossibilitiesForEachCellInARow()
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        // Act
        puzzle.EliminateRowPossibilities(0);

        // Assert
        // 5 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[5]);
        // 4 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[4]);
        // 8 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[8]);
        // 3 is a possibility
        Assert.AreEqual(3, puzzle.Matrix[0, 1].Values[3]);
        Assert.AreEqual(3, puzzle.Matrix[0, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[3]);  // IsGivenValue means no possibilities
        Assert.IsTrue(puzzle.Matrix[0, 4].IsGivenValue);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[3]);  // IsGivenValue means no possibilities
        Assert.IsTrue(puzzle.Matrix[0, 7].IsGivenValue);
        // 1 is a possibility
        Assert.AreEqual(1, puzzle.Matrix[0, 1].Values[1]);
        Assert.AreEqual(1, puzzle.Matrix[0, 3].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[1]);  // IsGivenValue means no possibilities
        Assert.IsTrue(puzzle.Matrix[0, 4].IsGivenValue);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[1]);  // IsGivenValue means no possibilities
        Assert.IsTrue(puzzle.Matrix[0, 7].IsGivenValue);
    }

    [TestMethod]
    public void TestDistinctionRulesEliminatePossibilitiesForEachCellInAColumn()
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        // Act
        puzzle.EliminateColumnPossibilities(0);

        // Assert
        // 2 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[2]);
        // 4 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[4]);
        // 8 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[8]);
        // 5 is a possibility
        Assert.AreEqual(5, puzzle.Matrix[1, 0].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[7, 0].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[8, 0].Values[5]);
        // 7 is a possibility
        Assert.AreEqual(7, puzzle.Matrix[1, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[7, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[8, 0].Values[7]);
    }
    [TestMethod]
    public void TestDistinctionRulesEliminatePossibilitiesForEachCellInASquare()
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        // Act
        int[] squareCoords = new int[] { 0, 0 };
        puzzle.EliminateSquarePossibilities(squareCoords);

        // Assert
        // 5 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[2, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[5]);
        // 3 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[2, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[3]);
        // 2 is a possibility
        Assert.AreEqual(2, puzzle.Matrix[0, 0].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[2, 0].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[0, 1].Values[2]);
        // 7 is a possibility
        Assert.AreEqual(7, puzzle.Matrix[0, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[2, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[0, 1].Values[7]);


        // vvv Second Act & Assert to check for consistency with non-zero indexed positions for square. vvv

        // Act
        squareCoords = new int[] { 1, 1 };
        puzzle.EliminateSquarePossibilities(squareCoords);

        // Assert
        // 3 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[3]);
        // 7 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[7]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[7]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[7]);
        // 8 is not a possibility
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[8]);
        // 2 is a possibility
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[2]);  // IsGivenValue means no possibilities
        Assert.IsTrue(puzzle.Matrix[3, 3].IsGivenValue);
        Assert.AreEqual(2, puzzle.Matrix[5, 3].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[3, 4].Values[2]);
        // 6 is a possibility
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[6]);  // IsGivenValue means no possibilities
        Assert.IsTrue(puzzle.Matrix[3, 3].IsGivenValue);
        Assert.AreEqual(6, puzzle.Matrix[5, 3].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[3, 4].Values[6]);
    }
    [TestMethod]
    public void TestAllThreeDistinctionChecksRemovePossibilitiesForAGivenSquare()
    {
        // Create specal setup to test [1, 1] square for 6 eliminated possibilities, 2 for each distinction rule
        // Arrange
        // The superimosed matrix was used; some vals were entered here to be saved as "given" but
        // would have received the same value either way.
        int[,] givenMatrix =
        {
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 2, 0,   0, 0, 0 },

            { 0, 0, 0,   5, 0, 0,   0, 0, 0 },
            { 0, 0, 7,   0, 0, 0,   0, 3, 0 },
            { 0, 0, 0,   0, 0, 4,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 1, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
        };
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(givenMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        // Act
        puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell();

        // Assert:
        // coord [4,4] will not have 1, 2, 3, 4, 5, or 7 as possibilities
        Assert.AreEqual(6, puzzle.Matrix[4, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[4, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[7]);
        // coords [3,4] and 5,4] will not have 1, 2, 4, or 5 as possibilities
        Assert.AreEqual(6, puzzle.Matrix[3, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[3, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[5]);
        Assert.AreEqual(6, puzzle.Matrix[5, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[5, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[5]);
        // coords [4,3] and [4,5] will not have 3, 4, 5, or 7 as possibilities
        Assert.AreEqual(6, puzzle.Matrix[4, 3].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[4, 3].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[4, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 3].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[4, 3].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[4, 3].Values[7]);
        Assert.AreEqual(6, puzzle.Matrix[4, 5].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[4, 5].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[4, 5].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 5].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[4, 5].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[4, 5].Values[7]);
        // square [1,1] will not have 4 or 5 as possibilities
        // (exclude cells from previous tests in these tests)
        // (use [3, 3], [5, 5], [3, 5], &/or [5, 3])
        Assert.AreEqual(6, puzzle.Matrix[3, 5].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[3, 5].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[3, 5].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[3, 5].Values[5]);
        Assert.AreEqual(6, puzzle.Matrix[5, 3].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[5, 3].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[5]);
        // column 4 will not have 1 or 2 as possibilities
        // (exclude cells from previous tests in these tests)
        Assert.AreEqual(6, puzzle.Matrix[1, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[1, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[1, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[1, 4].Values[2]);
        Assert.AreEqual(6, puzzle.Matrix[8, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[8, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[2]);

        // row 4 will not have 7 or 3 as possibilities
        // (exclude cells from previous tests in these tests)
        Assert.AreEqual(6, puzzle.Matrix[4, 1].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[4, 1].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[4, 1].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 1].Values[7]);
        Assert.AreEqual(6, puzzle.Matrix[4, 8].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[4, 8].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[4, 8].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 8].Values[7]);
    }
    [TestMethod]
    public void TestSettingValueOfCellRequiresValueIsAPossibilityOfTheCell()
    {
        // Arrange
        int[,] givenMatrix =
        {
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 2, 0,   0, 0, 0 },

            { 0, 0, 0,   5, 0, 0,   0, 0, 0 },
            { 0, 0, 7,   0, 0, 0,   0, 3, 0 },
            { 0, 0, 0,   0, 0, 4,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 1, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
        };
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(givenMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell();
        Assert.AreEqual(0, puzzle.Matrix[4, 8].Values[7]);  // 7 is value to test w/these coordinates

        // Act
        int returnValue = puzzle.Matrix[4, 8].SetValue(7);

        // Assert
        Assert.AreEqual(0, returnValue);
        Assert.AreEqual(4, puzzle.Matrix[4, 8].Values[0]);
        Assert.AreEqual(4.ToString(), puzzle.Matrix[4, 8].Value);
    }
    [TestMethod]
    public void TestResetValueIfValueIsCurrentlyNotAPossibilityOfTheCell()
    {
        // Arrange
        int[,] givenMatrix =
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
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(givenMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);
        puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell();

        // Act
        puzzle.RemoveImpossibleValues();

        // Assert
        Assert.AreEqual(5, puzzle.Matrix[0, 2].Values[0]);  // control
        Assert.IsTrue(puzzle.Matrix[0, 2].IsGivenValue);
        Assert.AreEqual(3, puzzle.Matrix[4, 5].Values[0]);  // control
        Assert.IsTrue(puzzle.Matrix[4, 5].IsGivenValue);
        Assert.AreEqual(0, puzzle.Matrix[1, 1].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[3, 5].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[4, 7].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[8, 5].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[6, 7].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[3, 8].Values[0]);
    }
    [TestMethod]
    public void TestIfOnePossibilityLeftThenAssignPossibilityToValueAndIsMarkedAsConfirmed()
    {
        // Arrange
        int[,] givenMatrix =
        {
            { 1, 0, 3,   4, 0, 0,   0, 2, 0 },
            { 0, 0, 0,   0, 0, 8,   0, 0, 0 },
            { 0, 0, 0,   0, 5, 0,   0, 0, 0 },

            { 0, 0, 0,   0, 6, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 7, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
        };
        Cell[,] createdCellMatrix = Puzzle.CreateMatrix(givenMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);
        puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell();

        // Act
        puzzle.Matrix[0, 4].CheckAndUpdateValueIfOnePossibilityRemaining();

        // Assert
        Assert.AreEqual(9, puzzle.Matrix[0, 4].Values[0]);
        Assert.IsTrue(puzzle.Matrix[0, 4].IsConfirmedValue);
    }
}