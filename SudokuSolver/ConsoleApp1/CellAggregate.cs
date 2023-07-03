namespace SudokuSolver;

public abstract class CellAggregate
{
    public CellAggregate()
    {
        Cells = new List<Cell>();
    }
    public List<Cell> Cells { get; private set; }
    public List<int> Candidates => Cell.GetCandidates(Cells.ToList());

    public static bool GetIsSolvableBasedOnCandidates(List<Cell> cells)
    {
        int valuelessCellCount = 0;
        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                // If any cell without a value has no more candidates (after candidates have been updated)
                if (cell.Candidates.Count == 0)
                {
                    return false;
                }

                if (cell.Value[0] != 0)
                {
                    valuelessCellCount++;
                }
            }
        }

        // If quantity of cells with no values is greater than the number of available candidates
        List<int> candidates = Cell.GetCandidates(cells.ToList());
        if (candidates.Count < valuelessCellCount)
        {
            return false;
        }

        return true;
    }
}
