
namespace windows_search
{
    public class SearchLoop
    {
        /// <summary>
        /// Runs the search loop, allowing the user to search for files and open them
        /// </summary>
        /// <param name="fileTable">A dictornary with filenames and corresponding paths</param>
        public static void Run(Dictionary<string, List<string>> fileTable)
        {
            string? input = "";
            List<Result> lastResult = [];

            while (input != "exit")
            {
                Console.WriteLine("\nEnter file name to search, open <filename> or 'exit' to quit:");
                input = Console.ReadLine()?.Trim().ToLowerInvariant();
                string[] splitInput = input.Split(" ");

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                if (input == "exit")
                {
                    break;
                }

                if (splitInput[0] == "open")
                {
                    Open(lastResult, splitInput);
                    continue;
                }

                var results = FileHandler.FindFiles(fileTable, input);
                lastResult = results;

                if (results.Count == 0)
                {
                    Console.WriteLine("No similar file names found.");
                }
                else
                {
                    DisplayResults(results);
                }

            }
        }

        private static void Open(List<Result> lastResult, string[] splitInput)
        {
            if (lastResult.Count == 0)
            {
                Console.WriteLine("\nPlease search before trying to open a file.");
                return;
            }

            if (splitInput.Length < 2)
            {
                Console.WriteLine("\nPlease specify the file name to open.");
                DisplayResults(lastResult);
                return;
            }

            foreach (var result in lastResult)
            {
                if (result.Name.Equals(splitInput[1], StringComparison.InvariantCultureIgnoreCase))
                {
                    FileHandler.OpenFile(result);
                    Console.WriteLine("\nFile opened");
                    return;
                }
            }

            Console.WriteLine($"\nNo file or directory found named {splitInput[1] ?? ""}.");
            DisplayResults(lastResult);
        }

        private static void DisplayResults(List<Result> results)
        {
            Console.WriteLine($"\n{new string('=', 50)}\n");
            Console.WriteLine("Files:");
            foreach (Result result in results)
            {
                FileHandler.DisplayResults(result);
            }
            Console.WriteLine($"\n{new string('=', 50)}");
        }
    }
}
