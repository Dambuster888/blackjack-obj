import random

class BlackjackGame:
    def __init__(self):
        self.deck = ['A', '2', '3', '4','5', '6', '7', '8','9', '10', '10', '10', '10'] * 4
        self.drawn_cards = []

    def draw_card(self):
        new_card = random.choice(self.deck)
        while new_card in self.drawn_cards:
            new_card = random.choice(self.deck)
        self.drawn_cards.append(new_card)
        return new_card

    def choose_ace_value(self):
        ace_value = input("Do you want your ace to be a 1 or 11? ")
        while ace_value not in ["1", "11"]:
            ace_value = input("Please enter a 1 or 11: ")
        return int(ace_value)

    def calculate_total(self, hand):
        total = 0
        for card in hand:
            if card == "A":
                ace_value = self.choose_ace_value()
                total += ace_value
            else:
                total += int(card)
        return total

    def play_game(self):
        # Player's hand
        player_hand = [self.draw_card(), self.draw_card()]
        print("Your hand is:", player_hand)
        player_total = self.calculate_total(player_hand)
        print("Your total is:", player_total)

        # Dealer's hand
        dealer_hand = [self.draw_card(), self.draw_card()]
        dealer_total = self.calculate_total(dealer_hand)

        # Game loop
        while player_total < 21:
            action = input("Do you want to hit or stick? ").lower()
            if action == "hit":
                new_card = self.draw_card()
                player_hand.append(new_card)
                print("You drew:", new_card)
                player_total = self.calculate_total(player_hand)
                print("Your total is:", player_total)
                if player_total > 21:
                    print("Bust! You lose.")
                    return
            elif action == "stick":
                print("You chose to stick with a total of", player_total)
                while dealer_total < 17:
                    new_card = self.draw_card()
                    dealer_hand.append(new_card)
                    dealer_total = self.calculate_total(dealer_hand)
                print("Dealer's total is:", dealer_total)
                if dealer_total > 21 or dealer_total < player_total:
                    print("Congratulations! You win.")
                elif dealer_total > player_total:
                    print("Dealer wins.")
                else:
                    print("It's a draw.")
                return
            else:
                print("Invalid action. Please choose hit or stick.")

game = BlackjackGame()
game.play_game()
