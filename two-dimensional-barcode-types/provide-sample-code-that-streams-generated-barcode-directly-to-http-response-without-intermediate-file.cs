using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and returning it as an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, obtains the HTTP response,
    /// and displays basic information about the returned image.
    /// </summary>
    static void Main()
    {
        // Generate a barcode and obtain an HTTP response containing the image.
        HttpResponseMessage response = CreateBarcodeResponse("123ABC");

        // Output the HTTP status code to confirm the response was successful.
        Console.WriteLine($"Response status: {response.StatusCode}");

        // Read the image bytes from the response content synchronously.
        byte[] imageBytes = response.Content.ReadAsByteArrayAsync().Result;

        // Display the size of the barcode image in bytes.
        Console.WriteLine($"Barcode image size: {imageBytes.Length} bytes");

        // Show a short Base64 preview of the image for verification.
        Console.WriteLine($"Base64 preview: {Convert.ToBase64String(imageBytes).Substring(0, 30)}...");
    }

    /// <summary>
    /// Generates a Code128 barcode, writes it to a memory stream,
    /// and wraps the stream content in an <see cref="HttpResponseMessage"/>.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>An HTTP response containing the barcode image as PNG.</returns>
    static HttpResponseMessage CreateBarcodeResponse(string codeText)
    {
        // Initialize the barcode generator with the specified encoding type and text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Create a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode directly to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for reading.
                ms.Position = 0;

                // Create HTTP content from the stream's byte array.
                var content = new ByteArrayContent(ms.ToArray());

                // Set the appropriate MIME type for a PNG image.
                content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                // Construct the HTTP response with a 200 OK status and the image content.
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = content
                };

                // Return the prepared response to the caller.
                return response;
            }
        }
    }
}