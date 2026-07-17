// Title: Generate QR Code and download thumbnail for web UI
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and downloading a thumbnail image from cloud storage, suitable for displaying in a web application.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image handling category. It showcases the use of BarcodeGenerator, QR code parameters, and standard .NET HttpClient to retrieve external images. Developers often need to generate barcodes server‑side and combine them with remote assets for UI rendering, making this pattern common in ASP.NET web projects.
// Prompt: Generate QR Code barcode and retrieve thumbnail from cloud storage for display in web UI.
// Tags: qr code, barcode generation, thumbnail, cloud storage, aspnet, aspose.barcode, image retrieval

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode and downloading a thumbnail image from a cloud URL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code, saves it locally, downloads a thumbnail, and writes file paths to console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // 1. Generate a QR Code barcode and save it locally.
        // --------------------------------------------------------------------
        const string qrFilePath = "qr.png";
        const string qrText = "https://example.com";

        // Initialize the barcode generator with QR encoding and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Configure a high error‑correction level to improve readability after damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Persist the generated QR code image to the file system.
            generator.Save(qrFilePath);
        }

        Console.WriteLine($"QR code saved to: {Path.GetFullPath(qrFilePath)}");

        // --------------------------------------------------------------------
        // 2. Retrieve a thumbnail image from a cloud URL (simulated with a placeholder).
        // --------------------------------------------------------------------
        const string thumbnailUrl = "https://via.placeholder.com/150";
        const string thumbnailFilePath = "thumbnail.png";

        // Use HttpClient to download the image data.
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Synchronously request the image; in production code consider async/await.
                using (var response = httpClient.GetAsync(thumbnailUrl).Result)
                {
                    // Throw if the HTTP status is not successful.
                    response.EnsureSuccessStatusCode();

                    // Read the response stream and write it directly to a local file.
                    using (var stream = response.Content.ReadAsStreamAsync().Result)
                    using (var fileStream = new FileStream(thumbnailFilePath, FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                Console.WriteLine($"Thumbnail downloaded to: {Path.GetFullPath(thumbnailFilePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the download process.
                Console.WriteLine($"Failed to download thumbnail: {ex.Message}");
            }
        }

        // Note: In a real web UI, the generated QR code and the downloaded thumbnail would be
        // served to the client (e.g., via an ASP.NET controller). This console example demonstrates
        // the core barcode generation and image retrieval logic required for such a scenario.
    }
}