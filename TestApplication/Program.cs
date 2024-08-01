
using ArxOne.Rpm;

using var rpmStream = File.OpenRead(@"C:\Users\pascal\Desktop\dotnet-runtime-8.0-8.0.7-1.x86_64.rpm");
//using var rpmStream = File.OpenRead(@"C:\Users\pascal\Desktop\arxone-backup-10.2.18731.1544-1.noarch.rpm");
var rpmReader = new RpmReader(rpmStream);

var tags = rpmReader.Read();
var signature = rpmReader.Signature;

