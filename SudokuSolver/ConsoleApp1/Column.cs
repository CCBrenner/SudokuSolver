namespace SudokuSolver;

public class Column
{
    public Column(int id)
    {
        Cells = new Cell[10];
        Id = id;
    }

    public int Id { get; }
    public Cell[] Cells { get; private set; }  // [0] is not used
    public List<int> MissingValues => GetMissingValues();

    public void AddCellReference(Cell cell)
    {
        if (cell.Column == Id)
        {
            Cells[cell.Row] = cell;
        }
    }
    private List<int> GetMissingValues()
    {
        int[] missingValues = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                missingValues[cell.Values[0]] = 0;
            }
        }

        return missingValues.ToList();
    }
}
