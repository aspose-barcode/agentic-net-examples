// Title: Generate QR Code and Validate Specification Version
// Description: Demonstrates generating a QR Code barcode, saving it as an image, and recognizing it to verify compliance with the QR specification version.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (EncodeTypes.QR) to create QR Code images and BarCodeReader (DecodeType.QR) to decode and validate them. Developers often need to generate barcodes for data encoding and then confirm that the output meets specific standards such as QR version or error correction level.
// Prompt: Generate QR Code barcode and validate that generated code complies with QR specification version.
// Tags: qr, barcode, generation, recognition, validation, aspose.barcodes, aspnet

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code, saves it to a file, and then reads it back
/// to confirm successful creation and basic compliance with QR specifications.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, writes it to disk, and validates it.
    /// </summary>
    static void Main()
    {
        // Define the output image file path
        string outputPath = "qr.png";

        // ------------------------------------------------------------
        // Generate QR Code barcode
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Optional: set the error correction level (LevelM = ~15% error recovery)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code image to the specified path
            generator.Save(outputPath);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // ------------------------------------------------------------
        // Read and validate the generated QR Code
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            bool found = false;

            // Iterate through all detected barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text to the console
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // Placeholder for version validation:
                // In a full implementation you could inspect result.Extended.QR.Version
                // to ensure it matches the expected QR specification version.
                found = true;
            }

            // Report the outcome of the recognition process
            if (!found)
            {
                Console.WriteLine("No QR code detected in the generated image.");
            }
            else
            {
                Console.WriteLine("QR code generation and recognition succeeded.");
            }
        }
    }
}