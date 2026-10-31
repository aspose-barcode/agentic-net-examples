// Title: Generate QR Code with Binary Encoding from Byte Array
// Description: Demonstrates how to create a QR Code barcode using binary encoding mode from a byte array and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation with binary data. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and BarCodeImageFormat classes to encode raw bytes into a QR symbol, a common requirement for embedding binary payloads (e.g., firmware, encrypted data) in scannable form. Developers often need to generate such barcodes for data exchange, product authentication, or offline data storage.
// Prompt: Generate a QR Code barcode using binary encoding mode from a byte array and save as PNG.
// Tags: qr code, binary encoding, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode from a byte array using binary encoding mode
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a sample byte array containing the binary data to encode.
        byte[] data = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };

        // Build the full output path for the generated PNG image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_binary.png");

        // Create a BarcodeGenerator configured for QR Code symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the binary data as the code text.
            generator.SetCodeText(data);

            // Set the QR Code encoding mode to Binary to treat the data as raw bytes.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Save the generated QR Code image in PNG format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}