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

            Deck newDeck = new Deck();
            Card currentCard = newDeck.Deal();
            Console.WriteLine($"{currentCard.Rank()} of {currentCard.Suit()}");
            Console.WriteLine($"There are {newDeck.CardsRemaining()} cards left in the deck!");
            Console.WriteLine(currentCard.ShortName());
            //pause
            Console.Read();
        }
    }
}
