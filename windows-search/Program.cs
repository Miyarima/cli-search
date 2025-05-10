using System.Diagnostics;
using windows_search;

Console.WriteLine(DefaultStrings.separator);
Console.WriteLine("Starting to get files...");
var stopwatch = Stopwatch.StartNew();

var collector = new FileCollector();
var fileTable = collector.Collect();

if (fileTable == null || fileTable.Count == 0)
{
    Console.WriteLine("No files found.");
    return;
}
else
{
    Console.WriteLine($"total filenames added to dict: {fileTable.Count}");
    MemoryEstimator.EstimateDictionarySize(fileTable);
}

stopwatch.Stop();
Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
Console.WriteLine(DefaultStrings.separator);

SearchLoop.Run(fileTable);
