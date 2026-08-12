// Title: Generate QR Code with Low Error Correction and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with low error correction level and exporting it as a JPEG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR Code parameters such as error correction level and output format. It uses the BarcodeGenerator class together with EncodeTypes.QR and QRErrorLevel to produce barcodes, a common task for developers needing to embed scannable data in images for web or print applications.
// Prompt: Generate a QR Code barcode with error correction level low and export as JPEG.
// Tags: qr code, error correction, jpeg, aspose.barcode, barcode generation, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code with low error correction and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path (saved in the current working directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_low.jpg");

        // Initialize the barcode generator for QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure the QR Code to use low error correction level (LevelL)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;

            // Save the generated barcode as a JPEG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}