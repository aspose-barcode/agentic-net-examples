// Title: Toggle checksum for a barcode symbology and save as PNG
// Description: Demonstrates how to enable or disable checksum for a selected barcode symbology via command-line arguments and saves the generated image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and EnableChecksum to control checksum behavior. Typical scenarios include creating barcodes with or without checksum for validation purposes in inventory or shipping systems. Developers often need quick console utilities to produce barcode images with configurable options.
// Prompt: Build a console utility that accepts arguments to toggle checksum for a symbology and outputs the image path.
// Tags: barcode, symbology, checksum, png, aspose.barcode, generation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console utility that generates a barcode image with an optional checksum based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts two optional arguments:
    /// 1. Symbology name (e.g., Code128). Defaults to "Code128" if omitted.
    /// 2. Checksum toggle ("on"/"off", case‑insensitive). Defaults to "on".
    /// The program outputs the full path of the generated PNG image.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Resolve arguments or fall back to defaults
        // --------------------------------------------------------------------
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        string checksumArg   = args.Length > 1 ? args[1] : "on";

        // --------------------------------------------------------------------
        // Map the symbology name to the corresponding EncodeTypes enum value
        // --------------------------------------------------------------------
        FieldInfo field = typeof(EncodeTypes).GetField(
            symbologyName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // --------------------------------------------------------------------
        // Determine whether checksum should be enabled
        // --------------------------------------------------------------------
        EnableChecksum checksumSetting = (checksumArg.Equals("on", StringComparison.OrdinalIgnoreCase) ||
                                          checksumArg.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                                          checksumArg.Equals("true", StringComparison.OrdinalIgnoreCase))
                                          ? EnableChecksum.Yes
                                          : EnableChecksum.No;

        // --------------------------------------------------------------------
        // Generate the barcode image
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType))
        {
            // Sample code text; many symbologies accept numeric strings
            generator.CodeText = "1234567890";

            // Apply the checksum setting
            generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

            // Build a descriptive file name
            string fileName = $"{encodeType.TypeName}_{(checksumSetting == EnableChecksum.Yes ? "Yes" : "No")}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            // Save the barcode as a PNG image
            generator.Save(outputPath);

            // Output the full path of the generated image
            Console.WriteLine(outputPath);
        }
    }
}