// Title: Generate QR Code and Create Thumbnail for Web UI
// Description: Demonstrates generating a QR Code barcode, saving it as an image, creating a 100x100 thumbnail, and converting it to a Base64 string for embedding in a web page.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image processing category. It showcases the use of BarcodeGenerator (EncodeTypes.QR) to create QR Code barcodes, Aspose.Drawing for image manipulation, and typical steps developers need when preparing barcode images for web UI display, such as thumbnail creation and Base64 encoding. Useful for web developers integrating dynamic barcodes into HTML or JavaScript front‑ends.
// Prompt: Generate QR Code barcode and retrieve thumbnail from cloud storage for display in web UI.
// Tags: qr code, barcode generation, thumbnail, base64, aspose.barcode, aspose.drawing, image processing, web ui

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation, thumbnail creation, and Base64 conversion for web UI display.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code image, generates a thumbnail, and outputs the Base64 string.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR Code.
        const string qrText = "https://example.com";

        // Create a temporary directory to store the generated QR Code image.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string qrImagePath = Path.Combine(tempDir, "qr.png");

        // 1. Generate QR Code barcode and save it to a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = qrText;
            // Optional: set a high error correction level for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save(qrImagePath);
        }

        // 2. Load the saved QR Code image and create a 100x100 thumbnail.
        using (Image originalImage = Image.FromFile(qrImagePath))
        {
            const int thumbWidth = 100;
            const int thumbHeight = 100;

            using (var thumbnail = new Bitmap(thumbWidth, thumbHeight))
            {
                using (var graphics = Graphics.FromImage(thumbnail))
                {
                    // Draw the original image scaled down to the thumbnail dimensions.
                    graphics.DrawImage(originalImage, new Rectangle(0, 0, thumbWidth, thumbHeight));
                }

                // 3. Convert the thumbnail to a Base64 string (simulating UI display).
                using (var ms = new MemoryStream())
                {
                    thumbnail.Save(ms, ImageFormat.Png);
                    string base64Thumb = Convert.ToBase64String(ms.ToArray());
                    Console.WriteLine("Thumbnail Base64:");
                    Console.WriteLine(base64Thumb);
                }
            }
        }

        // 4. Placeholder for cloud storage thumbnail retrieval.
        // In a real environment you would download the thumbnail from Azure Blob, AWS S3, etc.
        // Example (commented out because the SDK is not available in the snippet runner):
        /*
        // Azure Blob example:
        // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // using var downloadStream = new MemoryStream();
        // blobClient.DownloadTo(downloadStream);
        // downloadStream.Position = 0;
        // using var cloudImage = Image.FromStream(downloadStream);
        // // Process cloudImage as needed...
        */

        // Clean up temporary files and directories.
        try
        {
            if (File.Exists(qrImagePath))
                File.Delete(qrImagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit.
        }
    }
}