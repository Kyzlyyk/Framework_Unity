namespace Kyzlyk.Helpers.Utils
{
    public struct OrientationUtils
    {
        public static Direction ToOpposite(Direction direction)
        {
            return _ = direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Right => Direction.Left,
                Direction.Left => Direction.Right,
                _ => Direction.Right
            };
        }

        public static Horizontal ToHorizontal(Direction direction)
        {
            return _ = direction switch
            {
                Direction.Right => Horizontal.Right,
                Direction.Left => Horizontal.Left,
                _ => Horizontal.Zero
            };
        }

        public static Side ToOpposite(Side side)
        {
            return _ = side switch
            {
                Side.Right => Side.Left,
                Side.Left => Side.Right,
                Side.Top => Side.Bottom,
                Side.Bottom => Side.Top,
                Side.TopRight => Side.BottomLeft,
                Side.TopLeft => Side.BottomRight,
                Side.BottomRight => Side.TopLeft,
                Side.BottomLeft => Side.TopRight,
                _ => Side.BottomLeft,
            };
        }

        public static Vertical ToVertical(Direction direction)
        {
            return _ = direction switch
            {
                Direction.Up => Vertical.Up,
                Direction.Down => Vertical.Down,
                _ => Vertical.Zero
            };
        }
    }
}