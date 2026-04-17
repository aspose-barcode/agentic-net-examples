using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Default values
        string symbologyName = "Code128";
        string codeText = "123456";
        string outputPath = "barcode.png";

        // Parse command‑line arguments if provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            codeText = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve symbology name to EncodeTypes field via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Error: Unknown barcode type '{symbologyName}'.");
            return;
        }

        if (!(field.GetValue(null) is BaseEncodeType encodeType))
        {
            Console.WriteLine($"Error: Unable to obtain EncodeTypes for '{symbologyName}'.");
            return;
        }

        // Ensure output directory exists
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory '{directory}': {ex.Message}");
                return;
            }
        }

        // Generate and save the barcode
        try
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode saved to '{outputPath}'.");
        }
        catch (BarCodeException ex)
        {
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}