using SudokuSolver;

namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        MethodTwo("vol20matrix33");
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
        ledger.RecordNewTxn(3, 3, 2, 3);
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

        // Print stopwatch time
        Console.WriteLine($"Solve time: {solver.StopwatchTime} seconds\n");

        // Print the transactions
        Console.WriteLine($"Number of Txns in Ledger: {ledger.Txns.Count:N0}");
        Console.WriteLine($"Number of ValueTxns: {ledger.ValueTxns.Count:N0}");
        Console.WriteLine($"Number of CandidateTxns: {ledger.CandidateTxns.Count:N0}\n");

        // Print proportions
        decimal valueTxnsCount = ledger.ValueTxns.Count;
        decimal txnsCount = ledger.Txns.Count;
        Console.WriteLine($"ValueTxns % of Txns: {valueTxnsCount / txnsCount:P}\n");

        // Print rates
        Console.WriteLine($"Txns/second: {ledger.Txns.Count / solver.StopwatchTime:N1}");
        Console.WriteLine($"ValueTxns/second: {ledger.ValueTxns.Count / solver.StopwatchTime:N1}");
        Console.WriteLine($"CandidateTxns/second: {ledger.CandidateTxns.Count / solver.StopwatchTime:N1}\n");

        // Hold for viewing results
        Console.Read();
    }
}
