<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "01.input.txt");
var input = File.ReadAllLines(file);

var cur = 0;
var snacks = new List<int>();

foreach (var item in input.Concat(new[] { "" }))
{
	if (item == "")
	{
		snacks.Add(cur);			
		cur = 0;
		continue;
	}
	
	
	cur += int.Parse(item);
}

var top = snacks.OrderByDescending(c => c).Take(3).Dump();
var total = top.Sum(c => c).Dump();