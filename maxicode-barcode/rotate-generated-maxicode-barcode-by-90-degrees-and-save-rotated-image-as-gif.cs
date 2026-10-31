// Title: Rotate MaxiCode barcode and save as GIF
// Description: Demonstrates generating a MaxiCode barcode, rotating it 90 degrees, and saving the result as a GIF image.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, showcasing how to configure barcode generation parameters such as rotation and size, and how to export the barcode to common image formats. It uses the BarcodeGenerator class with EncodeTypes.MaxiCode and the BarCodeImageFormat enumeration. Developers often need to rotate barcodes for layout constraints or to meet specific printing requirements, and this snippet illustrates the typical steps required.
// Prompt: Rotate a generated MaxiCode barcode by 90 degrees and save the rotated image as GIF.
// Tags: maxicode, rotation, gif, image, barcode generation, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode, rotates it by 90 degrees, and saves the image as a GIF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// applies rotation, and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Build the full path for the resulting GIF file.
        string outputPath = Path.Combine(outputDir, "maxicode_rotated.gif");

        // Initialize the barcode generator for MaxiCode with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "ASPOSE"))
        {
            // Rotate the barcode image by 90 degrees.
            generator.Parameters.RotationAngle = 90f;
            // Set the X-dimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;
            // Save the generated barcode as a GIF image.
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to {outputPath}");
    }
}