namespace SudokuSolver;

public class BlockColumn
{
    public BlockColumn(int id)
    {
        Id = id;
        Blocks = new Block[10];
    }
    public int Id { get; }
    public Block[] Blocks { get; private set; }

    public void AddBlockReference(Block block)
    {
        if (block.BlockColumn == Id)
        {
            Blocks[block.BlockRow] = block;
        }
    }
}