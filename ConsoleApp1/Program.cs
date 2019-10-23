using System;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            Xo myGame = new Xo();
            myGame.start(out char choice);
            switch (choice)
            {
                case '1':
                    {
                        for (; ; )
                        {
                            myGame.printGrid();

                            if (myGame.isOver() || myGame.isFull())
                            {
                                myGame.outcome(out string winner, out short clr);

                                if (clr == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                }
                                else if (clr == 2)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                                Console.WriteLine("\n" + winner);
                                Console.ResetColor();
                                break;
                            }
                            myGame.run();
                        }
                        break;
                    }
                case '2':
                {
                    Environment.Exit(0);
                    break;
                }
                default:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Entry");
                    Console.ResetColor();

                    break;
                }
            }
            Console.WriteLine("Press Enter to exit" );
            Console.ReadLine();
        }
    }
}
