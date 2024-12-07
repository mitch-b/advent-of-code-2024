namespace AdventOfCode.Solutions.Utils;

public static class CollectionUtils
{
    public static T GetXY<T>(this IEnumerable<IEnumerable<T>> matrix, int x, int y) =>
        matrix.ElementAt(y).ElementAt(x);

    public static void SetXY<T>(this IEnumerable<IEnumerable<T>> matrix, int x, int y, T value)
    {
        var row = matrix.ElementAt(y).ToList();
        row[x] = value;
        row.CopyTo(matrix.ElementAt(y).ToArray());
    }
    
    public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> input)
        => input.Aggregate(input.First(), (intersector, next) => intersector.Intersect(next));

    public static string JoinAsStrings<T>(this IEnumerable<T> items, string delimiter = "") =>
        string.Join(delimiter, items);

    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values) => values.Count() == 1
        ? new[] { values }
        : values.SelectMany(v =>
            Permutations(values.Where(x => x?.Equals(v) == false)), (v, p) => p.Prepend(v));
}
