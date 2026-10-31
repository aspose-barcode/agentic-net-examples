// Title: PDF417 Barcode Linked Segment Detection Example
// Description: Demonstrates how to generate a PDF417 barcode with the IsLinked flag set and then read the barcode to verify the linked state.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating PDF417 barcodes and BarCodeReader for extracting extended PDF417 parameters such as IsLinked. Developers working with PDF417 symbology often need to control and inspect linking between barcode segments, making this pattern useful for batch processing, document scanning, and data validation scenarios.
// Prompt: Access PDF417 extended parameters to check if the barcode is linked to another segment.
// Tags: pdf417, barcode, linked, generation, recognition, extended-parameters, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple console demonstration of generating a PDF417 barcode with the IsLinked flag
/// and reading the barcode to inspect the linked state using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a PDF417 barcode, reads it back, and outputs the
    /// extended IsLinked property to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "Pdf417LinkedDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "linked.png");

        // Generate a PDF417 barcode with the IsLinked property set to true
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleData"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable the linked segment flag in the PDF417 extended parameters
            generator.Parameters.Barcode.Pdf417.IsLinked = true;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode image and output the extended IsLinked state
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsLinked: {result.Extended.Pdf417.IsLinked}");
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any errors that occur during cleanup
        }
    }
}