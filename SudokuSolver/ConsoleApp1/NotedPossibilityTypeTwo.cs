namespace SudokuSolver;

public class NotedPossibilityTypeTwo
{
    public NotedPossibilityTypeTwo(Cell cell, int[] pair)
    {
        Cell = cell;
        Pair = pair;
        IsExpired = false;
    }
    public Cell Cell { get; private set; }
    public int[] Pair { get; private set; }
    public bool IsExpired { get; private set; }

    public void EliminateAllOtherPossibilitiesExceptForCellChainPossibilties()
    {
        for (int i = 1; i < 10; i++)
            if (i != Pair[0] && i != Pair[1])
                Cell.EliminatePossibility(i);
    }

    public bool EliminatePossibility(int possibility)
    {
        Cell.EliminatePossibility(possibility);
        return UpdateIfIsExpired(possibility);
    }

    private bool UpdateIfIsExpired(int possibility)
    {
        foreach (var value in Pair)
        {
            if (value == possibility)
            {
                IsExpired = true;
                return true;
            }
        }
        return false;
    }
}
