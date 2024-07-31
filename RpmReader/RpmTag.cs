namespace ArxOne.Rpm;

public enum RpmTag
{
    // === Base package tags
    Name = 1000, // string
    Version = 1001, // string
    Release = 1002, // string
    Epoch = 1003, // int32
    License = 1014, // string
    Summary = 1004, // i18nstring
    Description = 1005, // i18nstring
    Os = 1021, // string
    Arch = 1022, // string
                 // === Informative package tags
    Buildhost = 1007, // string
    Buildtime = 1006, // int32
    Bugurl = 5012, // string
    Changelogname = 1081, // string array
    Changelogtext = 1082, // string array
    Changelogtime = 1080, // int32 array
    Cookie = 1094, // string
    Distribution = 1010, // string
    Disttag = 1155, // string
    Disturl = 1123, // string
    Encoding = 5062, // string
    Group = 1016, // i18nstring
    Modularitylabel = 5096, // string
    Optflags = 1122, // string
    Packager = 1015, // string
    Platform = 1132, // string
    Policies = 1150, // string array
    Policyflags = 5033, // int32 array
    Policynames = 5030, // string array
    Policytypes = 5031, // string array
    Policytypesindexes = 5032, // int32 array
    Rpmversion = 1064, // string
    Sourcepkgid = 1146, // bin
    Sourcerpm = 1044, // string
    Translationurl = 5100, // string
    UpstreamReleases = 5101, // string
    Url = 1020, // string
    Vcs = 5034, // string
    Vendor = 1011, // string
                   // === Packages with files
    Archivesize = 1046, // int32
    Dirnames = 1118, // string array
    Filedigestalgo = 5011, // int32
    Longarchivesize = 271, // int64
    Longsize = 5009, // int64
    Payloadcompressor = 1125, // string
    Payloadflags = 1126, // string
    Payloadformat = 1124, // string
    Prefixes = 1098, // string array
    Size = 1009, // int32
                 // === Per-file information
    Basenames = 1117, // string array
    Dirindexes = 1116, // int32 array
    Filedevices = 1095, // int32 array
    Filedigests = 1035, // string array
    Fileflags = 1037, // int32 array
    Filegroupname = 1040, // string array
    Fileinodes = 1096, // int32 array
    Filelangs = 1097, // string array
    Filelinktos = 1036, // string array
    Filemodes = 1030, // int16 array
    Filemtimes = 1034, // int32 array
    Filerdevs = 1033, // int16 array
    Filesizes = 1028, // int32 array
    Fileusername = 1039, // string array
    Fileverifyflags = 1045, // int32 array
    Longfilesizes = 5008, // int64 array
                          // --- Optional file information
    Classdict = 1142, // string array
    Dependsdict = 1145, // int32 array
    Filecaps = 5010, // string array
    Fileclass = 1141, // int32 array
    Filecolors = 1140, // int32 array
    Filedependsn = 1144, // int32 array
    Filedependsx = 1143, // int32 array
    Filesignaturelength = 5091, // int32
    Filesignatures = 5090, // string array
    Veritysignaturealgo = 277, // int32
    Veritysignatures = 276, // string array
                            // === Dependency information
                            // --- Hard dependencies
    Providename = 1047, // string array
    Provideversion = 1113, // string array
    Provideflags = 1112, // int32 array
    Requirename = 1049, // string array
    Requireversion = 1050, // string array
    Requireflags = 1048, // int32 array
    Conflictname = 1054, // string array
    Conflictversion = 1055, // string array
    Conflictflags = 1053, // int32 array
    Obsoletename = 1090, // string array
    Obsoleteversion = 1115, // string array
    Obsoleteflags = 1114, // int32 array
                          // --- Soft dependencies
    Enhancename = 5055, // string array
    Enhanceversion = 5056, // string array
    Enhanceflags = 5057, // int32 array
    Recommendname = 5046, // string array
    Recommendversion = 5047, // string array
    Recommendflags = 5048, // int32 array
    Suggestname = 5049, // string array
    Suggestversion = 5050, // string array
    Suggestflags = 5051, // int32 array
    Supplementname = 5052, // string array
    Supplementversion = 5053, // string array
    Supplementflags = 5054, // int32 array
    Ordername = 5035, // string array
    Orderversion = 5036, // string array
    Orderflags = 5037, // int32 array
                       // === Scriptlets
                       // --- Basic scriptlets
                       // %postin script is executed right after the package got installed
    Postin = 1024, // string
    Postinflags = 5021, // int32
    Postinprog = 1086, // string array
                       // %posttrans scripts are all executed at the end of the transaction that installed their packages
    Posttrans = 1152, // string
    Posttransflags = 5025, // int32
    Posttransprog = 1154, // string array
                          // %postuntrans scripts are all executed at the end of the transaction that removes their packages
    Postuntrans = 5104, // string
    Postuntransflags = 5108, // int32
    Postuntransprog = 5106, // string array
                            // %postun script is executed right after the package was removed
    Postun = 1026, // string
    Postunflags = 5023, // int32
    Postunprog = 1088, // string array
                       // %prein script is executed right before the package is installed
    Prein = 1023, // string
    Preinflags = 5020, // int32
    Preinprog = 1085, // string array
                      // %pretrans scripts are executed for to be installed packages before any packages are installed/removed
    Pretrans = 1151, // string
    Pretransflags = 5024, // int32
    Pretransprog = 1153, // string array
                         // %preuntrans scripts are executed for to be removed packages before any packages are installed/removed
    Preuntrans = 5103, // string
    Preuntransflags = 5107, // int32
    Preuntransprog = 5105, // string array
                           // %preun script is executed right before the package gets removed
    Preun = 1025, // string
    Preunflags = 5022, // int32
    Preunprog = 1087, // string array
                      // %verify script is executed when the package is verified (e.g. with rpm -V)
    Verifyscript = 1079, // string
    Verifyscriptflags = 5026, // int32
    Verifyscriptprog = 1091, // string array
                             // --- Triggers
    Triggerflags = 1068, // int32 array
    Triggerindex = 1069, // int32 array
    Triggername = 1066, // string array
    Triggerscriptflags = 5027, // int32 array
    Triggerscriptprog = 1092, // string array
    Triggerscripts = 1065, // string array
    Triggerversion = 1067, // string array
                           // --- File triggers
    Filetriggerflags = 5072, // int32 array
    Filetriggerindex = 5070, // int32 array
    Filetriggername = 5069, // string array
    Filetriggerpriorities = 5084, // int32 array
    Filetriggerscriptflags = 5068, // int32 array
    Filetriggerscriptprog = 5067, // string array
    Filetriggerscripts = 5066, // string array
    Filetriggerversion = 5071, // string array
    Transfiletriggerflags = 5082, // int32 array
    Transfiletriggerindex = 5080, // int32 array
    Transfiletriggername = 5079, // string array
    Transfiletriggerpriorities = 5085, // int32 array
    Transfiletriggerscriptflags = 5078, // int32 array
    Transfiletriggerscriptprog = 5077, // string array
    Transfiletriggerscripts = 5076, // string array
    Transfiletriggerversion = 5081, // string array
                                    // === Signatures and digests
    Dsaheader = 267, // bin
    Longsigsize = 270, // int64
    Payloaddigest = 5092, // string array
    Payloaddigestalgo = 5093, // int32
    Payloaddigestalt = 5097, // string array
    Rsaheader = 268, // bin
    Sha1header = 269, // string
    Sha256header = 273, // string
    Siggpg = 262, // bin
    Sigmd5 = 261, // bin
    Sigpgp = 259, // bin
    Sigsize = 257, // int32
                   // === Installed package headers only
    Filestates = 1029, // char array
    Installcolor = 1127, // int32
    Installtid = 1128, // int32
    Installtime = 1008, // int32
    Instprefixes = 1099, // string array
    Origbasenames = 1120, // string array
    Origdirindexes = 1119, // int32 array
    Origdirnames = 1121, // string array
                         // === Source packages
    Buildarchs = 1089, // string array
    Excludearch = 1059, // string array
    Excludeos = 1060, // string array
    Exclusivearch = 1061, // string array
    Exclusiveos = 1062, // string array
    Nopatch = 1052, // int32 array
    Nosource = 1051, // int32 array
    Patch = 1019, // string array
    Source = 1018, // string array
    Sourcepackage = 1106, // int32
    Spec = 5099, // string
                 // === Internal / special
    Headeri18ntable = 100, // string array
    Headerimmutable = 63, // bin
    Pubkeys = 266, // string array
                   // === Deprecated / Obsolete
    Filecontexts = 1147, // string array
    Fscontexts = 1148, // string array
    Gif = 1012, // bin
    Icon = 1043, // bin
    Oldenhancesname = 1159, // string array
    Oldenhancesversion = 1160, // string array
    Oldenhancesflags = 1161, // int32 array
    Oldfilenames = 1027, // string array
    Oldsuggestsname = 1156, // string array
    Oldsuggestsversion = 1157, // string array
    Oldsuggestsflags = 1158, // int32 array
    Patchesflags = 1134, // int32 array
    Patchesname = 1133, // string array
    Patchesversion = 1135, // string array
    Recontexts = 1149, // string array
    Removetid = 1129, // int32
    Xpm = 1013, // bin
                // === Extensions
    Archsuffix = 5098, // string
    Dbinstance = 1195, // int32
    Epochnum = 5019, // int32
    Evr = 5013, // string
    Fileclass2 = 1141, // string array
    Headercolor = 5017, // int32
    Nevr = 5015, // string
    Nevra = 5016, // string
    Nvr = 5014, // string
    Nvra = 1196, // string
    Filenames = 5000, // string array
    Filenlinks = 5045, // int32 array
    Fileprovide = 5001, // string array
    Filerequire = 5002, // string array
    Instfilenames = 5040, // string array
    Origfilenames = 5007, // string array
    Providenevrs = 5042, // string array
    Conflictnevrs = 5044, // string array
    Obsoletenevrs = 5043, // string array
    Enhancenevrs = 5061, // string array
    Recommendnevrs = 5058, // string array
    Requirenevrs = 5041, // string array
    Suggestnevrs = 5059, // string array
    Supplementnevrs = 5060, // string array
    Sysusers = 5109, // string array
    Filetriggerconds = 5086, // string array
    Filetriggertype = 5087, // string array
    Transfiletriggerconds = 5088, // string array
    Transfiletriggertype = 5089, // string array
    Triggerconds = 5005, // string array
    Triggertype = 5006, // string array
    Verbose = 5018, // int32
}