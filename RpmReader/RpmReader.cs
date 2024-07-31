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

    public void Read()
    {
        var lead = _deserializer.Deserialize<RpmLead>(_inputStream);
        if (!lead.IsValid)
            throw new InvalidDataException();

        var header = _deserializer.Deserialize<RpmHeader>(_inputStream);
        if (!header.IsValid)
            throw new InvalidDataException();
        var headerIndexes = new RpmHeaderIndex[header.NIndex];
        for (int index = 0; index < headerIndexes.Length; index++)
            headerIndexes[index] = _deserializer.Deserialize<RpmHeaderIndex>(_inputStream);
    }
}
