// Title: QR Code Generation, Base64 Encoding, and Decoding with DetectEncoding
// Description: Demonstrates generating a QR code containing Unicode text, converting the image to a Base64 string, then decoding it back while enabling automatic encoding detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes, BarCodeReader for decoding, and the DetectEncoding setting for handling Unicode data. Developers building services that need to exchange barcode images as Base64 strings—such as web APIs, mobile back‑ends, or document processing pipelines—can follow this pattern to reliably encode and decode barcodes.
// Prompt: Develop a backend service that receives base64‑encoded barcode images, decodes them with DetectEncoding enabled, and returns decoded text.
// Tags: qr, unicode, base64, barcode-generation, barcode-recognition, detectencoding, aspnet, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code with Unicode text, encodes the image to Base64,
/// then decodes the Base64 string back to an image and reads the barcode using DetectEncoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generate‑encode‑decode‑read workflow.
    /// </summary>
    static void Main()
    {
        // Define sample Unicode text to be encoded into the QR code.
        string sampleText = "مرحبا Aspose";

        // Generate a QR code image and obtain its Base64 representation.
        string base64Image;
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
        {
            // Set the module size (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Convert the image bytes to a Base64 string.
                byte[] imageBytes = ms.ToArray();
                base64Image = Convert.ToBase64String(imageBytes);
            }
        }

        // Decode the Base64 string back to a byte array representing the image.
        byte[] decodedBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream imageStream = new MemoryStream(decodedBytes))
        {
            // Initialize a barcode reader for QR codes with DetectEncoding enabled.
            using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.QR))
            {
                reader.BarcodeSettings.DetectEncoding = true;

                // Iterate through all detected barcodes and output the decoded text.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}