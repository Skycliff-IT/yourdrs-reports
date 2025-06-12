namespace Yourdrs.Reports.API.Extensions;

public static class CsvHelper
{
    public static string? ToCsvOrNull(string? input) =>
        string.IsNullOrWhiteSpace(input) ? null : input.Replace(" ", "");

    public static List<int> ParseCsvToIntList(string? csv)
    {
        if (string.IsNullOrWhiteSpace(csv))
            return new List<int>();

        return csv
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.TryParse(s.Trim(), out var val) ? val : (int?)null)
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .ToList();
    }
}
