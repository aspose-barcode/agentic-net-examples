// Title: Generate Codablock‑F barcode with multiline data and save as BMP
// Description: Demonstrates creating a Codablock‑F barcode containing multiple lines of text and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.CodablockF. It shows setting barcode parameters such as X‑dimension and aspect ratio, handling multiline input, and saving the result in BMP format—common tasks for developers needing high‑density 2‑D barcodes in desktop or web applications.
// Prompt: Generate a Codablock‑F barcode with multiline data and export the image as a BMP file.
// Tags: codablock-f, barcode generation, multiline data, bmp output, aspnet.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Codablock‑F barcode with multiline text and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the BMP image to be saved.
        string outputPath = Path.Combine(outputDir, "CodablockF_Multiline.bmp");

        // Define the barcode data with multiple lines separated by newline characters.
        string codeText = "Line1\nLine2\nLine3";

        // Initialize the barcode generator for Codablock‑F symbology with the multiline data.
        using (var generator = new BarcodeGenerator(EncodeTypes.CodablockF, codeText))
        {
            // Set the X dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Adjust the aspect ratio specific to Codablock‑F to control barcode shape.
            generator.Parameters.Barcode.Codablock.AspectRatio = 15;

            // Save the generated barcode as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}