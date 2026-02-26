using System;
using System.Collections.Generic;
using System.Text;

namespace OOPExample
{
    /*
    TODO:
    [x] create 52 cards 
    [ ] keep cards in a collection (list, stack, que) 
    [ ] deal a card and remove it from the collection 
    [ ] provide number of cards remaining
    [ ] shuffle the deck (refresh/recreate cards in a full deck. maybe randomize)
    */
    internal class Deck
    {
        private List<string> dinosaurs = new List<string>();
        Queue<string> numbers = new Queue<string>();
        Stack<string> letter = new Stack<string>();
        private List<Card> _deck = new List<Card>();

        
        public Deck() 
        {
            for (int suitIndex = 0; suitIndex < 3; suitIndex++)
            {
                for (int rankIndex = 0; rankIndex < 12; rankIndex++)
                {
                    Card currentCard = new Card(rankIndex, suitIndex);
                    this._deck.Add(currentCard);
                    //Console.WriteLine($"{currentCard.Rank()} of {currentCard.Suit()}");
                }
            }
            //this._deck.Shuffle();

            foreach  (Card theCard in this._deck.Shuffle())
            {
                Console.WriteLine($"{theCard.Rank()} of {theCard.Suit()}");
            }
        }
    }
}
