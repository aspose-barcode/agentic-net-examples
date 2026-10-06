// Title: Console utility to generate a barcode with optional checksum
// Description: Demonstrates generating a barcode image for a specified symbology and toggling its checksum based on command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and EnableChecksum classes. Typical use cases include creating barcode images for inventory, shipping, or retail applications where developers need to control checksum inclusion. The snippet illustrates common steps such as resolving a symbology via reflection, configuring generator parameters, and saving the result in PNG format.
// Prompt: Build a console utility that accepts arguments to toggle checksum for a symbology and outputs the image path.
// Tags: barcode, symbology, checksum, console, aspose.barcode, png, encode types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a console application that creates a barcode image using Aspose.BarCode,
/// allowing the caller to specify the symbology and whether the checksum is enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console utility.
    /// Accepts optional command‑line arguments: the symbology name and a checksum flag ("on" or "off").
    /// Generates a PNG barcode image and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments: [0] = symbology name, [1] = checksum flag.</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Set default values for symbology and checksum mode
        // --------------------------------------------------------------------
        string symbologyName = "Code39Extended";
        string checksumArg = "off";

        // --------------------------------------------------------------------
        // Override defaults with supplied arguments, if any
        // --------------------------------------------------------------------
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            checksumArg = args[1];

        // --------------------------------------------------------------------
        // Resolve the symbology name to a BaseEncodeType using reflection
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
        // Determine checksum mode based on the second argument
        // --------------------------------------------------------------------
        EnableChecksum checksumMode = string.Equals(
            checksumArg,
            "on",
            StringComparison.OrdinalIgnoreCase)
                ? EnableChecksum.Yes
                : EnableChecksum.No;

        // --------------------------------------------------------------------
        // Prepare the output directory and file name
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutput");
        Directory.CreateDirectory(outputDir);
        string fileName = $"{encodeType.TypeName}_{(checksumMode == EnableChecksum.Yes ? "ChecksumOn" : "ChecksumOff")}.png";
        string outputPath = Path.Combine(outputDir, fileName);

        // --------------------------------------------------------------------
        // Generate the barcode and save it as a PNG image
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "12345"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = checksumMode;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Inform the user where the image was saved
        // --------------------------------------------------------------------
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}