<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

string[] data = File.ReadAllLines("../day-03-test.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

data.Where(line => !string.IsNullOrWhiteSpace(line)).Select(line =>
{
	var half = line.Length / 2;
	// New C# feature ranges :)
	var left = line[..half];
	var right = line[half..];

	var both = left.Where(l => right.Contains(l)).Distinct().ToArray();

	var priority = both.Select(b => b switch
	{
		>= 'a' and <= 'z' => ((int)b - ('a' - 1)),
		>= 'A' and <= 'Z' => ((int)b - ('A' - 27)),
		_ => throw new Exception($"Invalid input: {b}")
	}).Sum();

	//new { line, left, right, priority }.Dump();
	return priority;
}).Sum().Dump("Part one");



var lines = data.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

var groups = new List<string[]>();

for (int start = 0; start < lines.Length; start += 3) {
	int end = start + 3;
	var group = lines[start..end];
	var item = group[0].Where(b => group[1].Contains(b)).Where(b => group[2].Contains(b)).Distinct().Single();
}



groups.Dump();