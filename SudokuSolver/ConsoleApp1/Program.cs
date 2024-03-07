using SudokuSolver;

namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        // Setup
        int[,] matrix = PuzzleBook.GetPuzzle("vol20matrix33");
        Puzzle puzzle = Puzzle.Create(matrix);
        var sudokuSolver = SudokuSolver.SudokuSolver.Create(puzzle);

        // Test
        ConsoleRender.RenderMatrixWithMetaData(puzzle);

        // Run
        sudokuSolver.PlayDefaultProgram();
    }
}
