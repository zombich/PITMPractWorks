using System.Diagnostics;

const string filePath = "server.log";
if (!File.Exists(filePath))
    File.WriteAllLines(filePath, GenerateFakeLogs(100_000));

var sw = Stopwatch.StartNew();
var result = AnalyzeLogs(filePath);
sw.Stop();

Console.WriteLine($"Ошибок: {result.errorCount}, предупреждений: {result.warningCount}");
Console.WriteLine($"Время выполнения: {sw.ElapsedMilliseconds} мс");

static (int errorCount, int warningCount) AnalyzeLogs(string path)
{
    int errorCount = 0;
    int warningCount = 0;

    using var reader = new StreamReader(path);

    string line;
    while ((line = reader.ReadLine()) != null)
        if (line.Contains("ERROR"))
            errorCount++;
        else if (line.Contains("WARNING"))
            warningCount++;

    return (errorCount, warningCount);
}

static IEnumerable<string> GenerateFakeLogs(int count)
{
    var rand = new Random();
    string[] types = { "INFO", "WARNING", "ERROR" };
    for (int i = 0; i < count; i++)
    {
        yield return $"{DateTime.Now:HH:mm:ss} [{types[rand.Next(types.Length)]}] Message {i}";
    }
}


