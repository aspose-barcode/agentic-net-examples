// Title: Barcode Generation with Optional Forced Checksum Visibility
// Description: Demonstrates generating a barcode image using Aspose.BarCode, allowing the caller to force the checksum to be displayed regardless of the symbology's default behavior.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use EncodeTypes, BarcodeGenerator, and related parameter settings to create barcodes. Typical use cases include producing printable barcode images for inventory, shipping, or retail, where developers often need to control checksum calculation and visibility across different symbologies.
// Prompt: Extend the barcode generation routine to accept a flag that forces checksum visibility regardless of symbology defaults.
// Tags: barcode, symbology, checksum, generation, image, aspose.barcode

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with optional forced checksum visibility using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses command‑line arguments, creates a barcode, optionally forces checksum display, and saves the image.
    /// </summary>
    /// <param name="args">Command‑line arguments: symbology name, code text, and optional flag to force checksum visibility.</param>
    static void Main(string[] args)
    {
        // Default values for symbology, data, and checksum visibility flag
        string symbologyName = "Code128";
        string codeText = "123456";
        bool forceShowChecksum = true;

        // Override defaults with command‑line arguments when provided
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            codeText = args[1];
        if (args.Length >= 3 && bool.TryParse(args[2], out bool flag))
            forceShowChecksum = flag;

        // Resolve the symbology name to a BaseEncodeType enum value via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Build a temporary file path for the output PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), $"barcode_{Guid.NewGuid():N}.png");

        // Create the barcode generator with the selected symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable checksum calculation (required for symbologies that support optional checksums)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Force the checksum to be shown if the flag is set
            generator.Parameters.Barcode.ChecksumAlwaysShow = forceShowChecksum;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output information about the generated barcode
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Symbology: {symbologyName}");
        Console.WriteLine($"CodeText: {codeText}");
        Console.WriteLine($"Checksum visibility forced: {forceShowChecksum}");
    }
}