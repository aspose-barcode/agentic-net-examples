// Title: Read and Log Barcodes from Generated Image
// Description: Generates a Code128 barcode image, reads it using Aspose.BarCode, and logs each detected barcode's text and type.
// Category-Description: This example demonstrates the combined use of Aspose.BarCode generation and recognition APIs. It showcases how to create a barcode with BarcodeGenerator, save it as an image, and then detect it with BarCodeReader. Developers working with barcode scanning, inventory systems, or document processing often need to generate barcodes and later validate or extract their data using BarCodeResult objects.
// Prompt: Invoke ReadBarCodes and iterate over the BarCodeResult array to log each barcode's text and type.
// Tags: barcode, generation, recognition, read, codetype, console, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, reading, and logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode image, reads it, and writes each detected barcode's text and type to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample_barcode.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file was not found: " + barcodePath);
            return;
        }

        // Read barcodes from the generated image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Retrieve all detected barcodes
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate over each detected barcode and log its text and type
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Text: {result.CodeText}, Type: {result.CodeTypeName}");
            }

            // If no barcodes were detected, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected in the image.");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored: cleanup failures should not affect program exit
        }
    }
}