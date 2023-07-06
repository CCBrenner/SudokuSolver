namespace SudokuSolver;

public record Txn(int id, int cellId, int indexOfValue, int previous, int newVal)
{
    public int Id { get; }
    public int CellId { get; }
    public int IndexOfValue { get; }
    public int Previous { get; }
    public int New { get; }
}
