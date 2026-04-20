using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // URL of the barcode image (default sample if not provided)
        string imageUrl = args.Length > 0 ? args[0] : "https://example.com/sample-barcode.png";

        // Expected service type (e.g., QR, Code128, etc.)
        string expectedServiceType = args.Length > 1 ? args[1] : "QR";

        // Validate the URL format
        if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
        {
            Console.WriteLine("Invalid image URL.");
            return;
        }

        // Download the image into a stream and decode the barcode
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                using (Stream imageStream = httpClient.GetStreamAsync(imageUrl).GetAwaiter().GetResult())
                {
                    // Initialize the reader for all supported barcode types
                    using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        // Perform recognition
                        var results = reader.ReadBarCodes();

                        if (results == null || results.Length == 0)
                        {
                            Console.WriteLine("No barcode detected in the image.");
                            return;
                        }

                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded Text : {result.CodeText}");

                            // Verify the service type against the expected value
                            if (string.Equals(result.CodeTypeName, expectedServiceType, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Service type matches the expected value.");
                            }
                            else
                            {
                                Console.WriteLine("Service type does NOT match the expected value.");
                            }

                            Console.WriteLine(); // Blank line between results
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to download image: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during barcode processing: {ex.Message}");
            }
        }
    }
}