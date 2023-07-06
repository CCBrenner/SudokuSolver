namespace SudokuSolver;

public class Row : CellAggregate
{
    public Row(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public Cell? Cell(int position) => Cells.FirstOrDefault(y => y.PosInRow == position);
    public bool IsSolvableBasedOnCandidatesOfRow => GetIsSolvableBasedOnCandidates(Cells.ToList());

    public static Row[] CreateArrayFromCellReferences(List<Cell> cells)
    {
        Row[] rows = new Row[10]
        {
            new Row(0),
            new Row(1),
            new Row(2),
            new Row(3),
            new Row(4),
            new Row(5),
            new Row(6),
            new Row(7),
            new Row(8),
            new Row(9),
        };

        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                rows[cell.RowId].AddCellReference(cell);
            }
        }

        return rows;
    }
    public void AssignRowReferenceToCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                cell.AssignRowReference(this);
            }
        }
    }

    public void AddCellReference(Cell cell)
    {
        if (cell.RowId == Id)
        {
            Cells[cell.ColumnId] = cell;
        }
    }

    public static void AssignRowReferenceToCellsPerRow(Row[] rows)
    {
        foreach (var row in rows)
        {
            row.AssignRowReferenceToCells();
        }
    }
    public static void AssignRowReferenceToCellsPerRow(List<Row> rows)
    {
        foreach (var row in rows)
        {
            row.AssignRowReferenceToCells();
        }
    }

    public static void EliminateCandidatesByDistinctInNeighborhood(Row[] rows)
    {
        foreach (var row in rows)
        {
            row.EliminateCandidates();
        }
    }
    public static void EliminateCandidatesByDistinctInNeighborhood(List<Row> rows)
    {
        foreach (var row in rows)
        {
            row.EliminateCandidates();
        }
    }

    private void EliminateCandidates()
    {
        // Get Given, and Confirmed numbers of row
        SortedSet<int> candidatesToEliminate = GetCandidatesToEliminate();

        // Eliminate candidates of cells in the row
        EliminateCandidatesFromCellsOfRow(candidatesToEliminate);
    }

    private void EliminateCandidatesFromCellsOfRow(SortedSet<int> candidatesToEliminate)
    {
        foreach (var candidate in candidatesToEliminate)
        {
            foreach (var cell in Cells)
            {
                if (cell is not null)
                {
                    cell.EliminateCandidate(candidate);
                }
            }
        }
    }
    public static bool IsSolvableBasedOnCandidates(Row[] rows)
    {
        foreach (var row in rows)
        {
            if (!row.IsSolvableBasedOnCandidatesOfRow)
            {
                return false;
            }
        }
        return true;
    }
    public static bool IsSolvableBasedOnCandidates(List<Row> rows)
    {
        foreach (var row in rows)
        {
            if (!row.IsSolvableBasedOnCandidatesOfRow)
            {
                return false;
            }
        }
        return true;
    }

    public static List<Row> CreateListFromCellReferences(List<Cell> cells)
    {
        List<Row> result = new()
        {
            new Row(1),
            new Row(2),
            new Row(3),
            new Row(4),
            new Row(5),
            new Row(6),
            new Row(7),
            new Row(8),
            new Row(9),
        };

        foreach (var cell in cells)
        {
            result[cell.RowId - 1].Cells.Add(cell);
        }

        return result;
    }
}
