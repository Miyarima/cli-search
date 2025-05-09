using System.Diagnostics;
using windows_search;

Console.WriteLine($"{new string('=', 50)}\n");
Console.WriteLine("Starting to get files...");
var stopwatch = Stopwatch.StartNew();

var collector = new FileCollector();
var fileTable = collector.Collect();

if (fileTable == null || fileTable.Count == 0)
{
    Console.WriteLine("No files found.");
    return;
}

Console.WriteLine($"total filenames added to dict: {fileTable.Count}");
MemoryEstimator.EstimateDictionarySize(fileTable);

stopwatch.Stop();
Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
Console.WriteLine($"\n{new string('=', 50)}");

var program = new SearchLoop();
SearchLoop.Run(fileTable);
