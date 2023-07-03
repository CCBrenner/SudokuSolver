namespace SudokuSolver;

public class ConsoleRender
{
    public static void RenderMatrixCellValues(Puzzle puzzle)
    {
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                Console.WriteLine();
            }
            string row = $"[   {puzzle.Matrix[i, 0].GetAllValues()} {puzzle.Matrix[i, 1].GetAllValues()} {puzzle.Matrix[i, 2].GetAllValues()} ] [ " +
                         $"  {puzzle.Matrix[i, 3].GetAllValues()} {puzzle.Matrix[i, 4].GetAllValues()} {puzzle.Matrix[i, 5].GetAllValues()} ] [ " +
                         $"  {puzzle.Matrix[i, 6].GetAllValues()} {puzzle.Matrix[i, 7].GetAllValues()} {puzzle.Matrix[i, 8].GetAllValues()} ]\n";
            Console.Write(row);
        }
    }
    public static string RenderMatrix(Puzzle puzzle)
    {
        string matrixRenderString = GetMatrixRenderString(puzzle);
        Console.Write(matrixRenderString);
        return matrixRenderString;
    }
    private static string GetMatrixRenderString(Puzzle puzzle)
    {
        string matrixRender = string.Empty;

        for (int i = 0; i < 9; i++)
        {
            matrixRender += $"[ {puzzle.Matrix[i, 0].Value} ][ {puzzle.Matrix[i, 1].Value} ][ {puzzle.Matrix[i, 2].Value} ]  " +
                            $"[ {puzzle.Matrix[i, 3].Value} ][ {puzzle.Matrix[i, 4].Value} ][ {puzzle.Matrix[i, 5].Value} ]  " +
                            $"[ {puzzle.Matrix[i, 6].Value} ][ {puzzle.Matrix[i, 7].Value} ][ {puzzle.Matrix[i, 8].Value} ]\n";
            if (i % 3 == 2 && i != 8)
                matrixRender += "\n";
        }

        return matrixRender;
    }
}
