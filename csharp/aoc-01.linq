<Query Kind="Statements" />

Directory.SetCurrentDirectory (Path.GetDirectoryName (Util.CurrentQueryPath));

string data = File.ReadAllText("../day-01-data.txt");

var elfTotals = data.Replace("\r", "")
    .Replace("\n\n", "|")
    .Trim()
    .Split('|')
    .Select(e => e.Split("\n").Select(int.Parse).Sum());

var ansA = elfTotals.Max();
var ansB = elfTotals.OrderByDescending(x => x).Take(3).Sum();

Console.WriteLine($"Answer A: {ansA}, Answer B: {ansB}");
