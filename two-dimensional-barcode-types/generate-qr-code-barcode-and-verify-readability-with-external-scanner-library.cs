// Title: Generate and validate QR Code barcode using Aspose.BarCode
// Description: Demonstrates how to create a QR Code image with Aspose.BarCode and then read it back to verify the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to produce a QR Code, configure its parameters (X‑dimension, ECI encoding, error correction), save the image, and then employ BarCodeReader to decode the QR Code. Developers working with barcode creation and validation, especially for QR codes in .NET applications, can use this pattern for testing and integration with external scanning solutions.
// Prompt: Generate a QR Code barcode and verify readability with external scanner library.
// Tags: qr code, barcode generation, barcode recognition, aspose.barcode, c#, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code using Aspose.BarCode,
/// saves it as a PNG image, and then reads the image back to verify
/// that the encoded text can be correctly decoded.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the QR image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeQrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "qr.png");

        // ------------------------------------------------------------
        // Define the text that will be encoded into the QR Code
        // ------------------------------------------------------------
        string codeText = "Hello Aspose QR!";

        // ------------------------------------------------------------
        // Generate the QR Code image with specific parameters
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of a single QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 8;

            // Use ECI encoding to support UTF‑8 characters
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Choose a moderate error correction level (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the QR Code image was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the QR Code from the saved image and compare the result
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                var result = results[0];
                Console.WriteLine($"Decoded text: {result.CodeText}");
                Console.WriteLine($"Match with original: {result.CodeText == codeText}");
            }
            else
            {
                Console.WriteLine("No barcode detected in the generated image.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and folder
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}