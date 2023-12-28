namespace Utils.Matrix;

public static class MovementHelpers
{
    private static class Movement
    {
        public static (int modX, int modY) Right => (1, 0);
        public static (int modX, int modY) Down => (0, 1);
        public static (int modX, int modY) Left => (-1, 0);
        public static (int modX, int modY) Up => (0, -1);
        public static (int modX, int modY) UpLeft => (-1, -1);
        public static (int modX, int modY) UpRight => (1, -1);
        public static (int modX, int modY) DownRight => (1, 1);
        public static (int modX, int modY) DownLeft => (-1, 1);
    }

    public static (int modX, int modY)[] GetHorizontalMovements() =>
    [
            Movement.Left,
            Movement.Right
    ];

    public static (int modX, int modY)[] GetVerticalMovements() =>
    [
            Movement.Up,
            Movement.Down
    ];


    public static (int modX, int modY)[] GetNonDiagnalMovements() =>
    [
            ..GetHorizontalMovements(),
            ..GetVerticalMovements()
    ];

    public static (int modX, int modY)[] GetAdjacentMovements() =>
    [
            ..GetNonDiagnalMovements(),
            Movement.UpLeft,
            Movement.UpRight,
            Movement.DownRight,
            Movement.DownLeft
    ];

    public static (int modX, int modY)[] GetAllWithin2Tiles(bool centerInclusive)
    {
        var directions = new List<(int, int)>();

        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                var isCenter = x == 0 && y == 0;
                if (isCenter && !centerInclusive) continue;
                directions.Add((x, y));
            }
        }

        return directions.ToArray();
    }

    public static (int modX, int modY) GetMovementModFromChar(this char c)
    {
        if (c == 'R') return Movement.Right;
        if (c == 'L') return Movement.Left;
        if (c == 'U') return Movement.Up;
        if (c == 'D') return Movement.Down;
        throw new Exception("Unknown movement character.");
    }
    public static (int modX, int modY) GetMovementModFromChar(this string c)
    {
        return GetMovementModFromChar(char.Parse(c));
    }
}