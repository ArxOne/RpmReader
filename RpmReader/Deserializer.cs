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

    private static object ReadValue(BinaryReader reader, PropertyInfo propertyInfo)
    {
        return ReadValue(reader, propertyInfo, propertyInfo.PropertyType);
    }

    private static object ReadValue(BinaryReader reader, FieldInfo fieldInfo)
    {
        return ReadValue(reader, fieldInfo, fieldInfo.FieldType);
    }

    private static object ReadValue(BinaryReader reader, MemberInfo memberInfo, Type memberType)
    {
        var marshalAs = memberInfo.GetCustomAttribute<MarshalAsAttribute>();
        return marshalAs is not null 
            ? ReadMarshaledValue(reader, memberType, marshalAs) 
            : ReadSimpleValue(reader, memberType);
    }

    private static object ReadSimpleValue(BinaryReader reader, Type memberType)
    {
        if (memberType == typeof(byte))
            return reader.ReadByte();
        if (memberType == typeof(short))
            return reader.ReadInt16BigEndian();
        if (memberType == typeof(int))
            return reader.ReadInt32BigEndian();
        if (memberType.IsEnum)
            return ReadSimpleValue(reader, Enum.GetUnderlyingType(memberType));
        throw new NotSupportedException();
    }

    private static object ReadMarshaledValue(BinaryReader reader, Type memberType, MarshalAsAttribute marshalAs)
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
}
