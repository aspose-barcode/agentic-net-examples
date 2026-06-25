using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates downloading an image and recognizing OneCode barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Downloads an image (from a provided URL or a default sample) and attempts to read OneCode barcodes.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can be a URL to an image.</param>
    static async Task Main(string[] args)
    {
        // Determine the image URL: use the first command‑line argument if supplied,
        // otherwise fall back to a predefined sample URL.
        string url = args.Length > 0 ? args[0] : "https://example.com/onecode.png";

        // HttpClient implements IDisposable; wrap it in a using block to ensure proper disposal.
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Send an asynchronous GET request to download the image.
                using (var response = await httpClient.GetAsync(url))
                {
                    // Verify that the HTTP request succeeded.
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Failed to download image. HTTP status: {response.StatusCode}");
                        return;
                    }

                    // Read the response content as a stream for barcode processing.
                    using (var imageStream = await response.Content.ReadAsStreamAsync())
                    {
                        // Initialize the BarCodeReader for the OneCode symbology using the image stream.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.OneCode))
                        {
                            // Perform the barcode recognition operation.
                            var results = reader.ReadBarCodes();

                            // If no barcodes were found, inform the user and exit.
                            if (results == null || results.Length == 0)
                            {
                                Console.WriteLine("No barcodes detected.");
                                return;
                            }

                            // Iterate through each detected barcode result.
                            foreach (var result in results)
                            {
                                // If the decoded text is missing, log a decoding error.
                                if (string.IsNullOrEmpty(result.CodeText))
                                {
                                    Console.WriteLine($"Decoding error for barcode type {result.CodeTypeName}: CodeText is empty.");
                                }
                                else
                                {
                                    // Output the successfully decoded barcode information.
                                    Console.WriteLine($"Detected OneCode barcode. Type: {result.CodeTypeName}, Text: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
            // Handle network‑related exceptions such as DNS failures or connectivity issues.
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error while downloading image: {ex.Message}");
            }
            // Catch any other unexpected exceptions to prevent the application from crashing.
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}