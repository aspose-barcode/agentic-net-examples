using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create output directory for barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Retrieve all symbology definitions from EncodeTypes
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            // Each field holds a BaseEncodeType instance
            BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
            if (encodeType == null)
                continue;

            string symName = field.Name;
            // Generic sample code text; may be invalid for some symbologies and will be caught
            string codeText = "1234567890";

            try
            {
                // Create generator with default checksum behavior
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Default;

                    string filePath = Path.Combine(outputDir, $"{symName}.png");
                    generator.Save(filePath);
                    Console.WriteLine($"Generated {symName} barcode saved to {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any generation errors and continue
                Console.WriteLine($"Failed to generate {symName}: {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}