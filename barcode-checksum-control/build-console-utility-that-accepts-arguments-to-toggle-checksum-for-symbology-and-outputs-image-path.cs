// Title: Generate barcode with optional checksum via command-line arguments
// Description: Demonstrates how to create a barcode image, toggle its checksum feature based on user input, and save the result to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and EnableChecksum to produce barcodes. Typical scenarios include command-line utilities or batch processing where developers need to control checksum settings for various symbologies. It shows how to resolve symbology names dynamically and output PNG files.
// Prompt: Build a console utility that accepts arguments to toggle checksum for a symbology and outputs the image path.
// Tags: barcode, symbology, checksum, console, aspose.barcode, generation, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console utility that generates a barcode image with an optional checksum based on command-line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, configures the barcode generator, and saves the image.
    /// </summary>
    /// <param name="args">Command-line arguments: [0] symbology name, [1] checksum toggle.</param>
    static void Main(string[] args)
    {
        // Default values for symbology and checksum toggle
        string symbologyName = "Code39Extended";
        string checksumArg = "yes";

        // Override defaults with provided arguments, if any
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            checksumArg = args[1];

        // Resolve symbology name to BaseEncodeType via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(
            symbologyName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

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

        // Determine checksum setting based on user input
        EnableChecksum checksumSetting;
        switch (checksumArg.Trim().ToLowerInvariant())
        {
            case "yes":
            case "true":
            case "on":
            case "enable":
                checksumSetting = EnableChecksum.Yes;
                break;
            case "no":
            case "false":
            case "off":
            case "disable":
                checksumSetting = EnableChecksum.No;
                break;
            default:
                checksumSetting = EnableChecksum.Default;
                break;
        }

        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string fileName = $"{symbologyName}_{checksumSetting}.png";
        string outputPath = Path.Combine(outputDir, fileName);

        try
        {
            // Generate barcode with the selected symbology and checksum setting
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