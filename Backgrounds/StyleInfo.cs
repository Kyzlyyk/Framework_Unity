namespace Kyzlyyk.Backgrounds
{
    public readonly struct StyleInfo
    {
        public StyleInfo(BackgroundStyle backgroundStyle)
        {
            BackgroundStyle = backgroundStyle;
        }

        public BackgroundStyle BackgroundStyle { get; }
    }
}