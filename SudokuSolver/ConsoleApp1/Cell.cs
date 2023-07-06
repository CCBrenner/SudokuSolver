namespace SudokuSolver;

public class Cell
{
    public Cell(int row, int column, int givenStartingValue = 0)
    {
        RowId = row;
        ColumnId = column;
        BlockId = GetBlockAssignment(RowId, ColumnId);
        Id = GetNumberAssignment(RowId, ColumnId);

        if (givenStartingValue == 0)
        {
            Values = new int[10]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };  // [0] is "Value"; all others are Candidates
            ValueStatus = ValueStatus.Undefined;
        }
        else
        {
            Values = new int[10];
            Values[0] = givenStartingValue;  // let [0] be the given value that never changes; Candidates receive assignment of 0
            ValueStatus = ValueStatus.Given;
        }

        PositivePencilMarkings = new int[10];
        TriedCandidates = new();
    }

    private const int NON_POSSIBILITY_PLACEHOLDER_VALUE = 0;

    public ValueStatus ValueStatus { get; private set; }
    public int Id { get; private set; }
    public int RowId { get; }
    public int PosInRow => ColumnId;
    public int ColumnId { get; }
    public int PosInCol => RowId;
    public int BlockId { get; }
    public int PosInBlock => GetPositionInBlock();
    public Row Row { get; private set; }
    public Column Column { get; private set; }
    public Block Block { get; private set; }
    public BlockRow BlockRow => Block.BlockRow;
    public BlockRow BlockColumn => Block.BlockRow;
    public Puzzle Puzzle { get; private set; }

    // [0] is value,
    // [1-9] are *negative* pencil markings (possibilities)
    public int[] Values { get; private set; }
    // [0] is placeholder for indexing purposes with no other purpose;
    // [1-9] are *positive* pencil markings (50/50 probabilities in most cases)
    public int[] PositivePencilMarkings { get; set; }
    public List<int> Candidates => GetCandidates();

    public string Value =>
        Values[0] == NON_POSSIBILITY_PLACEHOLDER_VALUE ? " " : Values[0].ToString();

    public List<int> TriedValuesAsCurrentCell { get; private set; }
    public List<int> CandidatesTried { get; private set; }
    public List<int> TriedCandidates { get; private set; }

    public bool HasCandidate(int number) =>
        Values[number] == number;
    private int GetNumberAssignment(int row, int column) =>
        ((row - 1) * 9) + column;

    public int UpdateValueBasedOnSingleCandidate()
    {
        int candidateCount = 0;
        int savedValueFromIteration = NON_POSSIBILITY_PLACEHOLDER_VALUE;

        foreach (var val in Values)
        {
            if (val != 0)
            {
                candidateCount++;
                savedValueFromIteration = val;
            }
        }

        if (candidateCount == 1 && savedValueFromIteration != 0)
        {
            Values[0] = savedValueFromIteration;
            if (Puzzle.NoExpectedCellValuesInCells)
                ValueStatus = ValueStatus.Confirmed;
            else
                ValueStatus = ValueStatus.Expected;
        }

        return savedValueFromIteration;
    }
    public int SetExpectedValue(int expectedValue)
    {
        if (!IsCandidate(expectedValue) && ValueStatus != ValueStatus.Given)
        {
            return NON_POSSIBILITY_PLACEHOLDER_VALUE;
        }

        Values[0] = expectedValue;
        ValueStatus = ValueStatus.Expected;

        return Values[0];
    }
    public int ReconcileValueWithCandidates()
    {
        if (ValueStatus == ValueStatus.Given || ValueStatus == ValueStatus.Confirmed || Values[Values[0]] != NON_POSSIBILITY_PLACEHOLDER_VALUE)
        {
            return Values[0];
        }

        Values[0] = NON_POSSIBILITY_PLACEHOLDER_VALUE;
        return Values[0];
    }
    public int EliminateCandidate(int candidate)
    {
        if (candidate == 0 || candidate > 9)
        {
            return NON_POSSIBILITY_PLACEHOLDER_VALUE;
        }

        Values[candidate] = NON_POSSIBILITY_PLACEHOLDER_VALUE;
        return candidate;
    }

    public int AssignConfirmedValue(int newCellValue)
    {
        if (Values[newCellValue] == NON_POSSIBILITY_PLACEHOLDER_VALUE || ValueStatus == ValueStatus.Given)
        {
            return NON_POSSIBILITY_PLACEHOLDER_VALUE;
        }

        Values[0] = newCellValue;
        ValueStatus = ValueStatus.Confirmed;

        // AddCellToQueueForChecking();  // not utilizing in brute force version; adding as reminder for non-brute force version

        return newCellValue;
    }

    public string GetAllValues()
    {
        string allValues = string.Empty;
        int spaceCounter = 0;
        int counter = 0;
        foreach (var val in Values)
        {
            if (counter == 0)
            {
                allValues += $"{val} ";
            }
            else if (val != 0)
            {
                allValues += val.ToString();
            }
            else
            {
                spaceCounter++;
            }
            counter++;
        }

        for (int i = 0; i < spaceCounter; i++)
        {
            allValues += " ";
        }

        return allValues;
    }
    private bool IsCandidate(int valueBeingChecked)
    {
        foreach (var val in Values)
        {
            if (val != NON_POSSIBILITY_PLACEHOLDER_VALUE && val == valueBeingChecked)
            {
                return true;
            }
        }
        return false;
    }
    private int GetBlockAssignment(int row, int column)
    {
        int rowMod = (row - 1) % 3;
        int blockRow = (row - 1 - rowMod) / 3;

        int colMod = (column - 1) % 3;
        int blockCol = (column - 1 - colMod) / 3;

        int blockAssignment = (blockRow * 3) + blockCol + 1; 

        return blockAssignment;
    }

    public static Cell[] CreateArrayFromCellReferencesOfMatrix(Cell[,] compositionMatrix)
    {
        Cell[] cells = new Cell[82];

        foreach (var cell in compositionMatrix)
        {
            cells[cell.Id] = cell;
        }

        return cells;
    }
    public static List<Cell> CreateListFromCellReferencesOfMatrix(Cell[,] compositionMatrix)
    {
        List<Cell> cells = new();

        foreach (var cell in compositionMatrix)
        {
            cells.Add(cell);
        }

        return cells;
    }

    public void AssignRowReference(Row row)
    {
        Row = row;
    }

    public void AssignColumnReference(Column column)
    {
        Column = column;
    }

    public void AssignBlockReference(Block block)
    {
        Block = block;
    }

    public static List<Cell> GetCellsWithCandidate(List<Cell> cells, int candidateNumber)
    {
        List<Cell> cellsWithCandidate = new();

        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                if (cell.HasCandidate(candidateNumber))
                {
                    cellsWithCandidate.Add(cell);
                }
            }
        }

        return cellsWithCandidate;
    }

    public static Row? GetCommonRow(List<Cell> cellsWithCandidate)
    {
        int rowId = 0;

        foreach (var cell in cellsWithCandidate)
        {
            if (rowId == 0)
            {
                rowId = cell.Row.Id;
            }
            else if (rowId != cell.Row.Id)
            {
                return null;
            }
        }

        return cellsWithCandidate[0].Row;
    }

    public static Column? GetCommonColumn(List<Cell> cellsWithCandidate)
    {
        int columnId = 0;

        foreach (var cell in cellsWithCandidate)
        {
            if (columnId == 0)
            {
                columnId = cell.Column.Id;
            }
            else if (columnId != cell.Column.Id)
            {
                return null;
            }
        }

        return cellsWithCandidate[0].Column;
    }

    public static List<Cell> GetCellsWithoutExceptions(List<Cell> cells, List<Cell> cellsWithCandidate)
    {
        List<Cell> result = new();

        foreach (var cellOne in cells)
        {
            if (cellOne is not null)
            {
                bool isException = false;

                foreach (var cellTwo in cellsWithCandidate)
                {
                    if (cellTwo is not null)
                    {
                        if (cellOne.Id == cellTwo.Id)
                        {
                            isException = true;
                            break;
                        }
                    }
                }

                if (!isException)
                {
                    result.Add(cellOne);
                }
            }
        }

        return result;
    }

    public static void EliminateCandidateFromCells(int candidateNumber, List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                cell.EliminateCandidate(candidateNumber);
            }
        }
    }

    public static List<int> GetCandidates(List<Cell> cells)
    {
        int[] tempArray = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // eliminate candidates based on cell values from tempArray
        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                tempArray[cell.Values[0]] = 0;
            }
        }

        // only the remaining candidates, minus all zeros, are returned
        List<int> result = GetCandidates(tempArray);

        return result;
    }

    private static List<int> GetCandidates(int[] values)
    {
        List<int> result = new();

        for (int i = 0; i < values.Count(); i++)
        {
            if (values[i] != 0)
            {
                result.Add(values[i]);
            }
        }

        return result;
    }

    public void AssignPuzzleReference(Puzzle puzzle)
    {
        Puzzle = puzzle;
    }

    private int GetPositionInBlock()
    {
        int rowMod = (RowId - 1) % 3;
        int colMod = (ColumnId - 1) % 3;
        return (rowMod * 3) + colMod + 1;
    }

    public void RemoveExpectedValueIfNotACandidate()
    {
        bool isCandidate = GetIsCandidate(Values[0]);

        if (!isCandidate)
        {
            Values[0] = NON_POSSIBILITY_PLACEHOLDER_VALUE;
        }
    }

    private bool GetIsCandidate(int value)
    {
        foreach (var candidate in Candidates)
        {
            if (value == candidate)
            {
                return true;
            }
        }

        return false;
    }

    private List<int> GetCandidates()
    {
        SortedSet<int> sortedSet = new();

        for (int i = 1; i < Values.Count(); i++)
        {
            sortedSet.Add(Values[i]);
        }

        IEnumerable<int> iEnum = sortedSet;
        List<int> result = iEnum.ToList();
        if (result[0] == 0) result.RemoveAt(0);

        return result;
    }

    public void Backtrack()
    {
        // empty tried values list
        TriedValuesAsCurrentCell = new List<int>();
    }

    public void AddCurrentValueToTriedCandidateList()
    {
        CandidatesTried.Add(Values[0]);
    }

    public List<int> GetRemainingCandidatesToTry()
    {
        List<int> remainingCandidates = new();

        foreach (var triedCandidate in CandidatesTried)
        {
            foreach (var candidate in Candidates)
            {
                if (triedCandidate != candidate)
                {
                    remainingCandidates.Add(candidate);
                }
            }
        }

        return remainingCandidates;
    }
    public void AddTriedCandidate(int candidate)
    {
        TriedCandidates.Add(candidate);
    }

    public int GetNextCandidate()
    {
        foreach (var val in Candidates)
        {
            if (!TriedCandidates.Contains(val))
            {
                return val;
            }
        }
        return NON_POSSIBILITY_PLACEHOLDER_VALUE;
    }

    public void ResetCandidateTracking()
    {
        TriedCandidates = new List<int>();
    }

    public void RehydrateCandidates()
    {
        for (int i = 1; i < Values.Count(); i++)
        {
            Values[i] = i;
        }
    }

    public void ResetValue()
    {
        Values[0] = NON_POSSIBILITY_PLACEHOLDER_VALUE;
    }
}
