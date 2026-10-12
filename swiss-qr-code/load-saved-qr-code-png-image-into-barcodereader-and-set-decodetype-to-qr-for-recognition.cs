// Title: Load and Decode a QR Code PNG using BarCodeReader
// Description: Demonstrates generating a QR code image, saving it as PNG, then loading it with BarCodeReader and decoding it as a QR symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a QR code, save it in PNG format, and then employ BarCodeReader with DecodeType set to QR for detection. Developers working with QR code creation and scanning can use these APIs for image‑based barcode processing in .NET applications.
// Prompt: Load a saved QR Code PNG image into BarCodeReader and set DecodeType to QR for recognition.
// Tags: qr code, barcode generation, barcode recognition, png, decode, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, saving it as PNG, and decoding it using BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image, reads it back, and outputs detection results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the QR code image
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr.png");

        // Generate a QR code image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // Load the saved QR code image and set DecodeType to QR for recognition
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored
        }
    }
}