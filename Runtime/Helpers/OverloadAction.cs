using System;

namespace Kyzlyk.Helpers
{
    /// <summary>
    /// Use in internal context.
    /// </summary>
    internal readonly ref struct OverloadAction_Intrl
    {
        internal static OverloadAction_Intrl Mark => new();
        internal static IntPtr Ptr => IntPtr.Zero;

        public static implicit operator int(OverloadAction_Intrl @void) => 0;
    }

    /// <summary>
    /// For marking overloaded method, constructor in another scenario.
    /// </summary>
    public readonly ref struct OverloadAction
    {
        public static OverloadAction Mark => new();
        public static IntPtr Ptr => IntPtr.Zero;

        public static implicit operator int(OverloadAction @void) => 0;
    }
}
