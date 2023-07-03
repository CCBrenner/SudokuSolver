using System.Diagnostics.Metrics;

namespace SudokuSolver;

public class Cell
{
    public Cell(int row, int column, int givenStartingValue = 0)
    {
        Row = row;
        Column = column;

        int rowModulus = row % 3;
        int squareRow = (row - rowModulus) / 3;
        int columnModulus = column % 3;
        int squareColumn = (column - columnModulus) / 3;
        Square = new int[2]{ squareRow, squareColumn };

        if (givenStartingValue == 0)
        {
            Values = new int[10]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };  // let [0] be entered value for that cell and all others be remaining possibilities
            IsGivenValue = false;
        }
        else
        {
            Values = new int[10];
            Values[0] = givenStartingValue;  // let [0] be the given value that never changes; other possibilities are quantity zero
            IsGivenValue = true;
        }

        IsExpectedValue = false;
        IsConfirmedValue = false;
        NotedPossibilities = new();
    }

    private const int NON_POSSIBILITY_PLACEHOLDER_VALUE = 0;

    public bool IsGivenValue { get; private set; }
    public bool IsExpectedValue { get; private set; }
    public bool IsConfirmedValue { get; private set; }
    public int Row { get; }
    public int Column { get; }
    public int[] Square { get; }
    public int[] Values { get; private set; }
    public List<NotedPossibilityTypeOne> NotedPossibilities { get; set; }
    public string Value =>
        Values[0] == NON_POSSIBILITY_PLACEHOLDER_VALUE ? " " : Values[0].ToString();

    public int[] ConvertSquareOfCellToBaseNineIndexing() =>
        new int[2] { (Square[0] * 3), (Square[1] * 3) };

    public int CheckAndUpdateValueIfOnePossibilityRemaining()
    {
        int nonZeroCounter = 0;
        int savedValueFromIteration = NON_POSSIBILITY_PLACEHOLDER_VALUE;

        int counter = 0;
        foreach (var val in Values)
        {
            if (val != 0 && counter != 0)
            {
                nonZeroCounter++;
                savedValueFromIteration = val;
            }
            counter++;
        }

        if (nonZeroCounter == 1 && savedValueFromIteration != 0)
        {
            Values[0] = savedValueFromIteration;
            IsConfirmedValue = true;
        }

        return savedValueFromIteration;
    }
    public int SetExpectedValue(int expectedValue)
    {
        if (!IsPossibleValue(expectedValue) && !IsGivenValue)
        {
            return NON_POSSIBILITY_PLACEHOLDER_VALUE;
        }

        Values[0] = expectedValue;
        IsExpectedValue = true;

        return Values[0];
    }
    public int ReconcileValueWithPossibilities()
    {
        if (IsGivenValue || IsConfirmedValue || Values[Values[0]] != NON_POSSIBILITY_PLACEHOLDER_VALUE)
        {
            return Values[0];
        }

        Values[0] = NON_POSSIBILITY_PLACEHOLDER_VALUE;
        return Values[0];
    }
    public int EliminatePossibility(int possibility)
    {
        if (possibility == 0 || possibility > 9)
        {
            return NON_POSSIBILITY_PLACEHOLDER_VALUE;
        }

        Values[possibility] = NON_POSSIBILITY_PLACEHOLDER_VALUE;
        return possibility;
    }

    public int SetValue(int newCellValue)
    {
        if (Values[newCellValue] == NON_POSSIBILITY_PLACEHOLDER_VALUE || IsGivenValue)
        {
            return NON_POSSIBILITY_PLACEHOLDER_VALUE;
        }

        Values[0] = newCellValue;
        return newCellValue;
    }

    public string GetAllValues()
    {
        string allValues = string.Empty;
        int spaceCounter = 0;
        int counter = 0;
        foreach (var val in Values)
        {
            if (counter == 0)
            {
                allValues += $"{val} ";
            }
            else if (val != 0)
            {
                allValues += val.ToString();
            }
            else
            {
                spaceCounter++;
            }
            counter++;
        }

        for (int i = 0; i < spaceCounter; i++)
        {
            allValues += " ";
        }

        return allValues;
    }
    private bool IsPossibleValue(int valueBeingChecked)
    {
        foreach (var val in Values)
        {
            if (val != NON_POSSIBILITY_PLACEHOLDER_VALUE && val == valueBeingChecked)
            {
                return true;
            }
        }
        return false;
    }
    /*
    public List<NotedPossibilityTypeTwo> GetTypeTwoNotedPossibilities()
    {
        List<NotedPossibilityTypeTwo> notedPossibilityPairs = new();

        for (int i = 0; i < NotedPossibilities.Count(); i++)
        {
            for (int j = 0; j < NotedPossibilities.Count(); j++)
            {
                // It is important to have only pairs in ascending order w/o redundancy; "i < j" is doing this here.
                if (i != j && i < j)
                {
                    int[] pair = new int[2] { i, j };
                    NotedPossibilityTypeTwo notedPossibiltyPair = new(this, pair);
                    notedPossibilityPairs.Add(notedPossibiltyPair);
                }
            }
        }

        return notedPossibilityPairs;
    }
    */
    private List<NotedPossibilityTypeTwo> GetUndisclosedNotedPossibilityPairsIfPresent()
    {
        foreach (var notedPossibility in NotedPossibilities)
        {
            foreach (var cell in notedPossibility.Cells)  // will always have maximum of 2 iterations
            {
                if (cell != this)
                {
                    bool cellsShareTwoCommonNotedPossibilities = cell.ConfirmIfSharesSecondNotedPossibility(this);
                }
            }
        }

        return new();
    }

    private bool ConfirmIfSharesSecondNotedPossibility(Cell otherCell)
    {
        foreach (var notedPossibility in NotedPossibilities)
            foreach (var cell in notedPossibility.Cells)  // will always have maximum of 2 iterations
                if (cell != this && cell == otherCell)
                    return true;
        return false;
    }
}
