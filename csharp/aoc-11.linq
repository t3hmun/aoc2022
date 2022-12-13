<Query Kind="Statements">
  <Namespace>System.Numerics</Namespace>
</Query>

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

string data = File.ReadAllText("../day-11-test.txt").Replace("\r", "");

string[] chunks = data.Split("\n\n");



// Elegant parsing.
Monkeys[] ParseMonkeys() => chunks.Select(c =>
{
	var lines = c.Split("\n");
	var monkey = int.Parse(lines[0].Split(" ")[1].Split(":")[0]); // They are in order, no point in this ><
	var items = lines[1].Split(":")[1].Trim().Split(",").Select(n => BigInteger.Parse(n)).ToList();
	var fun = MakeMagic(lines[2]);
	var test = BigInteger.Parse(lines[3].Split(" ").Last());
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

				var worry = monkey.Op(old) / new BigInteger(3);

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
	checked
	{
		var monkeys = ParseMonkeys();

		for (int i = 0; i < 10000; i++)
		{
			foreach (var monkey in monkeys)
			{
				foreach (var old in monkey.Items)
				{
					monkey.Inspections++;

					var worry = monkey.Op(old);

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

			if (new[] { 1, 20, 50, 100, 500, 1000, 2000 }.Contains(i + 1)) monkeys.Select(m => $"r{i + 1} {m.Inspections}").Dump();
		}

		var top2 = monkeys.OrderByDescending(m => m.Inspections).Dump().Select(m => m.Inspections).Take(2).ToArray();
		(top2[0] * top2[1]).Dump("Part Two");
	}
}

PartOne();

PartTwo();

Func<BigInteger, BigInteger> MakeMagic(string line)
{
	var parts = line.Split("=")[1].Trim().Split(" ");
	BigInteger? left = parts[0] == "old" ? null : BigInteger.Parse(parts[0]);
	var op = parts[1];
	BigInteger? right = parts[2] == "old" ? null : BigInteger.Parse(parts[2]);
	return (BigInteger old) => Magic(left ?? old, right ?? old, op.Single());
}

BigInteger Magic(BigInteger left, BigInteger right, char sign)
{
	if (sign == '*') return left * right;
	else if (sign == '+') return left + right;
	throw new Exception($"{sign} not handled");
}

record Monkeys(int Num, List<BigInteger> Items, Func<BigInteger, BigInteger> Op, BigInteger DivBy, int TrueTo, int FalseTo)
{
	public int Inspections { get; set; } = 0;

	public string Pretty() => $"{Num} {Inspections,8}    {string.Join(",", Items)}";

};