// Title: Generate QR HIBC LIC Barcode and Write to HTTP Response Stream
// Description: Demonstrates how to create a QR HIBC LIC barcode using Aspose.BarCode's ComplexBarcodeGenerator and write the PNG image directly to a response stream.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext and EncodeTypes to produce HIBC‑LIC QR codes. Typical scenarios include healthcare labeling, inventory tracking, and regulatory compliance where QR barcodes encode product and labeling information. Developers often need to configure barcode parameters such as module size and error correction before streaming the image to web clients.
// Prompt: Use ComplexBarcodeGenerator to produce a QR HIBC LIC barcode and write the image directly to an HTTP response stream.
// Tags: qr, hibc, lic, complexbarcode, png, aspose.barcode, image-output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR HIBC LIC barcode and writes the PNG image to a simulated HTTP response stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the barcode data, configures the generator, and streams the image.
    /// </summary>
    static void Main()
    {
        // Prepare HIBC LIC primary data for the QR barcode
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Simulate an HTTP response stream using a memory stream
        using (MemoryStream responseStream = new MemoryStream())
        {
            // Initialize the complex barcode generator with the prepared data
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Set the module (pixel) size for the barcode
                generator.Parameters.Barcode.XDimension.Pixels = 10;

                // Use a high error correction level to improve scan reliability
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated barcode image directly to the response stream in PNG format
                generator.Save(responseStream, BarCodeImageFormat.Png);
            }

            // Output the size of the generated image (useful for debugging or logging)
            Console.WriteLine($"Generated QR HIBC LIC barcode image size: {responseStream.Length} bytes");
        }
    }
}