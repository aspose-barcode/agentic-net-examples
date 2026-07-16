// Title: Generate QR Code with specific version and save as BMP
// Description: Demonstrates creating a QR Code barcode with version 10 using Aspose.BarCode and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code customization. It showcases the use of BarcodeGenerator, EncodeTypes, and QRVersion classes to control QR Code parameters such as version, and demonstrates saving the generated barcode in BMP format. Developers often need to generate QR codes with specific versions for size constraints or compatibility, and this pattern illustrates the typical workflow for such requirements.
// Prompt: Generate a QR Code barcode with version ten specified and export as BMP.
// Tags: qr code, barcode generation, bmp output, aspose.barcode, qrversion, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with a specified version and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code barcode, sets its version to 10, and saves it as "qr.bmp".
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the QR Code version to 10 (controls the matrix size and data capacity).
            generator.Parameters.Barcode.QR.Version = QRVersion.Version10;

            // Save the generated barcode to a BMP file.
            generator.Save("qr.bmp");
        }
    }
}