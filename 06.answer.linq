<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "06.input.txt");
var input = File.ReadAllLines(file);

const int LEN_PKG = 4;
const int LEN_MSG = 14;

var findHeader = (string datastream, int marker) =>
{
	for (var i = 0; i < datastream.Length - marker; i++)
	{
		var packet = datastream.Substring(i, marker);
		if (packet.Distinct().Count() == marker)
		{
			return i + marker;
		}
	}
	return 0;
};

foreach (var datastream in input)
{
	var pt1 = findHeader(datastream, LEN_PKG).Dump();
	var pt2 = findHeader(datastream, LEN_MSG).Dump();
}