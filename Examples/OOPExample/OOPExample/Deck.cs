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
            this.CreateCards();
            //this._deck.Shuffle();


        }

        private void CreateCards()
        {
            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int rankIndex = 0; rankIndex < 13; rankIndex++)
                {
                    Card currentCard = new Card(rankIndex, suitIndex);
                    this._deck.Add(currentCard);
                    //Console.WriteLine($"{currentCard.Rank()} of {currentCard.Suit()}");
                }
            }
        }
        /// <summary>
        /// Removes and returns the top card from the deck.
        /// </summary>
        /// <remarks>Callers should check the returned Card for validity if the deck may be empty. The
        /// method modifies the deck by removing the returned card.</remarks>
        /// <returns>A Card representing the top card of the deck. If the deck is empty, returns a Joker Card.</returns>
        public Card Deal()
        {
            Card temp = new Card(-1, -1);
            if (this._deck.Count > 0)
            {
                temp = this._deck.ElementAt(0);
                this._deck.RemoveAt(0);
            }
            return temp;
        }
        public Card DealRandom()
        {
            int randomCard = 0;
            Card temp = new Card(-1, -1);
            if (this._deck.Count > 0)
            {
                randomCard = RandomNumberZeroTo(this._deck.Count - 1);
                temp = this._deck.ElementAt(randomCard);
                this._deck.RemoveAt(randomCard);
            }
            return temp;
        }
        public int CardsRemaining()
        {
            return this._deck.Count;
        }
        private int RandomNumberZeroTo(int max)
        {
            //int _random = 0;
            //random number 0 to max inclusive
            Random _random = new Random();
            return _random.Next(0, max + 1);
        }

    }
}
