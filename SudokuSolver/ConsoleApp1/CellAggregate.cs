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

                if (cell.Values[0] != 0)
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
    protected SortedSet<int> GetCandidatesToEliminate()
    {
        // Go through each cell in the row & if they have any of the flags marked as true, add them to the SortedSet.
        SortedSet<int> candidatesToEliminate = new();

        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                /*
                if (cell.ValueStatus == ValueStatus.Given || cell.ValueStatus == ValueStatus.Confirmed)
                {
                    int possiblityToEliminate = cell.Values[0];
                    Console.WriteLine($"Cell ID: {cell.Id}, candidateToEliminate: {possiblityToEliminate}");
                    rowCandidatesToEliminate.Add(possiblityToEliminate);
                }*/
                if (cell.Values[0] != 0)
                {
                    int possiblityToEliminate = cell.Values[0];
                    //Console.WriteLine($"Cell ID: {cell.Id}, candidateToEliminate: {possiblityToEliminate}");
                    candidatesToEliminate.Add(possiblityToEliminate);
                }
            }
        }

        return candidatesToEliminate;
    }
}
