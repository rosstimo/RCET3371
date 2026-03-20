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
    [x] shuffle the deck (refresh/recreate cards in a full deck. maybe randomize)
    */

    /// <summary>
    /// Represents a standard deck of playing cards, providing functionality to shuffle, deal cards, and track the
    /// number of cards remaining.
    /// </summary>
    /// <remarks>The deck is initialized with 52 cards and can be shuffled or reset to its original state.
    /// Cards can be dealt either in order or randomly, depending on the value of the DealRandomly property. The class is
    /// not thread-safe and is intended for single-threaded use. Use the CardsRemaining method to check how many cards
    /// are left before dealing.</remarks>
    internal class Deck
    {
        private List<Card> _deck = new List<Card>();
        private readonly Random random = new Random(); // Single instance to ensure unique random numbers

        // If true, cards are dealt in random order; if false, cards are dealt in creation order.
        public bool DealRandomly { get; set; } = false;
       
        /// <summary>
        /// Creates a new deck and shuffles it.
        /// </summary>
        public Deck() 
        {
            this.Shuffle();
        }

        /// <summary>
        /// Resets the deck to 52 cards in standard order and enables random dealing.
        /// </summary>
        /// <remarks>Clears the deck and repopulates it. Sets DealRandomly to true.</remarks>
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
            this.DealRandomly = true;
        }

        /// <summary>
        /// Removes and returns a card. If DealRandomly is true, returns a random card; otherwise, returns the top card.
        /// </summary>
        /// <remarks>Call CardsRemaining() to check if the deck is not empty before dealing.</remarks>
        /// <returns>The removed card, or a Joker card if the deck is empty.</returns>
        public Card Deal()
        {
            if (this.DealRandomly)
            {
                return this.DealRandom();
            }
            else
            {
                return this.DealOrdered();
            }
        }
        /// <summary>
        /// Removes and returns the top card.
        /// </summary>
        /// <returns>The top card, or a Joker card if the deck is empty.</returns>
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
        /// Removes and returns a random card.
        /// </summary>
        /// <returns>A random card, or a Joker card if the deck is empty.</returns>
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
        /// <summary>
        /// Returns the number of cards left in the deck.
        /// </summary>
        /// <returns>The count of cards remaining.</returns>
        public int CardsRemaining()
        {
            return this._deck.Count;
        }

        /// <summary>
        /// Returns a random integer between 0 and max (inclusive).
        /// </summary>
        /// <param name="max">Maximum value (inclusive).</param>
        /// <returns>Random integer between 0 and max.</returns>
        private int RandomNumberZeroTo(int max)
        {
            return random.Next(0, max + 1);
        }

    }
}
