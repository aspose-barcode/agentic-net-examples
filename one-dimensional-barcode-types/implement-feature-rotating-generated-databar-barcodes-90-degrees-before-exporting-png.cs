// Title: Rotating DataBar Barcodes 90 Degrees and Exporting as PNG
// Description: Demonstrates how to generate various DataBar symbologies, rotate each barcode image by 90 degrees, and save them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create and manipulate barcode images. Typical use cases include preparing rotated barcodes for label layouts or printing requirements. Developers often need to adjust orientation before exporting to image formats.
// Prompt: Implement feature rotating generated DataBar barcodes 90 degrees before exporting PNG.
// Tags: databar, rotation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates DataBar barcodes, rotates them 90 degrees, and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output folder, generates rotated DataBar barcodes, and writes them to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output folder path relative to the current directory
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "DataBarOutputs");

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define a collection of DataBar symbologies with sample code texts and target file names
        var dataBarSamples = new (BaseEncodeType type, string codeText, string fileName)[]
        {
            (EncodeTypes.DatabarLimited, "(01)08888888888888", "DatabarLimited.png"),
            (EncodeTypes.DatabarOmniDirectional, "(01)12345678901231", "DatabarOmniDirectional.png"),
            (EncodeTypes.DatabarExpanded, "(01)12345678901231(21)12345", "DatabarExpanded.png"),
            (EncodeTypes.DatabarStacked, "(01)12345678901231", "DatabarStacked.png"),
            (EncodeTypes.DatabarStackedOmniDirectional, "(01)12345678901231", "DatabarStackedOmniDirectional.png")
        };

        // Iterate over each sample, generate, rotate, and save the barcode
        foreach (var sample in dataBarSamples)
        {
            // Create a BarcodeGenerator for the specific DataBar type and code text
            using (var generator = new BarcodeGenerator(sample.type, sample.codeText))
            {
                // Set rotation angle to 90 degrees (clockwise)
                generator.Parameters.RotationAngle = 90f;

                // Build the full output file path
                string outputPath = Path.Combine(outputFolder, sample.fileName);

                // Save the rotated barcode image as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user that processing is complete
        Console.WriteLine("DataBar barcodes generated and rotated successfully.");
    }
}