// Title: Generate QR HIBC LIC Barcode and Write to HTTP Response Stream
// Description: Demonstrates how to create a QR HIBC LIC barcode using Aspose.BarCode's ComplexBarcodeGenerator and output the PNG image directly to an HTTP response stream.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC QR LIC. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and related parameter settings to produce a barcode image suitable for web responses. Developers working with healthcare or logistics labeling often need to generate HIBC-compliant barcodes and stream them to clients without intermediate files.
// Prompt: Use ComplexBarcodeGenerator to produce a QR HIBC LIC barcode and write the image directly to an HTTP response stream.
// Tags: barcode, hibc, qr, lic, complexbarcode, generation, png, http, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a QR HIBC LIC barcode and writes the image to a simulated HTTP response stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode data, generates the image, and streams it.
    /// </summary>
    static void Main()
    {
        // Simulate an HTTP response stream using a MemoryStream.
        using (var responseStream = new MemoryStream())
        {
            // Prepare HIBC QR LIC primary data codetext.
            var complexCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCQRLIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = "12345",
                    LabelerIdentificationCode = "A999",
                    UnitOfMeasureID = 1
                }
            };

            // Generate the barcode and write directly to the response stream.
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Set the X-dimension (module size) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 10;

                // Save the barcode image as PNG into the stream.
                generator.Save(responseStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for any further processing (e.g., sending to client).
            responseStream.Position = 0;

            // Output diagnostic information about the generated image.
            Console.WriteLine($"Generated QR HIBC LIC barcode, stream length: {responseStream.Length} bytes");

            // Optional: display a short Base64 preview of the image.
            byte[] imageBytes = responseStream.ToArray();
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine($"Base64 preview (first 100 chars): {base64.Substring(0, Math.Min(100, base64.Length))}");
        }
    }
}