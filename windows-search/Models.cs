
namespace windows_search
{
    static class DefaultStrings
    {
        public static string separator = $"\n{new string('=', 50)}\n";
    }

    public struct Result
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public List<string> Paths { get; set; }
    }

    public struct PrintOption
    {
        public required bool top { get; set; }
        public required bool bottom { get; set; }
    }
}
