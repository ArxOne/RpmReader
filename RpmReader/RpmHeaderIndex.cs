namespace ArxOne.Rpm;

using System.Runtime.InteropServices;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// ReSharper disable UnassignedReadonlyField
[StructLayout(LayoutKind.Sequential, Pack = 0)]
public class RpmHeaderIndex
{
    public readonly RpmTag Tag;
    public readonly RpmType Type;
    public readonly int Offset;
    public readonly int Count;
}
