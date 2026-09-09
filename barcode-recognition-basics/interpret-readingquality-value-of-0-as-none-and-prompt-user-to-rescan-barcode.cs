// Title: ReadingQuality Evaluation and Rescan Prompt for Code128 Barcode
// Description: Demonstrates generating a Code128 barcode, reading it with Aspose.BarCode, and interpreting a ReadingQuality value of 0 as none, prompting the user to rescan.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader to decode it. Developers often need to assess the ReadingQuality of scanned barcodes to decide whether a rescan is required, especially in automated data‑capture scenarios.
// Prompt: Interpret a ReadingQuality value of 0 as none and prompt the user to rescan the barcode.
// Tags: barcode symbology, generation, recognition, readingquality, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, reads it, and checks the reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it, and evaluates the <c>ReadingQuality</c>.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file.
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully before attempting to read it.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image file and evaluate its reading quality.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through all detected barcodes (typically one in this example).
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Interpret a ReadingQuality of 0 as "none" and suggest a rescan.
                if (result.ReadingQuality == 0)
                {
                    Console.WriteLine("Reading quality is none, please rescan the barcode.");
                }
                else
                {
                    Console.WriteLine("Barcode read successfully with acceptable quality.");
                }
            }
        }

        // Clean up temporary files (optional). Errors during cleanup are ignored.
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored: cleanup failures should not affect program flow.
        }
    }
}