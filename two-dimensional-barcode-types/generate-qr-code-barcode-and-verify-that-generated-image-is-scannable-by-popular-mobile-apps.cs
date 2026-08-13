// Title: Generate and Verify QR Code Barcode with Aspose.BarCode
// Description: This example creates a QR Code image containing a URL, saves it to a temporary file, and then reads it back to confirm it can be decoded, ensuring the barcode is scannable.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition for QR Code symbology. It uses BarcodeGenerator to encode data, configures QR error correction, saves the image, and employs BarCodeReader with DecodeType.QR to validate the result. Developers working with QR Code creation, image output, and verification can use this pattern to ensure generated barcodes are readable by mobile scanning apps.
// Prompt: Generate QR Code barcode and verify that generated image is scannable by popular mobile apps.
// Tags: qr code, barcode generation, barcode recognition, image output, aspose.barcode, encode types, decode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR Code, saves it as an image, and verifies its readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, writes it to a temporary PNG file, and attempts to decode it.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_test.png");

        // -------------------- QR Code Generation --------------------
        // Create a BarcodeGenerator for QR symbology with the desired text (a URL in this case).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure a moderate error correction level to improve scan reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // -------------------- Verification of Generated QR Code --------------------
        // Ensure the image file was created before attempting to read it.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate QR Code image.");
            return;
        }

        bool decoded = false;

        // Initialize a BarCodeReader to decode QR Code symbols from the saved image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            // Iterate through all detected barcodes (should be one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded QR Code text: {result.CodeText}");
                decoded = true;
            }
        }

        // Report the outcome of the verification step.
        if (!decoded)
        {
            Console.WriteLine("The QR Code could not be decoded. It may not be scannable.");
        }
        else
        {
            Console.WriteLine("QR Code generation and verification succeeded.");
        }
    }
}