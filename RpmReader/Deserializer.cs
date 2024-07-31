using System.Buffers.Binary;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxOne.Rpm;

internal class Deserializer
{
    public T Deserialize<T>(Stream stream)
    {
        return (T)Deserialize(stream, typeof(T));
    }

    public object Deserialize(Stream stream, Type targetType)
    {
        using var reader = new BinaryReader(stream, Encoding.ASCII, true);
        var o = Activator.CreateInstance(targetType)!;
        foreach (var memberInfo in targetType.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField | BindingFlags.SetProperty))
        {
            switch (memberInfo)
            {
                case PropertyInfo propertyInfo when propertyInfo.GetSetMethod() is not null:
                    propertyInfo.SetValue(o, ReadValue(reader, propertyInfo));
                    break;
                case PropertyInfo:
                    break;
                case FieldInfo fieldInfo:
                    fieldInfo.SetValue(o, ReadValue(reader, fieldInfo));
                    break;
                case MethodBase:
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        return o;
    }

    private object ReadValue(BinaryReader reader, PropertyInfo propertyInfo)
    {
        return ReadValue(reader, propertyInfo, propertyInfo.PropertyType);
    }

    private object ReadValue(BinaryReader reader, FieldInfo fieldInfo)
    {
        return ReadValue(reader, fieldInfo, fieldInfo.FieldType);
    }

    private static object ReadValue(BinaryReader reader, MemberInfo memberInfo, Type memberType)
    {
        var marshalAs = memberInfo.GetCustomAttribute<MarshalAsAttribute>();
        if (marshalAs is not null)
        {
            if (memberType == typeof(string))
            {
                return marshalAs.Value switch
                {
                    UnmanagedType.ByValTStr => Encoding.ASCII.GetString(reader.ReadBytes(marshalAs.SizeConst)).TrimEnd('\0'),
                    _ => throw new NotSupportedException()
                };
            }
            if (memberType == typeof(byte[]))
            {
                return marshalAs.Value switch
                {
                    UnmanagedType.ByValArray => reader.ReadBytes(marshalAs.SizeConst),
                    _ => throw new NotSupportedException()
                };
            }
            throw new NotSupportedException();
        }

        if (memberType == typeof(byte))
            return reader.ReadByte();
        if (memberType == typeof(short))
            return reader.ReadInt16BigEndian();
        if (memberType == typeof(int))
            return reader.ReadInt32BigEndian();
        if (memberType.IsEnum)
            return ReadValue(reader, memberInfo, Enum.GetUnderlyingType(memberType));
        throw new NotSupportedException();
    }

    public object? ReadValue(byte[] blob, int offset, RpmType type, int count)
    {
        return type switch
        {
            RpmType.Null => null,
            RpmType.Char => blob[offset],
            RpmType.Int8 => blob[offset],
            RpmType.Int16 => BinaryPrimitives.ReadInt16BigEndian(blob[offset..(offset + 2)]),
            RpmType.Int32 => BinaryPrimitives.ReadInt32BigEndian(blob[offset..(offset + 4)]),
            RpmType.Int64 => BinaryPrimitives.ReadInt64BigEndian(blob[offset..(offset + 8)]),
            RpmType.String => ReadZStrings(blob, offset, 1).Single(),
            RpmType.Bin => blob[offset..(offset + count)].ToArray(),
            RpmType.StringArray => ReadZStrings(blob, offset, count).ToArray(),
            RpmType.I18Nstring => ReadZStrings(blob, offset, count).ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private IEnumerable<string> ReadZStrings(byte[] blob, int offset, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var zeroIndex = Array.IndexOf(blob, (byte)0, offset);
            var s = Encoding.ASCII.GetString(blob[offset..zeroIndex]);
            yield return s;
            offset = zeroIndex + 1;
        }
    }
}
