using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code thumbnail image using Aspose.BarCode.
/// The image is saved locally; in production it could be uploaded to cloud storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, resizes it for thumbnail use, and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string qrText = "https://example.com";

        // Build the full path for the output JPEG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_thumbnail.jpg");

        // Create a BarcodeGenerator for QR encoding with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Configure auto‑size mode to use interpolation for smoother scaling.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired thumbnail dimensions (200 points width and height).
            generator.Parameters.ImageWidth.Point = 200f;   // Width in points
            generator.Parameters.ImageHeight.Point = 200f;  // Height in points

            // Save the generated QR code directly as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the thumbnail was saved.
        Console.WriteLine($"QR code thumbnail saved to: {outputPath}");

        // Note: In a production environment, the file at 'outputPath' would be uploaded
        // to a cloud storage bucket using the appropriate SDK (e.g., AWS S3, Azure Blob,
        // Google Cloud Storage). This example uses local file I/O for simplicity.
    }
}