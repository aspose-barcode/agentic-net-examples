// Title: Generate Rotated Code128 Barcode and Save as BMP
// Description: This example creates a Code128 barcode, rotates it 90 degrees, and saves the image as a BMP file while preserving the orientation.
// Category-Description: Demonstrates Aspose.BarCode generation operations, focusing on barcode rotation and image format output. Uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure symbology, apply rotation, and export to BMP. Ideal for developers needing to produce oriented barcodes for printing or embedding in documents.
// Prompt: Generate a barcode, set its rotation angle, and save as a BMP file preserving orientation.
// Tags: code128, rotation, bmp, aspose.barcode, generation, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates a rotated Code128 barcode and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, generates the barcode,
    /// applies a 90‑degree rotation, and writes the result to a BMP file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting BMP file
        string outputPath = Path.Combine(outputDir, "RotatedBarcode.bmp");

        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Apply a 90‑degree rotation to the generated barcode image
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode as a BMP file, preserving its orientation
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}