// Title: Generate QR Code with Geographic Coordinates
// Description: Creates a QR Code containing a geo URI for location sharing and saves it as an image.
// Category-Description: This example demonstrates Aspose.BarCode's QR code generation and recognition capabilities. It shows how to embed geographic coordinates using the geo URI scheme, configure error correction, and read back the encoded data. Developers working with location-based services, mobile apps, or any scenario requiring QR code sharing of map coordinates will find this pattern useful.
// Prompt: Generate QR Code barcode and embed geographic coordinates for location sharing.
// Tags: qr, geo, barcode, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a QR Code containing geographic coordinates,
/// save it as an image, and then read back the encoded data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code with a geo URI, saves it,
    /// and verifies the content by decoding the saved image.
    /// </summary>
    static void Main()
    {
        // Define geographic coordinates (example: Eiffel Tower) and build the geo URI.
        double latitude = 48.8584;
        double longitude = 2.2945;
        string geoCodeText = $"geo:{latitude},{longitude}";

        // Generate a QR Code that encodes the geo URI.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, geoCodeText))
        {
            // Use high error correction level to improve scanning reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Ensure the QR Code uses UTF-8 encoding for the text payload.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Define the output file name and save the QR Code as a PNG image.
            string outputPath = "qr_location.png";
            generator.Save(outputPath);
            Console.WriteLine($"QR Code saved to: {Path.GetFullPath(outputPath)}");
        }

        // Verify that the QR Code image was created before attempting to read it.
        if (!File.Exists("qr_location.png"))
        {
            Console.WriteLine("Generated QR Code image not found.");
            return;
        }

        // Read the saved QR Code image and output the decoded text and region.
        using (BarCodeReader reader = new BarCodeReader("qr_location.png", DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected QR Code Text: {result.CodeText}");
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
            }
        }
    }
}