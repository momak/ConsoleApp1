using System;

namespace ConsoleApp1
{
    class Xo
    {
        private const int rows = 3;
        private const int cols = 3;

        private int[] prodRows = new int[rows]; // Array of products for all rows
        private int[] prodCols = new int[cols]; // .. for all columns
        private int[] prodDiag = new int[2]; // .. for all diagonal lines

        private int[,] grid = new int[rows, cols];

        public Xo()
        {
            // Each field is initialized to 1 ( a field that is available )
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {

                    grid[i, j] = 1;

                }
            }

            // Initializing all products to 1
            for (int i = 0; i < rows; ++i) // the condition could also be specified as i < cols, i < 3
            {
                prodRows[i] = 1;
                prodCols[i] = 1;

                //the diagonal Array's length is 2
                if (i < 2)
                {
                    prodDiag[i] = 1;
                }
            }
        }

        public bool isOver()
        {
            /*
            if the x and o symbols have a predefined value, in this case, 2 and 3, respectively
            then we can determine if 3 xs or os have been placed in a single row or column
            by taking the product of those values
            x^3 = 2^3 = 8
            o^3 = 3^3 = 27
            */

            // The values of all products must be refreshed before every cycle
            // because those values accumulate and the final conditions will never evaluate to true

            for (int i = 0; i < rows; ++i) // i<cols or i<3 works as well
            {
                prodRows[i] = 1;
                prodCols[i] = 1;

                if (i < 2)
                {
                    prodDiag[i] = 1;
                }
            }

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    switch (i)
                    {
                        //Row completion
                        case 0:
                            {
                                //First row
                                prodRows[0] *= grid[i, j];
                                break;
                            }
                        case 1:
                            {
                                //Second row
                                prodRows[1] *= grid[i, j];
                                break;
                            }
                        case 2:
                            {   //Third row
                                prodRows[2] *= grid[i, j];
                                break;
                            }
                    }

                    switch (j)
                    {
                        //Column completion
                        case 0:
                            {
                                //First column
                                prodCols[0] *= grid[i, j];
                                break;
                            }
                        case 1:
                            {
                                //Second column
                                prodCols[1] *= grid[i, j];
                                break;
                            }
                        case 2:
                            {
                                //Third column
                                prodCols[2] *= grid[i, j];
                                break;
                            }
                    }

                    //Primary diagonal
                    if (i == j)
                    {
                        prodDiag[0] *= grid[i, j];
                    }

                    //Secondary diagonal
                    if ((i + j) == cols - 1)
                    {
                        prodDiag[1] *= grid[i, j];
                    }
                }
            }

            bool over_rows = false, over_cols = false, over_diag = false;

            for (int i = 0; i < rows; ++i)
            {
                over_rows = (prodRows[i] == 8 || prodRows[i] == 27);
                over_cols = (prodCols[i] == 8 || prodCols[i] == 27);

                if (i < 2)
                {
                    over_diag = (prodDiag[i] == 8 || prodDiag[i] == 27);
                }

                if (over_rows || over_cols || over_diag)
                    break;
            }
            return (over_rows || over_cols || over_diag);
        }

        public void outcome(out string winner, out short clr)
        {
            winner = "";
            clr = 0;

            for (int i = 0; i < rows; ++i) // i<cols i<3 works as well
            {
                if ((prodRows[i] == 8) || (prodCols[i] == 8) || (prodDiag[0] == 8) || (prodDiag[1] == 8))
                {
                    winner = "You Won";
                    clr = 1;
                    break;
                }

                if ((prodRows[i] == 27) || (prodCols[i] == 27) || (prodDiag[0] == 27) || (prodDiag[1] == 27))
                {
                    winner = "The NPC Won";
                    clr = 2;
                    break;
                }

                winner = "DRAW";
                clr = 3;

            }

        }

        //Checks if there are any available fields left
        public bool isFull()
        {
            int counter = 0;

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {

                    if (grid[i, j] == 1)
                        counter++;

                }
            }
            return counter == 0;
        }


        //Checks if a specific field is available
        private bool isAvailable(int r, int c)
        {
            return grid[r, c] == 1;
        }

        public void printGrid()
        {
            Console.WriteLine("\n\nCurrent state:\n");

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    // Column identification C0, C1, C2
                    if (i == 0 && j == 0)
                    {

                        for (int k = 0; k < cols; ++k)
                        {
                            Console.Write("\tC" + k);
                        }

                        Console.WriteLine();
                    }

                    // Row identification R0, R1, R2
                    if (j == 0)
                        Console.Write("R" + i + "\t");

                    switch (grid[i, j])
                    {
                        // Symbol placement X, O , - (available field)
                        case 1:
                            {
                                Console.Write("-\t");
                                break;
                            }
                        case 2:
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("X\t");
                                Console.ResetColor();
                                break;
                            }
                        case 3:
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("O\t");
                                Console.ResetColor();
                                break;
                            }
                    }
                }
                Console.WriteLine();
            }
        }

        public void start(out char choice)
        {
            Console.WriteLine("TIC TAC TOE\n");
            Console.WriteLine("Press \'1\' to start a new game");
            Console.WriteLine("Press \'2\' to exit the game\n");

            Console.Write("Enter your choice: ");
            choice = Console.ReadKey().KeyChar;
        }

        public void run()
        {
            Console.WriteLine("\nChoose a field:");

            bool rowFlag = false;
            bool colFlag = false;

            Console.Write("Row index: ");
            char r = Console.ReadKey().KeyChar;
            int rowIndex = (int)char.GetNumericValue(r);

            if (rowIndex < 0 || rowIndex >= rows)
                rowFlag = true;

            Console.Write("\nColumn index: ");
            char c = Console.ReadKey().KeyChar;
            int columnIndex = (int)char.GetNumericValue(c);

            if (columnIndex < 0 || columnIndex >= cols)
                colFlag = true;


            if (rowFlag || colFlag)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid array index entered.");
                Console.ResetColor();
            }
            else if (!isAvailable(rowIndex, columnIndex))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nField unavailable");
                Console.ResetColor();
            }
            else
            {
                grid[rowIndex, columnIndex] = 2;

                if (!isOver())
                {
                    printGrid();

                    Random rand = new Random();
                    int r1, c1;

                    for (; ; )
                    {
                        r1 = rand.Next(0, rows);
                        c1 = rand.Next(0, cols);

                        if (isAvailable(r1, c1) || isFull())
                            break;

                    }
                    grid[r1, c1] = 3;
                }
            }

        }

    }
}