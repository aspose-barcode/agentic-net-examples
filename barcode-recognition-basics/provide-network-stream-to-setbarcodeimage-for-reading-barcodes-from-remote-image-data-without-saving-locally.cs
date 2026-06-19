using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates downloading a barcode image from a URL and decoding it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Downloads an image (or uses a default) and reads all supported barcodes.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be an image URL.</param>
    static void Main(string[] args)
    {
        // Determine the image URL: use first argument if provided, otherwise fallback to a sample URL.
        string imageUrl = args.Length > 0 ? args[0] : "https://example.com/sample-barcode.png";

        // Create an HttpClient instance for downloading the image stream.
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Download the image as a stream; Result blocks until the download completes.
                using (Stream imageStream = httpClient.GetStreamAsync(imageUrl).Result)
                {
                    // Initialize the barcode reader.
                    using (var reader = new BarCodeReader())
                    {
                        // Configure the reader to detect all supported barcode types.
                        reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                        // Provide the downloaded image stream to the reader.
                        reader.SetBarCodeImage(imageStream);

                        // Iterate through all detected barcodes and output their type and text.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                            Console.WriteLine($"Barcode Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Output any errors that occur during download or barcode processing.
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}