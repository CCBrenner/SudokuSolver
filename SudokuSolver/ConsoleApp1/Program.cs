using SudokuSolver;

namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        // use puzzle #33 from Sudolu Variety Colleection, Vol. 20
        int[,] matrix33 =
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


        //Cell[,] matrix = MatrixFactory.CreateMatrix(matrix33);
        Cell[,] matrix = MatrixFactory.CreateMatrix(givenMatrix);
        int[,] superimposeMatrix = MatrixFactory.GetSuperImposeMatrix();
        Puzzle puzzle = Puzzle.Create(matrix, superimposeMatrix);

        // Strategy:
        // Primarily brute force with support from candidate check/removal to minimize
        // the number of brute force operations/iterations.
        /*
        Why this strategy?: The goal of solving ANY sudoku puzzle means solving even the hardest puzzle there is.
        I (Collyn) personally have encountered many puzzles that I have not been able to solve, many being only more than 
        moderately difficult. My human process methods cannot achieve this currently, but they might in the future.
        This, and computers are much better at step forward/step back processes because they can perform them quickly, procedurally
        based on conditionals without challenge - much harder for a human to do.
        But, a pure brute force strategy can end up being a really long process due to the number of possible single-number configurations
        and combinations that can exist in a 9x9 grid. Any help to reduce the numer of checks (especially long periods of processing that
        could be eliminated based on a logic check) will reduce the number of operations and therefore the processing speed of the 
        sudolku puzzle solver.
        */
        // Tactics/Process:
        // 0. On a loop:
        while (true)
        {
            // 1. Remove candidates by various methods based on entered values (given, expected, and confirmed).
            puzzle.RemoveCandidates();

            // 2. Update values based on single candidate: if no expected values in matrix (first iteration; all values are "given"), then add confirmed values to cells;
            // otherwise, add values with ValueStatus of "ValueStatus.Expected";
            // expected values may not be correct and may have to be undone at some point in the brute force process.
            puzzle.UpdateCellValuesBasedOnSingleCandidate();

            // 3. Check if puzzle is still solvable based on remaining cell candidates of each neighborhood.
            // If solvable, proceed. If not, eliminate last expected value as option.
            // If another option is available for the cell, proceed with testing that candidate.
            // If no remaining candidates, then previous candidate is not the right candidate: reset node cell candidates,
            // eliminate previous cells's candidate as an option and try the next candidate. Start from the top with 1. again.
            if (!puzzle.IsSolvableBasedOnCandidates)
            {
                if (puzzle.CurrentCandidateIsLastCandidateOfCurrentCell)
                {
                    bool candidatesAreAvailableToTry = puzzle.BacktrackToLastCell();  // handles last candidate try as a possibility

                    if (!candidatesAreAvailableToTry)
                    {
                        break;
                    }

                    continue;
                }
                else
                {
                    // 4. Move to the next cell in the list (if first iteration, this cell is [0,0]), making that cell the new current cell.
                    // 5. Test a candidate/the next candidate by assigning it as the expected value of the current cell
                    puzzle.CurrentCell.AddCurrentValueToTriedCandidateList();
                    puzzle.CurrentCell.ProceedToNextCandidateToTry();
                }
                continue;
            }

            // 6. Repeat starting from 1. again.
        }
        // separate:
        /*
        puzzle.RemoveCandidates();
        puzzle.UpdateCellValuesBasedOnSingleCandidate();

        puzzle.RemoveUnconfirmedValuesThatDoNotHaveRespectiveCandidate();

        puzzle.UpdateCellValuesBasedOnSingleCandidate();

        puzzle.RemoveUnconfirmedValuesThatDoNotHaveRespectiveCandidate();

        ConsoleRender.RenderMatrixCellValuesV2(puzzle);
     
        Console.ReadLine();
        */
    }
}
