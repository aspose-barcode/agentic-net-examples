// Title: Decode OneCode barcode from a network stream and log errors
// Description: Downloads an image containing a USPS OneCode barcode, decodes it using Aspose.BarCode, and writes success or error messages to the console.
// Category-Description: This example demonstrates barcode recognition with Aspose.BarCode, focusing on the BarCodeReader class and DecodeType enumeration. It shows how to retrieve an image via HttpClient, feed the response stream to the reader, and handle typical outcomes such as successful decoding, missing code text, or exceptions. Developers working with barcode scanning in web or service scenarios can use this pattern to integrate barcode decoding into automated workflows.
// Prompt: Decode a OneCode barcode from a network response stream and log any decoding errors.
// Tags: onecode, barcode, decode, network, aspose.barcode, console

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to download an image containing a USPS OneCode barcode,
/// decode it using Aspose.BarCode, and log any decoding errors to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously downloads the image, decodes OneCode barcodes, and writes results.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the image URL.</param>
    static async Task Main(string[] args)
    {
        // Determine the image URL: use the first argument if supplied, otherwise a default placeholder.
        string imageUrl = args.Length > 0 ? args[0] : "https://example.com/sample_onecode.png";

        Console.WriteLine($"Downloading image from: {imageUrl}");

        try
        {
            // Create an HttpClient instance for the download; wrap in using to ensure disposal.
            using (HttpClient httpClient = new HttpClient())
            {
                // Asynchronously obtain the image stream from the URL.
                using (Stream imageStream = await httpClient.GetStreamAsync(imageUrl))
                {
                    // Specify the decode type for USPS OneCode barcodes.
                    BaseDecodeType decodeType = DecodeType.OneCode;

                    // Initialise the BarCodeReader with the image stream and the desired decode type.
                    using (BarCodeReader reader = new BarCodeReader(imageStream, decodeType))
                    {
                        // Perform barcode recognition; returns an array of results.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            // No barcodes were found in the image.
                            Console.WriteLine("No OneCode barcode detected in the image.");
                        }
                        else
                        {
                            // Iterate through each detected barcode.
                            foreach (BarCodeResult result in results)
                            {
                                // If the decoded text is missing, treat it as a decoding error.
                                if (string.IsNullOrEmpty(result.CodeText))
                                {
                                    Console.WriteLine($"[Error] Barcode detected but code text is missing. Type: {result.CodeTypeName}");
                                }
                                else
                                {
                                    // Successful decode – output type and decoded text.
                                    Console.WriteLine($"[Success] Detected OneCode barcode. Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during download or decoding.
            Console.WriteLine($"[Exception] {ex.GetType().Name}: {ex.Message}");
        }
    }
}