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
        /* Combined, for reference:
        [ 1 2 5 ]  [ 4 4 6 ]  [ 7 8 9 ]
        [ 4 5 3 ]  [ 7 8 2 ]  [ 1 2 3 ]
        [ 7 8 9 ]  [ 1 2 3 ]  [ 4 9 1 ]

        [ 8 3 4 ]  [ 7 6 7 ]  [ 8 1 1 ]
        [ 2 6 7 ]  [ 8 9 3 ]  [ 2 3 7 ]
        [ 8 6 1 ]  [ 2 3 4 ]  [ 5 6 9 ]

        [ 4 3 5 ]  [ 6 7 8 ]  [ 9 1 2 ]
        [ 6 7 8 ]  [ 9 1 2 ]  [ 1 4 5 ]
        [ 9 8 2 ]  [ 3 5 5 ]  [ 6 7 8 ]
        */

        // Act
        Row.EliminateCandidatesByDistinctInNeighborhood(puzzle.Rows);

        // Assert
        // 5 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[5]);
        // 4 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[4]);
        // 8 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[8]);
        // 3 is a candidate
        Assert.AreEqual(3, puzzle.Matrix[0, 1].Values[3]);
        Assert.AreEqual(3, puzzle.Matrix[0, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[3]);  // IsGivenValue means no candidates
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[0, 4].ValueStatus);
        Assert.AreEqual(0, puzzle.Matrix[0, 7].Values[3]);  // IsGivenValue means no candidates
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
        /* Combined, for reference:
        [ 1 2 5 ]  [ 4 4 6 ]  [ 7 8 9 ]
        [ 4 5 3 ]  [ 7 8 2 ]  [ 1 2 3 ]
        [ 7 8 9 ]  [ 1 2 3 ]  [ 4 9 1 ]

        [ 8 3 4 ]  [ 7 6 7 ]  [ 8 1 1 ]
        [ 2 6 7 ]  [ 8 9 3 ]  [ 2 3 7 ]
        [ 8 6 1 ]  [ 2 3 4 ]  [ 5 6 9 ]

        [ 4 3 5 ]  [ 6 7 8 ]  [ 9 1 2 ]
        [ 6 7 8 ]  [ 9 1 2 ]  [ 1 4 5 ]
        [ 9 8 2 ]  [ 3 5 5 ]  [ 6 7 8 ]
        */

        // Act
        Column.EliminateCandidatesByDistinctInNeighborhood(puzzle.Columns);

        // Assert
        // 2 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[2]);
        // 4 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[4]);
        // 8 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[1, 0].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[7, 0].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[8]);
        // 5 is a candidate
        Assert.AreEqual(5, puzzle.Matrix[1, 0].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[7, 0].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[8, 0].Values[5]);
        // 3 is a candidate
        Assert.AreEqual(3, puzzle.Matrix[1, 0].Values[3]);
        Assert.AreEqual(3, puzzle.Matrix[7, 0].Values[3]);
        Assert.AreEqual(3, puzzle.Matrix[8, 0].Values[3]);
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
        /* Combined, for reference:
        [ 1 2 5 ]  [ 4 4 6 ]  [ 7 8 9 ]
        [ 4 5 3 ]  [ 7 8 2 ]  [ 1 2 3 ]
        [ 7 8 9 ]  [ 1 2 3 ]  [ 4 9 1 ]

        [ 8 3 4 ]  [ 7 6 7 ]  [ 8 1 1 ]
        [ 2 6 7 ]  [ 8 9 3 ]  [ 2 3 7 ]
        [ 8 6 1 ]  [ 2 3 4 ]  [ 5 6 9 ]

        [ 4 3 5 ]  [ 6 7 8 ]  [ 9 1 2 ]
        [ 6 7 8 ]  [ 9 1 2 ]  [ 1 4 5 ]
        [ 9 8 2 ]  [ 3 5 5 ]  [ 6 7 8 ]
        */

        // Act
        Block.EliminateCandidatesByDistinctInNeighborhood(puzzle.Blocks);

        // Assert
        // 5 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[2, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[5]);
        // 3 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[2, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[3]);
        // 6 is a candidate
        Assert.AreEqual(6, puzzle.Matrix[0, 0].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[2, 0].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[0, 1].Values[6]);


        // vvv Second Act & Assert to check for consistency with non-zero indexed positions for block. vvv

        // Act
        Block.EliminateCandidatesByDistinctInNeighborhood(puzzle.Blocks);

        // Assert
        // 3 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[3]);
        // 7 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[7]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[7]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[7]);
        // 8 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[5, 3].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[8]);
        // 1 is a candidate
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[1]);  // IsGivenValue means no candidates
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[3, 3].ValueStatus);
        Assert.AreEqual(1, puzzle.Matrix[5, 3].Values[1]);
        Assert.AreEqual(1, puzzle.Matrix[3, 4].Values[1]);
        // 5 is a candidate
        Assert.AreEqual(0, puzzle.Matrix[3, 3].Values[5]);  // IsGivenValue means no candidates
        Assert.AreEqual(ValueStatus.Given, puzzle.Matrix[3, 3].ValueStatus);
        Assert.AreEqual(5, puzzle.Matrix[5, 3].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[3, 4].Values[5]);
    }
    [TestMethod]
    public void TestAllThreeDistinctionChecksRemovePossibilitiesForAGivenBlock()
    {
        // Create specal setup to test [1, 1] block for 6 eliminated candidates, 2 for each distinction rule
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
        Puzzle puzzle = Puzzle.Create(createdCellMatrix);

        // Act
        puzzle.RemoveCandidates();

        // Assert:

        ConsoleRender.RenderMatrixCellValuesV3(puzzle);

        // coord [4,4] will not have 1, 2, 3, 4, 5, or 7 as candidates
        Assert.AreEqual(6, puzzle.Matrix[4, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[4, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[7]);
        // coords [3,4] and 5,4] will not have 1, 2, 4, or 5 as candidates
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
        // coords [4,3] and [4,5] will not have 3, 4, 5, or 7 as candidates
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
        // block [1,1] will not have 4 or 5 as candidates
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
        // column 4 will not have 1 or 2 as candidates
        // (exclude cells from previous tests in these tests)
        Assert.AreEqual(6, puzzle.Matrix[1, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[1, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[1, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[1, 4].Values[2]);
        Assert.AreEqual(6, puzzle.Matrix[8, 4].Values[6]);  // control
        Assert.AreEqual(8, puzzle.Matrix[8, 4].Values[8]);  // control
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[2]);

        // row 4 will not have 7 or 3 as candidates
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
