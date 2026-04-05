using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Directory to store generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Retrieve all supported symbology types from EncodeTypes via reflection
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            // Each field holds a BaseEncodeType instance
            if (field.GetValue(null) is BaseEncodeType encodeType)
            {
                string symName = field.Name;
                string filePath = Path.Combine(outputDir, $"{symName}.png");

                try
                {
                    // Create barcode generator for the current symbology
                    using (var generator = new BarcodeGenerator(encodeType))
                    {
                        // Use default checksum behavior
                        generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Default;

                        // Set a generic code text; invalid formats will raise exceptions which we catch
                        generator.CodeText = "1234567890";

                        // Save the barcode image as PNG
                        generator.Save(filePath);
                        Console.WriteLine($"Generated {symName} barcode at {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during generation
                    Console.WriteLine($"Error generating {symName}: {ex.Message}");
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}