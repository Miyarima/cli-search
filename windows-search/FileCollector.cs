
namespace windows_search
{
    public class FileCollector
    {
        private readonly Dictionary<string, List<string>> fileTable;

        public FileCollector()
        {
            fileTable = [];
        }

        /// <summary>
        /// Collects files from the users' drives and stores them in a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> Collect()
        {
            string[] drives = Directory.GetLogicalDrives();

            foreach (var drive in drives)
            {
                Console.WriteLine($"Getting files from {drive}");
                GetFiles(drive);
            }

            return fileTable;
        }

        private void GetFiles(string path)
        {
            try
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    string name = Path.GetFileName(file);

                    if (!fileTable.TryGetValue(name, out List<string>? value))
                    {
                        value = [];
                        fileTable[name] = value;
                    }

                    value.Add(file);
                }

                foreach (var dir in Directory.GetDirectories(path))
                {
                    string name = Path.GetFileName(dir);

                    if (!fileTable.TryGetValue(name, out List<string>? value))
                    {
                        value = [];
                        fileTable[name] = value;
                    }

                    value.Add(dir);
                    GetFiles(dir);
                }
            }
            catch (Exception)
            {
                // skipping unauthorized directories
            }
        }
    }
}
