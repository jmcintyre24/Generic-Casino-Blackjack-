using System;
using Blackjack_CL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        //Track Wins/Stalemates/Losses and Games played.
        static double winCount = 0;
        static double lossCount = 0;
        static double stalemateCount = 0;
        static double gamesPlayed = 0;
        static void Main(string[] args)
        {
            //Allows for printing of UTF8 encodec.
            Console.OutputEncoding = Encoding.UTF8;
            string[] menuOptions = { "1. Play Blackjack", "2. Shuffle and Show Deck", "3. Exit" };
            int userChoice = 0;

            bool willExit = false;

            while (!willExit)
            {
                Deck baseDeck = new Deck();
                Hand playerHand = new Hand();
                Hand dealerHand = new Hand();

                //Shuffle the deck before it plays.
                baseDeck.Shuffle();
                //Welcome Message
                Console.Write("Welcome to the ");
                displayTitle();
                ReadChoice("Choice: ", menuOptions, out userChoice);

                //Play game
                if (userChoice == 1)
                {
                    bool playerCondition = true;
                    bool firstTurn = true;
                    bool dealerInitilize = true;
                    bool dealerPlay = false;

                    Console.Clear();
                    //Start blackjack by dealing two cards to each the dealer and player.
                    for (int ndx = 0; ndx < 2; ndx++)
                    {
                        playerHand.AddCard(baseDeck.Draw());
                        dealerHand.AddCard(baseDeck.Draw());
                    }

                    //DrawGame(playerHand, dealerHand, baseDeck, dealerInitilize);
                    string[] emptyArray = new string[] { string.Empty };
                    while (playerCondition)
                    {
                        Console.Clear();
                        DrawGame(playerHand, dealerHand, dealerInitilize, dealerPlay);
                        if ((dealerHand.Score == 21 || playerHand.Score == 21) && firstTurn)
                        {
                            dealerPlay = true;
                            dealerInitilize = false;
                            Console.Clear();
                            DrawGame(playerHand, dealerHand, dealerInitilize, dealerPlay);
                            if (playerHand.Score == 21 && dealerHand.Score == 21)
                            {
                                stalemate("The dealer and player have 21.");
                            }
                            else if (dealerHand.Score == 21)
                            {
                                playerLost("Dealer has 21");
                            }
                            else if (playerHand.Score == 21)
                            {
                                playerWon("Player has 21");
                            }
                            dealerInitilize = true;
                            endScene(ref baseDeck, ref playerHand, ref dealerHand, ref playerCondition, ref firstTurn, emptyArray);
                        }
                        else if (playerHand.Score < 21)
                        {
                            ReadChoice("Choice: ", emptyArray, out int gameplayChoice);
                            //Hitting
                            if (gameplayChoice == 1)
                            {
                                playerHand.AddCard(baseDeck.Draw());
                                firstTurn = false;
                            }
                            //Standing
                            else if (gameplayChoice == 2)
                            {
                                dealerPlay = true;

                                dealerPlayTurn(playerHand, dealerHand, baseDeck);
                                firstTurn = false;
                                endScene(ref baseDeck, ref playerHand, ref dealerHand, ref playerCondition, ref firstTurn, emptyArray);
                            }
                            else
                            {
                                Console.WriteLine("Input 1 or 2.");
                            }
                        }
                        else if (playerHand.Score == 21 && !firstTurn)
                        {
                            Console.WriteLine("21 Attained, standing.");
                            dealerPlay = true;

                            dealerPlayTurn(playerHand, dealerHand, baseDeck);
                            endScene(ref baseDeck, ref playerHand, ref dealerHand, ref playerCondition, ref firstTurn, emptyArray);
                        }
                        else
                        {
                            playerLost("Player has busted!");
                            endScene(ref baseDeck, ref playerHand, ref dealerHand, ref playerCondition, ref firstTurn, emptyArray);
                        }
                    }
                }
                //Shuffle
                else if (userChoice == 2)
                {
                    Console.WriteLine("Shuffled, showing deck.");
                    baseDeck.Shuffle();

                    baseDeck.Display();

                    Console.ReadKey();
                }
                //Exit
                else if (userChoice == 3)
                {
                    willExit = true;
                }
                else
                {
                    Console.Write("You input an invalid solution, try again.");
                }
                Console.Clear();
            }
        }
        //Dealer Logic
        static void dealerPlayTurn(Hand playerHand, Hand dealerHand, Deck baseDeck)
        {
            bool dealerCondition = false;

            while (!dealerCondition)
            {
                DrawGame(playerHand, dealerHand, false, true);
                if (dealerHand.Score < 17)
                {
                    dealerHand.AddCard(baseDeck.Draw());
                }
                else if (dealerHand.Score >= 17 && dealerHand.Score <= 20)
                {
                    if (dealerHand.Score > playerHand.Score)
                    {
                        playerLost("Dealer has a greater value hand without busting.");
                        dealerCondition = true;
                    }
                    else if (dealerHand.Score == playerHand.Score)
                    {
                        stalemate("Dealer and Player has the same score.");
                        dealerCondition = true;
                    }
                    else
                    {
                        playerWon("Player has a greater score then the Dealer");
                        dealerCondition = true;
                    }
                }
                else if (dealerHand.Score == 21)
                {
                    if (dealerHand.Score == playerHand.Score)
                    {
                        stalemate("Both the dealer and player attained 21");
                        dealerCondition = true;
                    }
                    else
                    {
                        playerLost("Dealer attained 21!");
                        dealerCondition = true;
                    }
                }
                //Dealer bust
                else
                {
                    playerWon("Dealer has busted!");
                    dealerCondition = true;
                }
                //Slow computers movements
                Thread.Sleep(1000);
            }
        }

        static void endScene(ref Deck baseDeck, ref Hand playerHand, ref Hand dealerHand, ref bool playerCondition, ref bool firstTurn, string[] emptyArray)
        {
            bool madeChoice = false;
            //Reset the hands and deck then shuffle.
            baseDeck = new Deck();
            baseDeck.Shuffle();
            playerHand.resetHand();
            dealerHand.resetHand();
            Console.Write("\n");

            while (!madeChoice)
            {
                Console.Write("Type 1 to ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("play again");
                Console.ResetColor();
                Console.Write("Type 2 to ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("show statistics");
                Console.ResetColor();
                Console.Write("Type 3 to ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("head back to the menu");
                Console.ResetColor();

                ReadChoice("Choice: ", emptyArray, out int selection);
                //Play Again
                if (selection == 1)
                {
                    for (int ndx = 0; ndx < 2; ndx++)
                    {
                        playerHand.AddCard(baseDeck.Draw());
                        dealerHand.AddCard(baseDeck.Draw());
                    }
                    firstTurn = true;
                    madeChoice = true;
                }
                //Show Statistics
                else if(selection == 2)
                {
                    displayStats();
                }
                //Quit
                else if (selection == 3)
                {
                    playerCondition = false;
                    firstTurn = true;
                    madeChoice = true;
                    Console.WriteLine("\nWould you like to reset the statistics?\n\n1. Reset\n2. Continue");
                    bool validChoice = false;
                    while (!validChoice)
                    {
                        ReadChoice("Choice: ", emptyArray, out int resetOption);
                        if (resetOption == 1)
                        {
                            winCount = 0;
                            stalemateCount = 0;
                            lossCount = 0;
                            gamesPlayed = 0;
                            validChoice = true;
                        }
                        else if (resetOption == 2)
                        {
                            validChoice = true;
                        }
                        else
                        {
                            Console.WriteLine("Please input 1 or 2");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You input an invalid selection.");
                }
            }
        }

        //DISPLAY METHODS BELOW//

        static void playerWon(string reason)
        {
            Console.Write("\n\n\tPlayer has ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("won");
            Console.ResetColor();
            Console.WriteLine("!");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n{reason}");
            Console.ResetColor();
            winCount++;
            gamesPlayed++;
        }

        static void stalemate(string reason)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\tStalemate has occuried");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n{reason}");
            Console.ResetColor();
            stalemateCount++;
            gamesPlayed++;
        }

        static void playerLost(string reason)
        {
            Console.Write("\n\n\tPlayer has ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("lost");
            Console.ResetColor();
            Console.WriteLine("!");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n{reason}");
            Console.ResetColor();
            lossCount++;
            gamesPlayed++;
        }

        //static void printOptions()
        //{
        //    Console.WriteLine("\n\nWould you like to: ");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.Write("\t1. Hit");
        //    Console.ResetColor();
        //    Console.Write(" or ");
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("2. Stand");
        //    Console.ResetColor();
        //}

        static void displayHand(Hand handToDraw, string nameOfHand, int posX, int posY)
        {
            int rePos = 1;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine($"{nameOfHand}");
            Console.Write(" ");

            for (int ndx = 0; ndx < handToDraw.handDeck.Count; ndx++)
            {
                Console.SetCursorPosition(posX, posY + rePos);
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine($"| {handToDraw.handDeck[ndx].Face} - {handToDraw.handDeck[ndx].Suit} |");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
                rePos++;
            }
        }

        static void displayStats()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Wins: {winCount}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Stalemates: {stalemateCount}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Losses: {lossCount}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Games Played: {gamesPlayed}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\nYour Win Ratio:");
            Console.ResetColor();
            Console.WriteLine($"\t{(winCount / (gamesPlayed - stalemateCount)) * 100}%");
            Console.WriteLine("\nPress any key to continue back to selection.");
            Console.ReadKey();
            Console.Clear();
        }

        static void DrawGame(Hand playerHand, Hand dealerHand, bool dealerInitial, bool dealerPlay)
        {

            displayHand(playerHand, "Your Hand: ", 0, 0);
            displayHand(dealerHand, "Dealer's Hand: ", 35, 0);
            //Change the pos of where it draws the score.
            Console.SetCursorPosition(Console.CursorLeft,(playerHand.handDeck.Count + 4));
            Console.Write($"\nHand Score: {playerHand.trackScore()}");
            if (dealerInitial == true)
            {
                Console.SetCursorPosition(35, Console.CursorTop);
                //Console.Write($"Hand Score: {dealerHand.trackScore()}");
                dealerHand.trackScore();
                int storConY = Console.CursorTop;
                Console.SetCursorPosition(36, 1);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(@"|/\/\/\/\|");
                Console.ResetColor();
                Console.SetCursorPosition(36, storConY);
                dealerInitial = false;
            }
            else if (dealerPlay == true)
            {
                Console.SetCursorPosition(35, Console.CursorTop);
                Console.Write($"Hand Score: {dealerHand.trackScore()}");
            }
            Console.WriteLine("\n\nWould you like to: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\t1. Hit");
            Console.ResetColor();
            Console.Write(" or ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("2. Stand");
            Console.ResetColor();
        }

        static void displayTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n");
            Console.WriteLine(@"   ____                      _         ____          _             ");
            Console.WriteLine(@"  / ___| ___ _ __   ___ _ __(_) ___   / ___|__ _ ___(_)_ __   ___  ");
            Console.WriteLine(@" | |  _ / _ \ '_ \ / _ \ '__| |/ __| | |   / _` / __| | '_ \ / _ \ ");
            Console.WriteLine(@" | |_| |  __/ | | |  __/ |  | | (__  | |__| (_| \__ \ | | | | (_) |");
            Console.WriteLine(@"  \____|\___|_| |_|\___|_|  |_|\___|  \____\__,_|___/_|_| |_|\___/ ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ResetColor();
        }
        static void ReadInteger(string prompt, ref int num)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{ prompt}");
            Console.ResetColor();

            while (!int.TryParse(Console.ReadLine(), out num))
            {
                //Console.Write($"\nYou did not make a valid selection!");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{prompt}");
                int conX = Console.CursorLeft;
                Console.Write(new string(' ',Console.WindowWidth - 10));
                Console.SetCursorPosition(conX, Console.CursorTop);
                Console.ResetColor();
            }
        }

        static void ReadChoice(string prompt, string[] options, out int selections)
        {
            for (int ndx = 0; ndx < options.Length; ndx++)
            {
                Console.WriteLine(options[ndx]);
            }

            int userChoice = 0;
            ReadInteger(prompt, ref userChoice);

            selections = userChoice;
        }
    }
}
