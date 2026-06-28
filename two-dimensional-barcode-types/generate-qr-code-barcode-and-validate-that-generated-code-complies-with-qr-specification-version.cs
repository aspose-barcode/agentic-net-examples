using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code image with Aspose.BarCode,
/// saving it to disk, and then reading/validating the QR code content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves it as an image, and then reads it back.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Create a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Optional QR settings can be configured here if supported by the library version.
            // Save the generated QR code image to the specified path.
            generator.Save(outputPath);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the QR code image.");
            return;
        }

        // Initialize a barcode reader to decode QR codes from the saved image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // If no QR codes were detected, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected in the generated image.");
                return;
            }

            // Iterate through each detected QR code and display its decoded text.
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine("QR code validation succeeded.");
            }
        }
    }
}