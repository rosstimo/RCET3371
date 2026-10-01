namespace BingoGame
{
    internal class Program
    {
        /* TODO
          [ ] display drawn balls
          [ ] draw a random ball
          [ ] if ball already drawn just draw another
          [ ] don't draw when all balls already drawn
          [ ] let user start a new game any time
          [ ] let user quit at any time
          [ ] 
         */
        static bool[,] ballTracker = new bool[15, 5];

        static void Main(string[] args)
        {
            DisplayBoard();
            //pause
            Console.ReadLine();
        }

        static void DisplayBoard()
        {
            string ballNumber;
            // header

            // iterate through array
            int rows = ballTracker.GetLength(0);
            int cols = ballTracker.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                Console.Write("|");
                for (int col = 0; col < cols; col++)
                {
                    ballNumber = ((rows * col) + row + 1).ToString();
                    Console.Write(ballNumber.PadLeft(2) + "|");
                }
                Console.WriteLine();
            }
        }
    }
}
