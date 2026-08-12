// Title: Generate QR Code with Embedded Binary Data
// Description: Demonstrates how to embed raw binary file content into a QR Code barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.QR to create QR Code barcodes. It shows how to set raw byte data as the code text, adjust error correction level, and export the barcode to common image formats. Developers working on data transfer, product labeling, or mobile scanning often need to embed binary payloads in QR codes using Aspose.BarCode.
// Prompt: Generate QR Code barcode and embed binary file content for small data transfer.
// Tags: qr code, binary data, embed, png, barcodegenerator, encode types, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code containing binary data and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the sample binary file
        string binaryFilePath = Path.Combine(tempFolder, "sample.bin");

        // Write a small set of binary data to the file
        byte[] sampleData = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF, 0x01, 0x02, 0x03 };
        File.WriteAllBytes(binaryFilePath, sampleData);

        // Read the binary content back into a byte array
        byte[] fileContent = File.ReadAllBytes(binaryFilePath);

        // Generate a QR Code barcode with the binary content embedded
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the raw bytes as the code text for the QR Code
            generator.SetCodeText(fileContent);

            // Optional: increase error correction level for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the output image path
            string qrImagePath = Path.Combine(tempFolder, "qr_code.png");

            // Save the QR Code image in PNG format
            generator.Save(qrImagePath, BarCodeImageFormat.Png);

            Console.WriteLine($"QR Code image saved to: {qrImagePath}");
        }

        // Clean up: (optional) delete temporary files if desired
        // Directory.Delete(tempFolder, true);
    }
}