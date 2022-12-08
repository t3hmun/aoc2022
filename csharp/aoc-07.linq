<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

// this is a mess. this is aoc.

string[] data = File.ReadAllLines("../day-07-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

var tree = BuildTree();
int partOneTotal = 0;

int totalSpace = 70000000;
int unusedSpaceRequired = 30000000;

int totalDataSize = PartOne(tree);
partOneTotal.Dump("Part One");

int spaceNeeded = unusedSpaceRequired - (totalSpace - totalDataSize);

//ew { totalDataSize, spaceNeeded }.Dump();

int partTwo = totalDataSize;

PartTwo(tree);

partTwo.Dump("Part Two");

int PartOne(Dir d)
{
	var below = d.Dirs.Select(sub => PartOne(sub)).Sum();
	var sum = d.Files.Select(f => f.size).Sum();
	var dirTotal = below + sum;

	if (dirTotal < 100000) partOneTotal += dirTotal;

	return dirTotal;
}


int PartTwo(Dir d)
{
	var below = d.Dirs.Select(sub => PartTwo(sub)).Sum();
	var sum = d.Files.Select(f => f.size).Sum();
	var dirTotal = below + sum;

	//new { d.Name, dirTotal, a = dirTotal > spaceNeeded, b = dirTotal < partTwo }.Dump();
	if (dirTotal > spaceNeeded && dirTotal < partTwo) partTwo = dirTotal;

	return dirTotal;
}



Dir BuildTree()
{
	// The data always starts with cd / so we can we know where we start
	var root = new Dir("/", new(), new(), null);
	var currentDir = root;
	foreach (var line in data.Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)))
	{
		if (line.StartsWith("$ ls"))
		{
			continue;
		}

		if (line.StartsWith("$ cd "))
		{
			var dir = line.Substring(5);
			if (dir == "..")
			{
				currentDir = currentDir?.parent ?? throw new Exception("bad structure");
			}
			else
			{
				var match = currentDir.Dirs.FirstOrDefault(d => d.Name == dir);
				if (match != null) currentDir = match;
				else throw new Exception();
			}

		}
		else if (line.StartsWith("dir"))
		{
			var dir = new Dir(line[4..], new(), new(), currentDir);
			if (currentDir.Dirs.Any(d => dir.Name == d.Name)) continue;
			currentDir.Dirs.Add(dir);
		}
		else if (Char.IsNumber(line.First()))
		{
			var file = Fil.From(line);
			if (currentDir.Files.Any(f => file.Name == f.Name)) continue;
			currentDir.Files.Add(file);
		}
		else
		{
			throw new Exception("unknown input");
		}
	}
	return root;
}

record Fil(string Name, int size)
{
	public static Fil From(string line)
	{
		var parts = line.Split(" ");
		return new(parts[1], int.Parse(parts[0]));
	}
}
record Dir(string Name, List<Dir> Dirs, List<Fil> Files, Dir? parent);