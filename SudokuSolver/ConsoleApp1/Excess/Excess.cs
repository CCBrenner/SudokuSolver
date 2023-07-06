


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




/* From Program.cs */
// Strategy:
// Primarily brute force with support from candidate check/removal to minimize
// the number of brute force operations/iterations.
/*
Why this strategy?: The goal of solving ANY sudoku puzzle means solving even the hardest puzzle there is.
I (Collyn) personally have encountered many puzzles that I have not been able to solve, many being only more than 
moderately difficult. My human process methods cannot achieve this currently, but they might in the future.
This, and computers are much better at step forward/step back processes because they can perform them quickly, procedurally
based on conditionals without challenge - much harder for a human to do.
But, a pure brute force strategy can end up being a really long process due to the number of possible single-number configurations
and combinations that can exist in a 9x9 grid. Any help to reduce the numer of checks (especially long periods of processing that
could be eliminated based on a logic check) will reduce the number of operations and therefore the processing speed of the 
sudolku puzzle solver.

// Tactics/Process:
// 0. On a loop:
while (true)
{
    // 1. Remove candidates by various methods based on entered values (given, expected, and confirmed).
    puzzle.RemoveCandidates();

    // 2. Update values based on single candidate: if no expected values in matrix (first iteration; all values are "given"), then add confirmed values to cells;
    // otherwise, add values with ValueStatus of "ValueStatus.Expected";
    // expected values may not be correct and may have to be undone at some point in the brute force process.
    puzzle.UpdateCellValuesBasedOnSingleCandidate();

    // 3. Check if puzzle is still solvable based on remaining cell candidates of each neighborhood.
    // If solvable, proceed. If not, eliminate last expected value as option.
    // If another option is available for the cell, proceed with testing that candidate.
    // If no remaining candidates, then previous candidate is not the right candidate: reset node cell candidates,
    // eliminate previous cells's candidate as an option and try the next candidate. Start from the top with 1. again.
    if (!puzzle.IsSolvableBasedOnCandidates)
    {
        if (puzzle.CurrentCandidateIsLastCandidateOfCurrentCell)
        {
            bool candidatesAreAvailableToTry = puzzle.BacktrackToLastCell();  // handles last candidate try as a possibility

            if (!candidatesAreAvailableToTry)
            {
                break;
            }

            continue;
        }
        else
        {
            // 4. Move to the next cell in the list (if first iteration, this cell is [0,0]), making that cell the new current cell.
            // 5. Test a candidate/the next candidate by assigning it as the expected value of the current cell
            puzzle.CurrentCell.AddCurrentValueToTriedCandidateList();
            //puzzle.CurrentCell.ProceedToNextCandidateToTry();
        }
        continue;
    }

    // 6. Repeat starting from 1. again.
}
*/


/* From Program.cs:
        puzzle.RemoveCandidates();
        puzzle.UpdateCellValuesBasedOnSingleCandidate();

        puzzle.RemoveUnconfirmedValuesThatDoNotHaveRespectiveCandidate();

        puzzle.UpdateCellValuesBasedOnSingleCandidate();

        puzzle.RemoveUnconfirmedValuesThatDoNotHaveRespectiveCandidate();

        ConsoleRender.RenderMatrixCellValuesV2(puzzle);
     
        Console.ReadLine();
        */
