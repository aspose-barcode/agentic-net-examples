// Title: Generate QR Code with Binary Encoding from Byte Array
// Description: Demonstrates how to create a QR Code barcode using binary encoding mode from a byte array and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with specific encoding modes. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and BarCodeImageFormat classes to produce QR Code images. Developers often need to encode raw binary data into QR Codes for data transfer, authentication, or compact storage, and this snippet illustrates the typical steps required.
// Prompt: Generate a QR Code barcode using binary encoding mode from a byte array and save as PNG.
// Tags: qr-code,binary-encoding,barcode-generation,png,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode using binary encoding mode from a byte array
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeOutput");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the PNG file that will store the QR Code.
        string outputPath = Path.Combine(outputDir, "QrEncodeModeBinary.png");

        // Byte array containing the raw binary data to encode into the QR Code.
        byte[] data = new byte[] { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };

        // Initialize the barcode generator for QR Code symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the binary data as the code text for the QR Code.
            generator.SetCodeText(data);

            // Configure the QR Code to use binary encoding mode.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}