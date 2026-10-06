// Title: Generate Code128 barcode with specific XDimension (0.33 mm)
// Description: Demonstrates how to create a Code128 barcode image with an X‑dimension of 0.33 mm, suitable for industry size standards.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical scenarios include creating barcodes for product labeling, inventory tracking, and packaging where precise dimensions are required. Developers often need to control barcode size parameters such as XDimension to meet regulatory or industry specifications.
// Prompt: Generate a barcode with XDimension of 0.33 mm to meet specific industry size standards.
// Tags: code128, xdimension, barcode generation, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with a custom XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Text to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the XDimension to 0.33 mm to meet size standards.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}