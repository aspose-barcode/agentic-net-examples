// Title: Read barcode text and symbology from a JPEG image using Aspose.BarCodeReader
// Description: Demonstrates generating a QR code image, saving it as JPEG, and then reading the barcode text and symbology type from the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeGenerator to create barcodes and BarCodeReader to decode them. Typical use cases include validating generated barcodes, extracting data from scanned images, and integrating barcode processing into .NET applications. Developers often need to work with EncodeTypes, DecodeType, and BarCodeResult classes for end‑to‑end barcode workflows.
// Prompt: Read barcode code text and symbology type from a JPEG image using BarCodeReader.
// Tags: barcode, symbology, read, jpeg, aspose.barcode, barcodereader, barcodegenerator, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, saves it as a JPEG, and reads the barcode data back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates a QR barcode image, reads it, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeReadDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the sample JPEG image
        string imagePath = Path.Combine(tempDir, "sample.jpg");

        // Generate a sample QR barcode image and save it as JPEG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size for better readability
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read barcode(s) from the generated JPEG image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output each detected barcode's text and symbology type
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                }
            }
        }

        // Cleanup: delete the temporary image file and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any cleanup errors (e.g., file in use)
        }
    }
}