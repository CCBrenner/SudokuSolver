using SudokuSolver;

namespace Program;

class Program
{
    public static void Main(string[] args)
    {

        // Configure puzzle
        // enter lines in CSV format, one line at a time
        /*
        Console.WriteLine("Enter lines:");
        Console.WriteLine();
        */




        // Ways to find a new square:

        // Find in row or column



        // 81 cells
        // 24 given to start on average (sample size of 1)


        // How I solve:
        // What I write down: When there are two possibilities based on a certain check
        // What this implies: each cell will need a record of high probability values (50/50 probabilty is the best) - what is the best way to store this data for procedure? Could use multiple data representations. Possibly running answer, and a second  


        // Easy ways to get numbers
        // If there is only one space left in a row or column or square (repeat this first for every row/column/square every time a new number is found)
        // If there are only two spaces left (use different checks to see if you can find one or the other cell, leaveing the other cell only one possibility)


        // Error handling:
        // If the program finds that it has either (1) done something it should not hve been able to do, or (2) cannot do anything more based on its programmed rules,
        // then quite with the type of error rovided and how much it was able to figure out at the time of the error. 



        // use puzzle #33 from Sudolu Variety Colleection, Vol. 20


        /*
        int[,] intMatrix =
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };
        */

        /*  using 2D cells:
        int[,] matrix33 =
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

        Cell[,] matrix = Puzzle.CreateMatrix(matrix33);

        Puzzle puzzle = new(matrix);

        puzzle.RenderMatrix();

        Console.ReadLine();

        int[] integerArr = { 1, 2, 3, 4 };
        */

        int[,] matrix33 =
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

        Cell[,] matrix = Puzzle.CreateMatrix(matrix33);
        //int[,] matrixToSuperimpose = MatrixFactory.GetSuperImposeMatrix();
        int[,] matrixToSuperimpose = MatrixFactory.GetBlankMatrix();
        Puzzle puzzle = Puzzle.Create(matrix, matrixToSuperimpose);
        /*
        ConsoleRender.RenderMatrix(puzzle);

        Console.ReadLine();
        */


        puzzle.ApplyAllThreeDisctinctionRulesToEliminatePossibilitiesForEachCell();
        puzzle.CheckAndUpdateValueOfEachCellIfOnePossibilityRemaining();

        puzzle.RemoveImpossibleValues();
        /*
        ConsoleRender.RenderMatrix(puzzle);

        Console.ReadLine();
        */


        puzzle.CheckAndUpdateValueOfEachCellIfOnePossibilityRemaining();

        puzzle.RemoveImpossibleValues();
        /*
        ConsoleRender.RenderMatrix(puzzle);

        Console.ReadLine();
        */

        ConsoleRender.RenderMatrixCellValues(puzzle);

        Console.ReadLine();

        // 1. Update puzzle so that possibilities are removed based on distinction rules
        // 2. Add factory method that creates a puzzle with given values and test values for all other positions; then apply possibilty rules and correct those that are incorrect by replacing ith ones that are correct; iterate until the puzzle has no conflicting values
    }
}
