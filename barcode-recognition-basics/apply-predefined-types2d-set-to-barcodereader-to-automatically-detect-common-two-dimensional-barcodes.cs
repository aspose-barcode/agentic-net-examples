// Title: Detect common 2D barcodes using Types2D preset
// Description: This example generates a QR code, then uses BarCodeReader with the Types2D preset to automatically detect common two‑dimensional barcodes.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition within the 2D symbology category. It showcases the BarcodeGenerator for creating QR codes and the BarCodeReader with DecodeType.Types2D to recognize QR, DataMatrix, PDF417, and other 2D barcodes. Developers use these APIs to embed barcode creation and scanning functionality in .NET applications, such as inventory systems, ticketing, and mobile scanning solutions.
// Prompt: Apply the predefined Types2D set to BarCodeReader to automatically detect common two‑dimensional barcodes.
// Tags: barcode, 2d, types2d, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code and detecting it using BarCodeReader with the Types2D preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image, reads it with Types2D detection, and outputs results.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated QR code image
        string imagePath = "qr.png";

        // Generate a QR code image with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Save the barcode as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Create a BarCodeReader configured to detect all common 2D barcodes (Types2D)
        using (var reader = new BarCodeReader(imagePath, DecodeType.Types2D))
        {
            // Read all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}