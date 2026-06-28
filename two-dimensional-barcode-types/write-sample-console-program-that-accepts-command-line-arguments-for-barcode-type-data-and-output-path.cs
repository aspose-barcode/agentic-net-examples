using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional arguments: symbology name, code text, and output file path.
    /// Generates the specified barcode and saves it to the given location.
    /// </summary>
    /// <param name="args">
    /// args[0] – symbology name (e.g., "Code128").
    /// args[1] – text to encode in the barcode.
    /// args[2] – output file path for the generated image.
    /// </param>
    static void Main(string[] args)
    {
        // Determine symbology, text, and output path, using defaults when arguments are missing.
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        string codeText = args.Length > 1 ? args[1] : "Sample123";
        string outputPath = args.Length > 2 ? args[2] : "barcode.png";

        // Resolve the symbology name to a BaseEncodeType enum value via reflection.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field == null)
        {
            Console.WriteLine($"Unknown barcode type: {symbologyName}");
            return;
        }

        // Retrieve the enum value from the reflected field.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for: {symbologyName}");
            return;
        }

        // Ensure the directory for the output file exists; create it if necessary.
        try
        {
            string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error preparing output directory: {ex.Message}");
            return;
        }

        // Create a barcode generator, generate the image, and save it to the specified path.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            try
            {
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode: {ex.Message}");
            }
        }
    }
}