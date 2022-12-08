<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "07.input.txt");
var input = File.ReadAllLines(file);

var cwd = new Stack<string>();
var fs = new Dictionary<string, Dir> { [""] = new Dir() };

for (var i = 0 ; i < input.Length; i++)
{
	switch (input[i].Split(' '))
	{
		case ["$", "cd", "/"]:
			cwd.Clear();
			break;
			
		case ["$", "cd", ".."]:
			cwd.Pop();
			break;
			
		case ["$", "cd", var dir]:
			cwd.Push(dir);
			break;

		case ["$", "ls"]:
			var path = string.Join("/", cwd.Reverse());
			while (i + 1 < input.Length && !input[i + 1].StartsWith('$'))
				switch (input[++i].Split(' '))
				{
					case ["dir", var p]:
						var dir = new Dir();
						fs[path].Children.Add(dir);
						fs[string.Join("/", path, p).TrimStart('/')] = dir;
						break;
					case [var size, _]:
						fs[path].Size += long.Parse(size);
						break;
				}
			break;
	}
}

var pt1 = fs
	.Where(x => x.Value.TotalSize <= 100_000)
	.Sum(x => x.Value.TotalSize)
	.Dump("pt1");

var DISK_TOTAL = 70_000_000;
var DISK_REQ = 30_000_000;

var available = DISK_TOTAL - fs[""].TotalSize;
var needToFree = DISK_REQ - available;

var pt2 = fs
	.OrderBy(x => x.Value.TotalSize)
	.First(x => x.Value.TotalSize >= needToFree)
	.Value.TotalSize
	.Dump("pt2");


var stats = fs.Select(kv => new { Path = "/" + kv.Key, kv.Value.TotalSize }).Dump();

class Dir
{
	public long Size = 0;
	public List<Dir> Children = new();
	public long TotalSize => Size + Children.Sum(c => c.TotalSize);
}