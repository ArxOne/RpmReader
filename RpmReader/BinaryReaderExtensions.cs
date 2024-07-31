using System.Buffers.Binary;

namespace ArxOne.Rpm;

public static class BinaryReaderExtensions
{
    public static Int16 ReadInt16BigEndian(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(2);
        return BinaryPrimitives.ReadInt16BigEndian(bytes);
    }

    public static Int32 ReadInt32BigEndian(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(4);
        return BinaryPrimitives.ReadInt32BigEndian(bytes);
    }
}