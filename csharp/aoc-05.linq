<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

string[] data = File.ReadAllLines("../day-05-test.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();


int stackCount = (data.First().Length + 1) / 4;
stackCount.Dump();
var stacks = new List<char>[stackCount];

for (int x = 0; x < stackCount; x++)
{
	stacks[x] = new List<char>();
}


int i;
for (i = 0; i < data.Length; i++)
{
	var line = data[i];
	if (line.StartsWith(" 1")) break;

	for (int j = 0; j < stackCount; j++)
	{
		int idx = (j * 4) + 1;
		var crate = line[idx];
		if (crate != ' ')
		{
			stacks[j].Add(line[idx]);
			Console.WriteLine($"item {line[idx]} i: {i} j: {j}");
		}
	}
}

stacks.Dump();