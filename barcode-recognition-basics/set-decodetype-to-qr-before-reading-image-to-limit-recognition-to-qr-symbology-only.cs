using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code image if missing and reading it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image when not present and reads its content.
    /// </summary>
    static void Main()
    {
        // Define the file path for the QR code image.
        string imagePath = "qr_sample.png";

        // If the image file does not exist, create a QR code image with sample text.
        if (!File.Exists(imagePath))
        {
            // Initialize a BarcodeGenerator for QR encoding with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Save the generated QR code to the specified path.
                generator.Save(imagePath);
            }
        }

        // Load the QR code image into a Bitmap object for processing.
        using (var bitmap = new Bitmap(imagePath))
        {
            // Create a BarCodeReader that is configured to decode only QR symbology.
            using (var reader = new BarCodeReader(bitmap, DecodeType.QR))
            {
                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the type of the detected barcode.
                    Console.WriteLine("Detected Type: " + result.CodeTypeName);
                    // Output the decoded text contained in the barcode.
                    Console.WriteLine("Decoded Text: " + result.CodeText);
                }
            }
        }
    }
}