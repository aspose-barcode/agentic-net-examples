// Title: Generate QR Code and Return PNG via REST Endpoint (example)
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it as a PNG byte array. The PNG can be sent from a REST API as a response stream.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code symbology. It shows how to use the BarcodeGenerator class with EncodeTypes.QR, configure image format, and obtain a byte array suitable for web services. Developers building REST endpoints often need to generate barcodes on‑the‑fly and return them as image streams without writing to disk.
// Prompt: Generate QR Code barcode and provide a REST endpoint that returns barcode as PNG stream.
// Tags: qr code, barcode generation, png output, aspnet core, aspose.barcode, rest endpoint

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple console demonstration of generating a QR Code barcode and obtaining its PNG representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code PNG, writes it to a temporary file, and outputs diagnostic information.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code.
        string sampleText = "Hello Aspose QR!";

        // Generate the QR Code as a PNG byte array.
        byte[] pngData = GenerateQrCodePng(sampleText);

        // Determine a temporary file path for demonstration purposes.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_sample.png");

        // Write the PNG bytes to the file system.
        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            fileStream.Write(pngData, 0, pngData.Length);
        }

        // Output the location and size of the generated PNG.
        Console.WriteLine($"QR code PNG generated: {outputPath}");
        Console.WriteLine($"Byte size: {pngData.Length}");
    }

    /// <summary>
    /// Generates a QR Code barcode for the specified text and returns it as a PNG byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the QR Code.</param>
    /// <returns>Byte array containing the PNG image of the QR Code.</returns>
    static byte[] GenerateQrCodePng(string codeText)
    {
        // Initialize the barcode generator with QR encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the stream contents as a byte array.
                return ms.ToArray();
            }
        }
    }
}