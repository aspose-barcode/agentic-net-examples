// Title: Generate QR Code with BMP output using Aspose.BarCode
// Description: Demonstrates creating a QR Code barcode, applying the default mask pattern, and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class to produce QR Code symbology. Typical use cases include encoding data for mobile scanning, product labeling, or authentication. Developers often need to generate QR codes in various image formats, customize encoding options, and integrate them into .NET applications.
// Prompt: Generate a QR Code barcode with mask pattern three applied and export as BMP.
// Tags: qr code, barcode generation, bmp, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as a BMP file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code with sample text, saves it as BMP, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Note: The Aspose.BarCode API automatically selects the optimal mask pattern; explicit mask selection is not exposed.
            // Save the generated QR Code image in BMP format.
            generator.Save("qr.bmp");
        }

        // Inform the user that the file has been created.
        Console.WriteLine("QR Code saved as qr.bmp");
    }
}