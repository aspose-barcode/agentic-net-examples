// Title: Generate QR Code with Automatic Sizing Based on Payload Length
// Description: Demonstrates creating QR Code barcodes where the symbol size automatically adjusts to fit the length of the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR, AutoSizeMode, and QR error correction settings. Typical use cases include dynamically sized QR codes for varying data payloads in web, mobile, or desktop applications. Developers often need to generate QR codes that adapt to content length without manually selecting versions.
// Prompt: Generate QR Code barcode and enable automatic size to adapt to payload length.
// Tags: qr, barcode, generation, autosize, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates QR Code barcodes with automatic size adaptation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes for sample payloads and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Sample payloads of varying lengths
        List<string> payloads = new List<string>
        {
            "Short",
            "This is a medium length text for QR code.",
            "This is a longer text payload intended to test the automatic sizing capability of the QR code generator. It includes multiple sentences and enough characters to increase the QR code version automatically."
        };

        // Generate a QR code for each payload
        for (int i = 0; i < payloads.Count; i++)
        {
            string text = payloads[i];
            string filePath = Path.Combine(outputDir, $"qr_{i + 1}.png");

            // Initialize the barcode generator for QR code symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the text to encode
                generator.CodeText = text;

                // Enable automatic size adaptation based on the payload
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

                // Optional: set a higher error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image as PNG
                generator.Save(filePath);
            }

            // Inform the user about the saved file
            Console.WriteLine($"Generated QR code saved to: {filePath}");
        }
    }
}