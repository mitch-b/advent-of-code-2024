using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Utils.Classes;

namespace AdventOfCode.Solutions.Year2024.Day08;

class Solution : SolutionBase
{
    private readonly Regex antennaMatch = new(@"[a-zA-Z0-9]", RegexOptions.Compiled);
    private readonly string[][] puzzle;
    public Solution() : base(08, 2024, "Resonant Collinearity", false) 
    {
        puzzle = Input.SplitToMatrix<string>();
    }

    protected override string SolvePartOne()
    {
        var antinodePositions = new List<Coordinate>();
        foreach (var y in Enumerable.Range(0, puzzle.Length))
        {
            foreach (var x in Enumerable.Range(0, puzzle[y].Length))
            {
                var xy = new Coordinate(x, y);
                var item = puzzle.GetXY(xy);
                if (antennaMatch.IsMatch(item))
                {
                    foreach (var y2 in Enumerable.Range(0, puzzle.Length))
                    {
                        foreach (var x2 in Enumerable.Range(0, puzzle[y2].Length))
                        {
                            var xy2 = new Coordinate(x2, y2);
                            if (xy == xy2) // skip same antenna
                            //if (xy.X == xy2.X && xy.Y == xy2.Y) // skip same antenna
                                continue;
                            var item2 = puzzle.GetXY(xy2);
                            if (item == item2)
                            {
                                var adjacentCoordinates = GetAdjacentCoordinates(xy, xy2);
                                foreach (var adjacentCoordinate in adjacentCoordinates)
                                {
                                    if (adjacentCoordinate.InBounds(puzzle))
                                    {
                                        antinodePositions.Add(adjacentCoordinate);
                                    }
                                }
                                break;
                            }
                        }
                    }

                }
            }
        }

        // if (Debug)
        // {
        //     PrintPuzzle(antinodePositions, false);
        //     PrintPuzzle(antinodePositions, true);
        // }

        var distinctAntinodes = antinodePositions
            .Distinct()
            .OrderBy(c => c.Y)
            .ThenBy(c => c.X)
            .ToList();
        return distinctAntinodes.Count().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    private IEnumerable<Coordinate> GetAdjacentCoordinates(Coordinate a, Coordinate b)
    {
        var coords = new List<Coordinate> {
            GetNextCoordinate(a, a, b, (x, y) => x + y, (x, y) => x + y),
            GetNextCoordinate(a, a, b, (x, y) => x - y, (x, y) => x - y),
            GetNextCoordinate(b, a, b, (x, y) => x + y, (x, y) => x + y),
            GetNextCoordinate(b, a, b, (x, y) => x - y, (x, y) => x - y)
        };
        return [.. coords.Where(c => c != a && c != b && c.InBounds(puzzle))];
    }

    private Coordinate GetNextCoordinate(Coordinate from, Coordinate a, Coordinate b, Func<int, int, int> xAdder, Func<int, int, int> yAdder)
    {
        return new(xAdder(from.X, (b.X - a.X)), yAdder(from.Y, (b.Y - a.Y)));
    }

    private Coordinate GetNextCoordinate(Coordinate from, Coordinate a, Coordinate b)
    {
        return new(from.X + (b.X - a.X), from.Y + (b.Y - a.Y));
    }

    private void PrintPuzzle(List<Coordinate> antinodePositions, bool overwriteAntennas)
    {
        string[][] puzzleCopy = [..puzzle];
        foreach (var antinode in antinodePositions)
        {
            if (puzzleCopy.GetXY(antinode) == "." || overwriteAntennas)
            {
                puzzleCopy[antinode.Y][antinode.X] = "#";
            }
        }
        var i = 0;
        foreach (var row in puzzleCopy)
        {
            // print i with padded zeroes to 3 digits
            Console.WriteLine($"{i++:D3} {string.Join("", row)}");
        }
    }

}
