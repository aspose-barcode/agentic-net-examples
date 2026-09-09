// Title: Read FoundCount Property After Barcode Recognition
// Description: Demonstrates how to generate a Code128 barcode, read it, and use the FoundCount property to determine how many barcodes were detected in the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator for creating barcodes and BarCodeReader for detecting them, focusing on the FoundCount property. Developers often need to verify detection results when processing scanned images, and this snippet illustrates typical API classes and workflow.
// Prompt: Read the FoundCount property to verify the total number of barcodes detected in the source image.
// Tags: barcode generation, barcode recognition, foundcount, code128, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode image, reads it, and reports the number of barcodes found.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it, and displays detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader to detect Code128 barcodes in the generated image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Perform the recognition process
            reader.ReadBarCodes();

            // Output the total number of barcodes detected (FoundCount)
            Console.WriteLine($"FoundCount: {reader.FoundCount}");

            // List each detected barcode with its type and text
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}