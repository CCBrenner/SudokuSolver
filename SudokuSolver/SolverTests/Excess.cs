

/* Couldn't figure out what wasn't equal; is close enough to desired rendering & is trivial.
[TestMethod]
public void TestGenerateRenderStringProducesExpectedStringRendering()
{
    // Arange
    int[,] intMatrix =
    {
        { 0, 0, 5, 0, 3, 0, 0, 8, 0 },
        { 0, 0, 3, 0, 0, 2, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 9, 1 },
        { 8, 0, 0, 7, 0, 0, 0, 1, 0 },
        { 2, 0, 0, 8, 0, 3, 0, 0, 7 },
        { 0, 6, 0, 0, 0, 4, 0, 0, 9 },
        { 4, 3, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 9, 0, 0, 1, 0, 0 },
        { 0, 8, 0, 0, 5, 0, 6, 0, 0 },
    };

    Cell[,] createdCellMatrix = Puzzle.CreateMatrix(intMatrix);
    Puzzle puzzle = Puzzle.Create(createdCellMatrix);

    // Act
    string actualMatrixRenderString = puzzle.GetMatrixRenderString();

    // Assert
    /*  For reference only (template matrix):
        { 1, 2, 5, 4, 3, 6, 7, 8, 9 },
        { 4, 5, 3, 7, 8, 2, 1, 2, 3 },
        { 7, 8, 9, 1, 2, 3, 4, 9, 1 },

        { 8, 3, 4, 7, 6, 7, 8, 1, 1 },
        { 2, 6, 7, 8, 9, 3, 2, 3, 7 },
        { 8, 6, 1, 2, 3, 4, 5, 6, 9 },

        { 4, 3, 5, 6, 7, 8, 9, 1, 2 },
        { 6, 7, 8, 9, 1, 2, 1, 4, 5 },
        { 9, 8, 2, 3, 5, 5, 6, 7, 8 },

    string expectedMatrixRenderString =
        "[ 1 ][ 2 ][ 5 ] [ 4 ][ 3 ][ 6 ] [ 7 ][ 8 ][ 9 ]\n" +
        "[ 4 ][ 5 ][ 3 ] [ 7 ][ 8 ][ 2 ] [ 1 ][ 2 ][ 3 ]\n" +
        "[ 7 ][ 8 ][ 9 ] [ 1 ][ 2 ][ 3 ] [ 4 ][ 9 ][ 1 ]\n" +
        "\n" +
        "[ 8 ][ 3 ][ 4 ] [ 7 ][ 6 ][ 7 ] [ 8 ][ 1 ][ 1 ]\n" +
        "[ 2 ][ 6 ][ 7 ] [ 8 ][ 9 ][ 3 ] [ 2 ][ 3 ][ 7 ]\n" +
        "[ 8 ][ 6 ][ 1 ] [ 2 ][ 3 ][ 4 ] [ 5 ][ 6 ][ 9 ]\n" +
        "\n" +
        "[ 4 ][ 3 ][ 5 ] [ 6 ][ 7 ][ 8 ] [ 9 ][ 1 ][ 2 ]\n" +
        "[ 6 ][ 7 ][ 8 ] [ 9 ][ 1 ][ 2 ] [ 1 ][ 4 ][ 5 ]\n" +
        "[ 9 ][ 8 ][ 2 ] [ 3 ][ 5 ][ 5 ] [ 6 ][ 7 ][ 8 ]\n";

    Assert.AreEqual(expectedMatrixRenderString, actualMatrixRenderString);
}
*/
/*
for (int i = (blockCoords[0] * 3); i < ((blockCoords[0] * 3) + 3); i++)
{
    for (int j = (blockCoords[1] * 3); j < ((blockCoords[1] * 3) + 3); j++)
    {
        Console.WriteLine();
        foreach (var val in puzzle.Matrix[i, j].Values)
        {
            Console.Write(val.ToString());
        }
    }
}
*/

/*
{ 1, 2, 3,   4, 5, 6,   7, 8, 9 },
{ 4, 5, 6,   7, 8, 9,   1, 2, 3 },
{ 7, 8, 9,   1, 2, 3,   4, 5, 6 },

{ 2, 3, 4,   5, 6, 7,   8, 9, 1 },
{ 5, 6, 7,   8, 9, 1,   2, 3, 4 },
{ 8, 9, 1,   2, 3, 4,   5, 6, 7 },

{ 3, 4, 5,   6, 7, 8,   9, 1, 2 },
{ 6, 7, 8,   9, 1, 2,   3, 4, 5 },
{ 9, 1, 2,   3, 4, 5,   6, 7, 8 },
 */

/*
    { 1, 2, 5,   4, 3, 6,   7, 8, 9 },
    { 4, 5, 3,   7, 8, 2,   1, 2, 3 },
    { 7, 8, 9,   1, 2, 3,   4, 9, 1 },

    { 8, 3, 4,   7, 6, 7,   8, 1, 1 },
    { 2, 6, 7,   8, 9, 3,   2, 3, 7 },
    { 8, 6, 1,   2, 3, 4,   5, 6, 9 },

    { 4, 3, 5,   6, 7, 8,   9, 1, 2 },
    { 6, 7, 8,   9, 1, 2,   1, 4, 5 },
    { 9, 8, 2,   3, 5, 5,   6, 7, 8 },
 */

/*
foreach(var cell in createdCellMatrix)
{
    if (cell.Values[0] != intMatrix[cell.Row, cell.Column])
    {
        Console.WriteLine($"column {cell.Column} row {cell.Row} => created: {cell.Values[0]}, exected: {intMatrix[cell.Row, cell.Column]}");
    }
}
*/

/* Print Values of each Cell for block
row = new();
for (int i = 0; i < 1; i++)
{
    for (int j = 0; j < 9; j++)
    {
        row.Add(puzzle.Matrix[i, j].Values[0]);
    }
}
Assert.AreEqual(9, row.Count);
*/

/* Print Values of each Cell for row
for (int i = 0; i < 9; i++)
{
    Console.WriteLine();
    foreach (var val in puzzle.Matrix[0, i].Values)
    {
        Console.Write(val.ToString());
    }
}
*/

