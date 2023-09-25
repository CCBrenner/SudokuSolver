namespace SudokuSolver;

public class TxnLedger
{
	public TxnLedger()
    {
        Txns = new();
        ValueTxns = new();
        CandidateTxns = new();
    }

    public List<Txn> Txns { get; private set; }
    public List<Txn> ValueTxns { get; private set; }
    public List<Txn> CandidateTxns { get; private set; }
    public Puzzle Puzzle { get; private set; }

    public void RecordNewTxn(int cellId, int indexOfValue, int previousValue, int newValue)
    {
        int txnId = Txns.Count + 1;
        Txn newTxn = new(txnId, cellId, indexOfValue, previousValue, newValue);

        Txns.Add(newTxn);

        if (indexOfValue == 0)
            ValueTxns.Add(newTxn);
        else
            CandidateTxns.Add(newTxn);
    }
    public void StepThroughTxnsByValue()
    {
        int intervalInMilliseconds = 1000;

        // Create Matrix of cells through process of superimposition; cells are created here
        Cell[,] matrix = MatrixFactory.CreateMatrix(Puzzle.StartingIntMatrix);
        Cell[,] compositionMatrix = Puzzle.SuperimposeNonGivenCellValues(matrix, Puzzle.StartingIntMatrixToSuperimpose);

        // Create starter list of references to cells of previously created matrix
        List<Cell> cells = Cell.CreateListFromCellReferencesOfMatrix(compositionMatrix);

        StepThroughTxns stepper = new(cells, this);
        stepper.StepForwardAutomaticallyOnInterval(intervalInMilliseconds);
        // Reconstructing using txns
        // Txns used in order forward and backward give the state of the sudoku puzzle at that time of assignment
        // Animate data changes on an interval
        // Every step needs to be correct


        // Then allow user to change speed of animation
        // Then allow user to control steps
        // Then allow user to go backward
        // Then allow user to skip to exact txn
    }
    /*
    private static void StepForwardByValue(ref int txn, ref List<Cell> cells)
    {
        // Take txn data and update cells to the next matrix state
        txn++;
        Cell cell = cells.FirstOrDefault(x => x.Id == txn.CellId);
    }

    private int GetValueTxnCount(List<Txn> txns)
    {
        int count = 0;
        for (int i = 0; i < Txns.Count; i++)
        {
            if (Txns[i].IndexOfValue == 0)
            {
                count++;
            }
        }
        return count;
    }
    */
    public void AssignPuzzleReference(Puzzle puzzle)
    {
        Puzzle = puzzle;
    }
}
