using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static int Main(string[] args)
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

        // Resolve symbology name to EncodeTypes static field
        BaseEncodeType encodeType = ResolveEncodeType(symbologyName);
        if (encodeType == null)
        {
            Console.Error.WriteLine($"Error: Unknown symbology '{symbologyName}'.");
            return 1;
        }

        try
        {
            // Create generator, set data and save image
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Ensure the directory for the output file exists
                string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode saved to '{outputPath}'.");
            return 0;
        }
        catch (BarCodeException ex)
        {
            Console.Error.WriteLine($"Barcode generation failed: {ex.Message}");
            return 2;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            return 3;
        }
    }

    // Uses reflection to map a string name to a public static EncodeTypes field.
    private static BaseEncodeType ResolveEncodeType(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        // EncodeTypes fields are of type SymbologyEncodeType which derives from BaseEncodeType
        FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field == null)
            return null;

        object value = field.GetValue(null);
        return value as BaseEncodeType;
    }
}