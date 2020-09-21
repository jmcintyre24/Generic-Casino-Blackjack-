using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack_CL
{
    public class Hand
    {
        private List<Card> _handDeck = new List<Card>();

        public List<Card> handDeck
        {
            get { return _handDeck; }
            set { _handDeck = value; }
        }

        private int _Score = 0;

        public int Score
        {
            get { return _Score; }
            set { _Score = value; }
        }

        public void AddCard(Card card)
        {
            _handDeck.Add(card);
        }

        public int trackScore()
        {
            //Set it back to zero, then reiterate and add all the cards value again.
            _Score = 0;

            for (int ndx = 0; ndx < handDeck.Count; ndx++)
            {
                if (handDeck[ndx] != null)
                {
                    _Score += _handDeck[ndx].Value;
                }
            }
            //Handling Ace
            if (_Score > 21)
            {
                for (int i = 0; i < handDeck.Count; i++)
                {
                    if (handDeck[i].Face == "A ")
                    {
                        _Score -= 10;
                    }
                }
            }

            return _Score;
        }

        public void resetHand()
        {
            _handDeck.Clear();
        }
    }
}
