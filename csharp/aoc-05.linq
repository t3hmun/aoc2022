<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

// This doesn't look nice but it works.

string[] data = File.ReadAllLines("../day-05-test.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

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


var p1 = new Stack<char>[stackCount];
var p2 = new Stack<char>[stackCount];

for (int j = 0; j < lists.Length; j++)
{
	var p1Stack = new Stack<char>();
	var p2Stack = new Stack<char>();
	p1[j] = p1Stack;
	p2[j] = p2Stack;
	lists[j].Reverse();
	foreach (var crate in lists[j])
	{
		p1Stack.Push(crate);
		p2Stack.Push(crate);
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
		var crate = p1[source - 1].Pop();
		p1[target - 1].Push(crate);
	}
	var popped =  new List<char>();
	for (int j = 0; j < quantity; j++)
	{
		var crate = p2[source - 1].Pop();
		popped.Add(crate);
	}
	popped.Reverse();
	foreach (var crate in popped) {
		p2[target - 1].Push(crate);
	}
	//stacks.Dump($"{i}");
}

var ans1 = "";

foreach (var s in p1)
{
	var crate = s.Peek();
	ans1 += crate;
}

var ans2 = "";

foreach (var s in p2)
{
	var crate = s.Peek();
	ans2 += crate;
}
//stacks.Dump();

ans1.Dump("Part One");
ans2.Dump("Part Two");