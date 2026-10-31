// Title: Read Only 2D Barcodes with BarCodeReader
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader to scan only 2D symbologies, improving performance by ignoring 1D barcodes.
// Category-Description: This example belongs to the Aspose.BarCode reading and decoding category. It showcases the use of BarCodeReader together with DecodeType.Types2D to limit processing to two‑dimensional barcodes such as QR, DataMatrix, and PDF417. Developers often need fast barcode detection in image streams where only 2D codes are expected, and this pattern reduces CPU overhead by skipping 1D symbologies.
// Prompt: Configure BarCodeReader to read only 2D barcodes and ignore 1D symbologies for faster processing.
// Tags: barcode, symbology, read, 2d, decode, aspose.barcode, barcodereader, types2d

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code and reads only 2D barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code image, reads it with BarCodeReader configured for 2D types, and outputs results.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "qr.png");

        // Generate a sample QR code (2D barcode) and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize BarCodeReader to process only 2D symbologies using the predefined Types2D set
        using (var reader = new BarCodeReader(imagePath, DecodeType.Types2D))
        {
            Console.WriteLine("Reading 2D barcodes:");
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the detection results
            if (results.Length == 0)
            {
                Console.WriteLine("No 2D barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Attempt to clean up the temporary folder and its contents
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}