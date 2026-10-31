// Title: Save Code128 barcode as BMP with custom foreground color
// Description: Demonstrates generating a Code128 barcode, applying a custom blue foreground color, and saving it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator and its Parameters. Developers often need to set colors, sizes, and output formats when creating barcodes for printing or embedding in documents. The snippet shows typical usage of EncodeTypes, BarCodeImageFormat, and Aspose.Drawing.Color for visual customization.
// Prompt: Save a Code128 barcode to a BMP file using a custom foreground color.
// Tags: code128, barcode-generation, bmp, foreground-color, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates saving a Code128 barcode as a BMP image with a custom foreground color using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies color, saves the image, and writes the result path.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code128.bmp");

        // Initialize the barcode generator with Code128 symbology and the data to encode
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

        // Set the barcode's foreground (bar) color to blue
        generator.Parameters.Barcode.BarColor = Color.Blue;

        // Save the generated barcode as a BMP image
        generator.Save(outputPath, BarCodeImageFormat.Bmp);

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}