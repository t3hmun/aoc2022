<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

// This will be O(6n^2) ~ O(n^2) if it is where n=width=height, the data for this is square shaped.
// Memory is about 2*n^2

string[] lines = File.ReadAllLines("../day-08-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

int height = lines.Count();
int width = lines[0].Length;

new { height, width }.Dump();

// I think a normal [][] might be simpler but [,] implies non-jagged so better meaning and I just want to use it.
// Matrix probably requires one giant chunk of contiguous memory, arrays of arrays probably does not.
// None of this matters.
int[,] matrix = new int[width, height];

bool[,] visible = new bool[width, height];

// These are just out of curiosity
int iterations = 0;
int maxIteration = width * height * 4;


for (int r = 0; r < height; r++)
{
	iterations++;
	string line = lines[r];
	for (int c = 0; c < width; c++)
	{
		iterations++;
		matrix[r, c] = int.Parse(line[c].ToString());
	}
}

bool Core(int r, int c, ref int tallest)
{
	int here = matrix[r, c];
	if (here > tallest)
	{
		tallest = here;
		visible[r, c] = true;
	}

	return tallest == 9; // Nothing can be visible behind the 9, the tallest possible.
}

for (int r = 0; r < height; r++)
{
	//left to right
	int tallest = -1;
	for (int c = 0; c < width; c++)
	{
		iterations++;
		if (Core(r, c, ref tallest)) break;
	}
	// right to left
	tallest = -1;
	for (int c = width - 1; c >= 0; c--)
	{
		iterations++;
		if (Core(r, c, ref tallest)) break;
	}
}


for (int c = 0; c < width; c++)
{
	//top to bottom 
	int tallest = -1;
	for (int r = 0; r < height; r++)
	{
		iterations++;
		if (Core(r, c, ref tallest)) break;
	}
	// bottom to top 
	tallest = -1;
	for (int r = height - 1; r >= 0; r--)
	{
		iterations++;
		if (Core(r, c, ref tallest)) break;
	}
}


matrix.Dump();
visible.Dump();

var totalVisible = Enumerable.Range(0, width).SelectMany(c => Enumerable.Range(0, height).Select(r => visible[r, c])).Count(v => v == true).Dump("Anwser One");
iterations.Dump("part one iterations");

// This is for fun, not needed.
long[,] scores = new long[width, height];
long highScore = 0;
int p2Iterations = 0;


long CalcScore(int r, int c)
{
	bool Core(int here, ref int side, int r, int c)
	{
		p2Iterations++;
		side++;
		int there = matrix[r, c];
		return (there >= here);
	}
	
	var here = matrix[r, c];
	// look right
	int right = 0;
	int left = 0;
	int up = 0;
	int down = 0;
	for (int t = c + 1; t < width; t++) if (Core(here, ref right, r, t)) break;
	for (int t = c - 1; t >= 0; t--) if (Core(here, ref left, r, t)) break;
	for (int t = r + 1; t < height; t++) if (Core(here, ref down, t, c)) break;
	for (int t = r - 1; t >= 0; t--) if (Core(here, ref up, t, c)) break;

	return left * right * up * down;
}

for (int r = 0; r < height; r++)
{
	string line = lines[r];
	for (int c = 0; c < width; c++)
	{
		long score = CalcScore(r, c);
		scores[r, c] = score;
		if (score > highScore) highScore = score;
	}
}

p2Iterations.Dump("part two iterations");
highScore.Dump("Part Two");