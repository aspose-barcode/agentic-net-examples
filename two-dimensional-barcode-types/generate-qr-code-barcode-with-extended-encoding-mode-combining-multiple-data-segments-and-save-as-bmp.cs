// Title: Generate QR Code with Extended Encoding and Save as BMP
// Description: Demonstrates how to create a QR Code using Aspose.BarCode with extended encoding mode that combines multiple data segments, then saves the image as a BMP file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with advanced encoding options. It showcases the use of BarcodeGenerator, QrExtCodetextBuilder, and related QR parameters such as EncodeMode and ErrorLevel. Developers often need to combine different data types in a single QR symbol and export it to various image formats for integration into applications.
// Prompt: Generate a QR Code barcode with Extended encoding mode combining multiple data segments and save as BMP.
// Tags: qr code, extended encoding, bmp, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code with extended encoding mode,
/// combines several data segments, and saves the result as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated BMP image.
        string outputPath = "qr_extended.bmp";

        // Build the extended QR code text by adding multiple data segments.
        QrExtCodetextBuilder builder = new QrExtCodetextBuilder();
        builder.AddPlainCodetext("Hello");                                 // Plain text segment
        builder.AddECICodetext(ECIEncodings.UTF8, "World");                // UTF-8 encoded segment
        builder.AddPlainCodetext("12345");                                 // Another plain text segment

        // Initialize the QR Code generator with the QR symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Enable extended encoding mode to allow mixed data segments.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;

            // Assign the combined extended codetext to the generator.
            generator.CodeText = builder.GetExtendedCodetext();

            // Optionally set the error correction level (Level M provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}