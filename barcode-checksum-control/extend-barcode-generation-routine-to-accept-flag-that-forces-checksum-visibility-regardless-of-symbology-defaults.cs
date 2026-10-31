// Title: Barcode Generation with Optional Forced Checksum Visibility
// Description: Demonstrates generating a barcode using Aspose.BarCode and optionally forcing the checksum to be displayed regardless of the symbology's default behavior.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use EncodeTypes, BarcodeGenerator, and related parameter settings to create barcode images. Typical use cases include producing barcodes for inventory, shipping, or retail where developers may need to control checksum visibility across different symbologies. The snippet serves as a reference for developers searching for barcode creation patterns with checksum customization in .NET.
// Prompt: Extend the barcode generation routine to accept a flag that forces checksum visibility regardless of symbology defaults.
// Tags: barcode, symbology, checksum, generation, png, aspose.barcode, encode-types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a barcode image and optionally forces checksum visibility.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments: symbology name, code text, and a flag to force checksum visibility.
    /// </summary>
    /// <param name="args">
    /// args[0] – symbology name (e.g., "Code128").
    /// args[1] – code text to encode.
    /// args[2] – boolean flag ("true"/"false") to force checksum visibility.
    /// </param>
    static void Main(string[] args)
    {
        // Default parameters
        string symbologyName = "Code128";
        string codeText = "123456";
        bool forceChecksumVisibility = false;

        // Parse command-line arguments if provided
        if (args.Length > 0)
            symbologyName = args[0];
        if (args.Length > 1)
            codeText = args[1];
        if (args.Length > 2)
            bool.TryParse(args[2], out forceChecksumVisibility);

        // Resolve symbology name to BaseEncodeType via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, $"barcode_{symbologyName}.png");

        // Generate the barcode image
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            if (forceChecksumVisibility)
            {
                // Enable checksum calculation and force its display
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            }

            // Save the barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output information about the generated barcode
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Symbology: {symbologyName}");
        Console.WriteLine($"CodeText: {codeText}");
        Console.WriteLine($"ForceChecksumVisibility: {forceChecksumVisibility}");
    }
}