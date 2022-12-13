<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

string data = File.ReadAllText("../day-11-test.txt").Replace("\r", "");

string[] chunks = data.Split("\n\n");



// Elegant parsing.
Monkeys[] ParseMonkeys() => chunks.Select(c =>
{
	var lines = c.Split("\n");
	var monkey = int.Parse(lines[0].Split(" ")[1].Split(":")[0]); // They are in order, no point in this ><
	var items = lines[1].Split(":")[1].Trim().Split(",").Select(n => long.Parse(n)).ToList();
	var fun = MakeMagic(lines[2]);
	var test = int.Parse(lines[3].Split(" ").Last());
	var trueTo = int.Parse(lines[4].Split(" ").Last());
	var falseTo = int.Parse(lines[5].Split(" ").Last());
	return new Monkeys(monkey, items, fun, test, trueTo, falseTo);

}).ToArray();

void PartOne()
{
	var monkeys = ParseMonkeys();

	for (int i = 0; i < 20; i++)
	{
		foreach (var monkey in monkeys)
		{
			foreach (var old in monkey.Items)
			{
				monkey.Inspections++;

				var worry = Convert.ToInt64(Math.Floor(monkey.Op(old) / 3.0));

				if (worry % monkey.DivBy == 0)
				{
					monkeys[monkey.TrueTo].Items.Add(worry);
				}
				else
				{
					monkeys[monkey.FalseTo].Items.Add(worry);
				}
			}
			monkey.Items.Clear();
		}

		//monkeys.Select(m => $"r{i + 1}  " + m.Pretty()).Dump();
	}

	var top2 = monkeys.Select(m => m.Inspections).OrderByDescending(m => m).Dump().Take(2).ToArray();
	(top2[0] * top2[1]).Dump("Part One");
}





void PartTwo()
{
		var monkeys = ParseMonkeys();

		for (int i = 0; i < 10000; i++)
		{
			foreach (var monkey in monkeys)
			{
				foreach (var old in monkey.Items)
				{
					monkey.Inspections++;

					var worry = Convert.ToInt64(monkey.Op(old));

					if (worry % monkey.DivBy == 0)
					{
						monkeys[monkey.TrueTo].Items.Add(worry);
					}
					else
					{
						monkeys[monkey.FalseTo].Items.Add(worry);
					}
				}
				monkey.Items.Clear();
			}

			//monkeys.Select(m => $"r{i + 1}  " + m.Pretty()).Dump();
		}

		var top2 = monkeys.OrderByDescending(m => m.Inspections).Dump().Select(m => m.Inspections).Take(2).ToArray();
		(top2[0] * top2[1]).Dump("Part Two");
}

PartOne();

PartTwo();

Func<long, long> MakeMagic(string line)
{
	var parts = line.Split("=")[1].Trim().Split(" ");
	long? left = parts[0] == "old" ? null : long.Parse(parts[0]);
	var op = parts[1];
	long? right = parts[2] == "old" ? null : long.Parse(parts[2]);
	return (long old) => Magic(left ?? old, right ?? old, op.Single());
}

long Magic(long left, long right, char sign)
{
	if (sign == '*') return left * right;
	else if (sign == '+') return left + right;
	throw new Exception($"{sign} not handled");
}

record Monkeys(int Num, List<long> Items, Func<long, long> Op, double DivBy, int TrueTo, int FalseTo)
{
	public int Inspections { get; set; } = 0;

	public string Pretty() => $"{Num} {Inspections,8}    {string.Join(",", Items)}";

};