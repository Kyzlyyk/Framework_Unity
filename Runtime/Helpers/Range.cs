namespace Kyzlyk.Helpers
{
    public interface IRangeable
    {
        int ToInt();
    }

    public readonly struct Range<T> where T : IRangeable
    {
        public Range(T startValue, T endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
        }

        public T StartValue { get; }
        public T EndValue { get; }

        public int Length => EndValue.ToInt() - StartValue.ToInt();

        public bool Contains(T value)
        {
            int intValue = value.ToInt();
            if (intValue >= StartValue.ToInt() && intValue <= EndValue.ToInt())
                return true;

            return false;
        }
    }

    public readonly struct Range
    {
        public Range(int startValue, int length)
        {
            StartValue = startValue;
            Length = length;
        }

        public int StartValue { get; }
        public int Length { get; }

        public int EndValue => StartValue + Length + 1;

        public bool Contains(int value)
        {
            if (value >= StartValue && value <= EndValue)
                return true;

            return false;
        }
        
        public static bool Contains(int start, int end, int value)
        {
            if (value >= start && value <= end)
                return true;

            return false;
        }
        
        public int Random()
        {
            return UnityEngine.Random.Range(StartValue, EndValue + 1);
        }
    }
}