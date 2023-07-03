/*  Originally taken from "puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell()"
foreach (var cell in Matrix)
{
    // get values in row
    int[] rowValues = GetRowValues(cell);
    // get values in column
    int[] colValues = GetColumnValues(cell);
    // get values in block
    int[,] blockValues = GetBlockValues(cell);
    // consolidate set of pencilMarkings for removal
    SortedSet<int> nonPossibilities = GenerateNonPossibilites(rowValues, colValues, blockValues);
    // remove pencilMarkings from cell
    cell.RemovePossibilitiesFromCell(nonPossibilities);
    // Update cell value if only one pencilMarking; throw exception if there are no more pencilMarkings
    int updatedCellValue = cell.CheckAndUpdateValueIfOnePossibilityRemaining();
    // update value of cell if expected value is not a possible value (update to what?)
    // NEED METHOD HERE.
}
*/
/*  Originally taken from "puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell()"
List<int> totalCellValuesUpdated = new();

while (true)
{
    int cellValueUpdateCount = 0;

    foreach (var cell in Matrix)
    {
        // get values in row
        int[] rowValues = GetRowValues(cell);
        // get values in column
        int[] colValues = GetColumnValues(cell);
        // get values in block
        int[,] blockValues = GetBlockValues(cell);
        // consolidate set of pencilMarkings for removal
        SortedSet<int> nonPossibilities = GenerateNonPossibilites(rowValues, colValues, blockValues);
        // remove pencilMarkings from cell
        cell.RemovePossibilitiesFromCell(nonPossibilities);
        // Update cell value if only one pencilMarking; throw exception if there are no more pencilMarkings
        int updatedCellValue = cell.CheckAndUpdateValueIfOnePossibilityRemaining();
        // update value of cell if expected value is not a possible value (update to what?)
        // NEED METHOD HERE.
        // increment update if cell is updated
        if (updatedCellValue != 0)
        {
            cellValueUpdateCount++;
        }
        // move to next cell in matrix
    }

    if (cellValueUpdateCount == 0) break;

    totalCellValuesUpdated.Add(cellValueUpdateCount);
}

return totalCellValuesUpdated;
return new List<int>();
*/

/* from Cell:
public List<PositivePencilMarkingTypeTwo> GetTypeTwoPositivePencilMarkings()
{
    List<PositivePencilMarkingTypeTwo> positivePencilMarkingPairs = new();

    for (int i = 0; i < PositivePencilMarkings.Count(); i++)
    {
        for (int j = 0; j < PositivePencilMarkings.Count(); j++)
        {
            // It is important to have only pairs in ascending order w/o redundancy; "i < j" is doing this here.
            if (i != j && i < j)
            {
                int[] pair = new int[2] { i, j };
                PositivePencilMarkingTypeTwo notedPossibiltyPair = new(this, pair);
                positivePencilMarkingPairs.Add(notedPossibiltyPair);
            }
        }
    }

    return positivePencilMarkingPairs;
}
*/

/* From Cell:
private List<PositivePencilMarkingTypeTwo> GetUndisclosedPositivePencilMarkingPairsIfPresent()
{
    foreach (var positivePencilMarking in PositivePencilMarkings)
    {
        foreach (var cell in positivePencilMarking.Cells)  // will always have maximum of 2 iterations
        {
            if (cell != this)
            {
                bool cellsShareTwoCommonPositivePencilMarkings = cell.ConfirmIfSharesSecondPositivePencilMarking(this);
            }
        }
    }

    return new();
}

private bool ConfirmIfSharesSecondPositivePencilMarking(Cell otherCell)
{
    foreach (var positivePencilMarking in PositivePencilMarkings)
        foreach (var cell in positivePencilMarking.Cells)  // will always have maximum of 2 iterations
            if (cell != this && cell == otherCell)
                return true;
    return false;
}*/
