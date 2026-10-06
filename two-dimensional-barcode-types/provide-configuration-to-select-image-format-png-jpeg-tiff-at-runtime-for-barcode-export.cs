// Title: Runtime selection of barcode image format (PNG, JPEG, TIFF)
// Description: Demonstrates how to choose the output image format for a generated barcode based on a command‑line argument.
// Category-Description: Shows a basic Aspose.BarCode generation scenario where the BarCodeImageFormat enum and BarcodeGenerator.Save method are used. This example belongs to the "Barcode generation and export" category, illustrating typical use cases such as exporting barcodes to different image types (PNG, JPEG, TIFF) for web or print workflows. Developers often need to dynamically select formats, manage output directories, and handle unsupported inputs.
// Prompt: Provide configuration to select image format (PNG, JPEG, TIFF) at runtime for barcode export.
// Tags: barcode symbology, generation, image format, png, jpeg, tiff, aspose.barcode, runtime configuration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it in a format
/// (PNG, JPEG, or TIFF) selected at runtime via a command‑line argument.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses the desired image format, prepares the output folder,
    /// generates the barcode, and saves it using the selected format.
    /// </summary>
    /// <param name="args">Optional first argument specifying the image format (PNG, JPEG, or TIFF).</param>
    static void Main(string[] args)
    {
        // Determine the requested image format from the first command‑line argument; default to PNG.
        string formatInput = args.Length > 0 ? args[0].Trim().ToUpperInvariant() : "PNG";

        BarCodeImageFormat imageFormat;
        string extension;

        // Map the textual format to the corresponding BarCodeImageFormat enum value and file extension.
        switch (formatInput)
        {
            case "PNG":
                imageFormat = BarCodeImageFormat.Png;
                extension = ".png";
                break;
            case "JPEG":
            case "JPG":
                imageFormat = BarCodeImageFormat.Jpeg;
                extension = ".jpg";
                break;
            case "TIFF":
                imageFormat = BarCodeImageFormat.Tiff;
                extension = ".tiff";
                break;
            default:
                Console.WriteLine($"Unsupported format '{formatInput}'. Defaulting to PNG.");
                imageFormat = BarCodeImageFormat.Png;
                extension = ".png";
                break;
        }

        // Ensure the output directory exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the barcode image.
        string filePath = Path.Combine(outputDir, $"barcode{extension}");

        // Create a barcode generator for Code128 with sample data.
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

        // Save the generated barcode using the selected image format.
        generator.Save(filePath, imageFormat);

        Console.WriteLine($"Barcode saved to {filePath} with format {imageFormat}.");
    }
}