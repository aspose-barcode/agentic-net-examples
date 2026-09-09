// Title: Runtime Selection of Barcode Image Format
// Description: Demonstrates how to choose PNG, JPEG, or TIFF output format for a generated barcode based on a command‑line argument.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It shows typical use cases such as dynamic image format selection for barcode export, a common requirement when integrating barcode generation into automated workflows or user‑driven applications. Developers often need to configure output formats at runtime to meet downstream processing or storage constraints.
// Prompt: Provide configuration to select image format (PNG, JPEG, TIFF) at runtime for barcode export.
// Tags: barcode symbology, generation, image format, runtime configuration, aspose.barcode, png, jpeg, tiff

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates a Code128 barcode and saves it in a format selected at runtime (PNG, JPEG, or TIFF).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Parses an optional command‑line argument to determine the image format,
    /// creates the output directory, generates the barcode, and saves it using the chosen format.
    /// </summary>
    /// <param name="args">Optional first argument specifying the desired image format (png, jpeg, or tiff).</param>
    static void Main(string[] args)
    {
        // Determine desired image format from command‑line argument or default to PNG
        string formatArg = args.Length > 0 ? args[0] : "png";
        BarCodeImageFormat imageFormat;
        string extension;

        // Map the textual format to Aspose.BarCode's enum and file extension
        switch (formatArg.Trim().ToLowerInvariant())
        {
            case "jpeg":
            case "jpg":
                imageFormat = BarCodeImageFormat.Jpeg;
                extension = "jpg";
                break;
            case "tiff":
            case "tif":
                imageFormat = BarCodeImageFormat.Tiff;
                extension = "tiff";
                break;
            case "png":
            default:
                imageFormat = BarCodeImageFormat.Png;
                extension = "png";
                break;
        }

        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExportDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, $"barcode.{extension}");

        // Generate a simple Code128 barcode and save it in the selected format
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            generator.Save(outputPath, imageFormat);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}