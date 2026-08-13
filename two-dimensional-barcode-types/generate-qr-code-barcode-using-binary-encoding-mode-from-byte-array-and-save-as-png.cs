// Title: Generate QR Code with Binary Encoding from Byte Array
// Description: Demonstrates how to create a QR Code barcode using binary encoding mode from a raw byte array and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with custom encoding settings. It showcases the use of BarcodeGenerator, EncodeTypes, and QREncodeMode to produce binary‑encoded QR symbols, a common requirement when embedding arbitrary binary data in barcodes. Developers often need to generate QR codes for data such as file hashes, binary payloads, or compact binary identifiers.
// Prompt: Generate a QR Code barcode using binary encoding mode from a byte array and save as PNG.
// Tags: qr code,binary encoding,barcode generation,aspose.barcode,png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.Generation; // QREncodeMode enum resides here

/// <summary>
/// Example program that generates a QR Code barcode using binary encoding mode from a byte array and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a sample byte array to encode in binary mode.
        byte[] data = { 0xDE, 0xAD, 0xBE, 0xEF, 0x01, 0x02, 0x03, 0x04 };

        // Build the output file path (saved in the current working directory).
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_binary.png");

        // Initialize the QR Code generator with the QR symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Configure the generator to use binary encoding mode for QR codes.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Assign the raw byte array as the code text to be encoded.
            generator.SetCodeText(data);

            // Save the generated QR Code as a PNG image.
            generator.Save(outputPath);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}