// Title: Generate QR Code with specific version and save as BMP
// Description: Demonstrates creating a QR Code barcode with version 10 using Aspose.BarCode and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure QR Code parameters such as version, and how to save the generated barcode in bitmap format. It uses the BarcodeGenerator class together with EncodeTypes and QRVersion enums, which are commonly employed by developers to produce custom QR Code images for printing, embedding, or further processing.
// Prompt: Generate a QR Code barcode with version ten specified and export as BMP.
// Tags: qr code, barcode generation, bmp output, aspose.barcode, qrversion, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code with a specific version and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated BMP image.
        string outputPath = "qr_version10.bmp";

        // Initialize the barcode generator for QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code version to 10 (controls the size and data capacity).
            generator.Parameters.Barcode.QR.Version = QRVersion.Version10;

            // Save the generated barcode image to the specified BMP file.
            generator.Save(outputPath);
        }

        // Inform the user where the BMP file has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}