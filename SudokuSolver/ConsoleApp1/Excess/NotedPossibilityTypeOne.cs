namespace SudokuSolver;
/*
public class PositivePencilMarkingTypeOne
{
    public PositivePencilMarkingTypeOne(Cell cellOne, Cell cellTwo)
    {
        Cells = new Cell[2] { cellOne, cellTwo };
    }
    public Cell[] Cells { get; private set; }
    public bool IsScopedRow => Cells[0].Row == Cells[1].Row;
    public bool IsScopedColumn => Cells[0].Column == Cells[1].Column;
    public bool IsScopedBlock
    {
        get
        {
            int cellOneBlockRow = (Cells[0].Row - (Cells[0].Row % 3)) / 3;  // gives block row coord between 0 and 2 inclusive
            int cellTwoBlockRow = (Cells[1].Row - (Cells[1].Row % 3)) / 3;  // gives block row coord between 0 and 2 inclusive
            bool blockRowsMatch = cellOneBlockRow == cellTwoBlockRow;

            int cellOneBlockColumn = (Cells[0].Column - (Cells[0].Column % 3)) / 3;  // gives block row coord between 0 and 2 inclusive
            int cellTwoBlockColumn = (Cells[1].Column - (Cells[1].Column % 3)) / 3;  // gives block row coord between 0 and 2 inclusive
            bool blockColumnsMatch = cellOneBlockColumn == cellTwoBlockColumn;

            return blockRowsMatch && blockColumnsMatch;
        }
    }
}
*/