namespace SudokuSolverV2.Solver;

public partial class Solver
{
    private Solver(int[,] puzzleValues)
    {
        PuzzleValues = puzzleValues;
    }
    public int[,] PuzzleValues { get; set; }  // 1-9 = Expected; 11-19 = Given; 21-29 = Confirmed
    public SortedSet<int>[,] PotentialValues { get; set; }  // Candidates
    public SortedSet<int>[,] TriedValues {  get; set; }  // Tried Candidates
    public int[] CurrentCellCoord { get; set; }  // Length == 2 (at all times; x & y)
    
    public static Solver Create(int[,] puzzleValues) => new(puzzleValues);
    public int[,] GeneratePuzzleValuesWithGivenCellValues(int[,] givenValuesMatrix)
    {
        foreach(int i in Enumerable.Range(1, 9))
        {
            foreach (int j in Enumerable.Range(1, 9))
            {
                if (givenValuesMatrix[i,j] != 0)
                {
                    givenValuesMatrix[i, j] += 10;  // Make it "Given"
                }
            }
        }
        return givenValuesMatrix;
    }
    public SortedSet<int> GetUntriedPotentialValues(
        SortedSet<int>[,] triedValues, 
        SortedSet<int>[,] potentialValues, 
        int[] currentCellCoord)
    {
        SortedSet<int> untriedValues = new();
        foreach (int value in potentialValues[currentCellCoord[0], currentCellCoord[1]])
        {
            if (!triedValues[currentCellCoord[0], currentCellCoord[1]].Contains(value))
            {
                untriedValues.Add(value);
            }
        }
        return untriedValues;
    }
    public ECellStatus GetCellStatus(int[,] puzzleValues, int[] currentCellCoord)
    {
        int currentCellValue = puzzleValues[currentCellCoord[0], currentCellCoord[1]];
        int modulo = currentCellValue % 10;
        currentCellValue -= modulo;
        if (currentCellValue > 20 || modulo == 0) return ECellStatus.Null;
        else return (ECellStatus)currentCellValue;
    }
    public int[,] AssignCellValueWithCorrectStatus(int[,] puzzleValues, ECellStatus cellStatus, int[] currentCellCoord)
    {
        int cellValue = puzzleValues[currentCellCoord[0], currentCellCoord[1]];
        while (cellValue > 9)
        {
            cellValue -= 10;
        }
        if (cellStatus == ECellStatus.Given)
        {
            puzzleValues[currentCellCoord[0], currentCellCoord[1]] = cellValue + 10;
        }
        else if (cellStatus == ECellStatus.Confirmed)
        {
            puzzleValues[currentCellCoord[0], currentCellCoord[1]] = cellValue + 20;
        }
        else if (cellStatus == ECellStatus.Null)
        {
            puzzleValues[currentCellCoord[0], currentCellCoord[1]] = 0;
        }
        return puzzleValues;
    }
    public int[] GetCoordOfNextCell(int[] currentCellCoord)
    {
        if (currentCellCoord[1] == 9)
        {
            currentCellCoord = new int[] { currentCellCoord[0]++, 1 };
        }
        else
        {
            currentCellCoord[1]++;
        }
        return currentCellCoord;
    }
    public int[] GetCoordOfPreviousCell(int[] currentCellCoord)
    {
        if (currentCellCoord[1] == 1)
        {
            currentCellCoord = new int[] { currentCellCoord[0]--, 9 };
        }
        else
        {
            currentCellCoord[1]--;
        }
        return currentCellCoord;
    }
    // Reset TriedValues to empty (before going to previous cell)
    public SortedSet<int>[,] EmptyTriedValuesAtCurrentCellCoord(SortedSet<int>[,] triedValues, int[] currentCellCoords)
    {
        triedValues[currentCellCoords[0], currentCellCoords[1]].Clear();
        return triedValues;
    }
    public SortedSet<int>[,] UpdatePotentialValues(SortedSet<int>[,] potentialValues)
    {
        // Rehydrate
        // SortedSet<int>[,] result = RemovePotentialValues(potentialValues);
        throw new NotImplementedException();
    }
    public SortedSet<int>[,] RemovePotentialValues(int[,] puzzleValues, SortedSet<int>[,] potentialValues)
    {
        potentialValues = RemovePotentialValuesByDistinctInRow(puzzleValues, potentialValues);
        potentialValues = RemovePotentialValuesByDistinctInColumn(puzzleValues, potentialValues);
        potentialValues = RemovePotentialValuesByDistinctInBlock(puzzleValues, potentialValues);
        return potentialValues;
    }

    private SortedSet<int>[,] RemovePotentialValuesByDistinctInRow(int[,] puzzleValues, SortedSet<int>[,] potentialValues)
    {
        //Rows:
        foreach (int i in Enumerable.Range(1, 9))  // All Rows
        {
            SortedSet<int> potentialValuesToRemove = new();
            // Get:
            foreach (int j in Enumerable.Range(1, 9))  // All Cells in a Row
            {
                if (puzzleValues[i, j] != 0)
                {
                    potentialValuesToRemove.Add(puzzleValues[i, j]);
                }
            }
            potentialValuesToRemove.Remove(0);
            // Remove:
            foreach (int j in Enumerable.Range(1, 9))
            {
                foreach (var value in potentialValuesToRemove)
                {
                    potentialValues[i, j].Remove(value);
                }
            }
        }
        return potentialValues;
    }
    private SortedSet<int>[,] RemovePotentialValuesByDistinctInColumn(int[,] puzzleValues, SortedSet<int>[,] potentialValues)
    {
        foreach (int i in Enumerable.Range(1, 9))  // All Columns
        {
            SortedSet<int> potentialValuesToRemove = new();
            // Get:
            foreach (int j in Enumerable.Range(1, 9))  // All Cells in a Column
            {
                if (puzzleValues[j, i] != 0)
                {
                    potentialValuesToRemove.Add(puzzleValues[i, j]);
                }
            }
            potentialValuesToRemove.Remove(0);
            // Remove:
            foreach (int j in Enumerable.Range(1, 9))
            {
                foreach (var value in potentialValuesToRemove)
                {
                    potentialValues[j, i].Remove(value);
                }
            }
        }
        return potentialValues;
    }
    private SortedSet<int>[,] RemovePotentialValuesByDistinctInBlock(int[,] puzzleValues, SortedSet<int>[,] potentialValues)
    {
        //for (int xMod=0; xMod<7; xMod+=3)  // puzzle
        foreach (int a in Enumerable.Range(0, 2))  // puzzle
        {
            int xMod = a * 3;
            foreach (int b in Enumerable.Range(0, 2))  // block row
            {
                int yMod = b * 3;
                // Get:
                SortedSet<int> potentialValuesToRemove = new();
                foreach (int x in Enumerable.Range(1, 3))  // block
                {
                    foreach (int y in Enumerable.Range(1, 3))  // All Cells in a Block
                    {
                        if (puzzleValues[x+xMod, y+yMod] != 0)
                        {
                            potentialValuesToRemove.Add(puzzleValues[a, b]);
                        }
                    }
                }
                potentialValuesToRemove.Remove(0);
                // Remove:
                foreach (int x in Enumerable.Range(1, 3))  // block
                {
                    foreach (int y in Enumerable.Range(1, 3))
                    {
                        foreach (var value in potentialValuesToRemove)
                        {
                            potentialValues[x+xMod, y+yMod].Remove(value);
                        }
                    }
                }
            }
        }
        return potentialValues;
    }
}
