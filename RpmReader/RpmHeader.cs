using System.Runtime.InteropServices;

namespace ArxOne.Rpm;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// ReSharper disable UnassignedReadonlyField
[StructLayout(LayoutKind.Sequential, Pack = 0)]
public class RpmHeader
{
    private static readonly byte[] MagicValue = new byte[] { 0x8E, 0xAD, 0xE8, 0x01 };
    private static readonly byte[] ReservedValue = new byte[4];

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public readonly byte[] Magic;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public readonly byte[] Reserved;
    public int NIndex;
    public int HSize;

    public bool IsValid => Magic.SequenceEqual(MagicValue) && Reserved.SequenceEqual(ReservedValue);
}