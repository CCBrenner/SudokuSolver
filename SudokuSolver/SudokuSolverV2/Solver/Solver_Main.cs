namespace SudokuSolverV2.Solver;

public partial class Solver
{
    private Solver(int[,] puzzleValues)
    {
        PuzzleValues = puzzleValues;
        PotentialValues = HydratePotentialValues();
        TriedValues = new SortedSet<int>[,] { };
        CurrentCellCoord = new int[] { 1, 1 };
    }
    public int[,] PuzzleValues { get; set; }  // 1-9 = Expected; 11-19 = Given; 21-29 = Confirmed
    public SortedSet<int>[,] PotentialValues { get; set; }  // Candidates
    public SortedSet<int>[,] TriedValues {  get; set; }  // Tried Candidates
    public int[] CurrentCellCoord { get; set; }  // Length == 2 (at all times; rowNum & columnNum)
    
    public static Solver CreateWithoutValues() => new(PuzzleBook.GetPuzzle("allZeros"));
    public static Solver Create(int[,] puzzleValues) => new(puzzleValues);
    public void UpdatePuzzleValues(int[,] puzzleValues) => PuzzleValues = puzzleValues;
    public void UpdatePotentialValues()
    {
        // 1. Rehydrate PotentialValues
        // 2. Remove PotentialValues based on PuzzleValues
        throw new NotImplementedException();
    }
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
    public void UpdateCellValue(int newCellValue, int[] cellCoord)
    {
        PuzzleValues[cellCoord[0], cellCoord[1]] = newCellValue;
    }
    public void UpdateCurrentCellValue(int newCellValue)
    {
        PuzzleValues[CurrentCellCoord[0], CurrentCellCoord[1]] = newCellValue;
    }
    public void UpdateCellStatus(ECellStatus cellValueStatus)
    {
        int cellValue = PuzzleValues[CurrentCellCoord[0], CurrentCellCoord[1]];
        while (cellValue > 9)
        {
            cellValue -= 10;
        }
        if (cellValueStatus == ECellStatus.Given)
        {
            PuzzleValues[CurrentCellCoord[0], CurrentCellCoord[1]] = cellValue + 10;
        }
        else if (cellValueStatus == ECellStatus.Confirmed)
        {
            PuzzleValues[CurrentCellCoord[0], CurrentCellCoord[1]] = cellValue + 20;
        }
        else if (cellValueStatus == ECellStatus.Null)
        {
            PuzzleValues[CurrentCellCoord[0], CurrentCellCoord[1]] = 0;
        }
    }
    public void MoveCurrentCellCoordToNextCell()
    {
        if (CurrentCellCoord[1] == 9)
        {
            CurrentCellCoord = new int[] { CurrentCellCoord[0]++, 1 };
        }
        else
        {
            CurrentCellCoord[1]++;
        }
    }
    public void MoveCurrentCellCoordToPreviousCell()
    {
        if (CurrentCellCoord[1] == 1)
        {
            CurrentCellCoord = new int[] { CurrentCellCoord[0]--, 9 };
        }
        else
        {
            CurrentCellCoord[1]--;
        }
    }
    public void EmptyTriedValuesAtCurrentCellCoord()
    {
        TriedValues[CurrentCellCoord[0], CurrentCellCoord[1]].Clear();
    }
    public void RemovePotentialValues()
    {
        RemovePotentialValuesByDistinctInRow();
        RemovePotentialValuesByDistinctInColumn();
        RemovePotentialValuesByDistinctInBlock();
    }
    private void RemovePotentialValuesByDistinctInRow()
    {
        //Rows:
        foreach (int i in Enumerable.Range(1, 9))  // All Rows
        {
            SortedSet<int> potentialValuesToRemove = new();
            // Get:
            foreach (int j in Enumerable.Range(1, 9))  // All Cells in a Row
            {
                if (PuzzleValues[i, j] != 0)
                {
                    potentialValuesToRemove.Add(PuzzleValues[i, j]);
                }
            }
            potentialValuesToRemove.Remove(0);
            // Remove:
            foreach (int j in Enumerable.Range(1, 9))
            {
                foreach (var value in potentialValuesToRemove)
                {
                    PotentialValues[i, j].Remove(value);
                }
            }
        }
    }
    private void RemovePotentialValuesByDistinctInColumn()
    {
        foreach (int i in Enumerable.Range(1, 9))  // All Columns
        {
            SortedSet<int> potentialValuesToRemove = new();
            // Get:
            foreach (int j in Enumerable.Range(1, 9))  // All Cells in a Column
            {
                if (PuzzleValues[j, i] != 0)
                {
                    potentialValuesToRemove.Add(PuzzleValues[i, j]);
                }
            }
            potentialValuesToRemove.Remove(0);
            // Remove:
            foreach (int j in Enumerable.Range(1, 9))
            {
                foreach (var value in potentialValuesToRemove)
                {
                    PotentialValues[j, i].Remove(value);
                }
            }
        }
    }
    private void RemovePotentialValuesByDistinctInBlock()
    {
        //for (int xMod=0; xMod<7; xMod+=3)  // puzzle
        foreach (int a in Enumerable.Range(0, 2))  // puzzle
        {
            int xMod = a * 3;
            foreach (int b in Enumerable.Range(0, 2))  // block rowNum
            {
                int yMod = b * 3;
                // Get:
                SortedSet<int> potentialValuesToRemove = new();
                foreach (int x in Enumerable.Range(1, 3))  // block
                {
                    foreach (int y in Enumerable.Range(1, 3))  // All Cells in a Block
                    {
                        if (PuzzleValues[x+xMod, y+yMod] != 0)
                        {
                            potentialValuesToRemove.Add(PuzzleValues[a, b]);
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
                            PotentialValues[x+xMod, y+yMod].Remove(value);
                        }
                    }
                }
            }
        }
    }
    public static int[,] CreateSeedValuesMatrixBySuperimposition(int[,] originalMatrix, int[,] superimposeMatrix)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (originalMatrix[i, j] == 0)
                {
                    originalMatrix[i, j] = superimposeMatrix[i, j];
                }
            }

        }
        return originalMatrix;
    }
    public SortedSet<int> GetPotentialCandidatesByRow(int rowNum)
    {
        SortedSet<int> potentialValuesOfRow = new();
        foreach (int columnNum in Enumerable.Range(1, 9))
        {
            foreach(int potVal in PotentialValues[rowNum, columnNum])
            {
                potentialValuesOfRow.Add(potVal);
            }
        }
        return potentialValuesOfRow;
    }
    public SortedSet<int> GetPotentialCandidatesByColumn(int columnNum)
    {
        SortedSet<int> potentialValuesOfRow = new();
        foreach (int rowNum in Enumerable.Range(1, 9))
        {
            foreach (int potVal in PotentialValues[rowNum, columnNum])
            {
                potentialValuesOfRow.Add(potVal);
            }
        }
        return potentialValuesOfRow;
    }
    public SortedSet<int> GetPotentialCandidatesByBlock(int[] blockCoord)
    {
        SortedSet<int> potentialValuesOfBlock = new();
        int xMod = blockCoord[0] * 3;
        int yMod = blockCoord[1] * 3;
        // Get:
        foreach (int x in Enumerable.Range(1, 3))  // block
        {
            foreach (int y in Enumerable.Range(1, 3))  // All Cells in a Block
            {
                foreach (int potVal in PotentialValues[x + xMod, y + yMod])
                {
                    potentialValuesOfBlock.Add(potVal);
                }
            }
        }
        return potentialValuesOfBlock;
    }
    public SortedSet<int>[,] HydratePotentialValues()
    {
        SortedSet<int> temp = new SortedSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };  // template
        return new SortedSet<int>[,]
        {
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) },
            { new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp), new(temp) }
        };
    }
}
