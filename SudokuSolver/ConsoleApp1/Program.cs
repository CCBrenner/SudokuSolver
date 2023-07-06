using SudokuSolver;

namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        // Setup
        int[,] startingMatrix = PuzzleBook.GetPuzzle("dellPuzzleLoversMay2012puzzle157");
        Cell[,] matrix = MatrixFactory.CreateMatrix(startingMatrix);
        Puzzle puzzle = Puzzle.Create(matrix);
        BruteForceSolver solver = new BruteForceSolver(puzzle);

        // Solve sudoku puzzle
        bool puzzleWasSolved = solver.Solve();

        // Final solve status
        string finalStatus = puzzleWasSolved ? "Solved" : "Not Solved";
        Console.WriteLine($"{finalStatus}\n");

        // Print the solved puzzle
        ConsoleRender.RenderMatrixCellValuesV3(puzzle);

        // Hold for viewing results
        Console.Read();
    }
}
