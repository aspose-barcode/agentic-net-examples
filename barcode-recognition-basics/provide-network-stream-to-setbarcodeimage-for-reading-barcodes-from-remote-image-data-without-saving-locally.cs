// Title: Read barcodes directly from a network stream using Aspose.BarCode
// Description: Demonstrates how to download a barcode image via HTTP and decode it without saving to disk.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with a Stream source. It highlights key API classes such as BarCodeReader and DecodeType for reading various symbologies from remote image data, a common requirement for web services and automated scanning pipelines.
// Prompt: Provide a network stream to SetBarCodeImage for reading barcodes from remote image data without saving locally.
// Tags: barcode, recognition, network stream, http, aspose.barcode, decode, all-supported-types

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcodes from a remote image via a network stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Downloads the barcode image from the specified URL (or a default) and decodes all supported symbologies.
    /// </summary>
    /// <param name="args">Optional command‑line argument containing the image URL.</param>
    static void Main(string[] args)
    {
        // Use the first argument as the image URL, or fall back to a default example URL.
        string url = args.Length > 0 ? args[0] : "https://example.com/barcode.png";

        // HttpClient handles the HTTP request to fetch the image data.
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Retrieve the image as a network stream without writing to disk.
                using (Stream networkStream = httpClient.GetStreamAsync(url).Result)
                {
                    // Initialize the barcode reader with the stream and request all supported symbologies.
                    using (BarCodeReader reader = new BarCodeReader(networkStream, DecodeType.AllSupportedTypes))
                    {
                        Console.WriteLine("Reading barcodes from network stream:");
                        // Iterate through all detected barcodes and output their type and value.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during download or decoding.
                Console.WriteLine($"Error reading barcode from network stream: {ex.Message}");
            }
        }
    }
}