<Query Kind="Statements">
  <Namespace>System.Numerics</Namespace>
</Query>

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

string[] lines = File.ReadAllLines("../day-12-test.txt").Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

int xLen = lines[0].Length;
int yLen = lines.Length;

int[,] grid = new int[yLen, xLen];
int[,] big = new int[(yLen*2)-1, (xLen*2)-1];
Point start = new(0, 0);
Point end;

for (int y = 0; y < yLen; y++)
{
	string line = lines[y];
	for (int x = 0; x < xLen; x++)
	{
		var ch = line[x];
		if (ch == 'S') ch = 'a';
		else if (ch == 'E') 
		{
			end = new(x,y);
			ch = 'z';
		}

		grid[y, x] = ch - 'a';
		big[y*2, x*2] = ch - 'a';
	}
}


for (int y = 0; y < yLen; y++)
{
	string line = lines[y];
	for (int x = 0; x < xLen; x++)
	{
		int X = x*2;
		int Y = y*2;
		
		
	}
}
		grid.Dump();
big.Dump();

record Point(int x, int y);