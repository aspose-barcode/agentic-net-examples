// Title: Toggle checksum for barcode symbology via console arguments
// Description: Demonstrates how to generate a barcode image with optional checksum using Aspose.BarCode, based on command‑line parameters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to select a symbology, configure checksum settings, and save the barcode as an image. It uses BarcodeGenerator, EncodeTypes, and related parameter classes, which are common tasks for developers creating barcodes programmatically.
// Prompt: Build a console utility that accepts arguments to toggle checksum for a symbology and outputs the image path.
// Tags: barcode, symbology, checksum, console, aspose.barcode, generation, png, encode types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console utility that generates a barcode image with an optional checksum based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts two optional arguments: the symbology name and a flag to enable/disable checksum.
    /// Generates the barcode and writes the image path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments: [0] = symbology name, [1] = checksum flag (yes/true/on).</param>
    static void Main(string[] args)
    {
        // Default values for symbology and checksum flag
        string symbologyName = "Code39Extended";
        string checksumArg = "yes";

        // Override defaults with provided arguments, if any
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            checksumArg = args[1];

        // Resolve the symbology name to a BaseEncodeType enum value using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for symbology: {symbologyName}");
            return;
        }

        // Determine whether checksum should be enabled based on the second argument
        EnableChecksum checksumSetting = (checksumArg.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                                          checksumArg.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                          checksumArg.Equals("on", StringComparison.OrdinalIgnoreCase))
                                          ? EnableChecksum.Yes
                                          : EnableChecksum.No;

        // Prepare the output directory and file name
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string fileName = $"{encodeType.TypeName}_{(checksumSetting == EnableChecksum.Yes ? "ChecksumOn" : "ChecksumOff")}.png";
        string outputPath = Path.Combine(outputDir, fileName);

        // Generate the barcode and save it as a PNG image
        try
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "123456"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}