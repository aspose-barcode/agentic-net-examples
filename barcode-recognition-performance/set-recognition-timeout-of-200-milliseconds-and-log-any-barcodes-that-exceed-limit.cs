// Title: Barcode recognition with timeout and logging
// Description: Demonstrates setting a 200 ms recognition timeout and logging barcodes that exceed the limit.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showing how to configure the BarCodeReader timeout, generate sample barcodes, and handle timeout scenarios. It uses BarcodeGenerator, BarCodeReader, and related parameter classes, which developers commonly use for batch scanning and performance tuning.
// Prompt: Set a recognition timeout of 200 milliseconds and log any barcodes that exceed the limit.
// Tags: barcode, recognition, timeout, logging, code128, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample barcode images, then reads them with a 200 ms timeout,
/// logging any barcode that exceeds the timeout limit.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcodes, scans them with a timeout,
    /// and outputs results or timeout notifications to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a folder for sample barcode images
        // ------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // ------------------------------------------------------------
        // Generate a few sample barcode images (Code128)
        // ------------------------------------------------------------
        string[] sampleTexts = { "1234567890", "ABCDEF", "9876543210" };
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                // Optional visual parameters
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(filePath);
            }
        }

        // ------------------------------------------------------------
        // Scan each image with a recognition timeout of 200 ms
        // ------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imageFile in imageFiles)
        {
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            using (var reader = new BarCodeReader(imageFile))
            {
                // Set the timeout (in milliseconds)
                reader.Timeout = 200;

                try
                {
                    bool anyResult = false;

                    // Attempt to read all barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        anyResult = true;
                        Console.WriteLine($"File: {Path.GetFileName(imageFile)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }

                    // If no results were returned, the timeout was exceeded
                    if (!anyResult)
                    {
                        Console.WriteLine($"Timeout exceeded while reading {Path.GetFileName(imageFile)}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any unexpected errors (including possible timeout aborts)
                    Console.WriteLine($"Error processing {Path.GetFileName(imageFile)}: {ex.Message}");
                }
            }
        }
    }
}