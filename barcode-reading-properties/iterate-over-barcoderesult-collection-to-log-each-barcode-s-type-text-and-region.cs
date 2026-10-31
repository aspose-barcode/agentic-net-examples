// Title: Iterate over BarCodeResult collection and log barcode details
// Description: Demonstrates generating a QR code, reading it back, and iterating over the BarCodeResult collection to output each barcode's type, text, and region.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for detecting them. Developers commonly need to generate barcodes for labeling, then read and process them in applications such as inventory management, ticketing, or data capture. The key API classes used are BarcodeGenerator, BarCodeReader, BarCodeResult, and related parameter classes.
// Prompt: Iterate over BarCodeResult collection to log each barcode's type, text, and region.
// Tags: barcode symbology, generation, recognition, qr, result iteration, console output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates a QR barcode, reads it, and logs each detected barcode's details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it back, iterates over the results, and outputs type, text, and region information.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ------------------------------------------------------------
        // Generate a sample QR barcode and save it as a PNG file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Set the module (pixel) size for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read all supported barcodes from the generated image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate over each detection result and log its details
            foreach (BarCodeResult result in results)
            {
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}, Angle={result.Region.Angle}");
                Console.WriteLine("---");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}