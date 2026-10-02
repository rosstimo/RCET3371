namespace BingoGame
{
    internal class Program
    {
        /* TODO
          [x] display drawn balls
          [x] draw a random ball
          [x] if ball already drawn just draw another
          [x] don't draw when all balls already drawn
          [ ] let user start a new game any time
          [x] let user quit at any time
          [ ] 
         */
        static bool[,] ballTracker = new bool[15, 5];
        static int ballsDrawn = 0;

        static string userPrompt = "Press Enter to draw a ball";
        static void Main(string[] args)
        {
            string userInput = "";
            do
            {
                DrawBall();
                DisplayBoard();
                userInput = Console.ReadLine();
            } while (userInput != "Q" && userInput != "q");

            //pause
            Console.ReadLine();
        }

        static void DisplayBoard()
        {
            string ballNumber;
            string[] header = { "B", "I", "N", "G", "O" };
            string seperator = "_";
            Console.Clear();
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
            Console.WriteLine(userPrompt);
            Console.WriteLine("Press N to start a new game or Q to quit");
            //Console.WriteLine($"Balls Drawn: {ballsDrawn}");
        }

        static void DrawBall()
        {
            Random randy = new Random();
            int row, col;

            do
            {
                row = randy.Next(15);
                col = randy.Next(5);
            } while (ballTracker[row, col] && ballsDrawn < 75);

            if (ballsDrawn < 75) 
            {
                ballsDrawn++;
                ballTracker[row, col] = true;
            }
            if (ballsDrawn >= 75)
            {
                userPrompt = "All balls drawn!";
            }
        }
    }
}
