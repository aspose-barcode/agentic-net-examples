// Title: Generate QR Code with Binary Encoding from Byte Array
// Description: Demonstrates how to encode a byte array using QR Code's binary mode and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QREncodeMode to create QR Code barcodes. Typical use cases include encoding raw binary data for scanning applications, where developers need to produce high‑density QR images in common formats like PNG.
// Prompt: Generate a QR Code barcode using binary encoding mode from a byte array and save as PNG.
// Tags: qr, binary, png, generation, aspose.barcode, barcode symbology

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the QR Code binary encoding example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code from a byte array using binary mode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a sample byte array that will be encoded into the QR Code.
        byte[] data = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };

        // Determine a temporary file path for the output PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "QrBinary.png");

        // Create a BarcodeGenerator for QR Code symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the raw binary data as the code text.
            generator.SetCodeText(data);

            // Configure the QR Code to use binary encoding mode.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Save the generated QR Code image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}