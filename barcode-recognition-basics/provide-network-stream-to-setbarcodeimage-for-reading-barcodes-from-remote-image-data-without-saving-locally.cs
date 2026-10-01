// Title: Read Barcode from Remote Image Using Memory Stream
// Description: Demonstrates downloading a barcode image from a URL into a memory stream and recognizing it with Aspose.BarCode without saving the file locally.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to use BarCodeReader and its SetBarCodeImage method. It illustrates typical scenarios where developers need to process barcode images received over a network (e.g., from web services or APIs) without persisting them to disk. Key classes include BarCodeReader, BarCodeResult, and MemoryStream, which together enable efficient, in‑memory barcode detection.
// Prompt: Provide a network stream to SetBarCodeImage for reading barcodes from remote image data without saving locally.
// Tags: barcode recognition, memory stream, network stream, aspose.barcode, setbarcodeimage, remote image, qr code

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that downloads a barcode image from a remote URL,
/// loads it into a memory stream, and reads the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // URL of the sample image that contains a QR code.
        const string imageUrl = "https://raw.githubusercontent.com/aspose-barcode/Aspose.BarCode-for-.NET/master/Examples/Resources/qr.png";

        // Variable to hold the downloaded image data.
        MemoryStream barcodeStream = null;

        // Download the image using HttpClient and store it in a memory stream.
        using (var httpClient = new HttpClient())
        {
            try
            {
                using (var response = httpClient.GetAsync(imageUrl).Result)
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Failed to download image. Status code: {response.StatusCode}");
                        return;
                    }

                    using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                    {
                        barcodeStream = new MemoryStream();
                        responseStream.CopyTo(barcodeStream);
                        barcodeStream.Position = 0; // Reset stream position for reading.
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while downloading image: {ex.Message}");
                return;
            }
        }

        // Initialize BarCodeReader and provide the memory stream as input.
        using (var reader = new BarCodeReader())
        {
            try
            {
                // Set the image source for the reader.
                reader.SetBarCodeImage(barcodeStream);

                // Iterate through all detected barcodes and output their details.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during barcode recognition: {ex.Message}");
            }
        }

        // Release the memory stream resources.
        barcodeStream?.Dispose();
    }
}