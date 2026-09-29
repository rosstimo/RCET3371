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
            // header

            // iterate through array

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Console.Write("Col");
                }
                Console.WriteLine("row");
            }

        }
    }
}
