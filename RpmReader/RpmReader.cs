using System.Diagnostics;

namespace ArxOne.Rpm;

#pragma warning disable S1104
// ReSharper disable UnassignedReadonlyField
public class RpmReader
{
    private readonly Stream _inputStream;
    private readonly Deserializer _deserializer;

    public RpmDictionary? Signature { get; private set; }
    public RpmDictionary? Header { get; private set; }

    public RpmReader(Stream inputStream)
    {
        _inputStream = inputStream;
        _deserializer = new Deserializer();
        CheckDuplicateTags();
    }

    private static bool _duplicatesChecked;

    [Conditional("DEBUG")]
    private static void CheckDuplicateTags()
    {
        if (_duplicatesChecked)
            return;

        _duplicatesChecked = true;

        var namesByValue = new Dictionary<int, ICollection<string>>();
        foreach (var name in Enum.GetNames<RpmTag>())
        {
            var value = (int)Enum.Parse<RpmTag>(name);
            if (!namesByValue.TryGetValue(value, out var names))
                namesByValue[value] = names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            names.Add(name);
        }

        foreach (var (value, names) in namesByValue)
        {
            if (names.Count > 1)
                Console.WriteLine($"Duplicate tag names for value {value}: {string.Join(',', names)}");
        }
    }

    public IReadOnlyDictionary<RpmTag, object?> Read()
    {
        var lead = _deserializer.Deserialize<RpmLead>(_inputStream);
        if (!lead.IsValid)
            throw new InvalidDataException();

        Signature = ReadHeaderIndexes();
        Header = ReadHeaderIndexes();
        return Header;
    }

    private RpmDictionary ReadHeaderIndexes()
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

        return new(headerIndexes, blob);
    }
}
