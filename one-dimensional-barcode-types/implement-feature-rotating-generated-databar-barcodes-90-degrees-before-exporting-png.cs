// Title: Rotate DataBar barcodes 90 degrees and export as PNG
// Description: Demonstrates generating DataBar barcodes, rotating them 90° clockwise, and saving the images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create DataBar symbologies, apply rotation via Parameters.RotationAngle, and export to common image formats. Developers often need to adjust barcode orientation for label layouts or scanning requirements, making this pattern useful for printing and UI scenarios.
// Prompt: Implement feature rotating generated DataBar barcodes 90 degrees before exporting PNG.
// Tags: databar, rotation, png, barcode generation, aspose.barcode, aspose.drawing, image export

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a set of DataBar barcodes, rotates each 90 degrees, and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates, rotates, and saves DataBar barcodes.
    /// </summary>
    static void Main()
    {
        // Define an array of barcode configurations: type, data, and output file name.
        var barcodes = new (BaseEncodeType type, string codeText, string fileName)[]
        {
            (EncodeTypes.DatabarOmniDirectional, "(01)12345678901231", "DatabarOmniDirectional.png"),
            (EncodeTypes.DatabarLimited, "(01)08888888888888", "DatabarLimited.png"),
            (EncodeTypes.DatabarExpanded, "(01)12345678901231(10)ABCD", "DatabarExpanded.png")
        };

        // Iterate over each configuration, generate the barcode, rotate it, and save as PNG.
        foreach (var (type, codeText, fileName) in barcodes)
        {
            // Create a BarcodeGenerator for the specified DataBar type and data.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Apply a 90-degree rotation to the generated barcode image.
                generator.Parameters.RotationAngle = 90f;

                // Export the rotated barcode to a PNG file.
                generator.Save(fileName, BarCodeImageFormat.Png);

                // Inform the user that the file has been saved.
                Console.WriteLine($"Saved rotated barcode to {fileName}");
            }
        }
    }
}