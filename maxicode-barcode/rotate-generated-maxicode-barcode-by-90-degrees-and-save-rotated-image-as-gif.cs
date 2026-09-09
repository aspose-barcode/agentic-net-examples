// Title: Rotate MaxiCode barcode and save as GIF
// Description: Generates a MaxiCode barcode, rotates it 90 degrees, and saves the result as a GIF image.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation with image manipulation. It uses the BarcodeGenerator class to create a MaxiCode symbology, applies a rotation via the Parameters.RotationAngle property, and saves the output in GIF format. Developers working with barcode imaging often need to adjust orientation for printing or UI display, and this pattern shows the typical workflow for such operations.
// Prompt: Rotate a generated MaxiCode barcode by 90 degrees and save the rotated image as GIF.
// Tags: maxicode, rotation, gif, barcode generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a MaxiCode barcode, rotate it, and save it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, rotates, and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode_rotated.gif");

        // Create a barcode generator for MaxiCode with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "ASPOSE"))
        {
            // Rotate the generated barcode image by 90 degrees.
            generator.Parameters.RotationAngle = 90;

            // Save the rotated barcode as a GIF file.
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the rotated barcode image was saved.
        Console.WriteLine($"Rotated MaxiCode barcode saved to: {outputPath}");
    }
}