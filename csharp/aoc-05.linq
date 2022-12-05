<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

// This doesn't look nice but it works.

string[] data = File.ReadAllLines("../day-05-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

int stackCount = (data.First().Length + 1) / 4;
stackCount.Dump();
var lists = new List<char>[stackCount];

for (int x = 0; x < stackCount; x++)
{
	lists[x] = new List<char>();
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
			lists[j].Add(line[idx]);
		}
	}
}


var stacks = new Stack<char>[stackCount];

for (int j = 0; j < lists.Length; j++)
{
	var stack = new Stack<char>();
	stacks[j] = stack;
	lists[j].Reverse();
	foreach (var crate in lists[j])
	{
		stack.Push(crate);
	}
}

i++; // skip blank line

var rg = new Regex(@"move (\d+) from (\d+) to (\d+)");

for (; i < data.Length; i++)
{
	var line = data[i];
	if (string.IsNullOrWhiteSpace(line)) break;
	var match = rg.Match(line);
	var quantity = int.Parse(match.Groups[1].Value);
	var source = int.Parse(match.Groups[2].Value);
	var target = int.Parse(match.Groups[3].Value);

	for (int j = 0; j < quantity; j++)
	{
		var crate = stacks[source - 1].Pop();
		stacks[target - 1].Push(crate);
	}
	//stacks.Dump($"{i}");
}

var result = "";

foreach (var s in stacks)
{
	var crate = s.Peek();
	result += crate;
}

//stacks.Dump();

result.Dump("Part One");