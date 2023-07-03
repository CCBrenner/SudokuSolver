


// Configure puzzle
// enter lines in CSV format, one line at a time
/*
Console.WriteLine("Enter lines:");
Console.WriteLine();
*/




// Ways to find a new block:

// Find in row or column



// 81 cells
// 24 given to start on average (sample size of 1)


// How I solve:
// What I write down: When there are two candidates based on a certain check
// What this implies: each cell will need a record of high probability values (50/50 probabilty is the best) - what is the best way to store this data for procedure? Could use multiple data representations. Possibly running answer, and a second  


// Easy ways to get numbers
// If there is only one space left in a row or column or block (repeat this first for every row/column/block every time a new number is found)
// If there are only two spaces left (use different checks to see if you can find one or the other cell, leaveing the other cell only one candidate)


// Error handling:
// If the program finds that it has either (1) done something it should not hve been able to do, or (2) cannot do anything more based on its programmed rules,
// then quite with the type of error rovided and how much it was able to figure out at the time of the error. 


/*  Originally taken from "puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell()"
foreach (var cell in Matrix)
{
    // get values in row
    int[] rowValues = GetRowValues(cell);
    // get values in column
    int[] colValues = GetColumnValues(cell);
    // get values in block
    int[,] blockValues = GetBlockValues(cell);
    // consolidate set of candidates for removal
    SortedSet<int> nonPossibilities = GenerateNonPossibilites(rowValues, colValues, blockValues);
    // remove candidates from cell
    cell.RemovePossibilitiesFromCell(nonPossibilities);
    // Update cell value if only one candidate; throw exception if there are no more candidates
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
        // consolidate set of candidates for removal
        SortedSet<int> nonPossibilities = GenerateNonPossibilites(rowValues, colValues, blockValues);
        // remove candidates from cell
        cell.RemovePossibilitiesFromCell(nonPossibilities);
        // Update cell value if only one candidate; throw exception if there are no more candidates
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


/* From Block:
public static void EliminateCandidatesByNumber(Block[] blocks)
{
    foreach (var block in blocks)
    {
        if (block is not null)
        {
            block.EliminateCandidatesByNumber();
        }
    }
}
private void EliminateCandidatesByNumber()
{
    for (int i = 1; i < 10; i++)
    {
        EliminateCandidatesByNumberByNeighboringBlocks(i);
    }
}

private void EliminateCandidatesByNumberByNeighboringBlocks(int number)
{
    // how many neighboring blocks is number in?
    List<Cell> relevantConfirmedCells = new();

    foreach (var block in BlockNeighbors)
    {
        Cell? confirmedCell = block.NumberIsConfirmed(number);

        if (confirmedCell is not null)
        {
            relevantConfirmedCells.Add(confirmedCell);
        }
    }

    if (relevantConfirmedCells.Count == 4)
    {
        Cell cellHavingDiscoveredValue = DeduceCellValueByNumberByNotTwoRowsAndNotTwoColumns(relevantConfirmedCells);
        cellHavingDiscoveredValue.AssignConfirmedValue(number);
    }
    else if (relevantConfirmedCells.Count == 3)
    {

    }
    else if (relevantConfirmedCells.Count == 2)
    {

    }
    else if (relevantConfirmedCells.Count == 1)
    {

    }
    else
    {

    }
}
*/
