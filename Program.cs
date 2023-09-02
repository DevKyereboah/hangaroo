using System;
using System.Collections.Generic;
using System.Linq;

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

                string[] playAgainConfirmation = {"yes", "YES", "Y", "y", "Yes"}; 
                if (!playAgainConfirmation.Contains(playAgainInput)){
                    continuePlaying = false;
                }
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

            Dictionary<string, string[]> wordListsSummary = new Dictionary<string, string[]>
            {
                { "easy", new string[] { "a small domesticated carnivorous mammal", "a domesticated carnivorous mammal", "a covering for the head", "the star around which the Earth orbits" } },
                { "normal", new string[] { "a fruit with red or yellow skin and firm white flesh", "a long curved fruit with a yellow skin", "a small, round stone fruit that is typically bright or dark red", "a small, sweet fruit with a thin skin" } },
                { "hard", new string[] { "a very large herbivorous mammal with a trunk", "a large African mammal with a very long neck and forelegs", "a large Australian marsupial with powerful hind legs", "a large, thick-skinned animal with one or two horns on its snout" } }
            };


            Console.WriteLine("Welcome to Hangaroo!");
            Console.Write("Choose a level (easy/normal/hard): ");
            string difficultyLevel = Console.ReadLine()!.ToLower();

            if (!wordLists.ContainsKey(difficultyLevel))
            {
                Console.WriteLine("Invalid difficulty level. Playing on normal difficulty.");
                difficultyLevel = "normal";
            }

            string[] puzzles = wordLists[difficultyLevel];
            string[] puzzlesDescription = wordListsSummary[difficultyLevel];
            int maxTrials = 4;
            int currentTrials = 0;
            int score = 0;

            Random random = new Random();
            var randomIndexValue = random.Next(puzzles.Length);
            string currentPuzzle = puzzles[randomIndexValue];
            string currentDescription = puzzlesDescription[randomIndexValue];

            char[] guessedLetters = new char[currentPuzzle.Length];
            HashSet<char> guessedLettersSet = new HashSet<char>();

            Console.WriteLine($"You've chosen {difficultyLevel} difficulty level.");
            Console.WriteLine("Try to guess the letters of the hidden word.");

            while (currentTrials < maxTrials)
            {
                Console.WriteLine($"Puzzle description: {currentDescription}");
                Console.WriteLine($"Current puzzle: {DisplayPuzzle(currentPuzzle, guessedLetters)}");
                Console.WriteLine($"Remaining trials: {maxTrials - currentTrials}");
                Console.Write("Enter your guess(single character and press enter): ");
                string input = Console.ReadLine()!.ToLower();

                if (input.Length == 1 && char.IsLetter(input[0]))
                {
                    char guess = input[0];

                    if (!guessedLettersSet.Contains(guess))
                    {
                        guessedLettersSet.Add(guess);
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
                            Console.WriteLine("Correct guess, enter the next character!");
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
                        Console.WriteLine("You've already guessed this letter. Please enter a new letter.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a single letter.");
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
    }
}
