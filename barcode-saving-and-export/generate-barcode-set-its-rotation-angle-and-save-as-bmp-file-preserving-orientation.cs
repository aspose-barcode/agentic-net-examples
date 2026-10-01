// Title: Generate rotated Code128 barcode and save as BMP
// Description: Demonstrates creating a Code128 barcode, rotating it 90 degrees, and saving the image as a BMP file while preserving orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to encode data, apply rotation via the Parameters.RotationAngle property, and export the result in a specific image format. Typical use cases include creating printable barcodes with custom orientation for labels, packaging, or UI display. Developers often need to control barcode orientation and output format using the Generation API.
// Prompt: Generate a barcode, set its rotation angle, and save as a BMP file preserving orientation.
// Tags: barcode, code128, rotation, bmp, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a rotated Code128 barcode and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies a 90-degree rotation, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology.
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build the full output path for the BMP file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.bmp");

        // Initialize the barcode generator with the chosen symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the rotation angle (allowed values: 0, 90, 180, 270 degrees).
            generator.Parameters.RotationAngle = 90f;

            // Save the barcode as a BMP image, preserving the specified orientation.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}