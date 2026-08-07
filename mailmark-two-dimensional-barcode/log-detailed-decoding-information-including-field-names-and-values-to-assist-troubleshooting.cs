// Title: Generate and Decode Code128 Barcode with Detailed Logging
// Description: Demonstrates creating a Code128 barcode, decoding it using Aspose.BarCode, and outputting all available decoding fields.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for barcode creation and BarCodeReader with DecodeType.AllSupportedTypes for decoding. Developers commonly use these APIs to embed barcodes in documents, read them from images, and troubleshoot decoding issues by examining detailed result properties.
// Prompt: Log detailed decoding information, including field names and values, to assist troubleshooting.
// Tags: code128, barcode generation, barcode recognition, decoding, logging, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing; // Required for Bitmap if needed
using Aspose.Drawing.Imaging; // Required for image format enums

/// <summary>
/// Demonstrates barcode generation and detailed decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, decodes it, and writes detailed result information to the console.
    /// </summary>
    static void Main()
    {
        // Generate a sample Code128 barcode and store it in a memory stream
        var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890");
        using (var barcodeStream = new MemoryStream())
        {
            // Save the generated barcode image as PNG into the stream
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for reading

            // Create a BarCodeReader to decode the barcode from the stream
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.AllSupportedTypes))
            {
                // Perform recognition and retrieve all detected barcodes
                BarCodeResult[] results = reader.ReadBarCodes();

                // Log detailed information for each detected barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Type Name   : {result.CodeTypeName}");
                    Console.WriteLine($"Code Text        : {result.CodeText}");
                    Console.WriteLine($"Confidence       : {result.Confidence}");
                    Console.WriteLine($"Reading Quality  : {result.ReadingQuality}");

                    // Region bounds
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region X         : {rect.X}");
                    Console.WriteLine($"Region Y         : {rect.Y}");
                    Console.WriteLine($"Region Width     : {rect.Width}");
                    Console.WriteLine($"Region Height    : {rect.Height}");

                    // Orientation angle
                    Console.WriteLine($"Region Angle     : {result.Region.Angle}");

                    // Corner points of the detected barcode region
                    var points = result.Region.Points;
                    for (int i = 0; i < points.Length; i++)
                    {
                        Console.WriteLine($"Region Point {i} : X={points[i].X}, Y={points[i].Y}");
                    }

                    Console.WriteLine(new string('-', 40));
                }

                // If no barcodes were found, inform the user
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected in the image.");
                }
            }
        }
    }
}