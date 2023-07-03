namespace SudokuSolver;

public class NotedPossibilityTypeOne
{
    public NotedPossibilityTypeOne(Cell cellOne, Cell cellTwo)
    {
        Cells = new Cell[2] { cellOne, cellTwo };
    }
    public Cell[] Cells { get; private set; }
    public bool IsScopedRow => Cells[0].Row == Cells[1].Row;
    public bool IsScopedColumn => Cells[0].Column == Cells[1].Column;
    public bool IsScopedSquare
    {
        get
        {
            int cellOneSquareRow = (Cells[0].Row - (Cells[0].Row % 3)) / 3;  // gives square row coord between 0 and 2 inclusive
            int cellTwoSquareRow = (Cells[1].Row - (Cells[1].Row % 3)) / 3;  // gives square row coord between 0 and 2 inclusive
            bool squareRowsMatch = cellOneSquareRow == cellTwoSquareRow;

            int cellOneSquareColumn = (Cells[0].Column - (Cells[0].Column % 3)) / 3;  // gives square row coord between 0 and 2 inclusive
            int cellTwoSquareColumn = (Cells[1].Column - (Cells[1].Column % 3)) / 3;  // gives square row coord between 0 and 2 inclusive
            bool squareColumnsMatch = cellOneSquareColumn == cellTwoSquareColumn;

            return squareRowsMatch && squareColumnsMatch;
        }
    }
}
