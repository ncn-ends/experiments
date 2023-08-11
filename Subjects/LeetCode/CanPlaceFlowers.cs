namespace Subjects.LeetCode;

public class CanPlaceFlowers
{
    public static bool Do(int[] flowerbed, int n)
    {
        var count = 0;
        for (int i = 0; i < flowerbed.Length; i++)
        {
            var c = flowerbed[i];

            if (c == 1) continue;
            if (i < flowerbed.Length - 1 && flowerbed[i + 1] == 1) continue;
            if (i > 0 && flowerbed[i - 1] == 1) continue;

            count++;
            i++;
        }

        return n <= count;
    }
}