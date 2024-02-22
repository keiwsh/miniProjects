using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zahlenraten
{
    internal class Leaderboard
    {
        const string LeaderboardFile = "leaderboard.json";

        /// <summary>
        /// Lädt das Leaderboar aus der Datenbank und gibt an der aktuellen Stelle der Konsole aus
        /// </summary>
        /// <param name="difficulty"></param>
        public void DisplayLeaderboard(int difficulty = 100)
        {
            var oldForeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            string leaderboardHeader = "********************* Leaderboard *********************";
            Console.SetCursorPosition((Console.WindowWidth - leaderboardHeader.Length) / 2, Console.CursorTop);
            Console.WriteLine(leaderboardHeader);
            Console.ForegroundColor = oldForeColor;



            var leader = GetLeaderboard(difficulty);
            foreach (var item in leader)
            {
                // Console.SetCursorPosition((Console.WindowWidth - item.Length) / 2, Console.CursorTop);
                Console.SetCursorPosition(40, Console.CursorTop);
                Console.WriteLine(item);
            }
        }

        public List<string> GetLeaderboard(int difficulty = 100)
        {
            var test = GetDatabasePath();
            var result = new List<string>();

            var leaderBoard = ReadLeaderboardDb();
            leaderBoard = leaderBoard.Where(x => x.Difficulty == difficulty).ToList();
            leaderBoard.Sort();

            if (leaderBoard.Count < 1)
            {
                result.Add("Sei der Erste!");
                return result;
            }

            for (int i = 0; i <= 5 && i < leaderBoard?.Count; i++)
            {
                //var entry = new LeaderboardEntry() { Difficulty = difficulty, Name = $"Spieler{i}", Trials = i + 1 };
                var entry = leaderBoard[i];
                result.Add($"{i + 1}.\t{entry.Name.PadRight(30)}\t{entry.Trials.ToString().PadLeft(2)}");
                //result[i - 1] = $"{i}.\tSpieler{i}\t{i + 1}";
            }

            return result;

        }

        public string GetDatabasePath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, LeaderboardFile);
            return path;
        }

        public List<LeaderboardEntry>? ReadLeaderboardDb()
        {
            var result = new List<LeaderboardEntry>();

            try
            {
                if (File.Exists(GetDatabasePath()))
                {
                    var stream = new StreamReader(GetDatabasePath());
                    var content = stream.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        result = JsonSerializer.Deserialize<List<LeaderboardEntry>>(content);
                    }
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen der Datenbank!\n\n{ex.Message}");
            }

            return result;
        }

        public void WriteLeaderboardDb(List<LeaderboardEntry> allEntries)
        {
            if (!allEntries.Any()) return;

            try
            {
                var writer = new StreamWriter(GetDatabasePath());
                writer.Write(JsonSerializer.Serialize(allEntries));
                writer.Flush();
                writer.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Schreiben des Leaderboards!\n\n{ex.Message}");
            }

        }


        /// <summary>
        /// Überprüft, ob die aktuellen Spielergebnisse in die High-Score aufgenommen werden sollen.
        /// Falls ja, wird der Highscore für "die Liga" aktualisiert und gespeichert.
        /// 
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="gameData"></param>
        public void UpdateLeaderboard(int difficulty = 100, string username = "", int numberOfTrials = 1000)
        {
            var leaderList = ReadLeaderboardDb();

            var newEntry = new LeaderboardEntry() { Difficulty = difficulty, Name = username, Trials = numberOfTrials };

            leaderList?.Add(newEntry);

            WriteLeaderboardDb(leaderList);

        }


    }
}
