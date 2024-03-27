using System;
using System.Collections.Generic;
using System.Linq;

public class BlackjackGame
{
    private List<string> deck;
    private List<string> drawnCards;

    public BlackjackGame()
    {
        deck = Enumerable.Repeat(new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "10", "10", "10" }, 4).SelectMany(x => x).ToList();
        drawnCards = new List<string>();
    }

    private string DrawCard()
    {
        Random random = new Random();
        string newCard = deck[random.Next(deck.Count)];
        while (drawnCards.Contains(newCard))
        {
            newCard = deck[random.Next(deck.Count)];
        }
        drawnCards.Add(newCard);
        return newCard;
    }

    private int ChooseAceValue()
    {
        Console.Write("Do you want your ace to be a 1 or 11? ");
        string aceValueInput = Console.ReadLine();
        int aceValue;
        while (!int.TryParse(aceValueInput, out aceValue) || (aceValue != 1 && aceValue != 11))
        {
            Console.Write("Invalid input. Please enter 1 or 11: ");
            aceValueInput = Console.ReadLine();
        }
        return aceValue;
    }

    private int CalculateTotal(List<string> hand)
    {
        int total = 0;
        foreach (string card in hand)
        {
            if (card == "A")
            {
                int aceValue = ChooseAceValue();
                total += aceValue;
            }
            else
            {
                total += int.Parse(card);
            }
        }
        return total;
    }

    public void PlayGame()
    {
        // Player's hand
        List<string> playerHand = new List<string> { DrawCard(), DrawCard() };
        Console.WriteLine("Your hand is: " + string.Join(", ", playerHand));
        int playerTotal = CalculateTotal(playerHand);
        Console.WriteLine("Your total is: " + playerTotal);

        // Dealer's hand
        List<string> dealerHand = new List<string> { DrawCard(), DrawCard() };
        int dealerTotal = CalculateTotal(dealerHand);

        // Game loop
        while (playerTotal < 21)
        {
            Console.Write("Do you want to hit or stick? ");
            string action = Console.ReadLine().ToLower();
            if (action == "hit")
            {
                string newCard = DrawCard();
                playerHand.Add(newCard);
                Console.WriteLine("You drew: " + newCard);
                playerTotal = CalculateTotal(playerHand);
                Console.WriteLine("Your total is: " + playerTotal);
                if (playerTotal > 21)
                {
                    Console.WriteLine("Bust! You lose.");
                    return;
                }
            }
            else if (action == "stick")
            {
                Console.WriteLine("You chose to stick with a total of " + playerTotal);
                while (dealerTotal < 17)
                {
                    string newCard = DrawCard();
                    dealerHand.Add(newCard);
                    dealerTotal = CalculateTotal(dealerHand);
                }
                Console.WriteLine("Dealer's total is: " + dealerTotal);
                if (dealerTotal > 21 || dealerTotal < playerTotal)
                {
                    Console.WriteLine("Congratulations! You win.");
                }
                else if (dealerTotal > playerTotal)
                {
                    Console.WriteLine("Dealer wins.");
                }
                else
                {
                    Console.WriteLine("It's a draw.");
                }
                return;
            }
            else
            {
                Console.WriteLine("Invalid action. Please choose hit or stick.");
            }
        }
    }

    public static void Main(string[] args)
    {
        BlackjackGame game = new BlackjackGame();
        game.PlayGame();
    }
}
