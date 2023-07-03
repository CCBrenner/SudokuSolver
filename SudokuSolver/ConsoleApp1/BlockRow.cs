namespace SudokuSolver;

public class BlockRow
{
    public BlockRow(int id)
    {
        Id = id;
        Blocks = new List<Block>();
    }
    public int Id { get; }
    public List<Block> Blocks { get; private set; }

    public static BlockRow[] CreateArrayFromBlockReferences(Block[] blocks)
    {
        BlockRow[] blockRows = new BlockRow[4]
        {
            new BlockRow(0),
            new BlockRow(1),
            new BlockRow(2),
            new BlockRow(3),
        };

        foreach (var block in blocks)
        {
            if (block is not null)
            {
                blockRows[block.BlockRowId].AddBlockReference(block);
            }
        }

        return blockRows;
    }

    public static void AssignBlockRowReferenceToBlocksPerBlockRow(BlockRow[] blockRows)
    {
        foreach (var blockRow in blockRows)
        {
            blockRow.AssignBlockRowReferenceToBlocks();
        }
    }
    public static void AssignBlockRowReferenceToBlocksPerBlockRow(List<BlockRow> blockRows)
    {
        foreach (var blockRow in blockRows)
        {
            blockRow.AssignBlockRowReferenceToBlocks();
        }
    }

    private void AssignBlockRowReferenceToBlocks()
    {
        foreach (var block in Blocks)
        {
            if (block is not null)
            {
                block.AssignBlockRowReference(this);
            }
        }
    }

    public void AddBlockReference(Block block)
    {
        if (block.BlockRowId == Id)
        {
            Blocks[block.BlockColumnId] = block;
        }
    }

    public List<Block> GetNeighborsOfBlock(Block block)
    {
        List<Block> result = new();

        foreach (var locBlock in Blocks)
        {
            if (locBlock.Id != block.Id)
            {
                result.Add(locBlock);
            }
        }

        return result;
    }

    public static List<BlockRow> CreateListFromBlockReferences(List<Block> blocks)
    {
        List<BlockRow> result = new()
        {
            new BlockRow(1),
            new BlockRow(2),
            new BlockRow(3),
        };

        foreach (var block in blocks)
        {
            result[block.BlockRowId - 1].Blocks.Add(block);
        }

        return result;
    }
}
