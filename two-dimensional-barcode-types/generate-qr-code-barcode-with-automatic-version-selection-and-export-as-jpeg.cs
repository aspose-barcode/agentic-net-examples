// Title: Generate QR Code with Automatic Version Selection and Save as JPEG
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode with automatic version selection and exporting it to a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code symbology. It showcases the use of the BarcodeGenerator class to encode data, rely on the library's automatic version selection, and save the result in a common image format. Developers often need to generate QR codes for URLs, product information, or contact data and export them for web or print usage.
// Prompt: Generate a QR Code barcode with automatic version selection and export as JPEG.
// Tags: qr code, barcode generation, jpeg output, aspose.barcode, encode types, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR Code (automatic version selection is the default behavior)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR Code
            generator.CodeText = "https://example.com";

            // Save the generated QR Code as a JPEG image file
            generator.Save("qr.jpg");
        }
    }
}