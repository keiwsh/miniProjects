using System.Reflection.PortableExecutable;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Zahlenraten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintHeader();
            string nameAsk = "Wie heisst Du?";
            Console.SetCursorPosition((Console.WindowWidth - nameAsk.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(nameAsk);
            var username = Console.ReadLine();

            Console.WriteLine("\nRate eine Zahl zwischen 1 bis 100");
            Console.ForegroundColor = ConsoleColor.White;

            do
            {
                Console.Clear();
                PrintHeader();
                PrintPlayer(username);

                // Zufallszahl generieren
                var rnd = new Random();
                int difficultyLevel = 100;

                int numberToGuess = rnd.Next(1, difficultyLevel);
                int nbrTrials = 0;
                int guessedNumber;



                // Schwierigkeitsgrad auswählbar machen (1 -> 1..9, 2 -> 1..100, 3 -> 1..1000)
                // Feedback (im Header?) über den aktuellen Schwierigkeitsgrad = OK
                #region Difficulty
                // ... setzen von Schwierigkeitsgrad
                var chosenDifficulty = -1;
                do
                {
                    string promptDifficultyChoosing = "Schwierigkeitsgrad auswählen (1: bis 10, 2: bis 100, 3: bis 1000)"; 
                    Console.SetCursorPosition((Console.WindowWidth - promptDifficultyChoosing.Length) / 2, Console.CursorTop);
                    Console.WriteLine(promptDifficultyChoosing);
                    chosenDifficulty = GetNumber(1);
                } while (chosenDifficulty < 1 || chosenDifficulty > 3);

                //var chosenDifficulty = GetNumber(1);
                Console.SetCursorPosition(0, 0);
                string difficulty = "";

                if (chosenDifficulty == 1)
                {
                    difficulty = "EASY";
                    difficultyLevel = 10;
                }

                else if (chosenDifficulty == 2)
                {
                    difficulty = "MEDIUM";
                    difficultyLevel = 100;
                }

                else if (chosenDifficulty == 3)
                {
                    difficulty = "HARD";
                    difficultyLevel = 1000;
                }

                var oldForeColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((Console.WindowWidth - difficulty.Length) / 2, Console.CursorTop + 2);
                Console.WriteLine(difficulty);
                Console.ForegroundColor = oldForeColor;


                string easyChoosing = $"Du hast die Schwierigkeitsgradstufe '{chosenDifficulty}' ausgewählt";
                Console.SetCursorPosition((Console.WindowWidth - easyChoosing.Length) / 2, Console.CursorTop + 1);
                Console.WriteLine(easyChoosing);
                string easyText = $"Du musst die Zahl von 1 bis {difficultyLevel} erraten.";
                Console.SetCursorPosition((Console.WindowWidth - easyText.Length) / 2, Console.CursorTop);
                Console.WriteLine(easyText + "\n");
                #endregion Difficulty

                numberToGuess = rnd.Next(1, difficultyLevel);



                var leaderboard = new Leaderboard();
                leaderboard.DisplayLeaderboard(difficultyLevel);

                // Leaderboard - je nach ausgewählem Level
                // Top x Leader pro Level anzeigen (Header?), um zu zeigen, was es zu schlagen gilt
                // Leaderboard.Display()

                // High-Score persistent machen (speichern) und auslesen


                do
                {
                    nbrTrials++;
                    PrintScore(nbrTrials.ToString());

                    Console.Write($"\n \n {nbrTrials}. Versuch: ");
                    guessedNumber = GetNumber((difficultyLevel - 1).ToString().Length);
                    Console.WriteLine("");

                    if (guessedNumber == numberToGuess)
                    {
                        Console.WriteLine("\n");
                    }

                    else if (guessedNumber > numberToGuess)
                    {
                        Console.WriteLine("Die Zahl ist kleiner.");
                    }

                    else if (guessedNumber < numberToGuess)
                    {
                        Console.WriteLine("Die Zahl ist grösser.");
                    }
                } while (guessedNumber != numberToGuess);

                leaderboard.UpdateLeaderboard(difficultyLevel, username, nbrTrials);

                if (nbrTrials < 2)
                {
                    Console.WriteLine($"Wow! Du hast die Zahl in {nbrTrials} Versuchen erraten.");
                }

                else if (nbrTrials < 5)
                {
                    Console.WriteLine($"Klasse! Du hast die Zahl in {nbrTrials} Versuchen erraten.");
                }
                else if (nbrTrials < 10)
                {
                    Console.WriteLine($"Meh, Du hast die Zahl in {nbrTrials} Versuchen erraten.");
                }

                else 
                {
                    Console.WriteLine($"Sehr schwache Leistung, du hast die Zahl in {nbrTrials} Versuchen erraten.");
                }



                // Neustartoption
                Console.WriteLine($"Möchtest du neu starten? Antworte bitte mit Ja oder Nein.");
            } while (Console.ReadLine().ToLower() == "ja");

        }

        #region Helpers
        /// <summary>
        /// Schreibt die aktuelle Anzahl der Versuche in die rechte obere Ecke der Console.
        /// </summary>
        /// <param name="Trials"></param>
        private static void PrintScore(string Trials)
        {
            var oldBackColor = Console.BackgroundColor;
            var oldForeColor = Console.ForegroundColor;
            var oldPosition = Console.GetCursorPosition();

            Console.ForegroundColor = ConsoleColor.Blue;
            string score = "ANZAHL VERSUCHE: " + Trials;
            Console.SetCursorPosition(Console.WindowWidth - score.Length, 0);
            Console.WriteLine(score);

            Console.BackgroundColor = oldBackColor;
            Console.ForegroundColor = oldForeColor;
            Console.SetCursorPosition(oldPosition.Left, oldPosition.Top);
        }



        private static void PrintPlayer(string username)
        {
            var oldBackColor = Console.BackgroundColor;
            var oldForeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            string player = username;
            Console.SetCursorPosition(0,0);
            Console.WriteLine("WILLKOMMEN " + player);

            Console.ForegroundColor = oldForeColor;
            Console.BackgroundColor = oldBackColor;
        }

        private static void PrintHeader()
        {
            string header = "NUMBERS GUESSING GAME";

            var oldBackColor = Console.BackgroundColor;
            var oldForeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((Console.WindowWidth - header.Length) / 2, Console.CursorTop);
            Console.WriteLine(header);

            Console.ForegroundColor = oldForeColor;
            Console.BackgroundColor = oldBackColor;
        }

        private static int GetNumber(int nbrDigits)
        {
            // User-Input abfragen
            int guessedNumber;

            ConsoleKeyInfo key;
            string userInput = "";
            do
            {
                key = Console.ReadKey(true);
                if (key.KeyChar >= 48 && key.KeyChar <= 57)
                {
                    userInput += key.KeyChar;
                    Console.Write(key.KeyChar);
                }

                else if (key.Key == ConsoleKey.Backspace && !string.IsNullOrEmpty(userInput)) 
                {
                    userInput = userInput.Substring(0, userInput.Length - 1);
                    Console.Write("\b \b");
                }


            } while ( (key.Key != ConsoleKey.Enter && userInput.Length != nbrDigits) || userInput.Length < nbrDigits);

            // Double-Digits auto-entering removed.
            //while (key.Key != ConsoleKey.Enter || userInput.Length < nbrDigits) ;  

            guessedNumber = int.Parse(userInput);
            return guessedNumber;
        }

        private static int GetUserInput_old()
        {
            // User-Input abfragen
            int guessedNumber;
            do
            {
                string? userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out guessedNumber))
                {
                    Console.WriteLine("Bitte gib nur Zahlen ein!");
                }

            } while (guessedNumber <= 0);
            return guessedNumber;
        }
        #endregion Helpers
    }
}
