using SudokuSolverV2.Solver;

namespace SolverTests.V2UnitTests;

[TestClass]
public class GenericTests
{


    /*
    [TestMethod]
    public void TestSettingValueOfCellRequiresValueIsAPotentialValueOfTheCell() 
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
        int[,] seedMatrix = Solver.CreateMatrixBySuperimposition(givenMatrix, superImposeMatrix);
        Solver solver = Solver.Create(seedMatrix);  //Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        //puzzle.LoadMatrixAsCellValues(seedMatrix);

        solver.RemovePotentialValues();//puzzle.PerformCandidateElimination();

        
        Assert.IsFalse(solver.PotentialValues[4, 8].Contains(7));  // 7 is value to test w/these coordinates
        //Assert.AreEqual(0, puzzle.Matrix[4, 8].Values[7]);  // 7 is value to test w/these coordinates

        // Act
        Puzzle puzzle = new();
        int returnValue = puzzle.Matrix[4, 8].AssignConfirmedValue(7);


        // Assert
        Assert.AreEqual(0, returnValue);
        Assert.AreEqual(4, puzzle.Matrix[4, 8].Values[0]);
        Assert.AreEqual(4, puzzle.Matrix[4, 8].Values[0]);
    }
    */
    /*
    [TestMethod]
    public void TestIfOnePossibilityLeftThenAssignPossibilityToValue()
    {
        // Arrange
        int[,] startingMatrix =  // not from Puzzle Book
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
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(startingMatrix);
        puzzle.PerformCandidateElimination();

        // Act
        puzzle.UpdateCellValuesBasedOnSingleCandidate();

        // Assert
        Assert.IsTrue(puzzle.Matrix[0, 4].Candidates.Count == 1);
        Assert.AreEqual(9, puzzle.Matrix[0, 4].Values[0]);
    }
    */
    /*
    [TestMethod]
    public void TestCellCandidatesReturnsListOfNonZeroCandidates()
    {
        // Arrange
        int[,] startingMatrix =  // "SS, V20, P33" puzzle (1:40 to solve)
        {
            { 0, 0, 5, 0, 3, 0, 0, 8, 0 },
            { 0, 0, 3, 0, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 9, 1 },

            { 8, 0, 0, 7, 0, 0, 0, 1, 0 },
            { 2, 0, 0, 8, 0, 3, 0, 0, 7 },
            { 0, 6, 0, 0, 0, 4, 0, 0, 9 },

            { 4, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 9, 0, 0, 1, 0, 0 },
            { 0, 8, 0, 0, 5, 0, 6, 0, 0 },
        };
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(startingMatrix);

        // Act
        puzzle.PerformCandidateElimination();

        // Assert
        Assert.IsTrue(puzzle.Matrix[0, 1].Candidates.Contains(1));
        Assert.IsTrue(puzzle.Matrix[0, 1].Candidates.Contains(2));
        Assert.IsTrue(puzzle.Matrix[0, 1].Candidates.Contains(7));

        Assert.IsFalse(puzzle.Matrix[0, 1].Candidates.Contains(3));
        Assert.IsFalse(puzzle.Matrix[0, 1].Candidates.Contains(5));
        Assert.IsFalse(puzzle.Matrix[0, 1].Candidates.Contains(8));
    }
    [TestMethod]
    public void TestBruteForceSudokuSolverSolvesPuzzles()
    {
        // Arrange
        int[,] startingMatrix = new int[9, 9]  // "R2" puzzle
        {
            { 0, 0, 7,   4, 6, 0,   2, 0, 0 },
            { 0, 3, 0,   0, 0, 0,   4, 0, 0 },
            { 0, 9, 0,   5, 0, 0,   6, 0, 0 },

            { 2, 0, 0,   1, 0, 0,   5, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 7, 0,   6, 0, 0,   0, 9, 0 },

            { 0, 0, 3,   0, 0, 1,   0, 5, 0 },
            { 0, 1, 0,   7, 0, 0,   0, 8, 0 },
            { 0, 0, 0,   0, 3, 4,   0, 0, 0 },
        };
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(startingMatrix);

        // Act
        bool actualPuzzleWasSolved = puzzle.Solve();

        // Assert
        ConsoleRender.RenderMatrix(puzzle);
        Assert.IsTrue(actualPuzzleWasSolved);
        Assert.AreEqual(5, puzzle.Matrix[8, 8].Values[0]);
        Assert.AreEqual(4, puzzle.Matrix[4, 4].Values[0]);
        Assert.AreEqual(6, puzzle.Matrix[0, 0].Values[0]);
        Assert.AreEqual(6, puzzle.Matrix[8, 1].Values[0]);
        Assert.AreEqual(1, puzzle.Matrix[3, 7].Values[0]);
        Assert.AreEqual(4, puzzle.Matrix[2, 3].Values[0]);
    }
    */
    /*
    [TestMethod]
    public void TestUpdateCandidatesHydratesAndRemovesCandidatesToLeavePossibleCandidatesOnly()
    {
        // Arrange
        int[,] startingMatrix =
        {
            { 0, 0, 5, 0, 3, 0, 0, 8, 0 },
            { 0, 0, 3, 0, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 9, 1 },

            { 8, 0, 0, 7, 0, 0, 0, 1, 0 },
            { 2, 0, 0, 8, 0, 3, 0, 0, 7 },
            { 0, 6, 0, 0, 0, 4, 0, 0, 9 },

            { 4, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 9, 0, 0, 1, 0, 0 },
            { 0, 8, 0, 0, 5, 0, 6, 0, 0 },
        };
        Cell[,] createdCellMatrix = MatrixFactory.CreateCellMatrix(startingMatrix);
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver(createdCellMatrix);
        puzzle.PerformCandidateElimination();
        puzzle.Matrix[0, 0].SetExpectedValue(1);
        puzzle.Matrix[0, 1].SetExpectedValue(2);
        puzzle.Matrix[0, 4].SetExpectedValue(4);
        puzzle.Matrix[0, 4].ResetValue();

    }/*
    [TestMethod]
    public void TestGetNextCandidateGrabsTheNextUntriedCandidateAvailableForTheCell()
    {

    }*/
}
