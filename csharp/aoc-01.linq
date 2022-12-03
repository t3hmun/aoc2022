<Query Kind="Statements" />

Directory.SetCurrentDirectory (Path.GetDirectoryName (Util.CurrentQueryPath));

string data = File.ReadAllText("../day-01-data.txt");

data.Replace("\r", "").Replace("\n\n", "|").Trim().Split('|').Select(e => e.Split("\n").Select(int.Parse).Sum()).Max().Dump("Part 1");
data.Replace("\r", "").Replace("\n\n", "|").Trim().Split('|').Select(e => e.Split("\n").Select(int.Parse).Sum()).OrderByDescending(x => x).Take(3).Sum().Dump("Part 2");


