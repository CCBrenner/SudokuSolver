namespace SudokuSolver;

public class SudokuSolver
{
    private SudokuSolver(Puzzle puzzle)
    {
        Puzzle = puzzle;
        Solver = BruteForceSolver.Create(Puzzle);
    }

    public BruteForceSolver Solver { get; private set; }
    public Puzzle Puzzle { get; private set; }

    public static SudokuSolver Create(Puzzle puzzle) => new(puzzle);
    public void PlayDefaultProgram()
    {
        PlayProgramTwo();
    }
    public void PlayProgramOne()
    {
        // Print unsolved puzzle starting point
        RenderMatrix();

        // Solve sudoku puzzle
        bool puzzleWasSolved = Solve();

        // Final solve status
        PrintFinalSolveStatus(puzzleWasSolved);

        // Print the solved puzzle
        RenderMatrix();

        // Hold for viewing results
        Console.Read();
    }
    public void PlayProgramTwo()
    {
        // Print unsolved puzzle starting point
        RenderMatrix();

        // Solve sudoku puzzle
        bool puzzleWasSolved = Solve();

        // Final solve status
        PrintFinalSolveStatus(puzzleWasSolved);

        // Print the solved puzzle
        RenderMatrix();

        // Print stopwatch time
        PrintStopwatchTime();

        // Print standard txn info of puzzle
        PrintTxnInfo();

        // Hold for viewing results
        Console.Read();
    }
    public void RenderMatrix()
    {
        ConsoleRender.RenderMatrix(Puzzle);
    }
    public bool Solve()
    {
        return Solver.Solve();
    }
    public void PrintFinalSolveStatus(bool puzzleWasSolved)
    {
        string finalStatus = puzzleWasSolved ? "Solved" : "Not Solved";
        Console.WriteLine(finalStatus + "\n");
    }
    public void PrintStopwatchTime()
    {
        Console.WriteLine($"Solve time: {Solver.StopwatchTime} seconds\n");
    }
    public void PrintTxnInfo()
    {
        ConsoleRender.RenderStandardTxnInfo(Puzzle.Ledger, Solver);
    }
    public void StepThroughTxns()
    {
        Puzzle.Ledger.StepThroughTxnsByValue();
    }
}
