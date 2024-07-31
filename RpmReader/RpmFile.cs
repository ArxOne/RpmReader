namespace ArxOne.Rpm;

#pragma warning disable CS8618
[Flags]
public enum RpmFile
{
    Config = (1 << 0),
    Doc = (1 << 1),
    Donotuse = (1 << 2),
    Missingok = (1 << 3),
    Noreplace = (1 << 4),
    Specfile = (1 << 5),
    Ghost = (1 << 6),
    License = (1 << 7),
    Readme = (1 << 8),
    Exclude = (1 << 9),
}