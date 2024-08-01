using System.Runtime.InteropServices;

namespace ArxOne.Rpm;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// ReSharper disable UnassignedReadonlyField
[StructLayout(LayoutKind.Sequential, Pack = 0)]
public class RpmLead
{
    private static readonly byte[] MagicValue = [0xED, 0xAB, 0xEE, 0xDB];

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public readonly byte[] Magic;
    public readonly byte Major, Minor;
    public readonly short Type;
    public readonly short ArchNum;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 66)] public readonly string Name;
    public readonly short OsNum;
    public readonly short SignatureType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public readonly byte[] Reserved;

    public bool IsValid => Magic.SequenceEqual(MagicValue) && SignatureType == 5;
}
