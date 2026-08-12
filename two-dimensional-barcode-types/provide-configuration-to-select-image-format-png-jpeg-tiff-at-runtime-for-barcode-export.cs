// Title: Runtime selection of barcode image format (PNG, JPEG, TIFF)
// Description: Demonstrates how to choose the output image format for a generated barcode at runtime using Aspose.BarCode.
// Category-Description: Shows a basic Aspose.BarCode generation scenario where the BarCodeImageFormat enum is used to control the file type of the saved barcode image. This example belongs to the barcode generation and export category, illustrating typical use cases such as dynamic format selection based on user input or configuration. Developers working with barcode creation often need to switch between PNG, JPEG, and TIFF without changing code, and this snippet provides a clear pattern.
// Prompt: Provide configuration to select image format (PNG, JPEG, TIFF) at runtime for barcode export.
// Tags: barcode symbology, generation, image format, png, jpeg, tiff, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it in a format
/// (PNG, JPEG, or TIFF) selected at runtime via command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses the desired image format, builds an output path,
    /// generates a barcode, and saves it using the selected format.
    /// </summary>
    /// <param name="args">Optional first argument specifying the image format (png, jpeg, tiff).</param>
    static void Main(string[] args)
    {
        // Determine desired image format (png, jpeg, tiff). Default to PNG if none provided.
        string formatArg = args.Length > 0 ? args[0] : "png";
        BarCodeImageFormat format;

        // Map the string argument to the corresponding BarCodeImageFormat enum value.
        switch (formatArg.ToLowerInvariant())
        {
            case "png":
                format = BarCodeImageFormat.Png;
                break;
            case "jpeg":
            case "jpg":
                format = BarCodeImageFormat.Jpeg;
                break;
            case "tiff":
            case "tif":
                format = BarCodeImageFormat.Tiff;
                break;
            default:
                Console.WriteLine("Unsupported format specified. Falling back to PNG.");
                format = BarCodeImageFormat.Png;
                break;
        }

        // Build output file path in the temporary folder, using a GUID to avoid collisions.
        string fileName = Path.Combine(
            Path.GetTempPath(),
            $"barcode_{Guid.NewGuid():N}.{format.ToString().ToLowerInvariant()}");

        // Generate a simple Code128 barcode and save it using the selected format.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            generator.Save(fileName, format);
        }

        Console.WriteLine($"Barcode saved to: {fileName}");
    }
}