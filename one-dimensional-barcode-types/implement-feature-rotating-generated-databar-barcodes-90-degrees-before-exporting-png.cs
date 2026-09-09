// Title: Rotating DataBar Omnidirectional barcode 90° and exporting as PNG
// Description: Demonstrates how to generate a DataBar Omnidirectional barcode, rotate it 90 degrees, and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing barcode creation, transformation, and image export. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to illustrate common tasks such as setting dimensions, applying rotation, and saving to various formats. Developers looking for barcode rendering and manipulation techniques can reference this collection for quick implementation patterns.
// Prompt: Implement feature rotating generated DataBar barcodes 90 degrees before exporting PNG.
// Tags: databar, omnidirectional, rotation, png, aspose.barcode, barcode, generation, csharp, example

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates rotating a DataBar Omnidirectional barcode 90 degrees and saving it as a PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies rotation, and writes the output file path.
    /// </summary>
    static void Main()
    {
        // Determine the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define the full path for the rotated barcode image
        string filePath = Path.Combine(outputDir, "DatabarOmniDirectional_Rotated90.png");

        // Create a barcode generator for DataBar Omnidirectional symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, "(01)12345678901231"))
        {
            // Set the X-dimension (module width) in pixels for better visual quality
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply a 90-degree rotation to the generated barcode
            generator.Parameters.RotationAngle = 90;

            // Save the rotated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated barcode image
        Console.WriteLine("Barcode generated at: " + filePath);
    }
}