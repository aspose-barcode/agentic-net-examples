// Title: Generate QR Code with Embedded Binary Data
// Description: Demonstrates creating a QR Code barcode that encodes binary file content using Aspose.BarCode, useful for small data transfers.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation with binary encoding. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and BarCodeImageFormat classes to embed raw bytes into a QR symbol. Developers often need to transfer small files or configuration data via QR codes, and this pattern illustrates the typical workflow for such scenarios.
// Prompt: Generate QR Code barcode and embed binary file content for small data transfer.
// Tags: qr code, binary data, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code containing the raw bytes of a small binary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Prepares a sample binary file (if missing), reads its bytes,
    /// encodes them into a QR Code using binary mode, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Ensure a sample binary file exists; create one with placeholder data if not.
        string binaryFilePath = "sample.bin";
        if (!File.Exists(binaryFilePath))
        {
            byte[] sampleData = { 0xDE, 0xAD, 0xBE, 0xEF };
            File.WriteAllBytes(binaryFilePath, sampleData);
        }

        // Load the entire binary content into memory.
        byte[] fileBytes = File.ReadAllBytes(binaryFilePath);

        // Initialize the QR Code generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the binary data as the code text.
            generator.SetCodeText(fileBytes);

            // Switch the QR encoder to binary mode to handle raw bytes.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Define the output image path and save the QR Code as a PNG file.
            string outputImagePath = "qr_binary.png";
            generator.Save(outputImagePath, BarCodeImageFormat.Png);

            // Inform the user where the image was saved.
            Console.WriteLine($"QR Code saved to: {Path.GetFullPath(outputImagePath)}");
        }
    }
}