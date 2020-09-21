using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack_CL
{
    public class Deck
    {
        protected List<Card> deckPile = new List<Card>();

        //Why the extra blanks? To create an even column when displaying the cards.
        string[] Faces = { "A ", "K ", "Q ", "J ", "10", "9 ", "8 ", "7 ", "6 ", "5 ", "4 ", "3 ", "2 " };
        Dictionary<string, int> FacesAndValues = new Dictionary<string, int>();
        string[] Suits = { "\u2660", "\u2663", "\u2665", "\u2666" };
        public Deck()
        {
            //deckPile.Capacity = 52;

            setDictionary();

            for (int ndx = 0; ndx < Faces.Length; ndx++)
            {
                string faceHolder = Faces[ndx];

                for (int index = 0; index < Suits.Length; index++)
                {
                    Card card = new Card();
                    card.Face = faceHolder;
                    card.Suit = Suits[index];

                    deckPile.Add(card);
                }
            }
            //Sets the value per card
            foreach (var card in FacesAndValues)
            {
                for (int ndx = 0; ndx < deckPile.Count; ndx++)
                {
                    if(deckPile[ndx].Face == card.Key)
                    {
                        deckPile[ndx].Value = card.Value;
                    }
                }
            }
        }

        public void Shuffle()
        {
            Card[] deckPileShuffleHolder = new Card[deckPile.Count];

            Random rng = new Random();

            for (int ndx = 0; ndx < deckPile.Count; ndx++)
            {
                int ranNum = rng.Next(ndx + 1);

                deckPileShuffleHolder[ndx] = deckPile[ndx];
                deckPile[ndx] = deckPile[ranNum];
                deckPile[ranNum] = deckPileShuffleHolder[ndx];
            }
        }

        public Card Draw()
        {
            Card hold = new Card();

            //Go through deck until it finds a card not set to null
            for (int ndx = 0; ndx < deckPile.Count; ndx++)
            {
                if (deckPile[ndx] != null)
                {
                    hold = deckPile[ndx];
                    deckPile[ndx] = null;
                    break;
                }
            }

            return hold;
        }

        public void Display()
        {
            for (int ndx = 0; ndx < deckPile.Count; ndx++)
            {
                if (ndx == 0)
                {
                    Console.Write(" ");
                }

                if (ndx % 4 != 3)
                {
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write($"| {deckPile[ndx].Face} - {deckPile[ndx].Suit} |");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"| {deckPile[ndx].Face} - {deckPile[ndx].Suit} |");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
            }
        }

        public void setDictionary()
        {
            int defaultValue = 11;

            for(int ndx = 0; ndx < Faces.Length; ndx++)
            {
                if (defaultValue == 11)
                {
                    FacesAndValues.Add(Faces[ndx], (defaultValue));
                    defaultValue--;
                }
                else if (ndx <= 4 && defaultValue == 10)
                {
                    FacesAndValues.Add(Faces[ndx], (defaultValue));
                }
                else
                {
                    defaultValue--;
                    FacesAndValues.Add(Faces[ndx], (defaultValue));
                }
            }
        }
    }
}
