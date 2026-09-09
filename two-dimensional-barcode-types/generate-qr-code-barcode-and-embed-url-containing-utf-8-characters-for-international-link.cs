// Title: Generate QR Code with UTF‑8 URL using Aspose.BarCode
// Description: Demonstrates creating a QR Code that encodes a URL containing international (UTF‑8) characters and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation with ECI (Extended Channel Interpretation) encoding. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and ECIEncodings classes to produce QR symbols for international links. Developers often need to embed Unicode URLs in QR codes for multilingual web pages, marketing materials, or mobile app deep linking.
// Prompt: Generate QR Code barcode and embed a URL containing UTF‑8 characters for international link.
// Tags: qr code, barcode generation, utf-8, eci, png, aspose.barcode, encode types

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code containing a UTF‑8 URL and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the output folder, configures the QR generator with ECI UTF‑8 encoding,
    /// writes the QR image to disk, and prints the file location.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "QrDemo");
        Directory.CreateDirectory(outputDir);

        // Full path of the PNG file to be created
        string outputPath = Path.Combine(outputDir, "InternationalUrlQr.png");

        // URL that includes UTF‑8 characters (Chinese example domain and path)
        string url = "https://例子.测试/路径?查询=值";

        // Initialize the QR Code generator with the QR symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Enable ECI mode so the QR can carry UTF‑8 data
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Optional: increase the size of each QR module for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Assign the URL text to the barcode, explicitly using UTF‑8 encoding
            generator.SetCodeText(url, Encoding.UTF8);

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}