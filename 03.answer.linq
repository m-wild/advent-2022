<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "03.input.txt");
var input = File.ReadAllLines(file);

var priority = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
	.Select((x, i) => new { x, i })
	.ToDictionary(y => y.x, x => x.i + 1);

var pt1 = input
	.Select(x => {
		Debug.Assert(x.Length % 2 == 0);
		return Enumerable.Intersect(
			x.Substring(0, x.Length / 2),
			x.Substring(x.Length / 2)
		).Single();
	})
	.Sum(x => priority[x])
	.Dump();

var pt2 = input
	.Select((x, i) => new { grp = Math.Ceiling((i + 1) / 3.0), x })
	.GroupBy(x => x.grp)
	.Select(grp => 
		grp.First().x
			.Intersect(grp.Skip(1).First().x)
			.Intersect(grp.Skip(2).First().x)
			.Single()
	)
	.Sum(x => priority[x])
	.Dump();