using System;
using System.Collections.Generic;
using System.Text;

namespace OOPExample
{
    internal class Card
    {
        public int RankIndex
        {
            get;
        }
        public int SuitIndex
        {
            get;
        }

        public Card(int rankIndex, int suitIndex)
        {
            this.RankIndex = rankIndex;
            this.SuitIndex = suitIndex;
        }

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

        public string PrettyName()
        {
            if (this.Rank().ToString() == "Joker" | this.Suit().ToString() == "Joker")
            {
                return "Joker";
            }
            else
            {
                return $"{this.Rank()} of {this.Suit()}";
            }
        }

        public string ShortName()
        {
            return $"{this.Rank()[0]}{this.Suit()[0]}";
        }

    }
}
