namespace SudokuSolver;

public interface ISolver
{
    public System.Timers.Timer Timer { get; }
    public decimal StopwatchTime { get; }
}