using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a GS1 QR code image, saving it to disk,
/// and then reading it back to verify the content using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 QR code, writes it to a file, and reads it back for verification.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output image file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1qr.png");

        // GS1 QR code payload containing Application Identifier (01) for GTIN.
        string gs1CodeText = "(01)01234567890128";

        // Create a barcode generator for GS1 QR with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1QR, gs1CodeText))
        {
            // Optional: set the QR error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image to the output path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");

        // Verify that the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader for GS1 QR codes using the saved image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1QR))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // Check if any barcodes were detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output the decoded text of each detected barcode.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                }
            }
        }
    }
}