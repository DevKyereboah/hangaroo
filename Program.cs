using System;

namespace HangarooGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuePlaying = true;

            while (continuePlaying)
            {
                PlayHangaroo();
                Console.Write("Do you want to play again? (yes/no): ");
                string playAgainInput = Console.ReadLine().ToLower();
                continuePlaying = playAgainInput == "yes";
            }

            Console.WriteLine("Thanks for playing Hangaroo!");
        }

        static void PlayHangaroo()
        {
           

           

            Dictionary<string, string[]> wordLists = new Dictionary<string, string[]>
            {
                { "easy", new string[] { "cat", "dog", "hat", "sun" } },
                { "normal", new string[] { "apple", "banana", "cherry", "grape" } },
                { "hard", new string[] { "elephant", "giraffe", "kangaroo", "rhinoceros" } }
            };

            Console.WriteLine("Welcome to Hangaroo!");
            Console.Write("Choose a difficulty level (easy/normal/hard): ");
            string difficultyLevel = Console.ReadLine().ToLower();

            if (!wordLists.ContainsKey(difficultyLevel))
            {
                Console.WriteLine("Invalid difficulty level. Playing on normal difficulty.");
                difficultyLevel = "normal";
            }

             string[] puzzles = wordLists[difficultyLevel];
            int maxTrials = 4;
            int currentTrials = 0;
            int score = 0;
            int maxHints = 2; // Maximum number of hints
            int remainingHints = maxHints;
            Random random = new Random();
            string currentPuzzle = puzzles[random.Next(puzzles.Length)];
            char[] guessedLetters = new char[currentPuzzle.Length];

            Console.WriteLine($"You've chosen {difficultyLevel} difficulty level.");
            Console.WriteLine("Try to guess the letters of the hidden word.");


            while (currentTrials < maxTrials)
            {
                Console.WriteLine($"Current puzzle: {DisplayPuzzle(currentPuzzle, guessedLetters)}");
                Console.WriteLine($"Remaining trials: {maxTrials - currentTrials}");
                Console.WriteLine($"Remaining hints: {remainingHints}");
                Console.Write("Enter your guess or enter a number for a hint: ");
                string input = Console.ReadLine().ToLower();

                if (input.Length == 1 && char.IsLetter(input[0]))
                {
                    char guess = input[0];

                    if (IsValidGuess(guess, guessedLetters))
                    {
                        bool found = false;
                        for (int i = 0; i < currentPuzzle.Length; i++)
                        {
                            if (currentPuzzle[i] == guess)
                            {
                                guessedLetters[i] = guess;
                                found = true;
                            }
                        }

                        if (found)
                        {
                            Console.WriteLine("Correct guess!");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect guess!");
                            currentTrials++;
                        }

                        if (IsPuzzleSolved(currentPuzzle, guessedLetters))
                        {
                            score += 100;
                            Console.WriteLine($"Congratulations! You solved the puzzle for the word {currentPuzzle}. Current score: {score}");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid guess. Please enter a letter you haven't guessed before.");
                    }
                }
                else if (int.TryParse(input, out int hintNumber) && hintNumber >= 1 && hintNumber <= currentPuzzle.Length && remainingHints > 0)
                {
                    remainingHints--;
                    char hintLetter = currentPuzzle[hintNumber - 1];
                    Console.WriteLine($"Hint: Try guessing the letter '{hintLetter}'.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            if (IsPuzzleSolved(currentPuzzle, guessedLetters))
            {
                Console.WriteLine("You won!");
            }
            else
            {
                Console.WriteLine("You lost! The correct answer was: " + currentPuzzle);
            }
        }

        // ... (previous code)

        static string DisplayPuzzle(string puzzle, char[] guessedLetters)
        {
            string display = "";
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (guessedLetters[i] == '\0')
                {
                    display += "_";
                }
                else
                {
                    display += guessedLetters[i];
                }

                display += " ";
            }
            return display;
        }

        static bool IsValidGuess(char guess, char[] guessedLetters)
        {
            if (!char.IsLetter(guess))
            {
                return false;
            }

            guess = char.ToLower(guess);
            foreach (char letter in guessedLetters)
            {
                if (guess == letter)
                {
                    return false;
                }
            }

            return true;
        }

        static bool IsPuzzleSolved(string puzzle, char[] guessedLetters)
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (puzzle[i] != guessedLetters[i])
                {
                    return false;
                }
            }
            return true;
        }

        static char GetHintLetter(string puzzle, char[] guessedLetters)
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (guessedLetters[i] == '\0')
                {
                    return puzzle[i];
                }
            }
            return '\0'; // No more hints available
        }
    }
}

 
