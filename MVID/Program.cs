using System;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

class Program
{
    static void Main()
    {
        foreach (var pattern in new[] { "*.dll", "*.exe" })
        {
            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, pattern, SearchOption.AllDirectories))
            {
                using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var peReader = new PEReader(stream))
                {
                    try
                    {
                        Console.Write(file);
                        Console.Write("\t");
                        var reader = peReader.GetMetadataReader();
                        Console.WriteLine(reader.GetGuid(reader.GetModuleDefinition().Mvid));
                    }
                    catch (InvalidOperationException)
                    {
                        // Not a managed assembly...
                    }
                }
            }
        }
    }
}
