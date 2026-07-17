// Title: Generate QR Code with Quiet Zone and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with a four‑module quiet zone and exporting it to a JPEG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes to produce QR Code symbology. Typical use cases include encoding URLs, contact information, or product data for mobile scanning. Developers often need to control visual parameters such as quiet zones and output formats (e.g., JPEG, PNG) when integrating barcodes into web or print media.
// Prompt: Generate a QR Code barcode with quiet zone of four modules and export as JPEG.
// Tags: qr code, barcode generation, quiet zone, jpeg, aspose.barcode

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code barcode with the default quiet zone
/// (four modules) and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // The default quiet zone for QR codes in Aspose.BarCode is four modules,
            // which satisfies the requirement, so no additional configuration is needed.

            // Save the generated barcode as a JPEG image file.
            generator.Save("qr.jpeg");
        }
    }
}