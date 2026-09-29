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
        static void Main(string[] args)
        {
            DisplayBoard();
            //pause
            Console.ReadLine();
        }

        static void DisplayBoard()
        {
            int ballNumber;
            // header

            // iterate through array
    
            for (int row = 1; row <= 15; row++)
            {
                for (int col = 1; col <= 5; col++)
                {
                    Console.Write(col);
                }
                Console.WriteLine(row);
            }

        }
    }
}
