namespace Kyzlyk.Helpers
{
    public enum Direction
    {
        Right = 1,  
        Up = 2,
        Left = 3,
        Down = 4
    }
    
    public enum Horizontal
    {
        Zero = 0,
        Right = 1,
        Left = -1
    }

    public enum Vertical
    {
        Zero = 0,
        Up = 1,
        Down = -1
    }

    public enum Side
    {
        Zero = 0,
        Right = 1,
        Left = 2,
        Top = 3,
        Bottom = 4,
        TopRight = 5,
        TopLeft = 6,
        BottomRight = 7,
        BottomLeft = 8
    }
    
    public enum Corner
    {
        TopRight = 1,
        TopLeft = 2,
        BottomRight = 3,
        BottomLeft = 4
    }
}
