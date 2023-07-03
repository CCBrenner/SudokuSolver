/*  Originally taken from "puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell()"
foreach (var cell in Matrix)
{
    // get values in row
    int[] rowValues = GetRowValues(cell);
    // get values in column
    int[] colValues = GetColumnValues(cell);
    // get values in square
    int[,] squareValues = GetSquareValues(cell);
    // consolidate set of possibilities for removal
    SortedSet<int> nonPossibilities = GenerateNonPossibilites(rowValues, colValues, squareValues);
    // remove possibilities from cell
    cell.RemovePossibilitiesFromCell(nonPossibilities);
    // Update cell value if only one possibility; throw exception if there are no more possibilities
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
        // get values in square
        int[,] squareValues = GetSquareValues(cell);
        // consolidate set of possibilities for removal
        SortedSet<int> nonPossibilities = GenerateNonPossibilites(rowValues, colValues, squareValues);
        // remove possibilities from cell
        cell.RemovePossibilitiesFromCell(nonPossibilities);
        // Update cell value if only one possibility; throw exception if there are no more possibilities
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
