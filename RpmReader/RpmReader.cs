using System.Text;

namespace ArxOne.Rpm;

#pragma warning disable S1104
// ReSharper disable UnassignedReadonlyField
public class RpmReader
{
    private readonly Stream _inputStream;
    private readonly Deserializer _deserializer;

    public RpmReader(Stream inputStream)
    {
        _inputStream = inputStream;
        _deserializer = new Deserializer();
    }

    public IReadOnlyDictionary<RpmTag, object?> Read()
    {
        var lead = _deserializer.Deserialize<RpmLead>(_inputStream);
        if (!lead.IsValid)
            throw new InvalidDataException();

        var signature = ReadHeaderIndexes();
        var info = ReadHeaderIndexes();
        return info;
    }

    private IReadOnlyDictionary<RpmTag, object?> ReadHeaderIndexes()
    {
        var (headerIndexes, blob) = ReadRawHeaderIndexes();

        var tags = new Dictionary<RpmTag, object?>();
        foreach (var headerIndex in headerIndexes)
            tags[headerIndex.Tag] = _deserializer.ReadValue(blob, headerIndex.Offset, headerIndex.Type, headerIndex.Count);
        return tags;
    }

    private (RpmHeaderIndex[] HeaderIndexes, byte[] Blob) ReadRawHeaderIndexes()
    {
        var alignTo8 = _inputStream.Position % 8;
        if (alignTo8 > 0)
            _inputStream.Seek(8 - alignTo8, SeekOrigin.Current);
        var header = _deserializer.Deserialize<RpmHeader>(_inputStream);
        if (!header.IsValid)
            throw new InvalidDataException();
        var headerIndexes = new RpmHeaderIndex[header.NIndex];
        for (int index = 0; index < headerIndexes.Length; index++)
            headerIndexes[index] = _deserializer.Deserialize<RpmHeaderIndex>(_inputStream);

        var blob = new byte[header.HSize];
        _inputStream.Read(blob);

        return (headerIndexes, blob);
    }
}
