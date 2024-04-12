import random

def numbers_guessing_game():
    lower_bound = 1
    upper_bound = 1000
    max_attempts = 10

    target_number = random.randint(lower_bound, upper_bound)
    attempts = 0
    
    print("Welcome to Kei's Numbers Guessing Game!")
    while True:
        player_guess = int(input("Enter your guess: "))
        attempts += 1
        
        if player_guess > target_number:
            print("Too high! Try a lower number.")
            
        elif player_guess < target_number:
            print("Too low! Try a higher number.")
            
        else:
            print("Congratulations! You guessed the correct number of {target_number} in {attempts} attempts.")
            break
        
numbers_guessing_game()

