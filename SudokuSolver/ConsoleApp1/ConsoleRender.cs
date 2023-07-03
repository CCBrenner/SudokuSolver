﻿namespace SudokuSolver;

public class ConsoleRender
{
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
    public static string RenderMatrix(Puzzle puzzle)
    {
        string matrixRenderString = GetMatrixRenderString(puzzle);
        Console.Write(matrixRenderString);
        return matrixRenderString;
    }
    private static string GetMatrixRenderString(Puzzle puzzle)
    {
        string matrixRender = string.Empty;

        for (int i = 0; i < 9; i++)
        {
            matrixRender += $"[ {puzzle.Matrix[i, 0].Value} ][ {puzzle.Matrix[i, 1].Value} ][ {puzzle.Matrix[i, 2].Value} ]  " +
                            $"[ {puzzle.Matrix[i, 3].Value} ][ {puzzle.Matrix[i, 4].Value} ][ {puzzle.Matrix[i, 5].Value} ]  " +
                            $"[ {puzzle.Matrix[i, 6].Value} ][ {puzzle.Matrix[i, 7].Value} ][ {puzzle.Matrix[i, 8].Value} ]\n";
            if (i % 3 == 2 && i != 8)
                matrixRender += "\n";
        }

        return matrixRender;
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
}
