// Title: Generate QR Code with Version 40 and Save as PNG
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, setting it to the maximum QR version (40), and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and QRVersion classes to customize QR Code parameters. Developers often need to generate QR codes of specific versions for size constraints or data capacity, and this snippet illustrates how to configure and export such barcodes.
// Prompt: Generate a QR Code barcode with version forty specified and save as PNG.
// Tags: qr code, barcode generation, png output, aspose.barcode, qrversion, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode with version 40 and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR Code symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to be encoded in the QR Code
            generator.CodeText = "Sample QR Code";

            // Configure the QR Code to use version 40 (the largest possible version)
            generator.Parameters.Barcode.QR.Version = QRVersion.Version40;

            // Save the generated QR Code as a PNG image file
            generator.Save("qr_version40.png");
        }
    }
}