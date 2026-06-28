using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code containing geographic coordinates,
/// saving it to a file, and then reading it back to verify the content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code with latitude/longitude data, saves it,
    /// and reads it back to display the decoded text.
    /// </summary>
    static void Main()
    {
        // Define sample geographic coordinates (latitude, longitude)
        string latitude = "37.7749";
        string longitude = "-122.4194";

        // Build the QR code text using the "geo:" URI scheme
        string codeText = $"geo:{latitude},{longitude}";

        // Destination file for the generated QR code image
        string outputPath = "qr_location.png";

        // ------------------------------------------------------------
        // Generate QR Code with the geographic coordinates
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Use high error correction level (Level H) for better readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Verify the generated QR Code by reading it back
        // ------------------------------------------------------------
        if (File.Exists(outputPath))
        {
            // Initialize a barcode reader for QR codes
            using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
            {
                // Iterate through all detected barcodes (should be one)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the decoded text to the console
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
        }
        else
        {
            // Inform the user if the image file could not be created
            Console.WriteLine($"Failed to create barcode image at {outputPath}");
        }
    }
}