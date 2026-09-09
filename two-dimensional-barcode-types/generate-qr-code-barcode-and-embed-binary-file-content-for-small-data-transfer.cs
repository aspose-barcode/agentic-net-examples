// Title: Generate QR Code with Embedded Binary Data
// Description: Demonstrates creating a QR Code that encodes binary file content, useful for small data transfers.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation with binary encoding. It showcases the use of BarcodeGenerator, EncodeTypes.QR, and QREncodeMode.Binary to embed raw byte arrays into a QR symbol. Developers often need to transfer small files or configuration data via QR codes, and this pattern illustrates the typical workflow for such scenarios.
// Prompt: Generate QR Code barcode and embed binary file content for small data transfer.
// Tags: qr code, binary data, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code containing binary file data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a QR Code with binary mode and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrBinaryDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Create a sample binary file with arbitrary data
        string binaryFilePath = Path.Combine(tempFolder, "sample.bin");
        byte[] sampleData = new byte[] { 0x01, 0x02, 0xFF, 0x00, 0xAB };
        File.WriteAllBytes(binaryFilePath, sampleData);

        // Read the binary content back into a byte array
        byte[] fileBytes = File.ReadAllBytes(binaryFilePath);

        // Generate a QR Code using binary encoding mode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the QR Code payload to the binary data
            generator.SetCodeText(fileBytes);
            // Configure the QR encoder to treat the data as binary
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Define the output image path and save the QR Code as PNG
            string outputPath = Path.Combine(tempFolder, "qr_binary.png");
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the QR Code image was saved
            Console.WriteLine($"QR Code saved to: {outputPath}");
        }
    }
}