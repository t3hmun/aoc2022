<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath)!);


string[] lines = File.ReadAllLines("../day-08-test.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();