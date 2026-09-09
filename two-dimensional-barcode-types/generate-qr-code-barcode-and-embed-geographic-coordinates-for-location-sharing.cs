// Title: Generate QR Code with Embedded Geographic Coordinates
// Description: Creates a QR code containing a geo URI that encodes latitude and longitude, and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation category. It demonstrates how to use the BarcodeGenerator class with EncodeTypes.QR, configure module size, error correction level, and ECI encoding, and output the result as a PNG image. Developers commonly use these APIs to embed location data, URLs, or other text into QR codes for mobile scanning and sharing.
// Prompt: Generate QR Code barcode and embed geographic coordinates for location sharing.
// Tags: qr code, geographic coordinates, barcode generation, aspose.barcode, png output, eciencoding, error correction

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code that encodes geographic coordinates using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR code and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define sample geographic coordinates (latitude, longitude)
        double latitude = 37.7749;
        double longitude = -122.4194;

        // Build a geo URI string in the format "geo:lat,lon"
        string geoUri = $"geo:{latitude},{longitude}";

        // Prepare an output folder in the system's temporary directory
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputFolder, "LocationQr.png");

        // Generate QR Code with the geo URI as its data payload
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, geoUri))
        {
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Use a high error correction level to improve scan reliability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Ensure the QR code uses UTF-8 encoding for the data string
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code with geographic coordinates saved to: {outputPath}");
    }
}