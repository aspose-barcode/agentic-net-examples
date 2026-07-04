// Title: Detect common 2D barcodes using Types2D preset
// Description: Demonstrates applying the predefined Types2D set to BarCodeReader to automatically detect common two‑dimensional barcodes in an image.
// Prompt: Apply the predefined Types2D set to BarCodeReader to automatically detect common two‑dimensional barcodes.
// Tags: barcode symbology, detection, 2d, types2d, aspose, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code (if missing) and reads it using the Types2D preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code image if needed and reads it with BarCodeReader using DecodeType.Types2D.
    /// </summary>
    static void Main()
    {
        // Define the path for the sample barcode image
        string imagePath = "sample_qr.png";

        // Generate a QR code image if it does not already exist
        if (!File.Exists(imagePath))
        {
            // Initialize a QR code generator with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
            {
                // Set a moderate error correction level (Level M)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code to the specified file
                generator.Save(imagePath);
            }
        }

        // Create a BarCodeReader that uses the predefined Types2D set to detect common 2D barcodes
        using (var reader = new BarCodeReader(imagePath, DecodeType.Types2D))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of the detected barcode
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");

                // Output the decoded text of the barcode
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}