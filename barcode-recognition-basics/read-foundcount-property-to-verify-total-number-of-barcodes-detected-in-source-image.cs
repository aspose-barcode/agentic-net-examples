// Title: Read FoundCount Property to Determine Number of Detected Barcodes
// Description: This example generates a Code128 barcode image, reads it back, and uses the BarCodeReader.FoundCount property to report how many barcodes were detected.
// Category-Description: Demonstrates Aspose.BarCode barcode recognition workflow, focusing on the BarCodeReader.FoundCount property. Shows how to generate a barcode with BarcodeGenerator, save it, then read it using BarCodeReader with DecodeType, retrieve total count, and iterate results. Useful for developers needing to verify detection counts in image processing, batch scanning, or quality checks.
// Prompt: Read the FoundCount property to verify the total number of barcodes detected in the source image.
// Tags: barcode, code128, foundcount, recognition, aspose.barcode, barcodegenerator, barcodereader, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates a barcode image, reads it, and reports the number of detected barcodes using the FoundCount property.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it, and outputs detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set barcode color
            generator.Parameters.BackColor = Color.White;         // Set background color
            generator.Save(barcodePath, BarCodeImageFormat.Png);   // Save image to disk
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found: " + barcodePath);
            return;
        }

        // Initialize a BarCodeReader to detect Code128 barcodes in the saved image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // The FoundCount property indicates how many barcodes were detected in the image
            int totalFound = reader.FoundCount;
            Console.WriteLine($"Total barcodes detected: {totalFound}");

            // Iterate through each detected barcode and display its type and decoded text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
            }
        }

        // Attempt to clean up temporary files and folder; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program outcome
        }
    }
}