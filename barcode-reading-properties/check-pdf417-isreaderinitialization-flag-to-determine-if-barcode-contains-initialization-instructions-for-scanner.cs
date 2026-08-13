// Title: PDF417 Reader Initialization Flag Demo
// Description: Demonstrates how to set and read the IsReaderInitialization flag in PDF417 barcodes, indicating scanner initialization instructions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on PDF417 symbology. It shows how to use BarcodeGenerator to embed initialization data via the Pdf417.IsReaderInitialization property and how to retrieve this flag with BarCodeReader and the Extended.Pdf417 API. Developers working with scanner configuration and PDF417 barcodes can use this pattern to embed and detect initialization commands.
// Prompt: Check PDF417 IsReaderInitialization flag to determine if barcode contains initialization instructions for the scanner.
// Tags: pdf417, readerinitialization, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates PDF417 barcodes with and without the IsReaderInitialization flag
/// and then reads the flag back using the barcode recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcodes, saves them, and processes each image
    /// to display the IsReaderInitialization flag value.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Create a PDF417 barcode with IsReaderInitialization = true
        // --------------------------------------------------------------------
        string initPath = Path.Combine(outputDir, "pdf417_init.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "INIT"))
        {
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = true;
            generator.Save(initPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Create a PDF417 barcode with IsReaderInitialization = false
        // --------------------------------------------------------------------
        string normalPath = Path.Combine(outputDir, "pdf417_normal.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "NORMAL"))
        {
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = false;
            generator.Save(normalPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Local function to read a barcode image and report the IsReaderInitialization flag
        // --------------------------------------------------------------------
        void ProcessImage(string imagePath)
        {
            // Verify that the image file exists before attempting to read it
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Initialize the barcode reader for PDF417 symbology
            using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
            {
                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Attempt to retrieve the IsReaderInitialization flag from the extended PDF417 data
                    bool isInit = false;
                    try
                    {
                        isInit = result.Extended.Pdf417.IsReaderInitialization;
                    }
                    catch
                    {
                        // If the property is unavailable (e.g., not a PDF417 barcode), treat as false
                        isInit = false;
                    }

                    // Output the detection results
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"  IsReaderInitialization: {isInit}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Process both generated images
        // --------------------------------------------------------------------
        ProcessImage(initPath);
        ProcessImage(normalPath);
    }
}