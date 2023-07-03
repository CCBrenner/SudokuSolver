namespace SudokuSolver;

public class BlockColumn
{
    public BlockColumn(int id)
    {
        Id = id;
        Blocks = new List<Block>();
    }
    public int Id { get; }
    public List<Block> Blocks { get; private set; }

    public static BlockColumn[] CreateArrayFromBlockReferences(Block[] blocks)
    {
        BlockColumn[] blockColumns = new BlockColumn[4]
        {
            new BlockColumn(0),
            new BlockColumn(1),
            new BlockColumn(2),
            new BlockColumn(3),
        };

        foreach (var block in blocks)
        {
            blockColumns[block.BlockColumnId].AddBlockReference(block);
        }

        return blockColumns;
    }

    public static void AssignBlockColumnReferenceToBlocksPerBlockColumn(BlockColumn[] blockColumns)
    {
        foreach (var blockRow in blockColumns)
        {
            blockRow.AssignBlockColumnReferenceToBlocks();
        }
    }
    public static void AssignBlockColumnReferenceToBlocksPerBlockColumn(List<BlockColumn> blockColumns)
    {
        foreach (var blockRow in blockColumns)
        {
            blockRow.AssignBlockColumnReferenceToBlocks();
        }
    }

    private void AssignBlockColumnReferenceToBlocks()
    {
        foreach (var block in Blocks)
        {
            if (block is not null)
            {
                block.AssignBlockColumnReference(this);
            }
        }
    }

    public void AddBlockReference(Block block)
    {
        if (block.BlockColumnId == Id)
        {
            Blocks[block.BlockRowId] = block;
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

    public static List<BlockColumn> CreateListFromBlockReferences(List<Block> blocks)
    {
        List<BlockColumn> result = new()
        {
            new BlockColumn(1),
            new BlockColumn(2),
            new BlockColumn(3),
        };

        foreach (var block in blocks)
        {
            result[block.BlockColumnId - 1].Blocks.Add(block);
        }

        return result;
    }
}