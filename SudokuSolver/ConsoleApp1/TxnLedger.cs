namespace SudokuSolver;

public class TxnLedger
{
	public TxnLedger()
    {
        Txns = new();
    }

    public List<Txn> Txns { get; private set; }

    public void RecordNewTxn(int cellId, int indexOfValue, int previousValue, int newValue)
    {
        int txnId = Txns.Count + 1;
        Txn newTxn = new(txnId, cellId, indexOfValue, previousValue, newValue);
        Txns.Add(newTxn);
    }
}
