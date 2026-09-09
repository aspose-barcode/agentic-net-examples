// Title: Adjust Postnet barcode bar height while preserving automatic width
// Description: Demonstrates how to set the BarHeight of a Postnet barcode to 40 points using Aspose.BarCode, while allowing the library to calculate the barcode width automatically.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating barcode parameter customization. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat for rendering barcodes. Developers often need to modify dimensions such as bar height without manually specifying width, to maintain proper scaling and readability across different output formats.
// Prompt: Adjust the BarHeight of a Postnet barcode to 40 points while keeping automatic width calculation.
// Tags: postnet, barcode, barheight, automatic width, aspnet, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the Postnet barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Postnet barcode with a custom bar height and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "PostnetExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "PostnetBarcode.png");

        // Initialize the barcode generator for Postnet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "123456"))
        {
            // Set the bar height to 40 points while keeping automatic width calculation
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Postnet barcode saved to: " + outputPath);
    }
}