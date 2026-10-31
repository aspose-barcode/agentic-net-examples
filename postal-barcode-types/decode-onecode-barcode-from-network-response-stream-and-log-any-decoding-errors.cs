// Title: Decode OneCode barcode from a stream and handle errors
// Description: Demonstrates decoding a OneCode barcode from an in‑memory network response stream, logging any errors that occur during recognition.
// Category-Description: This example belongs to the Aspose.BarCode decoding category, showcasing how to use BarCodeReader with a specific DecodeType (OneCode) to read barcodes from streams. Typical use cases include processing images received over HTTP, file uploads, or other network sources where the barcode image is available as a byte stream. Developers often need to handle unsupported symbologies gracefully and log decoding issues for diagnostics.
// Prompt: Decode a OneCode barcode from a network response stream and log any decoding errors.
// Tags: onecode, barcode decoding, error handling, stream, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code image, treats it as a network response stream,
/// and attempts to decode it as a OneCode barcode while logging any errors encountered.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, simulates a network stream,
    /// and tries to read it using OneCode decoding, outputting results or errors to the console.
    /// </summary>
    static void Main()
    {
        // Simulate a network response by creating a barcode image in memory.
        using (var imageStream = new MemoryStream())
        {
            // Generate a sample QR Code (OneCode generation is not supported by Aspose.BarCode).
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleData"))
            {
                // Set the X dimension (module size) to 4 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode to the memory stream in PNG format.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning to simulate reading from a network response.
            imageStream.Position = 0;

            // Attempt to decode the image as a OneCode barcode.
            try
            {
                using (var reader = new BarCodeReader(imageStream, DecodeType.OneCode))
                {
                    // Read all barcodes found in the stream.
                    var results = reader.ReadBarCodes();

                    // If no barcodes are detected, inform the user (OneCode is unsupported for this image).
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected (expected — OneCode recognition unsupported).");
                    }
                    else
                    {
                        // Output details of each detected barcode.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"CodeText: {result.CodeText}");
                            Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during decoding.
                Console.WriteLine($"Decoding error: {ex.Message}");
            }
        }
    }
}