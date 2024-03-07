using SudokuSolverV2;

namespace SolverTests.V2UnitTests;

[TestClass]
public class RemovePotentialValuesTests
{
    /*
    [TestMethod]
    public void TestDistinctionRulesRemovePossibilitiesForEachCellInARow()
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
        Row.RemoveCandidatesByDistinctInNeighborhood(puzzle.Rows);

        // Assert
        // Row 0:
        // 5 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 8].Values[5]);
        // 5 is a candidate for Matrix[0, 2]:
        Assert.AreEqual(5, puzzle.Matrix[0, 2].Values[5]);
        // 4 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[0, 8].Values[4]);
        // 4 is a candidate for Matrix[0, 4]:
        Assert.AreEqual(4, puzzle.Matrix[0, 4].Values[4]);
        // 3 is a candidate
        Assert.AreEqual(3, puzzle.Matrix[0, 0].Values[3]);
        Assert.AreEqual(3, puzzle.Matrix[0, 8].Values[3]);
        // 3 is NOT a candidate for [0, 3] or [4, 0]:
        Assert.AreEqual(0, puzzle.Matrix[0, 2].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[3]);
        // 1 is a candidate
        Assert.AreEqual(1, puzzle.Matrix[0, 0].Values[1]);
        Assert.AreEqual(1, puzzle.Matrix[0, 8].Values[1]);
        // 1 is NOT a candidate for [0, 3] or [0, 4]:
        Assert.AreEqual(0, puzzle.Matrix[0, 2].Values[1]);
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[1]);

        // Row 4:
        // 2 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[4, 6].Values[2]);
        // 2 is a candidate for Matrix[4, 0]:
        Assert.AreEqual(2, puzzle.Matrix[4, 0].Values[2]);
        // 3 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[4, 4].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 6].Values[3]);
        // 3 is a candidate for Matrix[4, 5]:
        Assert.AreEqual(3, puzzle.Matrix[4, 5].Values[3]);
        // 6 is a candidate
        Assert.AreEqual(6, puzzle.Matrix[4, 4].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[4, 6].Values[6]);
        // 6 is NOT a candidate for [4, 0] or [4, 5]:
        Assert.AreEqual(0, puzzle.Matrix[4, 0].Values[6]);
        Assert.AreEqual(0, puzzle.Matrix[4, 5].Values[6]);
        // 5 is a candidate
        Assert.AreEqual(5, puzzle.Matrix[4, 4].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[4, 6].Values[5]);
        // 5 is NOT a candidate for [4, 8] or [4, 5]:
        Assert.AreEqual(0, puzzle.Matrix[4, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[4, 5].Values[5]);
    }

    [TestMethod]
    public void TestDistinctionRulesRemovePossibilitiesForEachCellInAColumn()
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
        Column.RemoveCandidatesByDistinctInNeighborhood(puzzle.Columns);

        // Assert
        // Column 0:
        // 8 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[8]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[8]);
        // 8 is a candidate for Matrix[3, 0]:
        Assert.AreEqual(8, puzzle.Matrix[3, 0].Values[8]);
        // 2 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[8, 0].Values[2]);
        // 2 is a candidate for Matrix[4, 0]:
        Assert.AreEqual(2, puzzle.Matrix[4, 0].Values[2]);
        // 3 is a candidate
        Assert.AreEqual(3, puzzle.Matrix[0, 0].Values[3]);
        Assert.AreEqual(3, puzzle.Matrix[8, 0].Values[3]);
        // 3 is NOT a candidate for [3, 0] or [4, 0]:
        Assert.AreEqual(0, puzzle.Matrix[3, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[4, 0].Values[3]);
        // 5 is a candidate
        Assert.AreEqual(5, puzzle.Matrix[0, 0].Values[5]);
        Assert.AreEqual(5, puzzle.Matrix[8, 0].Values[5]);
        // 5 is NOT a candidate for [3, 0] or [4, 0]:
        Assert.AreEqual(0, puzzle.Matrix[3, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[4, 0].Values[5]);

        // Column 4:
        // 4 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[4]);
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[4]);
        // 4 is a candidate for Matrix[0, 4]:
        Assert.AreEqual(4, puzzle.Matrix[0, 4].Values[4]);
        // 5 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[3, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[5, 4].Values[5]);
        // 5 is a candidate for Matrix[8, 4]:
        Assert.AreEqual(5, puzzle.Matrix[8, 4].Values[5]);
        // 6 is a candidate
        Assert.AreEqual(6, puzzle.Matrix[3, 4].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[5, 4].Values[6]);
        // 6 is NOT a candidate for [0, 4] or [8, 4]:
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[6]);
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[6]);
        // 2 is a candidate
        Assert.AreEqual(2, puzzle.Matrix[3, 4].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[5, 4].Values[2]);
        // 2 is NOT a candidate for [0, 4] or [8, 4]:
        Assert.AreEqual(0, puzzle.Matrix[0, 4].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[2]);
    }
    [TestMethod]
    public void TestDistinctionRulesRemovePossibilitiesForEachCellInABlock()
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
        Block.RemoveCandidatesByDistinctInNeighborhood(puzzle.Blocks);

        // Assert
        // Block [0,0]:
        // 5 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[5]);
        // 5 is a candidate for Matrix[0, 2]:
        Assert.AreEqual(5, puzzle.Matrix[0, 2].Values[5]);
        // 3 is not a candidate
        Assert.AreEqual(0, puzzle.Matrix[0, 0].Values[3]);
        Assert.AreEqual(0, puzzle.Matrix[0, 1].Values[3]);
        // 3 is a candidate for Matrix[1, 2]:
        Assert.AreEqual(3, puzzle.Matrix[1, 2].Values[3]);
        // 6 is a candidate
        Assert.AreEqual(6, puzzle.Matrix[0, 0].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[0, 1].Values[6]);
        // 6 is NOT a candidate for [0, 2] or [1, 2]:
        Assert.AreEqual(0, puzzle.Matrix[0, 2].Values[6]);
        Assert.AreEqual(0, puzzle.Matrix[1, 2].Values[6]);
        // 2 is a candidate
        Assert.AreEqual(2, puzzle.Matrix[0, 0].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[0, 1].Values[2]);
        // 2 is NOT a candidate for [0, 2] or [1, 2]:
        Assert.AreEqual(0, puzzle.Matrix[0, 2].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[1, 2].Values[2]);

        // Block [1,2]:
        // 9 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[4, 7].Values[9]);
        Assert.AreEqual(0, puzzle.Matrix[5, 6].Values[9]);
        // 9 is a candidate for Matrix[7, 3]:
        Assert.AreEqual(9, puzzle.Matrix[7, 3].Values[9]);
        // 5 is NOT a candidate
        Assert.AreEqual(0, puzzle.Matrix[7, 4].Values[5]);
        Assert.AreEqual(0, puzzle.Matrix[6, 5].Values[5]);
        // 5 is a candidate for Matrix[8, 4]:
        Assert.AreEqual(5, puzzle.Matrix[8, 4].Values[5]);
        // 6 is a candidate
        Assert.AreEqual(6, puzzle.Matrix[7, 4].Values[6]);
        Assert.AreEqual(6, puzzle.Matrix[6, 5].Values[6]);
        // 6 is NOT a candidate for [7, 3] or [8, 4]:
        Assert.AreEqual(0, puzzle.Matrix[7, 3].Values[6]);
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[6]);
        // 2 is a candidate
        Assert.AreEqual(2, puzzle.Matrix[4, 7].Values[2]);
        Assert.AreEqual(2, puzzle.Matrix[5, 6].Values[2]);
        // 2 is NOT a candidate for [7, 3] or [8, 4]:
        Assert.AreEqual(0, puzzle.Matrix[7, 3].Values[2]);
        Assert.AreEqual(0, puzzle.Matrix[8, 4].Values[2]);
    }
    [TestMethod]
    public void TestAllThreeDistinctionChecksRemovePossibilitiesForAGivenBlock()
    {
        // CreateWithBruteForceSolver specal setup to test [1, 1] block for 6 eliminated candidates, 2 for each distinction rule
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
        Puzzle puzzle = Puzzle.CreateWithBruteForceSolver();
        puzzle.LoadMatrixAsCellValues(givenMatrix);

        // Act
        puzzle.PerformCandidateElimination();

        // Assert:

        ConsoleRender.RenderMatrix(puzzle);

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
    */
}


