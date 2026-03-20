using System;
using System.Collections.Generic;
using System.Text;

namespace OOPExample
{
    /// <summary>
    /// Represents a playing card. Rank indices 0–12: Ace–King. Suit indices 0–3: Spades, Diamonds, Clubs, Hearts. Out-of-range values are jokers.
    /// </summary>
    internal class Card
    {
        /// <summary>
        /// Gets the zero-based rank index (0–12: Ace–King, -1: Joker).
        /// </summary>
        public int RankIndex
        {
            get;
        }
        /// <summary>
        /// Gets the zero-based suit index (0–3: Spades, Diamonds, Clubs, Hearts, -1: Joker).
        /// </summary>
        public int SuitIndex
        {
            get;
        }

        /// <summary>
        /// Initializes a card. Out-of-range indices create a joker.
        /// </summary>
        /// <param name="rankIndex">Rank index (0–12: Ace–King, -1: Joker).</param>
        /// <param name="suitIndex">Suit index (0–3: Spades, Diamonds, Clubs, Hearts, -1: Joker).</param>
        public Card(int rankIndex = -1, int suitIndex = -1)
        {
            // If either index is out of range, set both to -1 for joker.
            if (rankIndex < 0 || rankIndex > 12 || suitIndex < 0 || suitIndex > 3)
            {
                rankIndex = -1;
                suitIndex = -1;
            }
            // Assign properties
            this.RankIndex = rankIndex;
            this.SuitIndex = suitIndex;
        }

        /// <summary>
        /// Returns the suit name or "Joker".
        /// </summary>
        /// <returns>Suit name or "Joker".</returns>
        public string Suit()
        {
            string _suit = "";  
            switch (this.SuitIndex)
            {
                case 0:
                    _suit = "Spades";
                    break;
                case 1:
                    _suit = "Diamonds";
                    break;
                case 2:
                    _suit = "Clubs";
                    break;
                case 3:
                    _suit = "Hearts";
                    break;
                default:
                    _suit = "Joker";
                    break;
            }
            return _suit;
        }

        /// <summary>
        /// Returns the rank name or "Joker".
        /// </summary>
        /// <returns>Rank name or "Joker".</returns>
        public string Rank()
        {
            string _rank = "";
            switch (this.RankIndex)
            {   
                case 0:
                    _rank = "Ace";
                    break;
                case 10:
                    _rank = "Jack";
                    break;
                case 11:
                    _rank = "Queen";
                    break;
                case 12:
                    _rank = "King";
                    break;
                case > 12:
                    _rank = "Joker";
                    break;
                case < 0:
                    _rank = "Joker";
                    break;
                default://2 through 10
                    _rank = (this.RankIndex + 1).ToString();
                    break;
            }
            return _rank;
        }

        /// <summary>
        /// Returns "Rank of Suit" or "Joker".
        /// </summary>
        /// <returns>Card display name.</returns>
        public string PrettyName()
        {
            if (this.Rank().ToString() == "Joker" || this.Suit().ToString() == "Joker")
            {
                return "Joker";
            }
            else
            {
                return $"{this.Rank()} of {this.Suit()}";
            }
        }

        /// <summary>
        /// Returns "JK" for jokers, "10" + suit for tens, or rank/suit initials.
        /// </summary>
        /// <returns>Abbreviated card identifier.</returns>
        public string ShortName()
        {
            if (this.Rank().ToString() == "Joker" || this.Suit().ToString() == "Joker")
            {
                return "JK";
            }
            if (this.Rank().ToString() == "10")
            {
                return $"10{this.Suit()[0]}";
            }
            return $"{this.Rank()[0]}{this.Suit()[0]}";
        }

    }
}
