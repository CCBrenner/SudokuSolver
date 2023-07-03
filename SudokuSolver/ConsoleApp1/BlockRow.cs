namespace SudokuSolver;

public class BlockRow
{
    public BlockRow(int id)
    {
        Id = id;
        Blocks = new Block[10];
    }
    public int Id { get; }
    public Block[] Blocks { get; private set; }

    public void AddBlockReference(Block block)
    {
        if (block.BlockRow == Id)
        {
            Blocks[block.BlockColumn] = block;
        }
    }
}
