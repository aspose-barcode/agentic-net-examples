// Title: Runtime selection of barcode image format (PNG, JPEG, TIFF)
// Description: Demonstrates how to choose the output image format for a generated barcode based on a command‑line argument, defaulting to PNG.
// Category-Description: This example belongs to the Aspose.BarCode image export category, illustrating the use of BarcodeGenerator, BarCodeImageFormat, and file handling to produce barcode images in various formats. Developers often need to generate barcodes dynamically and save them as PNG, JPEG, or TIFF depending on downstream processing or storage requirements. The snippet shows typical steps: parsing arguments, mapping to enum values, setting file extensions, and saving the image.
// Prompt: Provide configuration to select image format (PNG, JPEG, TIFF) at runtime for barcode export.
// Tags: barcode symbology, image export, runtime configuration, png jpeg tiff, aspose.barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates runtime selection of barcode image format and saving a Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional format argument, generates a Code128 barcode, and saves it in the chosen image format.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be PNG, JPEG, or TIFF.</param>
    static void Main(string[] args)
    {
        // Determine desired image format (PNG, JPEG, TIFF). Default to PNG if no argument is supplied.
        string formatArg = args.Length > 0 ? args[0] : "PNG";
        BarCodeImageFormat imageFormat;
        string extension;

        // Map the string argument to the BarCodeImageFormat enum (case‑insensitive) and validate supported formats.
        if (!Enum.TryParse(formatArg, true, out imageFormat) ||
            (imageFormat != BarCodeImageFormat.Png &&
             imageFormat != BarCodeImageFormat.Jpeg &&
             imageFormat != BarCodeImageFormat.Tiff))
        {
            Console.WriteLine($"Unsupported format \"{formatArg}\". Falling back to PNG.");
            imageFormat = BarCodeImageFormat.Png;
        }

        // Choose the appropriate file extension based on the selected image format.
        switch (imageFormat)
        {
            case BarCodeImageFormat.Jpeg:
                extension = ".jpg";
                break;
            case BarCodeImageFormat.Tiff:
                extension = ".tiff";
                break;
            default:
                extension = ".png";
                break;
        }

        // Build the full output path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode" + extension);

        // Create a simple Code128 barcode and configure optional visual parameters.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Example: set barcode color (optional).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode image in the chosen format.
            generator.Save(outputPath, imageFormat);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}