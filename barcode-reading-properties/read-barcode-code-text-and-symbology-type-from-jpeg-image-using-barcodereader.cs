// Title: Read barcode text and symbology from a JPEG image using BarCodeReader
// Description: Demonstrates how to generate a QR code image, save it as JPEG, and then read the barcode text and symbology type from that image using Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, BarCodeResult, and DecodeType to extract information from images. Typical use cases include scanning product labels, documents, or any image containing barcodes to retrieve encoded data. Developers often need to generate test images, read multiple barcode types, and handle temporary files, which this snippet illustrates.
// Prompt: Read barcode code text and symbology type from a JPEG image using BarCodeReader.
// Tags: barcode, symbology, read, jpeg, aspose.barcode, barcodereader, decode, qr, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR code image, saves it as JPEG, and reads the barcode data from the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample QR code, reads it back, and outputs the decoded information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the sample image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeReadDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.jpg");

        // Generate a sample QR code image and save it as JPEG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read all supported barcodes from the JPEG image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output each detected barcode's text and type information
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeType}");
                    Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this demo
        }
    }
}