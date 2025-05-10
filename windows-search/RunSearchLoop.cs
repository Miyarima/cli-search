
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
            List<Result> lastResult = [];
            bool running = true;

            while (running)
            {
                Console.WriteLine("Enter file name to search or 'exit' to quit: ");
                Console.Write("> ");
                string? input = Console.ReadLine()?.Trim().ToLowerInvariant();

                switch (input)
                {
                    case var s when string.IsNullOrEmpty(s):
                        Print(["Invalid input."]);
                        continue;
                    case "help":
                        Help();
                        continue;
                    case "exit":
                        Print(["Exiting..."]);
                        running = false;
                        break;
                }

                if (running) 
                {
                    string[] splitInput = input.Split(" ");

                    if (splitInput[0] == "open")
                    {
                        Open(lastResult, splitInput);
                        continue;
                    }

                    var results = FileHandler.FindFiles(fileTable, input);
                    lastResult = results;

                    if (results.Count == 0)
                    {
                        Print(["No similar file names found."]);
                    }
                    else
                    {
                        DisplayResults(results);
                    }
                }
            }
        }

        private static void Open(List<Result> lastResult, string[] splitInput)
        {
            if (lastResult.Count == 0)
            {
                Print(["Please search before trying to open a file."]);
                return;
            }

            if (splitInput.Length < 2)
            {
                Print(["Please specify the file name to open."]);
                DisplayResults(lastResult);
                return;
            }

            foreach (var result in lastResult)
            {
                if (result.Name.Equals(splitInput[1], StringComparison.InvariantCultureIgnoreCase))
                {
                    FileHandler.OpenFile(result);
                    Print(["File / directory opened"]);
                    return;
                }
            }

            Print([$"No file or directory found named {splitInput[1]}."], new PrintOption{ top = true, bottom = false});
            DisplayResults(lastResult);
        }

        private static void Help() 
        {
            Print([
                "- '<filename>'       |  Will fuzzy search for filenames simulare to the input",
                "- 'Open <filename>'  |  Will open the specified file or directory",
                "- 'Exit'             |  Exits the program",
                "- 'Help'             |  Opens this promt"
            ]);
        }

        private static void DisplayResults(List<Result> results)
        {
            Console.WriteLine(DefaultStrings.separator);
            Console.WriteLine("Files:");
            foreach (Result result in results)
            {
                FileHandler.DisplayResults(result);
            }
            Console.WriteLine(DefaultStrings.separator);
        }

        private static void Print(List<string> list, PrintOption? option = null)
        {
            if (!option.HasValue || option.Value.top)
            {
                Console.WriteLine(DefaultStrings.separator);
            }

            foreach (var str in list) 
            {
                Console.WriteLine(str);
            }

            if (!option.HasValue || option.Value.bottom)
            {
                Console.WriteLine(DefaultStrings.separator);
            }
        }
    }
}
