// Title: Set Barcode Resolution to 96 DPI and Save as PNG
// Description: Demonstrates setting the barcode image resolution to 96 DPI for standard screen display and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image resolution using the BarcodeGenerator class. Typical use cases include creating barcodes for web previews, reports, or UI elements where screen‑friendly DPI is required. Developers often need to adjust resolution, select symbology, and export to common image formats.
// Prompt: Set barcode resolution to 96 DPI for standard screen display, then render image for web preview.
// Tags: code128, barcode generation, resolution, png, aspose.barcode, image export

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, sets its resolution to 96 DPI,
/// and saves the result as a PNG image suitable for web preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Specify the text to encode in the barcode.
            generator.CodeText = "1234567890";

            // Configure the image resolution to 96 DPI (standard screen display).
            generator.Parameters.Resolution = 96f;

            // Save the barcode image to the specified file in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to {outputPath}");
    }
}