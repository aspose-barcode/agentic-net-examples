using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.Generation; // BarCodeImageFormat is in this namespace

/// <summary>
/// Demonstrates generating a QR code image, converting it to a Base64 data URI,
/// and outputting an HTML <img> tag that can be embedded in a Razor view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves it as a PNG, creates a Base64 data URI,
    /// and prints an HTML <img> tag.
    /// </summary>
    static void Main()
    {
        // NOTE: Full Razor page integration cannot be demonstrated in a console application.
        // The example generates a QR code image, converts it to a Base64 data URI,
        // and prints an HTML <img> tag that can be used in a Razor view.

        // Define QR code content and temporary output file path
        string qrContent = "https://example.com";
        string tempFile = Path.Combine(Path.GetTempPath(), "qr_code.png");

        // Generate QR code and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Optional: set error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            // Save the generated QR code image to the temporary file
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Verify that the PNG file was successfully created
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // Read the image bytes from the file
        byte[] imageBytes = File.ReadAllBytes(tempFile);
        // Convert the image bytes to a Base64 string
        string base64 = Convert.ToBase64String(imageBytes);
        // Build a data URI that embeds the Base64-encoded PNG image
        string dataUri = $"data:image/png;base64,{base64}";

        // Construct an HTML <img> tag using the data URI as the source
        string imgTag = $"<img src=\"{dataUri}\" alt=\"QR Code\" />";
        // Output the <img> tag to the console (can be copied into a Razor view)
        Console.WriteLine(imgTag);
    }
}