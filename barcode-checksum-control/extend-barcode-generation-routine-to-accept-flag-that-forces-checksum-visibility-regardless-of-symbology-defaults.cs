using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional arguments: code text, symbology name, and a flag to force checksum visibility.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the text to encode; default to "1234567890" if not provided.
        string codeText = args.Length > 0 ? args[0] : "1234567890";

        // Determine the symbology (barcode type); default to "Code128" if not provided.
        string symbologyName = args.Length > 1 ? args[1] : "Code128";

        // Determine whether to force the checksum digit to appear in the human‑readable text.
        // Accepts "true" (case‑insensitive) or "1" as true values.
        bool forceChecksumVisibility = args.Length > 2 &&
            (args[2].Equals("true", StringComparison.OrdinalIgnoreCase) || args[2] == "1");

        // Resolve the symbology name to the corresponding BaseEncodeType enum value using reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            // Inform the user if the provided symbology name is not recognized and exit.
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the reflected field value to BaseEncodeType.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a BarcodeGenerator with the selected encode type and code text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // If requested, configure the generator to always show the checksum digit.
            if (forceChecksumVisibility)
            {
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            }

            // Define the output file name and save the generated barcode as a PNG image.
            string outputPath = "barcode.png";
            generator.Save(outputPath);

            // Output the full path of the saved barcode image for user reference.
            Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}