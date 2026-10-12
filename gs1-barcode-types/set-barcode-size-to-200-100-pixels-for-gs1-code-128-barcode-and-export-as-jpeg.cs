// Title: Generate GS1 Code 128 barcode with custom size and save as JPEG
// Description: Demonstrates how to create a GS1 Code 128 barcode, set its dimensions to 200 × 100 pixels, and export it as a JPEG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to configure barcode parameters such as size, auto‑size mode, and image format using the BarcodeGenerator class. Typical use cases include creating product barcodes for labeling, inventory, or e‑commerce, where developers need precise control over image dimensions and output format.
// Prompt: Set barcode size to 200 × 100 pixels for a GS1 Code 128 barcode and export as JPEG.
// Tags: gs1code128, barcode generation, size, jpeg, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// sets a custom image size, and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1code128.jpg");

        // Create a BarcodeGenerator for GS1 Code 128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Use the nearest auto‑size mode to keep the barcode dimensions close to the specified size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set the desired image width and height in pixels.
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Save the generated barcode as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}