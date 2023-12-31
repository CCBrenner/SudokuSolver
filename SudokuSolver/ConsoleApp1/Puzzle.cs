﻿namespace SudokuSolver;

public class Puzzle
{
    private Puzzle(Cell[,] puzzleMatrix, int[,] startingMatrix, int[,] matrixToSuperimpose, List<Cell> cells, List<Row> rows, List<Column> columns, List<Block> blocks, List<BlockRow> blockRows, List<BlockColumn> blockClumns, TxnLedger ledger)
    {
        Matrix = puzzleMatrix;
        StartingIntMatrix = startingMatrix;
        StartingIntMatrixToSuperimpose = matrixToSuperimpose;
        Cells = cells;
        Rows = rows;
        Columns = columns;
        Blocks = blocks;
        BlockRows = blockRows;
        BlockClumns = blockClumns;
        Ledger = ledger;
    }

    public Cell[,] Matrix { get; private set; }
    public List<Cell> Cells { get; private set; }
    public List<Row> Rows { get; private set; }
    public List<Column> Columns { get; private set; }
    public List<Block> Blocks { get; private set; }
    public List<BlockRow> BlockRows { get; private set; }
    public List<BlockColumn> BlockClumns { get; private set; }
    public TxnLedger Ledger { get; }
    public int[,] StartingIntMatrix { get; }
    public int[,] StartingIntMatrixToSuperimpose { get; }

    public bool NoExpectedCellValuesInCells => GetNoExpectedCellValuesInCells();

    public bool IsSolvableBasedOnCandidates => GetIsSolvableBasedOnCandidates();

    public bool CurrentCandidateIsLastCandidateOfCurrentCell => CurrentCell.GetRemainingCandidatesToTry().Count != 0;

    public Cell CurrentCell { get; private set; }

    public static Puzzle Create(int[,] startingMatrix)
    {
        int[,] blankMatrix = MatrixFactory.GetBlankMatrix();
        return Create(startingMatrix, blankMatrix, new TxnLedger());
    }
    public static Puzzle Create(int[,] startingMatrix, TxnLedger ledger)
    {
        int[,] blankMatrix = MatrixFactory.GetBlankMatrix();
        return Create(startingMatrix, blankMatrix, ledger);
    }
    public static Puzzle Create(int[,] startingMatrix, int[,] matrixToSuperimpose)
    {
        return Create(startingMatrix, matrixToSuperimpose, new TxnLedger());
    }
    public static Puzzle Create(int[,] startingMatrix, int[,] matrixToSuperimpose, TxnLedger ledger)
    {
        // Create matrix of cells instead of ints from starting point
        Cell[,] matrix = MatrixFactory.CreateMatrix(startingMatrix);

        // Create Matrix of cells through process of superimposition
        Cell[,] compositionMatrix = SuperimposeNonGivenCellValues(matrix, matrixToSuperimpose);

        // Create list of references to cells of previously created matrix
        List<Cell> cells = Cell.CreateListFromCellReferencesOfMatrix(compositionMatrix);

        // Create rows array of cell references from cells list (one-to-many relationships)
        List<Row> rows = Row.CreateListFromCellReferences(cells.ToList());
        Row.AssignRowReferenceToCellsPerRow(rows);

        // Create columns array of cell references from cells list (one-to-many relationships)
        List<Column> columns = Column.CreateListFromCellReferences(cells.ToList());
        Column.AssignColumnReferenceToCellsPerColumn(columns);

        // Create blocks array of cell references from cells list (one-to-many relationships)
        List<Block> blocks = Block.CreateListFromCellReferences(cells.ToList());
        Block.AssignBlockReferenceToCellsPerBlock(blocks);

        // Create blockrows array of block references from blocks array (one-to-many relationships)
        List<BlockRow> blockRows = BlockRow.CreateListFromBlockReferences(blocks);
        BlockRow.AssignBlockRowReferenceToBlocksPerBlockRow(blockRows);

        // Create blockcolumns array of block references from blocks array (one-to-many relationships)
        List<BlockColumn> blockColumns = BlockColumn.CreateListFromBlockReferences(blocks);
        BlockColumn.AssignBlockColumnReferenceToBlocksPerBlockColumn(blockColumns);

        // Inject and create new puzzle
        Puzzle puzzle = new(compositionMatrix, startingMatrix, matrixToSuperimpose, cells, rows, columns, blocks, blockRows, blockColumns, ledger);
        puzzle.AssignPuzzleReferenceToCells();
        puzzle.AssignPuzzleReferenceToLedger();

        return puzzle;
    }

    private void AssignPuzzleReferenceToLedger()
    {
        Ledger.AssignPuzzleReference(this);
    }

    public void RemoveUnconfirmedValuesThatDoNotHaveRespectiveCandidate()
    {
        foreach (var cell in Matrix)
        {
            cell.ReconcileValueWithCandidates();
        }
    }
    public void RemoveCandidates()
    {
        // Eliminate by distinct in neighborhood
        Row.EliminateCandidatesByDistinctInNeighborhood(Rows);
        Column.EliminateCandidatesByDistinctInNeighborhood(Columns);
        Block.EliminateCandidatesByDistinctInNeighborhood(Blocks);

        // Eliminate by candidate lines
        Block.EliminateCandidatesByCandidateLines(Blocks);

        // Eliminate by double pairs

        // Eliminate by multiple lines

        // Eliminate by naked pairs, triples, quadruples

        // Eliminate by hidden pairs, triples, quadruples

        // Eliminate by X-wings

        // Eliminate by Swordfish
    }

    public void UpdateValues()
    {
        RemoveExpectedValuesBasedOnNotACandidate();
        UpdateCellValuesBasedOnSingleCandidate();
    }

    public void RemoveExpectedValuesBasedOnNotACandidate()
    {
        foreach (var cell in Cells)
        {
            if (cell.ValueStatus == ValueStatus.Expected)
            {
                cell.RemoveExpectedValueIfNotACandidate();
            }
        }
    }

    public void UpdateCellValuesBasedOnSingleCandidate()
    {
        foreach (var cell in Matrix)
        {
            cell.UpdateValueBasedOnSingleCandidate();
        }
    }

    public static Cell[,] SuperimposeNonGivenCellValues(Cell[,] compositionMatrix, int[,] matrixToSuperimpose)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (compositionMatrix[i, j].ValueStatus != ValueStatus.Given)
                {
                    compositionMatrix[i, j].SetExpectedValue(matrixToSuperimpose[i, j]);
                }
            }
        }

        return compositionMatrix;
    }

    private void AssignPuzzleReferenceToCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                cell.AssignPuzzleReference(this);
            }
        }
    }
    private bool GetNoExpectedCellValuesInCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                if (cell.ValueStatus == ValueStatus.Expected)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private bool GetIsSolvableBasedOnCandidates()
    {
        bool row = Row.IsSolvableBasedOnCandidates(Rows);
        bool column = Column.IsSolvableBasedOnCandidates(Columns);
        bool block = Block.IsSolvableBasedOnCandidates(Blocks);

        return row && column && block;
    }

    public bool BacktrackToLastCell()
    {
        // have cell reset regarding treatment during time as current cell
        CurrentCell.Backtrack();

        // 
        int previousCellId = CurrentCell.Id;
        int newCurrenCellId = previousCellId - 1;

        if (CurrentCell.Id == 1)
        {
            return false;
        }

        SetCurrentCell(newCurrenCellId);

        return true;
    }

    private void SetCurrentCell(int newCurrenCellId)
    {
        Cell? potentialNewCurrentCell = Cells.FirstOrDefault(x => x.Id == (newCurrenCellId - 1));
    }

    public void UpdateCandidates()
    {
        FillWithCandidates();
        RemoveCandidates();
    }

    private void FillWithCandidates()
    {
        foreach (var cell in Cells)
        {
            if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
            {
                cell.RehydrateCandidates();
            }
        }
    }
}
