
using ArxOne.Rpm;

using var rpmStream = File.OpenRead($@"C:\Users\pascal\Desktop\dotnet-runtime-8.0-8.0.7-1.x86_64.rpm");
var rpmReader = new RpmReader(rpmStream);

var tags = rpmReader.Read();
