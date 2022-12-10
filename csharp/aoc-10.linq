<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);

string[] lines = File.ReadAllLines("../day-10-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

int x = 1;
int cycle = 0;

int signal = 0;

void Update()
{
	if((cycle - 20) % 40 == 0 && cycle <= 220)
	{
		signal += cycle * x;

		//new { x, cycle, signal }.Dump();
	}
}

foreach (string line in lines)
{
	cycle ++;
	Update();
	
	
	if (line == "noop") continue;
	
	int num = int.Parse(line.Substring(5));
	
	cycle ++;
	Update();
	x += num;
}

new {x, cycle, signal}.Dump();