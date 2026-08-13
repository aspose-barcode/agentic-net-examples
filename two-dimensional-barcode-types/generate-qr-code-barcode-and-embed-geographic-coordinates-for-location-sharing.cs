// Title: Generate QR Code with Geographic Coordinates
// Description: Creates a QR Code containing a geo URI for location sharing and saves it as a PNG file.
// Category-Description: This example demonstrates Aspose.BarCode's QR Code generation capabilities, focusing on embedding location data using the geo: URI scheme. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and QRErrorLevel, and typical use cases like sharing map coordinates via scannable images. Developers working with barcode creation, QR encoding, or mobile location sharing will find this pattern useful.
// Prompt: Generate QR Code barcode and embed geographic coordinates for location sharing.
// Tags: qr code, barcode generation, geographic coordinates, location sharing, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates how to generate a QR Code that encodes geographic coordinates
/// using the Aspose.BarCode library and save it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code containing a geo URI
    /// and writes the resulting image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Sample geographic coordinates (latitude, longitude)
        double latitude = 37.7749;
        double longitude = -122.4194;

        // Encode coordinates in the "geo:" URI format commonly used for location sharing
        string codeText = $"geo:{latitude},{longitude}";

        // Prepare output folder and file path
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputFolder);
        string outputFile = Path.Combine(outputFolder, "LocationQR.png");

        // Create QR Code generator with the encoded text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set high error correction level to improve readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: adjust module size (x-dimension) for better image size
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the QR code image as PNG
            generator.Save(outputFile);
        }

        Console.WriteLine($"QR code with geographic coordinates saved to: {outputFile}");
        // Note: QR mask pattern selection is not exposed in the Aspose.BarCode API; the encoder applies optimal masking automatically.
    }
}