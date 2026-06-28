using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC QR LIC barcode and preparation of an HTTP response containing the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a memory stream, and creates a mock HTTP response with the image data.
    /// </summary>
    static void Main()
    {
        // Create HIBC QR LIC codetext with minimal secondary data (lot number only).
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123"
            }
        };

        // Use ComplexBarcodeGenerator to render the barcode into a PNG image stored in a memory stream.
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        using (var ms = new MemoryStream())
        {
            // Save the generated barcode image to the memory stream.
            generator.Save(ms, BarCodeImageFormat.Png);
            // Reset stream position to the beginning before reading.
            ms.Position = 0;
            // Extract the image bytes from the stream.
            byte[] imageBytes = ms.ToArray();

            // Create a mock HTTP response containing the PNG image.
            using (var response = new HttpResponseMessage(HttpStatusCode.OK))
            {
                // Set the response content to the image byte array.
                response.Content = new ByteArrayContent(imageBytes);
                // Specify the MIME type for PNG images.
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                // Output a diagnostic message indicating the response size.
                Console.WriteLine($"HTTP response prepared with {imageBytes.Length} bytes of PNG image.");
            }
        }
    }
}