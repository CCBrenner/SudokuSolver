namespace SudokuSolver;

public class BruteForceSolver
{
	public BruteForceSolver(Puzzle puzzle)
    {
        this.puzzle = puzzle;

        this.puzzle.Cells.Reverse();  // reversed (for creating RemainingCells stack)
        RemainingCells = new();
        foreach (var cell in this.puzzle.Cells)
        {
            if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
            {
                RemainingCells.Push(cell);
            }
        }
        this.puzzle.Cells.Reverse();  // reversed back to original order

        PreviousCells = new();
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

        while (true)
        {
            loopCounter++;

            // get the next candidate to try
            candidate = CurrentCell.GetNextCandidate();

            // if CurrentCell is the CellId 1 and htere are no other candidates to try, exit
            if (candidate == 0 && PreviousCells.Count == 0)
            {
                PuzzleIsSolvable = false;
                break;
            }

            // if there are no other candidates to try, then backtrack to previous cell
            if (candidate == 0)
            {
                candidate = Backtrack();
                continue;
            }

            // assign return value as value of current cell 
            CurrentCell.SetExpectedValue(candidate);
            if (RemainingCells.Count == 0) break;

            // also assign same return value to TriedCandidates stack in Cell instance
            CurrentCell.AddTriedCandidate(candidate);

            // since Value becomes free, add that value to all respective cells that would have it as a candidate
            // and then eliminate all non possible candidates
            puzzle.UpdateCandidates();

            // assign CurrentCell to PreviousCells stack in Puzzle instance
            GoToNextCell();
        }

        // return true if puzzle is solved; false if could not be solved
        return PuzzleIsSolvable;
    }

    private int Backtrack()
    {
        // when a cell has no more candidates to try when calling GetNextCandidate:
        // clear the TriedCandidates stack of CurrentCell until stack count == 0
        CurrentCell.ResetCandidateTracking();
        CurrentCell.ResetValue();

        // push cell unto RemainingCells stack && Pop cell from PreviousCells stack && Assign popped cell from stack to CurrentCell property
        GoToPreviousCell();

        // since Value becomes free, add that value to all respective cells that would have it as a candidate
        // and then eliminate all non possible candidates
        puzzle.UpdateCandidates();

        // get the next candidate to try
        int candidate = CurrentCell.GetNextCandidate();

        // if CurrentCell is the CellId 1 and htere are no other candidates to try, exit
        if (candidate == 0 && PreviousCells.Count == 0)
        {
            PuzzleIsSolvable = false;
            return candidate;
        }

        // if there are no other candidates to try, then backtrack to previous cell
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
    }
    private void GoToPreviousCell()
    {
        RemainingCells.Push(CurrentCell);
        CurrentCell = PreviousCells.Pop();
    }
}
