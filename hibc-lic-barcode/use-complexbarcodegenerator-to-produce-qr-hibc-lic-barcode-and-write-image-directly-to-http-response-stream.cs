// Title: Generate QR HIBC LIC Barcode and Output as Base64
// Description: Demonstrates using ComplexBarcodeGenerator to create a QR HIBC LIC barcode and encode the resulting PNG image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the ComplexBarcodeGenerator class together with HIBCLICSecondaryAndAdditionalDataCodetext to produce HIBC‑LIC barcodes, a common requirement in healthcare and pharmaceutical labeling. Developers often need to generate QR‑based HIBC LIC barcodes, customize error correction, and deliver the image directly to web clients via an HTTP response stream.
/// Prompt: Use ComplexBarcodeGenerator to produce a QR HIBC LIC barcode and write the image directly to an HTTP response stream.
/// Tags: barcode, hibc, qr, lic, complexbarcode, image, base64, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR HIBC LIC barcode using Aspose.BarCode
/// and writes the PNG image as a Base64 string (simulating an HTTP response).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, encodes it to Base64,
    /// and writes the result to the console. In a real web application the
    /// MemoryStream would be written directly to the HttpResponse output stream.
    /// </summary>
    static void Main()
    {
        // NOTE: The original request was to write the barcode image directly to an HTTP response stream.
        // The snippet runner executes as a console application, so we cannot provide an actual HttpResponse.
        // Instead, we generate the QR HIBC LIC barcode, encode the image as Base64, and output it to the console.
        // In a real ASP.NET environment you would write the MemoryStream directly to the response output stream.

        // Prepare secondary and additional data for the HIBC LIC barcode
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123",
            Quantity = 10,
            ExpiryDate = DateTime.Today.AddMonths(6),
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            DateOfManufacture = DateTime.Today
        };

        // Create the complex codetext for a QR HIBC LIC barcode
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set high error correction level for QR (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Produce the bitmap image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Store the image in a memory stream as PNG
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Position = 0;

                    // Convert the PNG bytes to a Base64 string (simulating HTTP response output)
                    string base64Image = Convert.ToBase64String(memoryStream.ToArray());
                    Console.WriteLine("Base64 PNG Image:");
                    Console.WriteLine(base64Image);
                }
            }
        }

        // Exit successfully
    }
}