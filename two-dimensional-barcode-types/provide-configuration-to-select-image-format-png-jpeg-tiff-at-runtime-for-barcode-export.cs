// Title: Runtime selection of barcode image format
// Description: Demonstrates how to choose PNG, JPEG, or TIFF output format for a generated barcode based on a command‑line argument.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator (EncodeTypes) and BarCodeImageFormat to create and export barcodes. Typical use cases include dynamic report generation, batch processing, or any scenario where the image format must be decided at runtime. Developers often need to switch between PNG, JPEG, and TIFF depending on downstream consumption or storage requirements.
// Prompt: Provide configuration to select image format (PNG, JPEG, TIFF) at runtime for barcode export.
// Tags: barcode symbology, generation, image format, png, jpeg, tiff, aspose.barcode, save

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it using a format
/// (PNG, JPEG, or TIFF) selected at runtime via a command‑line argument.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses the desired image format, creates a barcode, and saves it.
    /// </summary>
    /// <param name="args">Optional first argument specifying the image format (PNG, JPEG, or TIFF).</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Determine desired image format (default: PNG) from command line
        // ------------------------------------------------------------
        string formatArg = args.Length > 0 ? args[0] : "PNG";
        BarCodeImageFormat imageFormat;
        string fileExtension;

        // Map the textual format to Aspose.BarCode enum values and file extensions
        switch (formatArg.Trim().ToUpperInvariant())
        {
            case "PNG":
                imageFormat = BarCodeImageFormat.Png;
                fileExtension = "png";
                break;
            case "JPEG":
            case "JPG":
                imageFormat = BarCodeImageFormat.Jpeg;
                fileExtension = "jpg";
                break;
            case "TIFF":
                imageFormat = BarCodeImageFormat.Tiff;
                fileExtension = "tif";
                break;
            default:
                Console.WriteLine($"Unsupported format '{formatArg}'. Supported formats: PNG, JPEG, TIFF.");
                return;
        }

        // ------------------------------------------------------------
        // Build the full output file path in the current working directory
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), $"barcode.{fileExtension}");

        // ------------------------------------------------------------
        // Generate the barcode and save it using the selected image format
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            generator.Save(outputPath, imageFormat);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}