// Title: Generate QR Code with Version 10 and Save as BMP
// Description: Demonstrates creating a QR Code barcode with a specific version (10) using Aspose.BarCode and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code customization and image export. It showcases the use of BarcodeGenerator, EncodeTypes, QRVersion, and BarCodeImageFormat classes to configure QR Code parameters such as version and to save the generated barcode in a bitmap format. Developers often need to generate QR Codes with precise version control for data capacity and then output them as BMP for further processing or printing.
// Prompt: Generate a QR Code barcode with version ten specified and export as BMP.
// Tags: qr code, barcode generation, version control, bmp output, aspose.barcode, encode types, qrversion, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code with version 10 and saving it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code barcode, sets its version, and saves it as BMP.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output BMP file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "QRCodeVersion10.bmp");

        // Initialize the barcode generator with QR encoding and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code version to 10 (controls data capacity and size)
            generator.Parameters.Barcode.QR.Version = QRVersion.Version10;

            // Save the generated barcode as a BMP image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the BMP file was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}