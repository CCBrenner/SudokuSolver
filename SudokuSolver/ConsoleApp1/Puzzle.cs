using System.Diagnostics.Metrics;

namespace SudokuSolver;

public class Puzzle
{
    public Puzzle(Cell[,] puzzleMatrix)
    {
        Matrix = puzzleMatrix;
    }

    public Cell[,] Matrix { get; private set; }
    public static Cell[,] CreateMatrix(int[,] puzzleData)
    {
        Cell[,] matrix = new Cell[9, 9];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                matrix[i, j] = new Cell(i, j, puzzleData[i,j]);
            }
        }

        return matrix;
    }
    public static Puzzle Create(Cell[,] startingMatrix, int[,] matrixToSuperimpose)
    {
        // assign values to non-given positions without given flag marked
        Cell[,] compositionMatrix = SuperimposeNonGivenCellValues(startingMatrix, matrixToSuperimpose);

        // inject into newly create puzzle
        Puzzle puzzle = new(compositionMatrix);

        // return puzzle
        return puzzle;
    }

    public void ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell()
    {
        for (int i = 0; i < 9; i++)
        {
            EliminateRowPossibilities(i);
        }
        for (int i = 0; i < 9; i++)
        {
            EliminateColumnPossibilities(i);
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int[] squareIndex = new int[2] { i, j };
                EliminateSquarePossibilities(squareIndex);
            }
        }
    }

    public void EliminateRowPossibilities(int rowIndex)
    {
        // Get Given, and Confirmed numbers of row
        SortedSet<int> possibilitiesToEliminate = GetRowPossibilitiesToEliminate(rowIndex);

        // Eliminate possibilities of cells in the row
        EliminatePossibilitiesFromCellsOfRow(possibilitiesToEliminate, rowIndex);

        List<Cell> cellsToCheckForCellChain = GetCellsWithMoreThanOneNotedPossibilityInRow(rowIndex);

        EliminatePossibilitiesBasedOnCellChainsPresentInRow(cellsToCheckForCellChain);
    }

    private void EliminatePossibilitiesBasedOnCellChainsPresentInRow(List<Cell> cellsToCheckForCellChain)
    {
        if (cellsToCheckForCellChain.Count > 1)
        {
            // First check (2 cells share the same 2 noted possibilities, making them a cell chain of 2 cells):
            EliminatePossibilitiesInRowBasedOnTwoCellCellChain(cellsToCheckForCellChain);

            // Second check (3 cells in a chain, one leads into the next completing a circular cell chain):
            EliminatePossibilitiesInRowBasedOnThreeCellCellChain(cellsToCheckForCellChain);
        }
    }

    private void EliminatePossibilitiesInRowBasedOnThreeCellCellChain(List<Cell> cellsToCheckForCellChain)
    {



        /*
        // Get all pairs
        List<NotedPossibilityTypeTwo> typeTwoNotedPossibilityPairs = GetTypeTwoNotedPossibilityPairs(cellsToCheckForCellChain);

        // Search for cells with pairs that contain only one of the numbers of its pair, first the [0] then the [1] (by cell, by notedPossibility, then find cells with that possibilty
        foreach (var lvlOnePair in typeTwoNotedPossibilityPairs)
        {
            foreach (var lvlTwoPair in typeTwoNotedPossibilityPairs)
            {
                // Fours checks: 2^2 = 4
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (lvlOnePair.Pair[i] == lvlTwoPair.Pair[j])
                        {
                            // We've found the first match,
                            // now we search for the second match which overlaps with lvlOne and lvlTwo pairs on their notMatched values:
                            int notMatchedLvlOneValue = i == 0 ? 1 : 0;
                            int notMatchedLvlTwoValue = j == 0 ? 1 : 0;

                            int lvlOneMatchTargetValue = lvlOnePair.Pair[notMatchedLvlOneValue];
                            int lvlTwoMatchTargetValue = lvlTwoPair.Pair[notMatchedLvlTwoValue];

                            int[] targetPair = new int[2] { lvlOneMatchTargetValue, lvlTwoMatchTargetValue };

                            foreach (var lvlThreePair in typeTwoNotedPossibilityPairs)
                            {
                                if (lvlThreePair.Pair == targetPair)
                                {
                                    // We've found a 3-cell cell chain; there should only be one and we are safe to assume as such
                                    lvlOnePair.EliminateAllOtherPossibilitiesExceptForCellChainPossibilties();
                                    lvlTwoPair.EliminateAllOtherPossibilitiesExceptForCellChainPossibilties();
                                    lvlThreePair.EliminateAllOtherPossibilitiesExceptForCellChainPossibilties();

                                    // Eliminate cell chain pair values as possibilities from all other cells in the row
                                    List<int> possibilitiesToEliminate = new() { lvlOnePair.Pair[i], lvlThreePair.Pair[0], lvlThreePair.Pair[1] };
                                    foreach (var pair in typeTwoNotedPossibilityPairs)
                                        if (pair.Cell != lvlOnePair.Cell && pair.Cell != lvlTwoPair.Cell && pair.Cell != lvlThreePair.Cell)
                                            for (int k = 0; k < 3; k++)
                                                pair.EliminatePossibility(possibilitiesToEliminate[k]);
                                }
                            }
                        }
                    }
                }
            }
        }
        */
    }

    private void EliminatePossibilitiesInRowBasedOnTwoCellCellChain(List<Cell> cellsToCheckForCellChain)
    {



        /*
        List<NotedPossibilityTypeTwo> typeTwoNotedPossibilityPairs = GetTypeTwoNotedPossibilityPairs(cellsToCheckForCellChain);

        for (int i = 0; i < typeTwoNotedPossibilityPairs.Count; i++)
        {
            for (int j = 0; j < typeTwoNotedPossibilityPairs.Count; j++)
            {
                if (i != j && typeTwoNotedPossibilityPairs[i].Pair == typeTwoNotedPossibilityPairs[j].Pair)
                {
                    // 2-cell cell chain discovered; harden/eliminate possibilities for both sides of cell chain barrier within row scope:

                    // Eliminate all other possibilities from contained cells:
                    typeTwoNotedPossibilityPairs[i].EliminateAllOtherPossibilitiesExceptForCellChainPossibilties();
                    typeTwoNotedPossibilityPairs[j].EliminateAllOtherPossibilitiesExceptForCellChainPossibilties();

                    // Eliminate cell chain values as possibilities from other missing values
                    for (int k = 0; k < 9; k++)
                    {
                        if (k != i && k != j)
                        {
                            typeTwoNotedPossibilityPairs[k].EliminatePossibility(i);
                            typeTwoNotedPossibilityPairs[k].EliminatePossibility(j);
                        }
                    }
                    /* come back to and apply square scope 
                    // Check if cell chain also falls in square scope and handle accordingly:
                    bool cellChainAppliesToScopeOfASquare = CellChainAppliesToScopeOfASquare(notedPossibilityPairs[i].Cell, notedPossibilityPairs[j].Cell);
                    if (cellChainAppliesToScopeOfASquare)
                    {
                        // come back to and apply square scope 
                    }
                }
            }
        }
        */
    }
    /*
    private bool CellChainAppliesToScopeOfASquare(Cell cellOne, Cell cellTwo)
    {
        int cellOneRow = cellOne.Row;
        int cellTwoRow = cellTwo.Row;
        int cellOneSquareRow = ((cellOneRow - (cellOneRow % 3)) / 3);  // gives square row coord between 0 and 2 inclusive
        int cellTwoSquareRow = ((cellTwoRow - (cellTwoRow % 3)) / 3);  // gives square row coord between 0 and 2 inclusive

        int cellOneColumn = cellOne.Column;
        int cellTwoColumn = cellTwo.Column;
        int cellOneSquareColumn = ((cellOneColumn - (cellOneColumn % 3)) / 3);  // gives square row coord between 0 and 2 inclusive
        int cellTwoSquareColumn = ((cellTwoColumn - (cellTwoColumn % 3)) / 3);  // gives square row coord between 0 and 2 inclusive

        bool squareRowsMatch = cellOneSquareRow == cellTwoSquareRow;
        bool squareColumnsMatch = cellOneSquareColumn == cellTwoSquareColumn;

        bool cellChainAppliesToScopeOfASquare = squareRowsMatch && squareColumnsMatch;
        return cellChainAppliesToScopeOfASquare;
    }
    */
    /*
    private List<NotedPossibilityTypeTwo> GetTypeTwoNotedPossibilityPairs(List<Cell> cellsToCheckForCellChain)
    {
        List<NotedPossibilityTypeTwo> typeTwoNotedPossibilityPairs = new();

        foreach (var cell in cellsToCheckForCellChain)
            foreach (var pair in cell.GetTypeTwoNotedPossibilities())
                typeTwoNotedPossibilityPairs.Add(pair);

        return typeTwoNotedPossibilityPairs;
    }
    */
    private List<Cell> GetCellsWithMoreThanOneNotedPossibilityInRow(int rowIndex)
    {
        List<Cell> cellsToReturn = new();

        for (int i = 0; i < 9; i++)
        {
            Cell cell = Matrix[rowIndex, i];
            if (cell.NotedPossibilities.Count > 1)
            {
                cellsToReturn.Add(cell);
            }
        }

        return new List<Cell>();
    }

    private List<Cell> GetCellsWithMoreThanOneNotedPossibilityInColumn(int columnIndex)
    {
        List<Cell> cellsToReturn = new();

        for (int i = 0; i < 9; i++)
        {
            Cell cell = Matrix[i, columnIndex];
            if (cell.NotedPossibilities.Count > 1)
            {
                cellsToReturn.Add(cell);
            }
        }

        return new List<Cell>();
    }

    public void EliminateColumnPossibilities(int columnIndex)
    {
        // Get Given, and Confirmed numbers of column
        SortedSet<int> possibilitiesToEliminate = GetColumnPossibilitiesToEliminate(columnIndex);

        // Eliminate possibilities of cells in the column
        EliminatePossibilitiesFromCellsOfColumn(possibilitiesToEliminate, columnIndex);

        List<Cell> cellsToCheckForCellChain = GetCellsWithMoreThanOneNotedPossibilityInColumn(columnIndex);

        //EliminatePossibilitiesBasedOnCellChainsPresentInColumn(cellsToCheckForCellChain);
    }

    public SortedSet<int> EliminateSquarePossibilities(int[] squareIndex)
    {
        // Get Given, and Confirmed numbers of column
        SortedSet<int> possibilitiesToEliminate = GetSquarePossibilitiesToEliminate(squareIndex);

        // Eliminate possibilities of cells in the column
        SortedSet<int> eliminatedPossibilities = EliminatePossibilitiesFromCellsOfSquare(possibilitiesToEliminate, squareIndex);

        // Return eliminated possibilities
        return eliminatedPossibilities;
    }
    public void RemoveImpossibleValues()
    {
        foreach (var cell in Matrix)
        {
            cell.ReconcileValueWithPossibilities();
        }
    }
    public void CheckAndUpdateValueOfEachCellIfOnePossibilityRemaining()
    {
        foreach (var cell in Matrix)
        {
            cell.CheckAndUpdateValueIfOnePossibilityRemaining();
        }
    }

    private SortedSet<int> EliminatePossibilitiesFromCellsOfSquare(SortedSet<int> possibilitiesToEliminate, int[] squareIndex)
    {
        Cell[] cellsOfColumn = GetCellsOfSquare(squareIndex);
        SortedSet<int> eliminatedPossibilities = new();

        foreach (var possibility in possibilitiesToEliminate)
        {
            foreach (var cell in cellsOfColumn)
            {
                cell.EliminatePossibility(possibility);
            }
            eliminatedPossibilities.Add(possibility);
        }

        return eliminatedPossibilities;
    }

    private Cell[] GetCellsOfSquare(int[] squareIndex)
    {
        Cell[] cellsOfSquare = new Cell[9];

        int iCoefficient = squareIndex[0] * 3;
        int jCoefficient = squareIndex[1] * 3;

        int counter = 0;

        for (int i = iCoefficient; i < (iCoefficient + 3); i++)
        {
            for (int j = jCoefficient; j < (jCoefficient + 3); j++)
            {
                cellsOfSquare[counter] = Matrix[i, j];
                counter++;
            }
        }

        return cellsOfSquare;
    }

    private SortedSet<int> GetSquarePossibilitiesToEliminate(int[] squareIndex)
    {
        // Go through each cell in the column & if they have any of the flags marked as true, add them to the SortedSet.
        SortedSet<int> columnPossibilitiesToEliminate = new();

        int iCoefficient = squareIndex[0] * 3;
        int jCoefficient = squareIndex[1] * 3;

        for (int i = iCoefficient; i < (iCoefficient + 3); i++)
        {
            for (int j = jCoefficient; j < (jCoefficient + 3); j++)
            {
                if (Matrix[i, j].IsGivenValue || Matrix[i, j].IsConfirmedValue)
                {

                    int possiblityToEliminate = Matrix[i, j].Values[0];
                    columnPossibilitiesToEliminate.Add(possiblityToEliminate);
                }
            }
        }

        return columnPossibilitiesToEliminate;
    }

    private SortedSet<int> EliminatePossibilitiesFromCellsOfColumn(SortedSet<int> possibilitiesToEliminate, int columnIndex)
    {
        Cell[] cellsOfColumn = GetCellsOfColumn(columnIndex);
        SortedSet<int> eliminatedPossibilities = new();

        foreach (var possibility in possibilitiesToEliminate)
        {
            foreach (var cell in cellsOfColumn)
            {
                cell.EliminatePossibility(possibility);
            }
            eliminatedPossibilities.Add(possibility);
        }

        return eliminatedPossibilities;
    }

    private Cell[] GetCellsOfColumn(int columnIndex)
    {
        Cell[] cellsOfColumn = new Cell[9];

        for (int i = 0; i < 9; i++)
        {
            cellsOfColumn[i] = Matrix[i, columnIndex];
        }

        return cellsOfColumn;
    }

    private SortedSet<int> GetColumnPossibilitiesToEliminate(int columnIndex)
    {
        // Go through each cell in the column & if they have any of the flags marked as true, add them to the SortedSet.
        SortedSet<int> columnPossibilitiesToEliminate = new();

        for (int i = 0; i < 9; i++)
        {
            if (Matrix[i, columnIndex].IsGivenValue || Matrix[i, columnIndex].IsConfirmedValue)
            {
                int possiblityToEliminate = Matrix[i, columnIndex].Values[0];
                columnPossibilitiesToEliminate.Add(possiblityToEliminate);
            }
        }

        return columnPossibilitiesToEliminate;
    }

    private SortedSet<int> EliminatePossibilitiesFromCellsOfRow(SortedSet<int> possibilitiesToEliminate, int rowIndex)
    {
        Cell[] cellsOfRow = GetCellsOfRow(rowIndex);
        SortedSet<int> eliminatedPossibilities = new();

        foreach (var possibility in possibilitiesToEliminate)
        {
            foreach (var cell in cellsOfRow)
            {
                cell.EliminatePossibility(possibility);
            }
            eliminatedPossibilities.Add(possibility);
        }

        return eliminatedPossibilities;
    }

    private Cell[] GetCellsOfRow(int rowIndex)
    {
        Cell[] cellsOfRow = new Cell[9];

        for (int i = 0; i < 9; i++)
        {
            cellsOfRow[i] = Matrix[rowIndex, i];
        }
        
        return cellsOfRow;
    }
    private SortedSet<int> GetRowPossibilitiesToEliminate(int rowIndex)
    {
        // Go through each cell in the row & if they have any of the flags marked as true, add them to the SortedSet.
        SortedSet<int> rowPossibilitiesToEliminate = new();

        for (int i = 0; i < 9; i++)
        {
            if (Matrix[rowIndex, i].IsGivenValue || Matrix[rowIndex, i].IsConfirmedValue)
            {
                int possiblityToEliminate = Matrix[rowIndex, i].Values[0];
                rowPossibilitiesToEliminate.Add(possiblityToEliminate);
            }
        }

        return rowPossibilitiesToEliminate;
    }

    private static Cell[,] SuperimposeNonGivenCellValues(Cell[,] compositionMatrix, int[,] matrixToSuperimpose)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (!compositionMatrix[i, j].IsGivenValue)
                {
                    compositionMatrix[i, j].SetExpectedValue(matrixToSuperimpose[i, j]);
                }
            }
        }

        return compositionMatrix;
    }

    private void AddNotedPossibilityToCells(Cell cellOne, Cell cellTwo)
    {
        NotedPossibilityTypeOne newNotedPossibility = new(cellOne, cellTwo);
        cellOne.NotedPossibilities.Add(newNotedPossibility);
        cellTwo.NotedPossibilities.Add(newNotedPossibility);
    }
}
