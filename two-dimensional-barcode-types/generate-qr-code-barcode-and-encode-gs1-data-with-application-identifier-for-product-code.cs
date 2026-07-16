// Title: Generate QR Code with GS1 Application Identifier (GTIN) and decode it
// Description: Demonstrates creating a QR Code that encodes GS1 data using the (01) Application Identifier for a product GTIN, then reads it back to verify the content.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation and recognition category. It showcases the BarcodeGenerator for QR encoding with GS1 Application Identifiers and the BarCodeReader for decoding. Developers often need to embed standardized product identifiers in QR codes for inventory, logistics, and retail applications, and this snippet illustrates the typical API usage for such scenarios.
// Prompt: Generate QR Code barcode and encode GS1 data with Application Identifier for product code.
// Tags: qr code, gs1, product code, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR Code containing GS1 product data
/// and then reads the barcode back to verify its content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code with a GS1 Application Identifier,
    /// saves it as a PNG file, and then decodes the image to display the extracted information.
    /// </summary>
    static void Main()
    {
        // Path for the generated QR code image
        const string outputPath = "qr_gs1.png";

        // Create a QR code generator with GS1 Application Identifier (01) for a product GTIN
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "(01)01234567890123"))
        {
            // Set a high error correction level for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the QR code image to a PNG file
            generator.Save(outputPath);
            Console.WriteLine($"QR Code saved to {outputPath}");
        }

        // Verify the generated QR code by reading it back
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Error: Generated QR code file not found.");
            return;
        }

        // Initialize a reader for QR codes
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            // Use a high-quality preset for reliable detection
            reader.QualitySettings = QualitySettings.HighQuality;

            // Read all detected barcodes (should be one QR code)
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // Output the bounding rectangle of the detected barcode
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
            }
        }
    }
}