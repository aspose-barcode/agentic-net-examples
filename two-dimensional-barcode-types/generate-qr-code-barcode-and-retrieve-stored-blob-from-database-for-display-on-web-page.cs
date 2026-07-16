// Title: Generate QR Code and Convert to Base64 Data URL
// Description: Demonstrates creating a QR Code image, simulating its storage as a BLOB, and converting it to a Base64 data URL for web display.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image handling category. It showcases the use of BarcodeGenerator, QR encoding settings, and image format conversion, which are common tasks when developers need to embed barcodes in web pages or store them in databases as binary data.
// Prompt: Generate QR Code barcode and retrieve stored BLOB from database for display on web page.
// Tags: qr code, barcode generation, blob retrieval, base64, aspose.barcode, image conversion

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation, BLOB handling, and conversion to a Base64 data URL for web usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, reads it as a byte array, and outputs a Base64 data URL.
    /// </summary>
    static void Main()
    {
        // Define the QR code content and the file path where the image will be saved
        string qrContent = "https://example.com";
        string outputPath = "qr.png";

        // Generate QR code image using Aspose.BarCode and save it as a PNG file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Configure the QR code to use the highest error correction level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Persist the generated barcode to the specified file in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully before attempting to read it
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: Generated file '{outputPath}' not found.");
            return;
        }

        // Simulate retrieving the image BLOB from a database by reading the file's binary content
        byte[] imageBlob = File.ReadAllBytes(outputPath);

        // Convert the binary BLOB to a Base64 string, suitable for embedding in HTML <img> tags
        string base64Image = Convert.ToBase64String(imageBlob);
        string dataUrl = $"data:image/png;base64,{base64Image}";

        // Output the data URL; it can be used directly as the src attribute of an <img> element
        Console.WriteLine("QR Code Image Data URL:");
        Console.WriteLine(dataUrl);
    }
}