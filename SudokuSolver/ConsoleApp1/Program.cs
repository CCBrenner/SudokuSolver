using SudokuSolver;

namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        MethodTwo("dellPuzzleLoversMay2012puzzle157");
    }
    
    private static void MethodOne(string puzzleName)
    {
        // Setup
        int[,] startingMatrix = PuzzleBook.GetPuzzle(puzzleName);
        Cell[,] matrix = MatrixFactory.CreateMatrix(startingMatrix);
        Puzzle puzzle = Puzzle.Create(matrix);
        BruteForceSolver solver = new(puzzle);

        // initial constraint of candidates
        puzzle.RemoveCandidates();

        // Print unsolved puzzle starting point
        ConsoleRender.RenderMatrixCellValuesV3(puzzle);

        // Solve sudoku puzzle
        bool puzzleWasSolved = solver.Solve();

        // Final solve status
        string finalStatus = puzzleWasSolved ? "Solved" : "Not Solved";
        Console.WriteLine(finalStatus + "\n");

        // Print the solved puzzle
        ConsoleRender.RenderMatrixCellValuesV3(puzzle);

        // Hold for viewing results
        Console.Read();
    }

    private static void MethodTwo(string puzzleName)
    {
        // Setup
        int[,] startingMatrix = PuzzleBook.GetPuzzle(puzzleName);

        Cell[,] matrix = MatrixFactory.CreateMatrix(startingMatrix);
        TxnLedger ledger = new();
        Puzzle puzzle = Puzzle.Create(matrix, ledger);

        BruteForceSolver solver = new(puzzle);

        // initial constraint of candidates
        puzzle.RemoveCandidates();

        // Print unsolved puzzle starting point
        ConsoleRender.RenderMatrixCellValuesV3(puzzle);

        // Solve sudoku puzzle
        bool puzzleWasSolved = solver.Solve();

        // Final solve status
        string finalStatus = puzzleWasSolved ? "Solved" : "Not Solved";
        Console.WriteLine(finalStatus + "\n");

        // Print the solved puzzle
        ConsoleRender.RenderMatrixCellValuesV3(puzzle);

        // Hold for viewing results
        Console.Read();
    }
}
