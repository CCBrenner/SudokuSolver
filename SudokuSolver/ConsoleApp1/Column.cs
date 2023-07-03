namespace SudokuSolver;

public class Column : CellAggregate
{
    public Column(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public Cell? Cell(int position) => Cells.FirstOrDefault(y => y.PosInCol == position);
    public bool IsSolvableBasedOnCandidatesOfColumn => GetIsSolvableBasedOnCandidates(Cells.ToList());

    public static Column[] CreateArrayFromCellReferences(List<Cell> cells)
    {
        Column[] columns = new Column[10]
        {
            new Column(0),
            new Column(1),
            new Column(2),
            new Column(3),
            new Column(4),
            new Column(5),
            new Column(6),
            new Column(7),
            new Column(8),
            new Column(9),
        };

        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                columns[cell.ColumnId].AddCellReference(cell);
            }
        }

        return columns;
    }

    public static void AssignColumnReferenceToCellsPerColumn(Column[] columns)
    {
        foreach (var column in columns)
        {
            column.AssignColumnReferenceToCells();
        }
    }
    public static void AssignColumnReferenceToCellsPerColumn(List<Column> columns)
    {
        foreach (var column in columns)
        {
            column.AssignColumnReferenceToCells();
        }
    }

    public void AddCellReference(Cell cell)
    {
        if (cell.ColumnId == Id)
        {
            Cells[cell.RowId] = cell;
        }
    }
    public void AssignColumnReferenceToCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                cell.AssignColumnReference(this);
            }
        }
    }

    public static void EliminateCandidatesByDistinctInNeighborhood(Column[] columns)
    {
        foreach (var column in columns)
        {
            column.EliminateCandidates();
        }
    }
    public static void EliminateCandidatesByDistinctInNeighborhood(List<Column> columns)
    {
        foreach (var column in columns)
        {
            column.EliminateCandidates();
        }
    }

    private void EliminateCandidates()
    {
        // Get Given, and Confirmed numbers of column
        SortedSet<int> candidatesToEliminate = GetCandidatesToEliminate();

        // Eliminate candidates of cells in the column
        EliminateCandidatesFromCells(candidatesToEliminate);
    }

    private SortedSet<int> GetCandidatesToEliminate()
    {
        // Go through each cell in the column & if they have any of the flags marked as true, add them to the SortedSet.
        SortedSet<int> columnCandidatesToEliminate = new();

        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                if (cell.ValueStatus == ValueStatus.Given || cell.ValueStatus == ValueStatus.Confirmed)
                {
                    int possiblityToEliminate = cell.Values[0];
                    columnCandidatesToEliminate.Add(possiblityToEliminate);
                }
            }
        }

        return columnCandidatesToEliminate;
    }

    private void EliminateCandidatesFromCells(SortedSet<int> candidatesToEliminate)
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
    public static bool IsSolvableBasedOnCandidates(Column[] columns)
    {
        foreach (var column in columns)
        {
            if (!column.IsSolvableBasedOnCandidatesOfColumn)
            {
                return false;
            }
        }
        return true;
    }
    public static bool IsSolvableBasedOnCandidates(List<Column> columns)
    {
        foreach (var column in columns)
        {
            if (!column.IsSolvableBasedOnCandidatesOfColumn)
            {
                return false;
            }
        }
        return true;
    }

    public static List<Column> CreateListFromCellReferences(List<Cell> cells)
    {
        List<Column> result = new()
        {
            new Column(1),
            new Column(2),
            new Column(3),
            new Column(4),
            new Column(5),
            new Column(6),
            new Column(7),
            new Column(8),
            new Column(9),
        };

        foreach (var cell in cells)
        {
            result[cell.ColumnId - 1].Cells.Add(cell);
        }

        return result;
    }
}
