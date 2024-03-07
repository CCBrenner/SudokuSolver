
using SudokuSolverV2;
using SudokuSolverV2.Solver;

Solver solver = Solver.Create(PuzzleBook.GetPuzzle("R2"));

ConsoleRender.RenderPuzzleValues(solver.PuzzleValues);
ConsoleRender.RenderPotentialValues(solver.PotentialValues);

solver.PotentialValues[0, 0].Remove(3);

ConsoleRender.RenderMatrixWithMetaData(solver.PuzzleValues, solver.PotentialValues);
