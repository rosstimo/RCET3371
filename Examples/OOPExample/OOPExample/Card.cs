using System;
using System.Collections.Generic;
using System.Text;

namespace OOPExample
{
    /// <summary>
    /// Represents a playing card with a specified rank and suit.
    /// </summary>
    /// <remarks>
    /// Rank indices 0-12 represent Ace through King. Suit indices 0-3 represent Spades, Diamonds, Clubs, and Hearts.
    /// Out-of-range values are treated as jokers.
    /// </remarks>
    internal class Card
    {
        /// <summary>
        /// Gets the zero-based index of the rank represented by this instance. Valid range is 0-12: 0 corresponds to Ace, 1-9 to ranks 2-10, 10 to Jack, 11 to Queen, 12 to King. A value of -1 represents a joker; any value outside 0-12 is treated as a joker.
        /// </summary>
        public int RankIndex
        {
            get;
        }
        /// <summary>
        /// Gets the zero-based index of the suit represented by this instance. 0 corresponds to Spades, 1 to Diamonds, 2 to Clubs, 3 to Hearts. Values outside this range are treated as jokers.
        /// </summary>
        public int SuitIndex
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the Card class with the specified rank and suit indices. If either index is
        /// out of range, the card is treated as a joker.
        /// </summary>
        /// <remarks>If either rankIndex or suitIndex is invalid, both properties are set to -1,
        /// representing a joker card. This constructor allows for easy creation of standard cards or jokers by
        /// specifying appropriate indices.</remarks>
        /// <param name="rankIndex">The zero-based index of the card's rank, where 0 represents the lowest rank and 12 represents the highest.
        /// Specify -1 to indicate a joker. Values outside the range 0–12 will result in a joker.</param>
        /// <param name="suitIndex">The zero-based index of the card's suit, where 0 represents the first suit and 3 represents the last suit.
        /// Specify -1 to indicate a joker. Values outside the range 0–3 will result in a joker.</param>
        public Card(int rankIndex = -1, int suitIndex = -1)
        {
            //validate rank and suit index. If either is out of range, set both to -1 to represent a joker.
            if (rankIndex < 0 || rankIndex > 12 || suitIndex < 0 || suitIndex > 3)
            {
                rankIndex = -1;
                suitIndex = -1;
            }
            //assign properties
            this.RankIndex = rankIndex;
            this.SuitIndex = suitIndex;
        }

        /// <summary>
        /// Returns the name of the suit for the current card.
        /// </summary>
        /// <returns>One of "Spades", "Diamonds", "Clubs", "Hearts", or "Joker" if the suit index is invalid.</returns>
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
        /// Returns the display name of the card rank (e.g., "Ace", "Jack", "2").
        /// </summary>
        /// <remarks>
        /// Maps rank indices to standard playing card names: 0 → "Ace", 1-9 → "2"-"10", 10 → "Jack", 11 → "Queen", 12 → "King".
        /// Invalid indices (< 0 or > 12) return "Joker".
        /// </remarks>
        /// <returns>The rank name as a string.</returns>
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
        /// Returns a human-readable name for the card, formatted as "Rank of Suit" or "Joker" if the card is a joker.
        /// </summary>
        /// <remarks>Use this method to obtain a user-friendly representation of the card for display
        /// purposes, such as in UI elements or logs.</remarks>
        /// <returns>A string representing the card's display name. Returns "Joker" if the card is a joker; otherwise, returns
        /// the rank and suit in the format "Rank of Suit".</returns>
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
        /// Returns a short string representation of the card, consisting of abbreviated rank and suit identifiers.
        /// </summary>
        /// <remarks>Use this method to obtain a concise identifier for a card, suitable for display or
        /// logging where space is limited. The format varies based on the card type:
        /// Standard cards use the first character of rank and suit (e.g., "AS" for Ace of Spades),
        /// 10-rank cards use "10" followed by the suit's first character (e.g., "10H" for 10 of Hearts),
        /// and jokers are represented as "JK".</remarks>
        /// <returns>A string containing the abbreviated card identifier: "JK" for jokers, "10" + suit character 
        /// for tens (e.g., "10S"), or rank character + suit character for all other cards (e.g., "AS", "KD").</returns>
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
