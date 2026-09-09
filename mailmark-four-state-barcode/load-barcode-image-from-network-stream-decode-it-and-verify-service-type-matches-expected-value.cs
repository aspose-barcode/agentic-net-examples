// Title: Decode QR Code from Network Stream and Verify Symbology
// Description: Loads a barcode image from a URL, decodes it using Aspose.BarCode, and checks that the detected symbology matches the expected value.
// Category-Description: This example demonstrates how to retrieve a barcode image over HTTP, use Aspose.BarCode's BarCodeReader to recognize all supported symbologies, and validate the result. It showcases key API classes such as BarCodeReader, BarCodeResult, DecodeType, and BaseDecodeType—common in scenarios like inventory scanning, ticket validation, or any application that needs to process barcodes received from remote sources. Developers often need to combine network I/O with barcode recognition to automate verification workflows.
/// Prompt: Load a barcode image from a network stream, decode it, and verify service type matches expected value.
/// Tags: barcode, decoding, network, qrcode, aspose.barcode, csharp

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a barcode image from a remote URL, decoding it, and verifying that the detected symbology matches an expected value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Retrieves the image, decodes barcodes, and validates the symbology.
    /// </summary>
    static void Main()
    {
        // URL of the barcode image to process
        string imageUrl = "https://example.com/samplebarcode.png";

        // Expected symbology identifier (e.g., "QR")
        string expectedSymbology = "QR";

        // Resolve the expected symbology to a BaseDecodeType enum value using reflection
        var field = typeof(DecodeType).GetField(expectedSymbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown expected symbology: {expectedSymbology}");
            return;
        }
        BaseDecodeType expectedDecodeType = (BaseDecodeType)field.GetValue(null);

        // Create an HttpClient to download the image stream
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Synchronously fetch the image stream from the URL
                using (Stream stream = httpClient.GetStreamAsync(imageUrl).GetAwaiter().GetResult())
                {
                    // Initialize the barcode reader to recognize all supported types
                    using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
                    {
                        bool any = false;

                        // Iterate through all detected barcodes in the image
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            any = true;
                            Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");

                            // Compare the detected symbology with the expected one
                            if (result.CodeType.Equals(expectedDecodeType))
                            {
                                Console.WriteLine("Service type matches expected value.");
                            }
                            else
                            {
                                Console.WriteLine($"Service type mismatch. Expected: {expectedSymbology}, Got: {result.CodeTypeName}");
                            }
                        }

                        // Inform the user if no barcodes were found
                        if (!any)
                        {
                            Console.WriteLine("No barcode detected in the image.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during download or decoding
                Console.WriteLine($"Error processing barcode: {ex.Message}");
            }
        }
    }
}