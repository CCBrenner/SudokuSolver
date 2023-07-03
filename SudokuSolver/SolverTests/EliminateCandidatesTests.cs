using SudokuSolver;

namespace SolverTests;

[TestClass]
public class EliminateCandidatesTests
{
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
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);

        // Act
        Row.EliminateCandidatesByDistinctInNeighborhood(puzzle.Rows);

        // Assert
        // 5 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[5]);
        // 4 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[4]);
        // 8 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[8]);
        // 3 is a pencilMarking
        Assert.AreEqual(3, puzzle.Matrix[0, 1].Values[3]);
        Assert.AreEqual(3, puzzle.Matrix[0, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[3]);  // IsGivenValue means no pencilMarkings
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 4].ValueStatus);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[3]);  // IsGivenValue means no pencilMarkings
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 7].ValueStatus);
        // 1 is a pencilMarking
        Assert.AreEqual(1, puzzle.Matrix[0, 1].Values[1]);
        Assert.AreEqual(1, puzzle.Matrix[0, 3].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[1]);  // IsGivenValue means no pencilMarkings
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 4].ValueStatus);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[1]);  // IsGivenValue means no pencilMarkings
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 7].ValueStatus);
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
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);

        // Act
        Column.EliminateCandidatesByDistinctInNeighborhood(puzzle.Columns);

        // Assert
        // 2 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[2]);
        // 4 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[4]);
        // 8 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[8]);
        // 5 is a pencilMarking
        Assert.AreEqual(5, puzzle.Matrix[1, 0].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[7, 0].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[8, 0].Values[5]);
        // 7 is a pencilMarking
        Assert.AreEqual(7, puzzle.Matrix[1, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[7, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[8, 0].Values[7]);
    }
    [TestMethod]
    public void TestDistinctionRulesEliminatePossibilitiesForEachCellInABlock()
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
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, superImposeMatrix);

        // Act
        //int[] blockCoords = new int[] { 0, 0 };
        //puzzle.EliminateBlockPossibilities(blockCoords);
        Block.EliminateCandidatesByDistinctInNeighborhood(puzzle.Blocks);

        // Assert
        // 5 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[2, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[5]);
        // 3 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[2, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[3]);
        // 2 is a pencilMarking
        Assert.AreEqual(2, puzzle.Matrix[0, 0].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[2, 0].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[0, 1].Values[2]);
        // 7 is a pencilMarking
        Assert.AreEqual(7, puzzle.Matrix[0, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[2, 0].Values[7]);
        Assert.AreEqual(7, puzzle.Matrix[0, 1].Values[7]);


        // vvv Second Act & Assert to check for consistency with non-zero indexed positions for block. vvv

        // Act
        //blockCoords = new int[] { 1, 1 };
        //puzzle.EliminateBlockPossibilities(blockCoords);
        Block.EliminateCandidatesByDistinctInNeighborhood(puzzle.Blocks);

        // Assert
        // 3 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[3]);
        // 7 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[7]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[7]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[7]);
        // 8 is not a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[8]);
        // 2 is a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[2]);  // IsGivenValue means no pencilMarkings
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[3, 3].ValueStatus);
        Assert.AreEqual(2, puzzle.Matrix[5, 3].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[3, 4].Values[2]);
        // 6 is a pencilMarking
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[6]);  // IsGivenValue means no pencilMarkings
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[3, 3].ValueStatus);
        Assert.AreEqual(6, puzzle.Matrix[5, 3].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[3, 4].Values[6]);
    }
    [TestMethod]
    public void TestAllThreeDistinctionChecksRemovePossibilitiesForAGivenBlock()
    {
        // Create specal setup to test [1, 1] block for 6 eliminated pencilMarkings, 2 for each distinction rule
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
        Cell[,] createdCellMatrix = MatrixFactory.CreateMatrix(givenMatrix);
        int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(createdCellMatrix, matrixToSuperimpose);

        // Act
        puzzle.RemoveCandidates();

        // Assert:
        // coord [4,4] will not have 1, 2, 3, 4, 5, or 7 as pencilMarkings
        Assert.AreEqual(6, puzzle.Matrix[4, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[4, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[7]);
        // coords [3,4] and 5,4] will not have 1, 2, 4, or 5 as pencilMarkings
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
        // coords [4,3] and [4,5] will not have 3, 4, 5, or 7 as pencilMarkings
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
        // block [1,1] will not have 4 or 5 as pencilMarkings
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
        // column 4 will not have 1 or 2 as pencilMarkings
        // (exclude cells from previous tests in these tests)
        Assert.AreEqual(6, puzzle.Matrix[1, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[1, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[1, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[1, 4].Values[2]);
        Assert.AreEqual(6, puzzle.Matrix[8, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[8, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[2]);

        // row 4 will not have 7 or 3 as pencilMarkings
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
}
