// Title: Generate and Rotate GS1 Code 128 Barcode to BMP
// Description: Demonstrates creating a GS1 Code 128 barcode, rotating the image 90° clockwise, and saving it as a BMP file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128, apply image rotation via Parameters.RotationAngle, and export the result using BarCodeImageFormat. Typical use cases include product labeling, inventory management, and compliance with GS1 standards where developers need to generate, transform, and store barcodes in various image formats.
// Prompt: Create a GS1 Code 128 barcode, rotate the image 90 degrees clockwise, and store as BMP.
// Tags: gs1, code128, barcode, rotation, bmp, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating a GS1 Code 128 barcode, rotating it, and saving the image as BMP.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the barcode generation, rotation, and file saving.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Build the full path for the resulting BMP file.
        string outPath = Path.Combine(outputDir, "GS1Code128.bmp");

        // Sample GS1 Code 128 data (Application Identifier 01 - GTIN).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 Code 128 symbology and the sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Rotate the generated barcode image 90 degrees clockwise.
            generator.Parameters.RotationAngle = 90f;
            // Save the rotated barcode image as a BMP file.
            generator.Save(outPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 Code 128 barcode saved to: {outPath}");
    }
}