using SudokuSolver;

namespace SolverTests;

[TestClass]
public class GenericTests
{
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
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(givenMatrix);
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);

        puzzle.RemoveCandidates();
        Assert.AreEqual(0, puzzle.Matrix[4, 8].Values[7]);  // 7 is value to test w/these coordinates

        // Act
        int returnValue = puzzle.Matrix[4, 8].AssignConfirmedValue(7);

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
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(givenMatrix);
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);
        puzzle.RemoveCandidates();

        // Act
        puzzle.RemoveExpectedValuesBasedOnNotACandidate();

        // Assert
        Assert.AreEqual(5, puzzle.Matrix[0, 2].Values[0]);  // control
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 2].ValueStatus);
        Assert.AreEqual(3, puzzle.Matrix[4, 5].Values[0]);  // control
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[4, 5].ValueStatus);
        Assert.AreEqual(0, puzzle.Matrix[1, 1].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[3, 5].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[4, 7].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[8, 5].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[6, 7].Values[0]);
        Assert.AreEqual(0, puzzle.Matrix[3, 8].Values[0]);
    }
    [TestMethod]
    public void TestIfOnePossibilityLeftThenAssignPossibilityToValue()
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
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(givenMatrix);
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);
        puzzle.RemoveCandidates();

        // Act
        puzzle.Matrix[0, 5].UpdateValueBasedOnSingleCandidate();

        // Assert
        Assert.IsTrue(puzzle.Matrix[0, 5].Candidates.Count > 1);
        Assert.AreEqual(6, puzzle.Matrix[0, 5].Values[0]);
    }
}