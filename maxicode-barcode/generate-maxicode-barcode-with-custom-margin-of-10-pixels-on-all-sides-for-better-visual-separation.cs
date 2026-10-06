// Title: Generate MaxiCode barcode with custom margins
// Description: Demonstrates creating a MaxiCode barcode and applying a 10-pixel margin on all sides for visual separation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.MaxiCode. It covers setting barcode padding (margins), adjusting X‑dimension, and saving the result as an image. Developers working with barcode creation often need to control visual spacing and image output formats, making this a common pattern for generating printable or display‑ready barcodes.
// Prompt: Generate a MaxiCode barcode with a custom margin of 10 pixels on all sides for better visual separation.
// Tags: maxicode, barcode, padding, margin, image, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode barcode with a 10‑pixel margin on each side
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies padding, and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        // Initialize the barcode generator for MaxiCode with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample"))
        {
            // Set a uniform 10‑pixel margin on all sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Optionally adjust the X‑dimension (module size) of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}