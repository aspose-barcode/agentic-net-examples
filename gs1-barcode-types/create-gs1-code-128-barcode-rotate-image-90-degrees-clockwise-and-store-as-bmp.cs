// Title: Generate and Rotate GS1 Code 128 Barcode to BMP
// Description: Demonstrates creating a GS1 Code 128 barcode, rotating the image 90° clockwise, and saving it as a BMP file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128, apply image rotation via Parameters.RotationAngle, and export the result with BarCodeImageFormat. Typical use cases include producing GS1-compliant barcodes for packaging, applying orientation adjustments for printing workflows, and saving in various image formats. Developers often need to generate, transform, and store barcodes programmatically, and this snippet illustrates those common steps.
// Prompt: Create a GS1 Code 128 barcode, rotate the image 90 degrees clockwise, and store as BMP.
// Tags: gs1, code128, barcode, rotation, bmp, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode, rotates it, and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Build the full file path for the resulting BMP image.
        string outputPath = Path.Combine(outputFolder, "Gs1Code128_Bmp.bmp");

        // Initialize the barcode generator with GS1 Code 128 symbology and the required data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Rotate the generated barcode image 90 degrees clockwise.
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode as a BMP file.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}