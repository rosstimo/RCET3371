namespace OOPExample
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Card myCard = new Card(13,13);
            //Card AS = new Card(1,0);
            //Card AD = new Card(1,1);
            ////myCard.RankIndex = 1; //read only
            
            //Console.WriteLine($"{myCard.Rank()} of {myCard.Suit()}");
            //Console.WriteLine($"{AS.Rank()} of {AS.Suit()}");
            //Console.WriteLine($"{AD.Rank()} of {AD.Suit()}");

            string userInput = "";
            Deck newDeck = new Deck();
            newDeck.Shuffled = false;
            do
            {
                Console.Clear();
                Card currentCard = newDeck.Deal();
                Console.WriteLine(currentCard.PrettyName());
                Console.WriteLine($"There are {newDeck.CardsRemaining()} cards left in the deck!");
                Console.WriteLine(currentCard.ShortName());
                //pause
                userInput = Console.ReadLine();
            } while (userInput.ToLower() != "q");
        }
    }
}
