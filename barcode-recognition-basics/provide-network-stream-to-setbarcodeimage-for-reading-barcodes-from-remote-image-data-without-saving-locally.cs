// Title: Read Barcode from Remote Image via Network Stream
// Description: Demonstrates using Aspose.BarCode to read barcodes directly from a network stream without saving the image locally.
// Prompt: Provide a network stream to SetBarCodeImage for reading barcodes from remote image data without saving locally.
// Tags: barcode, read, network stream, aspose, c#

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that downloads a barcode image from a remote URL and reads barcodes using a network stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Downloads an image, sets it as the source for BarCodeReader, and outputs detected barcodes.
    /// </summary>
    static void Main()
    {
        // Remote image URL containing a barcode
        const string imageUrl = "https://example.com/barcode.png";

        // Create an HttpClient to download the image as a stream without writing to disk
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Retrieve the image data as a stream (synchronous wait for simplicity)
                using (Stream imageStream = httpClient.GetStreamAsync(imageUrl).Result)
                {
                    // Initialize the barcode reader
                    using (var reader = new BarCodeReader())
                    {
                        // Configure the reader to detect all supported barcode types
                        reader.SetBarCodeReadType(DecodeType.AllSupportedTypes);

                        // Assign the network stream as the image source for barcode detection
                        reader.SetBarCodeImage(imageStream);

                        // Process up to 5 detected barcodes and output their type and text
                        int processed = 0;
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            processed++;
                            if (processed >= 5) break;
                        }

                        // Inform the user if no barcodes were found
                        if (processed == 0)
                        {
                            Console.WriteLine("No barcodes were detected in the image.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during download or processing
                Console.WriteLine($"Error retrieving or processing the image: {ex.Message}");
            }
        }
    }
}