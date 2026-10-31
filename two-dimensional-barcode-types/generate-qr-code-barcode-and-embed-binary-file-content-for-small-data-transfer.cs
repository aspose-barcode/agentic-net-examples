// Title: Generate QR Code with Embedded Binary Data
// Description: This example creates a small binary file, encodes its bytes into a QR Code using binary mode, and saves the barcode as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode QR Code generation with binary encoding. It showcases the BarcodeGenerator class, QR encoding settings (EncodeMode, ErrorLevel), and saving to common image formats. Useful for developers needing to transfer small binary payloads via QR codes in applications such as data exchange, device provisioning, or secure token distribution.
// Prompt: Generate QR Code barcode and embed binary file content for small data transfer.
// Tags: qr code, binary data, barcode generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode that encodes binary file content using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a sample binary file, encodes it into a QR Code, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeQrBinaryDemo");
        Directory.CreateDirectory(tempDir);

        // Create a sample binary file with arbitrary data
        string binaryFilePath = Path.Combine(tempDir, "sample.bin");
        byte[] sampleData = new byte[] { 0x01, 0x02, 0xFF, 0x00, 0xAB, 0xCD };
        File.WriteAllBytes(binaryFilePath, sampleData);

        // Verify the file exists before proceeding
        if (!File.Exists(binaryFilePath))
        {
            Console.WriteLine("Binary file not found.");
            return;
        }

        // Read the binary content into a byte array
        byte[] fileBytes = File.ReadAllBytes(binaryFilePath);

        // Generate QR Code with binary mode enabled
        string outputPath = Path.Combine(tempDir, "qr_binary.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the QR Code payload to the binary data
            generator.SetCodeText(fileBytes);
            // Configure QR encoding to binary and set error correction level
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;
            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}