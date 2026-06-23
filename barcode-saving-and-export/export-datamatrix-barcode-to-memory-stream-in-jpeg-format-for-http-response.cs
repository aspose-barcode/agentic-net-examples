using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, converting it to JPEG,
/// and simulating an HTTP response containing the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, encodes it as JPEG, and displays basic information.
    /// </summary>
    static void Main()
    {
        // Define the text to be encoded in the barcode.
        string codeText = "Hello";

        // Initialize a DataMatrix barcode generator with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Create a memory stream to hold the JPEG image data.
            using (var memoryStream = new MemoryStream())
            {
                // Save the generated barcode into the memory stream in JPEG format.
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);

                // Reset the stream position to the beginning for reading.
                memoryStream.Position = 0;

                // Extract the JPEG bytes from the memory stream.
                byte[] jpegBytes = memoryStream.ToArray();

                // Simulate an HTTP response that would return the JPEG image.
                using (var response = new HttpResponseMessage(HttpStatusCode.OK))
                {
                    // Set the response content to the JPEG byte array.
                    response.Content = new ByteArrayContent(jpegBytes);

                    // Specify the content type header as JPEG image.
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                    // Output the size of the generated JPEG image.
                    Console.WriteLine($"Generated JPEG size: {jpegBytes.Length} bytes");

                    // Show a Base64 preview of the first 100 characters of the image data.
                    Console.WriteLine("Base64 preview (first 100 chars):");
                    Console.WriteLine(Convert.ToBase64String(jpegBytes).Substring(0, 100) + "...");
                }
            }
        }
    }
}