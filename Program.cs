using System;

namespace HangarooGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] puzzles = { "apple", "banana", "cherry" };
            int maxTrials = 4;
            int currentTrials = 0;
            int score = 0;
            int maxHints = 2; // Maximum number of hints
            int remainingHints = maxHints;
            Random random = new Random();
            string currentPuzzle = puzzles[random.Next(puzzles.Length)];
            char[] guessedLetters = new char[currentPuzzle.Length];

            Console.WriteLine("Welcome to Hangaroo!");
            Console.WriteLine("Try to guess the letters of the hidden word.");

            while (currentTrials < maxTrials)
            {
                Console.WriteLine($"Current puzzle: {DisplayPuzzle(currentPuzzle, guessedLetters)}");
                Console.WriteLine($"Remaining trials: {maxTrials - currentTrials}");
                Console.WriteLine($"Remaining hints: {remainingHints}");
                Console.Write("Enter your guess or 'h' for a hint: ");
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
                            Console.WriteLine($"Congratulations! You solved the puzzle. Current score: {score}");
                            currentPuzzle = puzzles[random.Next(puzzles.Length)];
                            guessedLetters = new char[currentPuzzle.Length];
                            currentTrials = 0;
                            remainingHints = maxHints; // Reset hints
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid guess. Please enter a letter you haven't guessed before.");
                    }
                }
                else if (input == "h" && remainingHints > 0)
                {
                    remainingHints--;
                    char hintLetter = GetHintLetter(currentPuzzle, guessedLetters);
                    Console.WriteLine($"Hint: Try guessing the letter '{hintLetter}'.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            Console.WriteLine($"Game Over. Your final score: {score}");
        }

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
