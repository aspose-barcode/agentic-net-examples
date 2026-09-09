// Title: Generate Code 39 Barcodes with Checksum and Save as SVG
// Description: Demonstrates how to generate Code 39 barcodes with checksum enabled for a list of strings and save each as an SVG file in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It shows how to use the BarcodeGenerator class together with EncodeTypes.Code39FullASCII to create multiple barcodes, enable checksum via the IsChecksumEnabled property, and export the results in SVG format. Typical use cases include automated document preparation, inventory labeling, and bulk barcode creation where developers need a repeatable job that processes collections of data.
// Prompt: Create a job that processes a folder, generates Code 39 barcodes with checksum enabled, and saves them as SVG.
// Tags: code39, checksum, svg, barcode generation, aspose.barcode, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Code 39 barcodes with checksum enabled
/// and saves each barcode as an SVG file in a temporary output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the barcode generation job.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the job
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeJob_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Output folder: " + outputFolder);

        // Sample data to encode
        List<string> codeTexts = new List<string>
        {
            "CODE39A",
            "12345",
            "HELLO-WORLD",
            "ASP.NET",
            "BARCODE123"
        };

        // Process each text string, generate a barcode, and save it as SVG
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(outputFolder, text + ".svg");
            try
            {
                // Initialize the barcode generator with Code 39 Full ASCII symbology
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, text))
                {
                    // Enable checksum for Code 39
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Save the generated barcode as an SVG file
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                }
                Console.WriteLine($"Generated barcode for \"{text}\" at {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for \"{text}\": {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation job completed.");
    }
}