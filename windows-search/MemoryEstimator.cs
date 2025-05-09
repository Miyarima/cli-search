
namespace windows_search
{
    public class MemoryEstimator
    {
        /// <summary>
        /// Estimates the in-memory size of a dictionary containing file names and their paths
        /// </summary>
        /// <param name="dict">A Dictionary containing filename and corresponding paths</param>
        public static void EstimateDictionarySize(Dictionary<string, List<string>> dict)
        {
            long estimatedMemory = 0;

            foreach (var kvp in dict)
            {
                estimatedMemory += 20 + kvp.Key.Length * 2;
                estimatedMemory += 24;

                foreach (var item in kvp.Value)
                {
                    estimatedMemory += 20 + item.Length * 2;
                }

                estimatedMemory += 32;
            }

            Console.WriteLine($"Estimated in-memory size: ~{estimatedMemory / (1024 * 1024):N0} MiB");
        }
    }
}