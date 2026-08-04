// Title: Generate QR HIBC LIC barcode and stream as PNG
// Description: Demonstrates creating a HIBC LIC QR barcode using Aspose.BarCode's ComplexBarcodeGenerator and writing the PNG image to a stream that can be sent in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use ComplexBarcodeGenerator with HIBCLICSecondaryAndAdditionalDataCodetext to produce QR HIBC LIC barcodes, configure parameters such as error correction level and colors, and output the result as an image format suitable for web delivery. Developers working with healthcare barcodes or needing to embed QR codes in HTTP responses will find this pattern useful.
// Prompt: Use ComplexBarcodeGenerator to produce a QR HIBC LIC barcode and write the image directly to an HTTP response stream.
// Tags: qr, hibc, lic, complexbarcode, aspose.barcode, generation, png, http, streaming

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR HIBC LIC barcode and writing it to a stream for HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures it, and writes the PNG image to a memory stream.
    /// </summary>
    static void Main()
    {
        // Create HIBC LIC QR complex codetext.
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            // Specify QR HIBC LIC symbology.
            BarcodeType = EncodeTypes.HIBCQRLIC,
            // Link character is mandatory for HIBC LIC.
            LinkCharacter = '+',
            // Populate secondary data (example fields).
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SN12345"
            }
        };

        // Generate the barcode and write it to a simulated HTTP response stream.
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set QR error correction level (optional).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set colors (optional).
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Simulated HTTP response stream.
            using (var responseStream = new MemoryStream())
            {
                // Save the barcode image directly to the stream as PNG.
                generator.Save(responseStream, BarCodeImageFormat.Png);

                // In a real HTTP scenario, the stream would be written to the response.
                // Here we just output the size and optionally save to a file for verification.
                Console.WriteLine($"Generated QR HIBC LIC barcode PNG size: {responseStream.Length} bytes");

                // Optional: write to a file to inspect the result.
                File.WriteAllBytes("hibc_qr.png", responseStream.ToArray());
            }
        }
    }
}