<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

string[] lines = File.ReadAllLines("../day-09-test.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

HashSet<Point> tails = new();
Point h = new(0, 0);
Point t = new(0, 0);
tails.Add(t);
foreach (string line in lines)
{
	var dir = line[0];
	var amount = int.Parse(line.Substring(2));
	for (int i = 0; i < amount; i++)
	{
		h = MoveH(h, dir);
		int xDiff = Math.Abs(h.x - t.x);
		int yDiff = Math.Abs(h.y - t.y);
		if (xDiff < 2 && yDiff < 2)
		{
			// no tail move
		}
		else if (xDiff == 2 && yDiff == 2)
		{
			throw new Exception("impossible move");
		}
		else
		{
			(int x,int y) diff = (h.x - t.x, h.y - t.y);
			t = diff switch
			{
				(2, _) => new(t.x + 1, t.y + diff.y),
				(-2, _) => new(t.x - 1, t.y + diff.y),
				(_, 2) => new(t.x + diff.x, t.y + 1),
				(_, -2) => new(t.x + diff.x, t.y - 1),
				_ => throw new Exception("Impossible h pos relative to t pos")
			};
		}

		tails.Add(t);
	}
}

tails.Count().Dump("part one");

Point MoveH(Point p, char d)
{
	return d switch
	{
		'U' => new(p.x + 1, p.y),
		'D' => new(p.x - 1, p.y),
		'R' => new(p.x, p.y + 1),
		'L' => new(p.x, p.y - 1),
		_ => throw new Exception($"Unknown direciton {d}")
	};
}

record Point(int x, int y);