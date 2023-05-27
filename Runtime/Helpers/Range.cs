namespace Kyzlyk.Helpers
{
    public readonly struct Range<T> where T : struct
    {
        public Range(T startValue, T endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
        }

        public T StartValue { get; }
        public T EndValue { get; }
    }
}