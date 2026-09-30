// Title: Force checksum visibility in barcode generation
// Description: Demonstrates generating a Code39 barcode with an optional flag to always display the checksum in the human‑readable text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class together with EncodeTypes to create barcodes. Typical use cases include customizing barcode appearance, enabling checksums, and controlling human‑readable text. Developers often need to adjust checksum visibility for compliance or readability, making this pattern useful across many barcode‑related projects.
// Prompt: Extend the barcode generation routine to accept a flag that forces checksum visibility regardless of symbology defaults.
// Tags: barcode, symbology, generation, checksum, code39, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a barcode image with optional forced checksum visibility.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses an optional command‑line argument to force checksum display,
    /// creates a Code39 barcode, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument should be a boolean indicating whether to force checksum visibility.</param>
    static void Main(string[] args)
    {
        // Determine whether to force checksum visibility based on the first argument.
        bool forceShowChecksum = false;
        if (args.Length > 0 && bool.TryParse(args[0], out bool parsed))
        {
            forceShowChecksum = parsed;
        }

        // Define barcode data and symbology (Code39 supports an optional checksum).
        string codeText = "12345";
        BaseEncodeType encodeType = EncodeTypes.Code39;

        // Prepare the output directory and file path.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Generate the barcode using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable checksum calculation (optional for Code39).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Apply the flag to always show the checksum in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = forceShowChecksum;

            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the result locations and flag status.
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Force checksum visibility: {forceShowChecksum}");
    }
}