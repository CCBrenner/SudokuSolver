namespace SudokuSolver;

public class Row
{
    public Row(int id)
    {
        Cells = new Cell[10];
        Id = id;
    }

    public int Id { get; }
    public Cell[] Cells { get; private set; }  // [0] is not used
    public List<int> MissingValues => GetMissingValues();


    public void AddCellReference(Cell cell)
    {
        if (cell.Row == Id)
        {
            Cells[cell.Column] = cell;
        }
    }
    private List<int> GetMissingValues()
    {
        int[] missingValues = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 1; i < 10; i++)
        {
            if (Cells[i] is not null)
            {
                missingValues[Cells[i].Values[0]] = 0;
            }
        }

        return missingValues.ToList();
    }
}
