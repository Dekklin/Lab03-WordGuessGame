using System;
using System.IO;
namespace WordGuessGame
{
    public class Program
    {
        static string path = "../../../WordList.txt";
        // initiates the main menu and creation of the default word bank
        static void Main(string[] args)
        {
            CreateFile(path);
            MainMenu();
        }
        //displays main menu
        static void MainMenu()
        {
            Console.WriteLine("Welcome to my word guess game, Please select an option with 1, 2, or 3");
            Console.WriteLine("1. Play New Game");
            Console.WriteLine("2. Admin View");
            Console.WriteLine("3. Quit game");
            string input = Console.ReadLine();
            while (input != "1" && input !=  "2" && input != "3")
                {
                    Console.WriteLine("Please Select with 1, 2 or 3");
                    input = Console.ReadLine();
                }
            InputHandleMain(input);
            }
        // handles navigation for the user in the main menu
        static void InputHandleMain(string input)
        {
            if(input == "1")
            {
                StartGame();
            }
            if(input == "2")
            {
                AdminView();
            }
            if(input == "3")
            {
                Environment.Exit(0);
            }
        }
        // initiates the start of the game, prompting the user for a letter guess, checks for if the letter is in the word and if it has been used before
        static void StartGame()
        {
            bool finishedGame = false;
            string word = RandomWordSelect();
            char[] randomWord = new char[word.Length];
            char[] guessedLetters = new char[30];
            char[] wrongLetters = new char[7];
            int attempts = 7;
            int correctIndex = 0;
            int wrongIndex = 0;
            for (int i = 0; i < randomWord.Length; i++)
                randomWord[i] = '*';
            string hiddenWord = string.Join("", randomWord);
            Console.WriteLine(randomWord);
            while (!finishedGame && attempts > 0)
            {
                bool newLetter = false;
                Console.WriteLine($"Lives Remaining: {attempts}");
                Console.WriteLine($"Heres your word: {hiddenWord}");
                Console.WriteLine($"Correct Letters Guessed: {string.Join(' ', guessedLetters)}");
                Console.WriteLine($"Incorrect Letters Guessed: {string.Join(' ', wrongLetters)}");

                char letterGuess = LetterGuess();
                if (!string.Join("", guessedLetters).Contains(guessedLetters.ToString()) && !string.Join("", wrongLetters).Contains(letterGuess.ToString()))
                    newLetter = true;
                while(!newLetter)
                {
                    Console.WriteLine("you already guessed that letter!");
                    letterGuess = LetterGuess();
                    if (!string.Join("", guessedLetters).Contains(guessedLetters.ToString()) && !string.Join("", wrongLetters).Contains(letterGuess.ToString()))
                        newLetter = true;
                }
                if (word.Contains(letterGuess.ToString()))
                {
                    Console.WriteLine("Correct");
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == letterGuess)
                            randomWord[i] = letterGuess;
                    }
                    hiddenWord = string.Join("", randomWord);
                    guessedLetters[correctIndex] = letterGuess;
                    correctIndex++;

                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Sorry but your word doesn't contain that letter, you have {attempts} lives left");
                }
                if (!hiddenWord.Contains("*"))
                {
                    finishedGame = true;
                }
                Console.Clear();

            }
                    Console.WriteLine($"Congrats you guessed the correct word! It was {word}");
            Console.WriteLine("Press any button to go back to the main menu");
            Console.ReadKey();
                MainMenu();
        }
        // takes in the user input for a letter, makes sure its just 1 character long, returns it as a lower case.
        public static char LetterGuess()
        {
            Console.WriteLine("Please guess a letter.");
            string letterGuess = "";
            while (letterGuess.Length != 1)
            {
                letterGuess = Console.ReadLine();
                if(letterGuess.Length != 1)
                Console.WriteLine("Please input one letter at a time");
            }
            return Convert.ToChar(letterGuess.ToLower());
        }
        //  randomly selects  word from word bank
        static string RandomWordSelect()
        {
            try
            {
                Random rnd = new Random();
                string[] wordList = File.ReadAllLines(path);
                int rando = rnd.Next(0, wordList.Length-1);
                Console.WriteLine(wordList[rando]);
                return wordList[rando];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Sorry but something went terribly wrong";
            }
        }
        // displays admin view
        static void AdminView()
        {
            Console.WriteLine("Welcome to admin view. What would you like to do?");
            Console.WriteLine("1. View word bank");
            Console.WriteLine("2. Add Word");
            Console.WriteLine("3. Reset word bank");
            Console.WriteLine("4. Back to main menu");
            string input = Console.ReadLine();
            while (input != "1" && input != "2" && input != "3" && input != "4")
            {
                Console.WriteLine("Please Select with 1, 2 or 3");
                input = Console.ReadLine();
            }
            if(input == "1")
            {
                ReadFile(path);
                AdminView();
            }
            if(input == "2")
            {
            Console.WriteLine("What word you like to add to the word bank?");
            string newWord = Console.ReadLine().ToLower();
                AddWord(newWord);
                AdminView();
            }
            if(input == "3")
            {
                DeleteFile(path);
                CreateFile(path);
                Console.WriteLine("Your word bank has now been reset to the default bank");
                AdminView();
            }
            if(input == "4")
            {
                MainMenu();
            }
        }
        // takes an input from the user to add a word to the word bank
        public static void AddWord(string newWord)
        {
            UpdateFile(newWord);
            Console.WriteLine($"{newWord} has now been added to the word bank.");
            AdminView();
        }
        // creates a file if it doesn't exist, executed on loading on the program and when you delete the file
        public static string CreateFile(string path)
        {
            try
            {
                if (!File.Exists(path) && path.Contains(".txt"))
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine("monkey");
                        sw.WriteLine("banana");
                        sw.WriteLine("ocean");
                        sw.WriteLine("coding");
                        sw.WriteLine("trampoline");
                    }
                    return "file created";
                }
                if (!path.Contains(".txt"))
                    return "not a valid .txt file";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "failed to create file";
            }
            return "file exists";
        }
        // Reads the word bank and give posts a list for the user to see
        public static string ReadFile(string path)
        {
        try
            {
                int wordCount = 0;
                string[] wordList = File.ReadAllLines(path);
                foreach (string value in wordList)
                {
                    wordCount++;
                    Console.WriteLine(value);
                }
                Console.WriteLine($"The word bank currently holds {wordCount} words");
                return "read file";
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went terribly wrong");
                Console.WriteLine(e);
                return "file does not exist";
            }
        }
        // updates a word that the user chooses to
        public static string UpdateFile(string word)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(word);
                return "updated file";
            }
        }
        // deletes the .txt file and repopulates it with the default words by calling CreateFile()
        public static string DeleteFile(string path)
        {
            try
            {
            File.Delete(path);
            return "file deleted";
            }
            catch (Exception)
            {
                return "file not found";
            }
        }

    }

}
