// Title: Batch generate multiple barcodes and save as SVG files
// Description: Demonstrates generating several barcodes of different symbologies in a loop and saving each as a separate SVG file for batch processing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to create various barcode types (e.g., Code39, Code128, QR, DataMatrix, Aztec) and export them to SVG format. Typical use cases include bulk barcode creation for inventory, shipping labels, or marketing materials. Developers often need to automate barcode production, customize appearance, and handle multiple formats in a single workflow.
// Prompt: Save multiple barcodes to separate SVG files in a loop for batch processing.
// Tags: barcode symbology, batch processing, svg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of different barcode types and saving each as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a set of barcodes and writes them to the file system.
    /// </summary>
    static void Main()
    {
        // Define the output folder for the generated SVG files.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Collection of barcode specifications to be generated.
        var barcodeInfos = new[]
        {
            new { EncodeType = EncodeTypes.Code39, CodeText = "CODE39-1" },
            new { EncodeType = EncodeTypes.Code128, CodeText = "CODE128-123" },
            new { EncodeType = EncodeTypes.QR, CodeText = "https://example.com" },
            new { EncodeType = EncodeTypes.DataMatrix, CodeText = "DM12345" },
            new { EncodeType = EncodeTypes.Aztec, CodeText = "AZTEC" }
        };

        int index = 1;
        // Iterate over each barcode definition and generate the corresponding SVG file.
        foreach (var info in barcodeInfos)
        {
            // Build a unique file name that includes the index and barcode type.
            string fileName = $"barcode_{index}_{info.EncodeType}.svg";
            string filePath = Path.Combine(outputFolder, fileName);

            // Create and configure the barcode generator for the current barcode.
            using (var generator = new BarcodeGenerator(info.EncodeType, info.CodeText))
            {
                // Optional: set the barcode color (default is black).
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Attempt to save the barcode as an SVG file.
                try
                {
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved {filePath}");
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during the save operation.
                    Console.WriteLine($"Failed to save {filePath}: {ex.Message}");
                }
            }

            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }
}