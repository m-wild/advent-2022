<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "04.input.txt");
var input = File.ReadAllLines(file);


var parse = (string x) => { 
	var split = x.Split('-');
	var from = int.Parse(split[0]);
	var to = int.Parse(split[1]);
	Debug.Assert(from <= to, $"{from} <= {to}");
	return Enumerable.Range(from, to - from + 1).ToHashSet();
};

var elves = input
	.Select(x => (
		elf0: parse(x.Split(',')[0]),
		elf1: parse(x.Split(',')[1])
	))
	.ToList();


var pt1 = elves.Count(x => x.elf0.IsSubsetOf(x.elf1) || x.elf1.IsSubsetOf(x.elf0))
	.Dump();
	
	
var pt2 = elves.Count(x => x.elf0.Overlaps(x.elf1) || x.elf1.Overlaps(x.elf0))
	.Dump();