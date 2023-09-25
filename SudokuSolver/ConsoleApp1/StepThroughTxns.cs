using System.Timers;

namespace SudokuSolver;

public class StepThroughTxns
{
    public StepThroughTxns(List<Cell> cells, TxnLedger ledger)
    {
        Cells = cells;
        Ledger = ledger;
        CurrentTxn = 0;

        NextTxns = new();
        Cells.Reverse();
        foreach (var txn in Ledger.Txns)
        {
            NextTxns.Push(txn);
        }
        Cells.Reverse();

        PreviousTxns = new();
    }

    public System.Timers.Timer Timer { get; private set; }
    public TxnLedger Ledger { get; private set; }
    public List<Cell> Cells { get; private set; }
    public int CurrentTxn { get; private set; }
    public Stack<Txn> PreviousTxns { get; private set; }
    public Stack<Txn> NextTxns { get; private set; }

    public void StepForwardAutomaticallyOnInterval(int intervalInMilliSeconds)
    {
        Timer = new(intervalInMilliSeconds);
        Timer.Elapsed += TimerElapsedAutomaticForward;
        Timer.Start();
        // What happens to the main process while timer is going? What will render to the console?
    }

    private void TimerElapsedAutomaticForward(object? sender, ElapsedEventArgs e)
    {
        // Pop a cell off of NextTxns
        var txn = NextTxns.Pop();

        // Apply change of txn N to specified cell of txn
        int cellId = txn.CellId;
        var cell = Cells.FirstOrDefault(cell => cell.Id == cellId);

        if (cell is not null)
        {
            if (cell.Values[txn.IndexOfValue] == txn.Previous)
            {
                cell.SetExpectedValueAsPlayback(txn);
            }
            else
            {
                // Current value of indexed value or candidate does not equal txn value
                throw new Exception();
            }
        }
        CurrentTxn++;  // initially: 0 => 1

        // Push cell to PreviousTxns
        PreviousTxns.Push(txn);

        // Print the new puzzle
        ConsoleRender.RenderMatrix(Cells);

        // Handle once solver is done solving
        Timer.Stop();
    }
}
