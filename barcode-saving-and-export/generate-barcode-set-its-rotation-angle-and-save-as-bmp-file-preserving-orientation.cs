// Title: Generate Rotated Code128 Barcode and Save as BMP
// Description: This example creates a Code128 barcode, rotates it 90 degrees, and saves the image as a BMP file while preserving the orientation.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with rotation. It uses BarcodeGenerator and its Parameters to set rotation angle, then saves using BarCodeImageFormat. Typical use cases include creating rotated barcodes for labels, packaging, or UI elements where orientation matters. Developers often need to adjust rotation and export to various image formats.
// Prompt: Generate a barcode, set its rotation angle, and save as a BMP file preserving orientation.
// Tags: code128, rotation, bmp, barcode generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a rotated Code128 barcode and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies rotation, and writes the file.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output BMP file in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "RotatedBarcode.bmp");

        // Create a barcode generator for Code128 symbology with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Rotate the barcode image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode as a BMP file, preserving its orientation
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}