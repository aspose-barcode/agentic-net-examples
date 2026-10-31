// Title: Download image from AWS S3 and read PDF417 linked state metadata
// Description: Demonstrates downloading a barcode image from an Amazon S3 bucket and using Aspose.BarCode to decode a PDF417 barcode, extracting its linked‑state metadata.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on PDF417 symbology. It showcases the BarCodeReader class for image‑based decoding, typical for scenarios where barcodes are stored remotely (e.g., cloud storage) and need to be processed server‑side. Developers often use this pattern to retrieve additional barcode properties such as linked state, error correction level, or segment information.
// Prompt: Download image from AWS S3 bucket and read PDF417 linked state metadata.
// Tags: pdf417, barcode, recognition, aws, s3, download, metadata, aspose.barcode, csharp

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that downloads a barcode image from an AWS S3 bucket,
/// decodes a PDF417 barcode using Aspose.BarCode, and outputs linked‑state metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously downloads the image, reads the PDF417 barcode, and displays its metadata.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // --------------------------------------------------------------------
        // 1. Define the public S3 URL of the barcode image.
        // --------------------------------------------------------------------
        string s3Url = "https://example-bucket.s3.amazonaws.com/sample.png";

        // --------------------------------------------------------------------
        // 2. Create a temporary file path to store the downloaded image.
        // --------------------------------------------------------------------
        string tempFilePath = Path.Combine(Path.GetTempPath(), "downloaded_barcode.png");

        // --------------------------------------------------------------------
        // 3. Download the image from S3 and write it to the temporary file.
        // --------------------------------------------------------------------
        try
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] data = await client.GetByteArrayAsync(s3Url);
                File.WriteAllBytes(tempFilePath, data);
                Console.WriteLine($"Image downloaded to: {tempFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to download image: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // 4. Verify that the file was successfully saved.
        // --------------------------------------------------------------------
        if (!File.Exists(tempFilePath))
        {
            Console.WriteLine("Downloaded file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // 5. Set up the decoder for PDF417 barcodes.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Pdf417;

        // --------------------------------------------------------------------
        // 6. Read the barcode and output its type, text, and linked‑state flag.
        // --------------------------------------------------------------------
        try
        {
            using (BarCodeReader reader = new BarCodeReader(tempFilePath, decodeType))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"IsLinked: {result.Extended.Pdf417.IsLinked}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading barcode: {ex.Message}");
        }
        finally
        {
            // --------------------------------------------------------------------
            // 7. Clean up the temporary file regardless of success or failure.
            // --------------------------------------------------------------------
            try
            {
                File.Delete(tempFilePath);
            }
            catch
            {
                // Suppress any exceptions during cleanup.
            }
        }
    }
}