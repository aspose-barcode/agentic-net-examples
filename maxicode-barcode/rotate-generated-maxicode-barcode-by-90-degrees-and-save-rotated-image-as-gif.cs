// Title: Rotate MaxiCode barcode by 90 degrees and save as GIF
// Description: Demonstrates generating a MaxiCode barcode, rotating it 90 degrees, and saving the result as a GIF image. Useful for scenarios where barcode orientation must match specific layout requirements.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.MaxiCode, adjust the Parameters.RotationAngle property, and export the barcode to common image formats such as GIF. Developers working with barcode generation often need to rotate barcodes for printing on rotated labels, packaging, or UI elements, and this snippet shows the essential steps.
// Prompt: Rotate a generated MaxiCode barcode by 90 degrees and save the rotated image as GIF.
// Tags: maxicode, rotation, gif, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode, rotates it 90 degrees, and saves it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies rotation, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output GIF file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeRotated.gif");

        // Initialize the barcode generator for MaxiCode symbology with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "ASPOSE"))
        {
            // Set the rotation angle to 90 degrees (clockwise).
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode image as a GIF file.
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the rotated barcode image has been saved.
        Console.WriteLine($"Rotated MaxiCode barcode saved to: {outputPath}");
    }
}