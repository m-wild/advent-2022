<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

Util.HtmlHead.AddStyles("body { font-family: 'Berkeley Mono', monospace; }");

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "10.input.txt");
var input = File.ReadAllLines(file);

Pt1(input);
Console.WriteLine();

Pt2(input);

static void Pt2(string[] input)
{
	int x = 1, op = 0, instruction = 0;
	for (var v = 1; v <= 6; v++)
	{
		for (var h = 0; h <= 39; h++)
		{
			var lit = h >= x - 1 && h <= x + 1;
			Console.Write(lit ? "#" : " ");

			switch (input[instruction].Split(' '))
			{
				case ["addx", var i]:
					if (op++ == 1)
					{
						x += int.Parse(i);

						op = 0;
						instruction++;
					}
					break;

				case ["noop"]:
					instruction++;
					break;
			}
		}
		Console.WriteLine();
	}
}

static void Pt1(string[] input)
{
	var x = 1;

	var samples = new Dictionary<int, int>
	{
		[20] = 0,
		[60] = 0,
		[100] = 0,
		[140] = 0,
		[180] = 0,
		[220] = 0
	};

	var op = 0;
	for (int cycle = 1, instruction = 0; cycle <= samples.Keys.Max() && instruction < input.Length; cycle++)
	{
		if (samples.ContainsKey(cycle))
		{
			samples[cycle] = x;
		}

		switch (input[instruction].Split(' '))
		{
			case ["addx", var i]:
				if (op++ == 1)
				{
					x += int.Parse(i);

					op = 0;
					instruction++;
				}
				break;

			case ["noop"]:
				instruction++;
				break;
		}
	}
	
	samples.Dump().Sum(x => x.Key * x.Value).Dump();
}
