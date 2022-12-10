<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

string[] lines = File.ReadAllLines("../day-10-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

int registerX = 1;
int cycle = 0;

int signal = 0;

char[,] canvas = new char[6, 40];
//canvas.Dump();
void AddSignal()
{
	if ((cycle - 20) % 40 == 0 && cycle <= 220)
	{
		signal += cycle * registerX;
	}
}

void WritePixel()
{
	int x = (cycle - 1) / 40;
	int y = (cycle - 1) % 40;
	canvas[x, y] = (y >= (registerX - 1) && y <= (registerX + 1)) ? '#' : '.';
}

foreach (string line in lines)
{
	cycle++;
	AddSignal();
	WritePixel();

	if (line == "noop") continue;

	int num = int.Parse(line.Substring(5));

	cycle++;
	AddSignal();
	WritePixel();
	registerX += num;
}

new { registerX, cycle, signal }.Dump();
canvas.Dump();