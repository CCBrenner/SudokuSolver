namespace SudokuSolver;
/*
public class PositivePencilMarkingTypeTwo
{
    public PositivePencilMarkingTypeTwo(Cell cell, int[] pair)
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

    public bool EliminatePossibility(int pencilMarking)
    {
        Cell.EliminatePossibility(pencilMarking);
        return UpdateIfIsExpired(pencilMarking);
    }

    private bool UpdateIfIsExpired(int pencilMarking)
    {
        foreach (var value in Pair)
        {
            if (value == pencilMarking)
            {
                IsExpired = true;
                return true;
            }
        }
        return false;
    }
}
*/