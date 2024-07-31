namespace ArxOne.Rpm;

using System.Runtime.InteropServices;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// ReSharper disable UnassignedReadonlyField
[StructLayout(LayoutKind.Sequential, Pack = 0)]
public class RpmHeaderIndex
{
    public RpmTag Tag;
    public RpmType Type;
    public int Offset;
    public int Count;
}
