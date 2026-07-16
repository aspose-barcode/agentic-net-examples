// Title: Generate QR Code Barcode at 200 DPI and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with a custom resolution of 200 DPI and exporting it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure image resolution and output format using the BarcodeGenerator class. Typical use cases include generating high‑resolution QR codes for print media, marketing materials, or product labeling. Developers often need to adjust DPI settings and choose appropriate image formats such as JPEG for web or print distribution.
// Prompt: Generate a QR Code barcode scaled to two hundred DPI and save as JPEG.
// Tags: qr code, barcode generation, jpeg output, resolution, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR Code barcode, sets its resolution to 200 DPI,
/// and saves the result as a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text/content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the image resolution to 200 DPI for higher quality output.
            generator.Parameters.Resolution = 200f;

            // Persist the generated barcode to a JPEG file.
            generator.Save("qr_code.jpeg");
        }
    }
}