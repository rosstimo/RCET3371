using System;
using System.Collections.Generic;
using System.Text;

namespace OOPExample
{
    /*
    TODO:
    [x] create 52 cards 
    [x] keep cards in a collection (list, stack, que) 
    [x] deal a card and remove it from the collection 
    [x] provide number of cards remaining
    [ ] shuffle the deck (refresh/recreate cards in a full deck. maybe randomize)
    */
    internal class Deck
    {
        private List<string> dinosaurs = new List<string>();
        Queue<string> numbers = new Queue<string>();
        Stack<string> letter = new Stack<string>();
        private List<Card> _deck = new List<Card>();

        //property that determines if the cards are dealt in order or randomly. If true, cards are dealt in random order. If false, cards are dealt in the order they were created.
        public bool Shuffled { get; set; } = false; 
       
        /// <summary>
        /// Initializes a new instance of the Deck class, which creates a standard 52-card deck and shuffles it. 
        /// </summary>
        public Deck() 
        {
            this.Shuffle();
        }

        /// <summary>
        /// Initializes the deck with a full set of cards and marks it as shuffled.
        /// </summary>
        /// <remarks>Call this method to reset the deck to its original state containing all standard
        /// cards. This operation clears any existing cards in the deck before repopulating it. After calling this
        /// method, the deck is ready for dealing or further operations.</remarks>
        public void Shuffle()
        {
            this._deck.Clear();
            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int rankIndex = 0; rankIndex < 13; rankIndex++)
                {
                    Card currentCard = new Card(rankIndex, suitIndex);
                    this._deck.Add(currentCard);
                }
            }
            this.Shuffled = true;
        }

        /// <summary>
        /// Removes and returns a card from the deck. The specific card returned depends on the value of the Shuffled property.
        /// If Shuffled is true, a random card is removed and returned. If Shuffled is false, the cards are removed in the order.
        /// </summary>
        /// <returns>A Card representing the removed card from the deck. If the deck is empty, returns a Joker Card.</returns>
        public Card Deal()
        {
            if (this.Shuffled)
            {
                return this.DealRandom();
            }
            else
            {
                return this.DealOrdered();
            }
        }
        /// <summary>
        /// Removes and returns the top card from the deck.
        /// </summary>
        /// <remarks>Callers should check the returned Card for validity if the deck may be empty. The
        /// method modifies the deck by removing the returned card.</remarks>
        /// <returns>A Card representing the top card of the deck. If the deck is empty, returns a Joker Card.</returns>
        private Card DealOrdered()
        {
            Card temp = new Card(-1, -1);
            if (this._deck.Count > 0)
            {
                temp = this._deck.ElementAt(0);
                this._deck.RemoveAt(0);
            }
            return temp;
        }
        /// <summary>
        /// Removes and returns a random card from the deck.
        /// </summary>
        /// <remarks>The method modifies the deck by removing the returned card. If called when the deck
        /// is empty, the returned card will have invalid or default values. This method is not thread-safe.</remarks>
        /// <returns>A randomly selected card from the deck. If the deck is empty, returns a Joker .</returns>
        private Card DealRandom()
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
