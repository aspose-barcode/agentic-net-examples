// Title: Generate Code128 barcode with optional checksum visibility
// Description: Demonstrates creating a Code128 barcode image and optionally forcing the checksum to be displayed, based on a command‑line flag.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and barcode parameters such as ChecksumAlwaysShow. Typical use cases include creating barcodes for inventory, shipping, or retail where checksum visibility may be required for compliance or readability. Developers often need to customize barcode appearance and output format, and this snippet shows a concise pattern for those scenarios.
// Prompt: Extend the barcode generation routine to accept a flag that forces checksum visibility regardless of symbology defaults.
// Tags: barcode, code128, checksum, generation, png, aspose.barcode, encode types, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with optional checksum visibility using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional "show" argument to force checksum display.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine whether to force checksum visibility based on the first argument.
        bool forceShowChecksum = false;
        if (args.Length > 0 && string.Equals(args[0], "show", StringComparison.OrdinalIgnoreCase))
        {
            forceShowChecksum = true;
        }

        // Prepare output directory and file path.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a barcode generator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply the checksum visibility flag regardless of the symbology's default behavior.
            generator.Parameters.Barcode.ChecksumAlwaysShow = forceShowChecksum;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved and the checksum flag state.
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Checksum visibility forced: {forceShowChecksum}");
    }
}