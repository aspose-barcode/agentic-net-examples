// Title: Generate QR Code with UTF‑8 URL using Aspose.BarCode
// Description: Demonstrates how to create a QR Code barcode that encodes a URL containing UTF‑8 characters and saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with QR symbology, ECI encoding, and image output. It shows how to configure QR encoding mode, set UTF‑8 ECI, adjust module size, and save the barcode. Developers working with internationalized data, QR codes, or custom encoding will find these patterns useful for generating barcodes programmatically.
// Prompt: Generate QR Code barcode and embed a URL containing UTF‑8 characters for international link.
// Tags: qr code,utf-8,barcode generation,aspose.barcode,encoding,png output

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code containing a UTF‑8 encoded URL and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates and saves a QR Code barcode.
    /// </summary>
    static void Main()
    {
        // Define the URL with UTF‑8 characters to encode in the QR Code.
        string url = "https://例子.测试/路径?参数=值";

        // Determine a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "QrCode.png");

        // Initialize the barcode generator for QR Code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the QR Code text and specify UTF‑8 encoding.
            generator.SetCodeText(url, Encoding.UTF8);

            // Configure QR encoding to use ECI (Extended Channel Interpretation) mode.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;

            // Specify UTF‑8 as the ECI encoding for the QR Code.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Adjust the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}