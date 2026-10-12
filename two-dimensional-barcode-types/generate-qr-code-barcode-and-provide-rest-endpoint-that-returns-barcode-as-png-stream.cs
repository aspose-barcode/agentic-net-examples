// Title: Generate QR Code and Return as PNG via Simulated REST Endpoint
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG byte array, and exposing it through a simulated REST method.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image output. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat for producing PNG images. Developers often need to generate barcodes on-the-fly for web APIs, mobile apps, or document workflows, and this pattern illustrates how to return the barcode as a byte stream suitable for HTTP responses.
// Prompt: Generate QR Code barcode and provide a REST endpoint that returns barcode as PNG stream.
// Tags: qr code,barcode generation,rest endpoint,png output,aspose.barcode,aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample console application that generates a QR Code barcode,
/// simulates a REST endpoint returning the barcode as a PNG byte array,
/// and writes the result to a file for verification.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates QR code data, invokes the simulated REST endpoint,
    /// and saves the returned PNG to disk.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code
        string codeText = "Hello Aspose QR!";

        // Call the simulated REST endpoint to obtain PNG bytes
        byte[] pngData = RestEndpoint(codeText);

        // Persist the PNG to a file so the result can be inspected
        string outputPath = "qr.png";
        File.WriteAllBytes(outputPath, pngData);
        Console.WriteLine($"QR code generated and saved to '{outputPath}'. Size: {pngData.Length} bytes.");
    }

    /// <summary>
    /// Simulates a REST API method that receives the QR code text,
    /// generates the barcode, and returns the PNG image as a byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the QR code.</param>
    /// <returns>Byte array containing the PNG representation of the QR code.</returns>
    static byte[] RestEndpoint(string codeText)
    {
        // Generate the QR code into a memory stream
        using (MemoryStream qrStream = GenerateQrCodeStream(codeText))
        {
            // Copy the stream contents to a new memory stream to obtain a clean byte array
            using (MemoryStream copy = new MemoryStream())
            {
                qrStream.CopyTo(copy);
                return copy.ToArray();
            }
        }
    }

    /// <summary>
    /// Generates a QR Code barcode and writes it as PNG data into a memory stream.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <returns>MemoryStream positioned at the beginning, containing PNG data.</returns>
    static MemoryStream GenerateQrCodeStream(string codeText)
    {
        // Prepare a memory stream to receive the PNG image
        MemoryStream ms = new MemoryStream();

        // Create a BarcodeGenerator for QR encoding
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: adjust the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as PNG into the memory stream
            generator.Save(ms, BarCodeImageFormat.Png);
        }

        // Reset stream position so callers can read from the beginning
        ms.Position = 0;
        return ms;
    }
}