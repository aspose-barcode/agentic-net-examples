// Title: Generate MaxiCode barcode with custom margins
// Description: Creates a MaxiCode barcode image and saves it as PNG, applying a 10‑pixel margin on all sides for visual separation.
// Category-Description: This example demonstrates Aspose.BarCode's barcode generation capabilities, specifically using the BarcodeGenerator class with EncodeTypes.MaxiCode. It shows how to configure barcode parameters such as padding and module size, which are common tasks when creating shipping labels, inventory tags, or other machine‑readable graphics. Developers working with barcode creation often need to adjust visual layout and output formats, making this a typical use case for the Aspose.BarCode API.
// Prompt: Generate a MaxiCode barcode with a custom margin of 10 pixels on all sides for better visual separation.
// Tags: maxicode, barcode, margin, padding, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a MaxiCode barcode with a custom 10‑pixel margin on each side
/// and save it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies padding, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Initialize the barcode generator for MaxiCode with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample"))
        {
            // Apply a custom margin of 10 pixels on all four sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Optional: adjust the module (dot) size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to {outputPath}");
    }
}