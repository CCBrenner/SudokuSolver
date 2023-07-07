namespace SudokuSolver;

public class Txn
{
    public Txn(int id, int cellId, int indexOfValue, int previous, int newVal)
    {
        Id = id;
        CellId = cellId;
        IndexOfValue = indexOfValue;
        Previous = previous;
        New = newVal;
    }
    // Definition of Transaction: An instance of changing a value or candidate to something different than what it was previously.
    public int Id { get; }
    public int CellId { get; }
    public int IndexOfValue { get; }
    public int Previous { get; }
    public int New { get; }
}
