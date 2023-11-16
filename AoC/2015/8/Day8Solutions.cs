using Utils;
using Utils.Extensions;
using static Utils.StringUtils;

namespace AoC.Y2015;

public static class Day8Solutions
{
    public static int SolvePart1()
    {
        var input = AocInputHandler.ImportHttp();
        var totalCount = 0;
        input.IterateOnEachLine((line, manager) =>
        {
            var literalCount = 0;
            var parsedCount = 0;

            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '"')
                {
                    literalCount++;
                    continue;
                }

                if (c == '\\')
                {
                    var c2 = SafeGet(line, i + 1);
                    if (c2 == '"' || c2 == '\\')
                    {
                        literalCount += 2;
                        parsedCount++;
                        i++;
                        continue;
                    }

                    if (c2 == 'x')
                    {
                        if (Char.IsLetterOrDigit(SafeGet(line, i + 2))
                            && Char.IsLetterOrDigit(SafeGet(line, i + 3)))
                        {
                            literalCount += 4;
                            parsedCount++;
                            i += 2;
                            continue;
                        }
                    }
                }

                literalCount++;
                parsedCount++;
            }

            totalCount += literalCount - parsedCount;
        });

        return totalCount;
    }

    public static int SolvePart2()
    {
        var input = AocInputHandler.ImportHttp();
        var totalCount = 0;
        input.IterateOnEachLine((line, manager) =>
        {
            var literalCount = 0;
            var encodedCount = 0;

            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '"')
                {
                    literalCount++;
                    encodedCount += 3;
                    continue;
                }

                if (c == '\\')
                {
                    var c2 = SafeGet(line, i + 1);
                    if (c2 == '"' || c2 == '\\')
                    {
                        literalCount += 2;
                        encodedCount += 4;
                        i++;
                        continue;
                    }

                    if (c2 == 'x')
                    {
                        if (Char.IsLetterOrDigit(SafeGet(line, i + 2))
                            && Char.IsLetterOrDigit(SafeGet(line, i + 3)))
                        {
                            literalCount += 4;
                            encodedCount += 5;
                            i += 2;
                            continue;
                        }
                    }
                }

                literalCount++;
                encodedCount++;
            }

            totalCount += encodedCount - literalCount;
        });

        return totalCount;
    }
}