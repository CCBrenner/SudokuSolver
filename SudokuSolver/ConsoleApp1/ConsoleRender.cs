using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SudokuSolver;

public class ConsoleRender
{
    public static void RenderMatrix(Puzzle puzzle)
    {
        string matrixRender = string.Empty;

        for (int i = 0; i < 9; i++)
        {
            matrixRender += $"[ {ValueRender(puzzle, i, 0)} ][ {ValueRender(puzzle, i, 1)} ][ {ValueRender(puzzle, i, 2)} ]  " +
                            $"[ {ValueRender(puzzle, i, 3)} ][ {ValueRender(puzzle, i, 4)} ][ {ValueRender(puzzle, i, 5)} ]  " +
                            $"[ {ValueRender(puzzle, i, 6)} ][ {ValueRender(puzzle, i, 7)} ][ {ValueRender(puzzle, i, 8)} ]\n";
            if (i % 3 == 2 && i != 8)
                matrixRender += "\n";
        }

        Console.Write(matrixRender);
    }
    private static string ValueRender(Puzzle puzzle, int rowId, int colId)
    {
        int value = puzzle.Matrix[rowId, colId].Values[0];
        return value == 0 ? " " : value.ToString();
    }
    public static void RenderMatrixCellValuesV1(Puzzle puzzle)
    {
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                Console.WriteLine();
            }
            string row = $"[   {puzzle.Matrix[i, 0].GetAllValues()} {puzzle.Matrix[i, 1].GetAllValues()} {puzzle.Matrix[i, 2].GetAllValues()} ] [ " +
                         $"  {puzzle.Matrix[i, 3].GetAllValues()} {puzzle.Matrix[i, 4].GetAllValues()} {puzzle.Matrix[i, 5].GetAllValues()} ] [ " +
                         $"  {puzzle.Matrix[i, 6].GetAllValues()} {puzzle.Matrix[i, 7].GetAllValues()} {puzzle.Matrix[i, 8].GetAllValues()} ]\n";
            Console.Write(row);
        }
    }
    public static void RenderMatrixCellValuesV2(Puzzle puzzle)
    {
        string row = $"            " +
            $"  {FormatCandidates(puzzle.Columns[0].Candidates)}  {FormatCandidates(puzzle.Columns[1].Candidates)}  {FormatCandidates(puzzle.Columns[2].Candidates)}    " +
            $"    {FormatCandidates(puzzle.Columns[3].Candidates)}  {FormatCandidates(puzzle.Columns[4].Candidates)}  {FormatCandidates(puzzle.Columns[5].Candidates)}    " +
            $"    {FormatCandidates(puzzle.Columns[6].Candidates)}  {FormatCandidates(puzzle.Columns[7].Candidates)}  {FormatCandidates(puzzle.Rows[8].Candidates)}  \n";

        Console.Write(row);

        for (int i = 0; i < 9; i++)
        {
            row = $"{FormatCandidates(puzzle.Rows[i].Candidates)}  " +
                         $"[   {puzzle.Matrix[i, 0].GetAllValues()} {puzzle.Matrix[i, 1].GetAllValues()} {puzzle.Matrix[i, 2].GetAllValues()} ] [ " +
                         $"  {puzzle.Matrix[i, 3].GetAllValues()} {puzzle.Matrix[i, 4].GetAllValues()} {puzzle.Matrix[i, 5].GetAllValues()} ] [ " +
                         $"  {puzzle.Matrix[i, 6].GetAllValues()} {puzzle.Matrix[i, 7].GetAllValues()} {puzzle.Matrix[i, 8].GetAllValues()} ]\n";
            Console.Write(row);

            if (i % 3 == 2)
            {
                string tenSpaces = "          ";
                row = $"{tenSpaces}  " +
                    $"    {tenSpaces}{{{FormatCandidates(puzzle.Blocks[i-2].Candidates)}}}{tenSpaces}" +
                    $"{tenSpaces}{tenSpaces}{{{FormatCandidates(puzzle.Blocks[i-1].Candidates)}}}{tenSpaces}" +
                    $"{tenSpaces}{tenSpaces}{{{FormatCandidates(puzzle.Blocks[i].Candidates)}}}{tenSpaces}\n\n";
                Console.Write(row);
            }
        }
    }
    public static void RenderMatrixCellValuesV3(Puzzle puzzle)
    {
        string row = string.Empty;

        for (int i = 0; i < 9; i++)
        {
            row = $"[ {puzzle.Matrix[i, 0].Values[0]} {puzzle.Matrix[i, 1].Values[0]} {puzzle.Matrix[i, 2].Values[0]} ]  " +
                  $"[ {puzzle.Matrix[i, 3].Values[0]} {puzzle.Matrix[i, 4].Values[0]} {puzzle.Matrix[i, 5].Values[0]} ]  " +
                  $"[ {puzzle.Matrix[i, 6].Values[0]} {puzzle.Matrix[i, 7].Values[0]} {puzzle.Matrix[i, 8].Values[0]} ]\n";
            Console.Write(row);

            if (i % 3 == 2)
                Console.WriteLine();
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
}
