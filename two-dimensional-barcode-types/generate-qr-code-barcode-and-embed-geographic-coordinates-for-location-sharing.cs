// Title: Generate QR Code with Geographic Coordinates
// Description: Creates a QR Code containing a geo URI for location sharing and saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce QR Code barcodes. It demonstrates setting QR-specific parameters such as error correction level and ECI encoding, which are common tasks when developers need to embed custom data (e.g., URLs, contact info, or location coordinates) into QR codes for mobile scanning and sharing.
// Prompt: Generate QR Code barcode and embed geographic coordinates for location sharing.
// Tags: qr code, barcode generation, geographic coordinates, aspose.barcode, png, ecoding, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code that encodes geographic coordinates using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated image.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
        Directory.CreateDirectory(tempDir);

        // Build the full path for the output PNG file.
        string outputPath = Path.Combine(tempDir, "LocationQr.png");

        // Initialize the barcode generator with QR encoding and a geo URI payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "geo:37.7749,-122.4194"))
        {
            // Set the module (pixel) size of the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Use the highest error correction level to improve scan reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Specify UTF-8 encoding for the QR code data.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated QR code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}