using AsmResolver.DotNet.Bundles;
string path = string.Empty;


Console.Title = "UnBundle, by vkdev";
if (args.Length > 0) {
    path = args[0];

    if (!File.Exists(path)) {
        Console.WriteLine("File not found: " + path);
        Thread.Sleep(1200);
        Environment.Exit(-1);
    }
}
else {
    do {
        Console.Write("Enter path to file: ");
        path = Console.ReadLine()!;
    } while (string.IsNullOrEmpty(path));
}

BundleManifest bundle = default;

try {
    bundle = BundleManifest.FromFile(path);
}
catch (Exception ex) {
    Console.WriteLine("Error when loading bundle");
    Console.WriteLine(ex.Message);
    Thread.Sleep(1200);
    Environment.Exit(1);
}

Console.WriteLine("Bundle loaded successfully");
Console.WriteLine("Bundle info,");
Console.WriteLine("Major Version: " + bundle.MajorVersion);
Console.WriteLine("Minor Version: " + bundle.MinorVersion);
Console.WriteLine("ID: " + bundle.BundleID);

int result = 0;
string dumpPath = Path.GetDirectoryName(path) + "\\UnBundle_results";

if (!Directory.Exists(dumpPath)) {
    try {
        Directory.CreateDirectory(dumpPath);
    }
    catch(Exception ex) {
        Console.WriteLine("Error when creating results directory");
        Console.WriteLine(ex.Message);
        Thread.Sleep(1200);
        Environment.Exit(2);
    }
}

Console.WriteLine("Dumping files, please wait...");
foreach (var bundleFile in bundle.Files) {
    
    try {
        File.WriteAllBytes(dumpPath + "\\" + bundleFile.RelativePath, bundleFile.GetData());
        Console.WriteLine(" " + bundleFile.RelativePath);
        result++; 
    } catch (Exception ex) {
        Console.WriteLine("Error when dumping file " + bundleFile.RelativePath);
        Console.WriteLine(ex.Message);
    }
    
}

Console.WriteLine("Dumped " + result + " files to " + dumpPath);

Console.WriteLine("Press any key to exit");
Console.ReadKey();
Environment.Exit(0);