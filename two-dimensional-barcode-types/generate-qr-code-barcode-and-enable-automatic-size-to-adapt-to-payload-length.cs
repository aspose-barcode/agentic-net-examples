// Title: Generate QR Code with Automatic Size Adaptation
// Description: Demonstrates creating QR Code barcodes where the symbol size automatically adjusts to the length of the input payload.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR codes. It highlights setting visual parameters such as XDimension and saving the result as PNG. Developers working with dynamic data often need barcodes that resize automatically based on payload length, making this pattern common in reporting, inventory, and mobile scanning scenarios.
// Prompt: Generate QR Code barcode and enable automatic size to adapt to payload length.
// Tags: qr code, barcode generation, automatic size, png, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates QR Code barcodes whose size adapts automatically to the payload length.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates output directory, generates QR codes for sample payloads,
    /// and saves them as PNG images.
    /// </summary>
    static void Main()
    {
        // Determine the output folder relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample payloads of varying length to demonstrate automatic size adaptation.
        string[] payloads = new string[]
        {
            "Short",
            "This is a longer text to demonstrate automatic QR code size adaptation based on payload length."
        };

        // Iterate over each payload, generate a QR code, and save it as a PNG file.
        for (int i = 0; i < payloads.Length; i++)
        {
            string text = payloads[i];
            string filePath = Path.Combine(outputDir, $"QrCode_{i + 1}.png");

            // Create a BarcodeGenerator for QR encoding with the current payload.
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set the module (pixel) size of the QR code; the overall size will scale automatically.
                gen.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated QR code image to the specified file path in PNG format.
                gen.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user where the QR code image has been saved.
            Console.WriteLine($"Generated QR code saved to: {filePath}");
        }
    }
}