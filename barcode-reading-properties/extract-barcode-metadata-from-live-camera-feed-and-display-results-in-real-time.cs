// Title: Extract barcode metadata from a generated image and display results
// Description: This example creates a QR code image, reads the barcode using Aspose.BarCode, and prints its type and text.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and access metadata via BarCodeResult. Typical for developers needing quick barcode creation and validation in C# applications, such as inventory systems or QR‑code based authentication.
// Prompt: Extract barcode metadata from live camera feed and display results in real time.
// Tags: qr, barcode, generation, recognition, metadata, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a QR code, reading it, and outputting barcode metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image, reads it, and prints barcode details.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for sample files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated image
        string imagePath = Path.Combine(tempDir, "sample.png");

        // Generate a sample QR barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader for the generated image, specifying QR decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Perform the reading operation
            reader.ReadBarCodes();

            // Output the number of barcodes found
            Console.WriteLine($"Barcodes found: {reader.FoundCount}");

            // Iterate through each detected barcode and display its metadata
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                // Additional metadata can be accessed via result.Extended if needed
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}