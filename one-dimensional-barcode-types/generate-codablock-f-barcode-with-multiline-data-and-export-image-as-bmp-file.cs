// Title: Generate Codablock‑F barcode with multiline data and save as BMP
// Description: Demonstrates creating a Codablock‑F barcode containing multiple lines of text and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.CodablockF. It shows setting barcode dimensions, aspect ratio, and saving the result in BMP format—common tasks for developers needing high‑density 2‑D barcodes in desktop or web applications.
// Prompt: Generate a Codablock‑F barcode with multiline data and export the image as a BMP file.
// Tags: codablock-f, barcode generation, multiline data, bmp output, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Codablock‑F barcode with multiline data and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary directory for the output and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "CodablockFExample");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the BMP image
        string outputPath = Path.Combine(outputDir, "CodablockF_Multiline.bmp");

        // Define multiline data using line‑feed characters
        string multilineData = "Line1\nLine2\nLine3";

        // Initialize the barcode generator with Codablock‑F symbology and the multiline data
        using (var generator = new BarcodeGenerator(EncodeTypes.CodablockF, multilineData))
        {
            // Set the X‑dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Adjust the aspect ratio specific to Codablock‑F
            generator.Parameters.Barcode.Codablock.AspectRatio = 15;

            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}