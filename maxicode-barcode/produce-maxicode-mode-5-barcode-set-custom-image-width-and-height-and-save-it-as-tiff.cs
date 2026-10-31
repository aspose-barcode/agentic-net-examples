// Title: Generate a MaxiCode Mode 5 barcode and save as TIFF
// Description: Demonstrates creating a MaxiCode Mode 5 barcode, customizing its image size, and exporting it to a TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.MaxiCode, configure MaxiCodeMode, adjust image dimensions via AutoSizeMode and pixel settings, and save the result in TIFF format. Developers working with high‑density 2‑D barcodes such as MaxiCode can reference this pattern for custom sizing and output requirements.
// Prompt: Produce a MaxiCode Mode 5 barcode, set custom image width and height, and save it as TIFF.
// Tags: maxicode, mode5, barcode generation, image size, tiff, aspnet.barcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 5 barcode, customizing its dimensions, and saving it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode5.tiff");
        // Text to encode in the MaxiCode barcode
        string codeText = "Sample MaxiCode Mode5";

        // Initialize the barcode generator for MaxiCode with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the MaxiCode mode to Mode5 (used for specific data structures)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode5;

            // Configure automatic sizing to choose the nearest size that fits the content
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            // Set custom image width and height in pixels
            generator.Parameters.ImageWidth.Pixels = 500f;
            generator.Parameters.ImageHeight.Pixels = 500f;

            // Optional: adjust the module (dot) size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}