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
}
