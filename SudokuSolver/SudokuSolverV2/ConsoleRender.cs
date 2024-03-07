namespace SudokuSolverV2;

public class ConsoleRender
{
    public static void RenderPuzzleValues(int[,] puzzleValues)
    {
        string row = string.Empty;

        foreach (int i in Enumerable.Range(0, 8))
        {
            row = $"[ {puzzleValues[i, 0]} {puzzleValues[i, 1]} {puzzleValues[i, 2]} ]  " +
                  $"[ {puzzleValues[i, 3]} {puzzleValues[i, 4]} {puzzleValues[i, 5]} ]  " +
                  $"[ {puzzleValues[i, 6]} {puzzleValues[i, 7]} {puzzleValues[i, 8]} ]\n";
            Console.Write(row);

            if (i % 3 == 2)
                Console.WriteLine();
        }
    }
    public static void RenderPotentialValues(SortedSet<int>[,] potentialValues)
    {
        string renderString = string.Empty;
        int cellNum = 0;
        foreach (var sortedSet in potentialValues)
        {
            cellNum++;
            renderString += $"{cellNum}: ";
            foreach (int cell in sortedSet)
            {
                renderString += $"{cell}";
            }
            renderString += $"\n";
        }
        Console.WriteLine(renderString);
    }
    // Bring back once methods for getting PotentialCandidates by Row, Column, and Block exist.
    public static void RenderMatrixWithMetaData(int[,] puzzleVals, SortedSet<int>[,] potentialVals)
    {
        /*string row = $"            " +
            $"  {FormatCandidates(puzzle.Columns[0].PotentialValues)}  {FormatCandidates(puzzle.Columns[1].PotentialValues)}  {FormatCandidates(puzzle.Columns[2].PotentialValues)}    " +
            $"    {FormatCandidates(puzzle.Columns[3].PotentialValues)}  {FormatCandidates(puzzle.Columns[4].PotentialValues)}  {FormatCandidates(puzzle.Columns[5].PotentialValues)}    " +
            $"    {FormatCandidates(puzzle.Columns[6].PotentialValues)}  {FormatCandidates(puzzle.Columns[7].PotentialValues)}  {FormatCandidates(puzzle.Rows[8].PotentialValues)}  \n";*/

        //Console.Write(row);
        string row;
        for (int i = 0; i < 9; i++)
        {
            //row = $"{FormatCandidates(puzzle.Rows[i].Candidates)}  " +
            row = $"[ {puzzleVals[i, 0]} {All(potentialVals[i, 0])} | {puzzleVals[i, 1]} {All(potentialVals[i, 1])} | {puzzleVals[i, 2]} {All(potentialVals[i, 2])} ]   " +
                $"[ {puzzleVals[i, 3]} {All(potentialVals[i, 3])} | {puzzleVals[i, 4]} {All(potentialVals[i, 4])} | {puzzleVals[i, 5]} {All(potentialVals[i, 5])} ]   " +
                $"[ {puzzleVals[i, 6]} {All(potentialVals[i, 6])} | {puzzleVals[i, 7]} {All(potentialVals[i, 7])} | {puzzleVals[i, 8]} {All(potentialVals[i, 8])} ]\n";
            Console.Write(row);
            
            if (i % 3 == 2)
            {
                Console.WriteLine();
                /*
                string tenSpaces = "          ";
                row = $"{tenSpaces}  " +
                    $"    {tenSpaces}{{{FormatCandidates(puzzle.Blocks[i - 2].Candidates)}}}{tenSpaces}" +
                    $"{tenSpaces}{tenSpaces}{{{FormatCandidates(puzzle.Blocks[i - 1].Candidates)}}}{tenSpaces}" +
                    $"{tenSpaces}{tenSpaces}{{{FormatCandidates(puzzle.Blocks[i].Candidates)}}}{tenSpaces}\n\n";
                Console.Write(row);
                */
            }
        }
    }

    private static string FormatCandidates(List<int> candidates)
    {
        int front = 0;
        int back = 0;

        int temp = 10 - candidates.Count;
        if (temp > 1)
        {
            int g = temp % 2;
            int p = temp - g;
            front = p / 2;
            back = front + g;
        }

        string result = string.Empty;

        for (int i = 0; i < front; i++)
        {
            result += " ";
        }

        result += string.Join("", candidates);


        for (int i = 0; i < back; i++)
        {
            result += " ";
        }

        return result;
    }
    private static string All(SortedSet<int> valsSet)
    {
        string returnStr = string.Empty;
        foreach (int val in valsSet)
        {
            returnStr += val;
        }
        while (returnStr.Length < 9)
        {
            returnStr += " ";
        }
        return returnStr;
    }
    /*
    public static string RenderCellInfo(Puzzle puzzle)
    {
        string cellInfo = string.Empty;

        foreach (var cell in puzzle.Cells)
        {
            string row = $"Cell: {{ Id:{cell.Id}, Row:{cell.Row}, Column:{cell.Column}, Block:{cell.Block}, BlockRow:{cell.BlockRow}, BlockColumn:{cell.BlockColumn}\n";
            cellInfo += row;
            Console.Write(row);
        }

        return cellInfo;
    }
    */
    /*
    public static void RenderTxns(TxnLedger ledger, int startingIndex = 1, int finishingIndex = 1000000000)
    {
        int count = 0;
        for (int i = startingIndex; i <= finishingIndex; i++)
        {
            RenderTxn(ledger, ledger.Txns[i - 1].Id);
            count++;
        }
        Console.WriteLine($"Total Results: {count}");
    }
    public static void RenderTxn(TxnLedger ledger, int txnId)
    {
        int txnIndex = txnId - 1;
        Console.WriteLine($"Txn# {ledger.Txns[txnIndex].Id} | CellId {ledger.Txns[txnIndex].CellId} | IoV {ledger.Txns[txnIndex].IndexOfValue} | Prev {ledger.Txns[txnIndex].Previous} | New {ledger.Txns[txnIndex].New}");
    }
    public static void RenderValueTxns(TxnLedger ledger, int startingIndex = 1, int finishingIndex = 1000000000)
    {
        int end = ledger.Txns.Count < finishingIndex ? ledger.Txns.Count + 1 : finishingIndex;
        int count = 0;
        for (int i = startingIndex; i < end; i++)
        {
            if (ledger.Txns[i - 1].IndexOfValue == 0)
            {
                RenderTxn(ledger, ledger.Txns[i - 1].Id);
                count++;
            }
        }
        Console.WriteLine($"Total Results: {count}");
    }

    internal static void RenderStandardTxnInfo(TxnLedger ledger, ISolver solver)
    {
        // Print the transactions
        Console.WriteLine($"Number of Txns in Ledger: {ledger.Txns.Count:N0}");
        Console.WriteLine($"Number of ValueTxns: {ledger.ValueTxns.Count:N0}");
        Console.WriteLine($"Number of CandidateTxns: {ledger.CandidateTxns.Count:N0}\n");

        // Print proportions
        decimal valueTxnsCount = ledger.ValueTxns.Count;
        decimal txnsCount = ledger.Txns.Count;
        Console.WriteLine($"ValueTxns % of Txns: {valueTxnsCount / txnsCount:P}\n");

        // Print rates
        Console.WriteLine($"Txns/second: {ledger.Txns.Count / solver.StopwatchTime:N1}");
        Console.WriteLine($"ValueTxns/second: {ledger.ValueTxns.Count / solver.StopwatchTime:N1}");
        Console.WriteLine($"CandidateTxns/second: {ledger.CandidateTxns.Count / solver.StopwatchTime:N1}\n");
    }
    */
}

