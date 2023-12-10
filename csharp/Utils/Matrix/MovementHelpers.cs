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
}