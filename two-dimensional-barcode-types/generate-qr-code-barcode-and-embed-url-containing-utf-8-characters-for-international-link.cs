using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code containing UTF‑8 characters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code for a URL with UTF‑8 characters and saves it as an image.
    /// </summary>
    static void Main()
    {
        // URL containing UTF‑8 characters (example with Chinese characters)
        string url = "https://例子.测试/路径?参数=值";

        // Create a QR Code generator within a using block to ensure proper disposal
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text (URL) to be encoded in the QR code
            generator.CodeText = url;

            // Enable ECI (Extended Channel Interpretation) encoding for UTF‑8 characters
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Optional: set the error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Define the output file path for the generated QR code image
            string outputPath = "qr_utf8.png";

            // Save the QR code image to the specified path
            generator.Save(outputPath);

            // Inform the user where the QR code image has been saved
            Console.WriteLine($"QR Code saved to: {outputPath}");
        }
    }
}