using System.Diagnostics;
using FuzzySharp;

namespace windows_search
{
    public class FileHandler
    {
        /// <summary>
        /// Finds files in the dictionary that match the input string using fuzzy search
        /// </summary>
        /// <param name="files">A Dictionary containing filename and corresponding paths</param>
        /// <param name="input">User input</param>
        /// <param name="results">How many different filenames it should return</param>
        /// <returns></returns>
        public static List<Result> FindFiles(Dictionary<string, List<string>> files, string input, int results = 5)
        {
            return [.. files.Keys
                .Select(name => new Result
                {
                    Name = name,
                    Score = Fuzz.Ratio(input.ToLowerInvariant(), name.ToLowerInvariant()),
                    Paths = files[name]
                })
                .Where(x => x.Score > 70)
                .OrderByDescending(x => x.Score)
                .Take(results)];
        }

        /// <summary>
        /// Displays the results of the search
        /// </summary>
        /// <param name="result">A result object containing the paths corresponding to the specific filename</param>
        public static void DisplayResults(Result result)
        {
            Console.WriteLine($"- {result.Name} (Score: {result.Score}) (Found: {result.Paths.Count})");
            foreach (var path in result.Paths)
            {
                Console.WriteLine($"    {path}");
            }
        }

        /// <summary>
        /// Opens the files if they have high enough score
        /// </summary>
        /// <param name="result">A result object containing the paths corresponding to the specific filename</param>
        /// <param name="score">How close the fuzzy search was to the user input</param>
        public static void OpenFile(Result result, int score = 90)
        {
            if (result.Score > score)
            {
                string filePath = result.Paths[0];
                if (Directory.Exists(filePath) || File.Exists(filePath))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true,
                            Verb = "open"
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to open {filePath}: {ex.Message}");
                    }
                }
            }
        }
    }
}
