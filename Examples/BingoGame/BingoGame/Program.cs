namespace BingoGame
{
    internal class Program
    {
        /* TODO
          [x] display drawn balls
          [x] draw a random ball
          [x] if ball already drawn just draw another
          [ ] don't draw when all balls already drawn
          [ ] let user start a new game any time
          [ ] let user quit at any time
          [ ] 
         */
        static bool[,] ballTracker = new bool[15, 5];

        static void Main(string[] args)
        {
            string userInput = "";
            do
            {
                DrawBall();
                DisplayBoard();
                Console.ReadLine();
            } while (userInput != "Q" || userInput != "q");

            //pause
            Console.ReadLine();
        }

        static void DisplayBoard()
        {
            string ballNumber;
            string[] header = { "B", "I", "N", "G", "O" };
            string seperator = "_";
            
            foreach (string letter in header)
            {
                Console.Write(letter.PadLeft(3));
                seperator += "___";
            }
            Console.WriteLine();
            Console.WriteLine(seperator);
            
            // header

            // iterate through array
            int rows = ballTracker.GetLength(0);
            int cols = ballTracker.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                Console.Write("|");
                for (int col = 0; col < cols; col++)
                {
                    if (ballTracker[row, col])
                    {
                        ballNumber = ((rows * col) + row + 1).ToString();
                    }
                    else
                    {
                        ballNumber = "";
                    }
                        Console.Write(ballNumber.PadLeft(2) + "|");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Place User Prompt Here");
        }

        static void DrawBall()
        {
            Random randy = new Random();
            int row, col;

            do
            {
                row = randy.Next(15);
                col = randy.Next(5);

            } while (ballTracker[row,col]);
            
            ballTracker[row, col] = true;
        }
    }
}
