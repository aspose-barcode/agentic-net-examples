// Title: Read Barcodes from a Remote Image via Network Stream
// Description: Demonstrates how to download a barcode image from a URL and decode it directly from a network stream without saving the file locally.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the BarCodeReader class with multiple DecodeType options. It illustrates typical scenarios where developers need to process barcode images received over HTTP, such as scanning documents in web services or mobile apps, without persisting the image to disk. The code highlights how to combine HttpClient, streams, and Aspose.BarCode to achieve fast, memory‑efficient barcode recognition.
// Prompt: Provide a network stream to SetBarCodeImage for reading barcodes from remote image data without saving locally.
// Tags: barcode, reading, network, stream, aspose.barcode, barcodereader, decode, pdf417, datamatrix, qr, code128

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcodes from an image retrieved over HTTP using a network stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Downloads an image from the specified URL (or a default one) and decodes supported barcode types directly from the response stream.
    /// </summary>
    /// <param name="args">Optional command‑line argument containing the image URL.</param>
    static void Main(string[] args)
    {
        // Determine the image URL: use the first argument if provided, otherwise fall back to a sample URL.
        string imageUrl = args.Length > 0 ? args[0] : "https://example.com/barcode.png";

        // Create an HttpClient instance for sending the request.
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Synchronously send a GET request to the image URL.
                using (HttpResponseMessage response = httpClient.GetAsync(imageUrl).Result)
                {
                    // Throw if the HTTP status is not successful.
                    response.EnsureSuccessStatusCode();

                    // Obtain the response content as a stream without writing to disk.
                    using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                    {
                        // Initialize BarCodeReader with the stream and the desired barcode symbologies.
                        using (BarCodeReader reader = new BarCodeReader(
                            stream,
                            DecodeType.Pdf417,
                            DecodeType.DataMatrix,
                            DecodeType.QR,
                            DecodeType.Code128))
                        {
                            Console.WriteLine("Reading barcodes from network stream:");

                            // Iterate through all detected barcodes and output their type and value.
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Output any errors that occur during download or decoding.
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}