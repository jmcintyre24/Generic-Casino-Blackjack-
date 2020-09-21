using System;
using Blackjack_CL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack_Testing
{
    [TestClass]
    public class CardTest : Deck
    {
        [TestMethod]
        public void Card_Testing()
        {
            Deck testDeck = new Deck();
            Hand testHand = new Hand();

            //Deck is unshuffled
            testHand.AddCard(deckPile[0]);
            testHand.AddCard(deckPile[24]);
            testHand.trackScore();
            Assert.AreEqual(19, testHand.Score);
        }
        [TestMethod]
        public void Card_TestingNumTwo()
        {
            Deck testDeck = new Deck();
            Hand testHand = new Hand();

            //Deck is unshuffled
            testHand.AddCard(deckPile[0]);
            testHand.AddCard(deckPile[24]);
            testHand.AddCard(deckPile[16]);
            testHand.trackScore();
            Assert.AreEqual(19, testHand.Score);
        }

        [TestMethod]
        public void Card_TestingNumThree()
        {
            Deck testDeck = new Deck();
            Hand testHand = new Hand();

            //Deck is unshuffled
            testHand.AddCard(deckPile[0]);
            testHand.AddCard(deckPile[24]);
            testHand.AddCard(deckPile[1]);
            testHand.AddCard(deckPile[2]);
            testHand.AddCard(deckPile[16]);
            testHand.trackScore();
            Assert.AreEqual(21, testHand.Score);
        }
    }
}
