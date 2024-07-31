namespace ArxOne.Rpm;

public class RpmReader
{
    private readonly Stream _inputStream;

    public RpmReader(Stream inputStream)
    {
        _inputStream = inputStream;
    }
}
