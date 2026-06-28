using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Parses command‑line arguments, resolves the barcode symbology,
    /// ensures the output directory exists, generates the barcode,
    /// and saves it to a file.
    /// </summary>
    /// <param name="args">
    /// Optional arguments:
    /// args[0] – symbology name (e.g., "Code128"),
    /// args[1] – text to encode,
    /// args[2] – output file path.
    /// </param>
    static void Main(string[] args)
    {
        // Parse command‑line arguments with safe defaults.
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        string codeText = args.Length > 1 ? args[1] : "Sample123";
        string outputPath = args.Length > 2 ? args[2] : "barcode.png";

        // Resolve symbology name to a BaseEncodeType via reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            // If the provided symbology is unknown, fall back to Code128.
            Console.WriteLine($"Unknown symbology '{symbologyName}'. Falling back to Code128.");
            field = typeof(EncodeTypes).GetField("Code128");
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Ensure the output directory exists.
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
            // Abort if the output path cannot be prepared.
            Console.WriteLine($"Failed to prepare output path: {ex.Message}");
            return;
        }

        // Generate the barcode image.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example settings: high resolution and standard colors.
            generator.Parameters.Resolution = 300f;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode to the specified file.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to '{outputPath}'.");
    }
}