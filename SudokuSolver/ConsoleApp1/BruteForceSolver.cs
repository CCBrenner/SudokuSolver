namespace SudokuSolver;

public class BruteForceSolver
{
	public BruteForceSolver(Puzzle puzzle)
    {
        this.puzzle = puzzle;
        this.puzzle.Cells.Reverse();
        RemainingCells = new();
        PreviousCells = new();
        foreach (var cell in this.puzzle.Cells)
        {
            if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
            {
                RemainingCells.Push(cell);
            }
        }
        this.puzzle.Cells.Reverse();
        CurrentCell = RemainingCells.Pop();
        PuzzleIsSolvable = true;
    }

    private Puzzle puzzle;
    public Cell CurrentCell { get; private set; }
	public Stack<Cell> RemainingCells { get; private set; }
	public Stack<Cell> PreviousCells { get; private set; }
    public bool PuzzleIsSolvable { get; private set; }

    public bool Solve()
    {
        int loopCounter = 1;
        int candidate;

        // initial constraint of candidates
        puzzle.RemoveCandidates();
        ConsoleRender.RenderMatrixCellValuesV3(puzzle);

        while (true)
        {
            //Console.WriteLine($"Main Loop Counter: {loopCounter}");
            //Console.WriteLine($"Current Cell ID: {CurrentCell.Id}");
            loopCounter++;

            //- GetNextCandidate (returns type int)
            candidate = CurrentCell.GetNextCandidate();

            if (candidate == 0 && PreviousCells.Count == 0)
            {
                PuzzleIsSolvable = false;
                break;
            }

            if (candidate == 0)
            {
                candidate = Backtrack();
                continue;
            }

            //- Assign return value as value of current cell 
            //Console.WriteLine($"Cell ID {CurrentCell.Id}; New Value: {candidate}");
            CurrentCell.SetExpectedValue(candidate);
            if (RemainingCells.Count == 0) break;

            //- Also assign same return value to TriedCandidates stack in Cell instance
            CurrentCell.AddTriedCandidate(candidate);

            // Since Value becomes free, add that value to all respective cells that would have it as a candidate
            // and then eliminate all non possible candidates
            puzzle.UpdateCandidates();

            //- Assign CurrentCell to PreviousCells stack in Puzzle instance
            GoToNextCell();

            //- Repeat from beginning of loop
        }

        // return true if puzzle is solved; false if could not be solved
        return PuzzleIsSolvable;
    }

    private int Backtrack()
    {
        //when a cell has no more candidates to try when calling GetNextCandidate:
        //- Clear the TriedCandidates stack of CurrentCell until stack count == 0
        CurrentCell.ResetCandidateTracking();
        CurrentCell.ResetValue();

        //- Push cell unto RemainingCells stack && Pop cell from PreviousCells stack && Assign popped cell from stack to CurrentCell property
        GoToPreviousCell();
        //Console.WriteLine($"Backtrack call back to Cell ID {CurrentCell.Id}");

        // Since Value becomes free, add that value to all respective cells that would have it as a candidate
        // and then eliminate all non possible candidates
        puzzle.UpdateCandidates();

        //- GetNextCandidate from CurrentCell
        int candidate = CurrentCell.GetNextCandidate();

        if (candidate == 0 && PreviousCells.Count == 0)
        {
            PuzzleIsSolvable = false;
            return candidate;
        }

        if (candidate == 0)
        {
            candidate = Backtrack();
        }

        return candidate;
    }
    private void GoToNextCell()
    {
        PreviousCells.Push(CurrentCell);
        CurrentCell = RemainingCells.Pop();
        //ConsoleRender.RenderMatrixCellValuesV2(puzzle);
    }
    private void GoToPreviousCell()
    {
        RemainingCells.Push(CurrentCell);
        CurrentCell = PreviousCells.Pop();
        //ConsoleRender.RenderMatrixCellValuesV2(puzzle);
    }
}
