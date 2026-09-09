// Title: Generate Rotated Code128 Barcode and Save as BMP
// Description: Demonstrates creating a Code128 barcode, applying a 90‑degree clockwise rotation, and saving the result as a BMP image while preserving the orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as rotation and output format. It uses the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to produce images. Developers often need to rotate barcodes for label layouts, packaging, or UI designs, and this snippet shows the typical steps for that scenario.
// Prompt: Generate a barcode, set its rotation angle, and save as a BMP file preserving orientation.
// Tags: code128, rotation, bmp, barcode generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with rotation and BMP output using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a rotated Code128 barcode and saves it as a BMP file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "RotatedBarcode.bmp");

        // Text to encode in the barcode
        string codeText = "ASPOSE123";

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply a 90‑degree clockwise rotation to the barcode image
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode as a BMP file, preserving its orientation
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}