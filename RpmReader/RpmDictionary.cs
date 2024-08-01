using System.Buffers.Binary;
using System.Collections;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace ArxOne.Rpm;

[DebuggerDisplay("{Count} items")]
public class RpmDictionary : IReadOnlyDictionary<RpmTag, object?>
{
    public ImmutableArray<byte> Storage { get; }
    public IReadOnlyDictionary<RpmTag, RpmHeaderIndex> HeaderIndexes { get; }

    public int Count => HeaderIndexes.Count;
    public IEnumerable<RpmTag> Keys => HeaderIndexes.Keys;
    public IEnumerable<object?> Values => HeaderIndexes.Select(h => ReadValue(h.Value));

    public Encoding Encoding { get; private set; } = Encoding.ASCII;

    public object? this[RpmTag key] => ReadValue(HeaderIndexes[key]);

    public RpmDictionary(IReadOnlyCollection<RpmHeaderIndex> headerIndexes, IReadOnlyList<byte> storage)
    {
        HeaderIndexes = headerIndexes.ToFrozenDictionary(h => h.Tag);
        Storage = [.. storage];
        Initialize();
    }

    private void Initialize()
    {
        if (TryGetValue(RpmTag.Encoding, out var encodingObject) && encodingObject is string encoding)
        {
            try
            {
                Encoding = Encoding.GetEncoding(encoding);
            }
            catch (ArgumentException)
            {
                // unknown encoding. Should not happen since only “utf-8” can be found
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<RpmTag, object?>> GetEnumerator()
    {
        // ReSharper disable once NotDisposedResourceIsReturned
        return HeaderIndexes.Select(h => new KeyValuePair<RpmTag, object?>(h.Key, ReadValue(h.Value))).GetEnumerator();
    }

    public bool ContainsKey(RpmTag key)
    {
        return HeaderIndexes.ContainsKey(key);
    }

    public bool TryGetValue(RpmTag key, out object? value)
    {
        if (!HeaderIndexes.TryGetValue(key, out var headerIndex))
        {
            value = default;
            return false;
        }

        value = ReadValue(headerIndex);
        return true;
    }

    private object? ReadValue(RpmHeaderIndex headerIndex)
    {
        var type = headerIndex.Type;
        return type switch
        {
            RpmType.Null => null,
            RpmType.Char => Storage[headerIndex.Offset],
            RpmType.Int8 => Storage[headerIndex.Offset],
            RpmType.Int16 => BinaryPrimitives.ReadInt16BigEndian(Storage.AsSpan(headerIndex.Offset, 2)),
            RpmType.Int32 => BinaryPrimitives.ReadInt32BigEndian(Storage.AsSpan(headerIndex.Offset, 4)),
            RpmType.Int64 => BinaryPrimitives.ReadInt64BigEndian(Storage.AsSpan(headerIndex.Offset, 8)),
            RpmType.String => ReadZStrings(headerIndex.Offset, 1).Single(),
            RpmType.Bin => Storage[headerIndex.Offset..(headerIndex.Offset + headerIndex.Count)].ToArray(),
            RpmType.StringArray => ReadZStrings(headerIndex.Offset, headerIndex.Count).ToArray(),
            RpmType.I18NString => ReadZStrings(headerIndex.Offset, headerIndex.Count).ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(headerIndex), type, $"Unknown type {type}")
        };
    }

    private IEnumerable<string> ReadZStrings(int offset, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var zeroIndex = Storage.IndexOf(0, offset);
            var s = Encoding.GetString(Storage.AsSpan(offset..zeroIndex));
            yield return s;
            offset = zeroIndex + 1;
        }
    }
}