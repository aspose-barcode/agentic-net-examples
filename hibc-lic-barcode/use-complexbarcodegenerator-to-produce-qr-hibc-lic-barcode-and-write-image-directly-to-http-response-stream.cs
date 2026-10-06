// Title: Generate QR HIBC LIC Barcode and Write to HTTP Response Stream
// Description: Demonstrates how to use Aspose.BarCode's ComplexBarcodeGenerator to create a QR HIBC LIC barcode and output the PNG image directly to a response stream.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext to produce QR HIBC LIC barcodes, a common requirement in healthcare and logistics for encoding product and labeling information. Developers working with advanced barcode symbologies, custom data structures, or needing to stream barcode images to web clients will find this pattern useful.
// Prompt: Use ComplexBarcodeGenerator to produce a QR HIBC LIC barcode and write the image directly to an HTTP response stream.
// Tags: barcode, qr, hibc, lic, complexbarcode, image-output, png, aspnet, aspnet-core, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Provides an example that generates a QR HIBC LIC barcode using Aspose.BarCode
/// and writes the resulting PNG image to a stream that simulates an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the barcode, configures its appearance, saves it to a stream,
    /// and displays basic information about the generated image.
    /// </summary>
    static void Main()
    {
        // Simulate an HTTP response stream using a memory buffer.
        using (var responseStream = new MemoryStream())
        {
            // Create HIBC LIC primary data codetext for a QR barcode.
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

            // Generate the barcode and write the PNG image directly to the response stream.
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Configure visual parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image to the stream in PNG format.
                generator.Save(responseStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning.
            responseStream.Position = 0;

            // Output information about the generated image for verification.
            Console.WriteLine($"Generated QR HIBC LIC barcode image size: {responseStream.Length} bytes");
            string base64 = Convert.ToBase64String(responseStream.ToArray());
            Console.WriteLine($"Base64 PNG: {base64}");
        }
    }
}