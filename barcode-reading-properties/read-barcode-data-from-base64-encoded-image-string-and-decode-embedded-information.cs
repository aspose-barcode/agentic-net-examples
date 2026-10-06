// Title: Decode QR Code from Base64 Image String
// Description: Demonstrates generating a QR code, converting it to a Base64 string, and then decoding the barcode from the image data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code, BarCodeImageFormat for image output, and BarCodeReader with DecodeType.AllSupportedTypes to extract embedded information. Developers often need to exchange barcode images as text (e.g., Base64) across services, then decode them without persisting files.
// Prompt: Read barcode data from a base64‑encoded image string and decode the embedded information.
// Tags: qr, barcode, base64, decode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, encodes it as Base64, and decodes the barcode from the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample QR code, prints its Base64 representation, and reads the barcode data from the image bytes.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR code and obtain its Base64 representation
        string base64Image = GenerateSampleBase64();

        // Output the Base64 string to the console
        Console.WriteLine("Base64 Image:");
        Console.WriteLine(base64Image);
        Console.WriteLine();

        // Decode the Base64 string back to raw image bytes
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Create a memory stream from the image bytes for barcode reading
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Initialize the barcode reader to recognize all supported types
            using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Display the decoded text and the type of barcode
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                }
            }
        }
    }

    /// <summary>
    /// Generates a QR code containing the text "Hello World" and returns its PNG representation as a Base64 string.
    /// </summary>
    /// <returns>Base64-encoded PNG image of the generated QR code.</returns>
    private static string GenerateSampleBase64()
    {
        // Create a QR code generator with the desired content
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Save the generated QR code to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the stream's byte array to a Base64 string
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}