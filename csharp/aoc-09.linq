<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

string[] lines = File.ReadAllLines("../day-09-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

HashSet<Point> partOneTails = new();
HashSet<Point> partTwoTails = new();
Point h = new(0, 0);
Point partOneT = new(0, 0);
Point[] knots = Enumerable.Range(1,9).Select(n => new Point(0,0)).ToArray();
partOneTails.Add(partOneT);


foreach (string line in lines)
{
	char direction = line[0];
	int repeat = int.Parse(line.Substring(2));
	for (int i = 0; i < repeat; i++)
	{
		h = MoveH(h, direction);
		
		partOneT = MoveTail(h, partOneT);
		partOneTails.Add(partOneT);
		Point head = h;
		for(int j = 0; j < 9; j ++)
		{
			knots[j] = MoveTail(head, knots[j]);
			head = knots[j];
		}
		partTwoTails.Add(knots[8]);
	}
}

partOneTails.Count().Dump("part one");
partTwoTails.Count().Dump("part two");

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


static Point MoveTail(Point h, Point t)
{
	(int x, int y) diff = (h.x - t.x, h.y - t.y);
	return diff switch
	{
		(2, 2) => new(t.x + 1, t.y + 1),
		(2, -2) => new(t.x + 1, t.y - 1),
		(-2, 2) => new(t.x - 1, t.y + 1),
		(-2, -2) => new(t.x - 1, t.y - 1),
		(2, _) => new(t.x + 1, t.y + diff.y),
		(-2, _) => new(t.x - 1, t.y + diff.y),
		(_, 2) => new(t.x + diff.x, t.y + 1),
		(_, -2) => new(t.x + diff.x, t.y - 1),
		_ => t
	};
}

record Point(int x, int y);