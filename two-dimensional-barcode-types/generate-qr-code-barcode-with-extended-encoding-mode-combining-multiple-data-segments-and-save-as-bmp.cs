// Title: Generate QR Code with Extended Encoding and Save as BMP
// Description: Demonstrates how to create a QR Code using the Extended encoding mode, combining plain text and ECI segments, and save the result as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with advanced encoding options. It showcases the use of QrExtCodetextBuilder to build multi‑segment code text, the EncodeTypes.QR symbology, and the QREncodeMode.Extended setting. Developers often need to generate QR Codes that mix different character sets or data types, and this snippet illustrates the typical workflow for such scenarios.
// Prompt: Generate a QR Code barcode with Extended encoding mode combining multiple data segments and save as BMP.
// Tags: qr code, extended encoding, barcode generation, bmp output, aspose.barcode, qrextcodetextbuilder

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code with Extended encoding mode and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds a multi‑segment QR Code text, configures the generator,
    /// and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_extended.bmp");

        // Build extended QR Code text with plain and ECI (UTF‑8) segments
        QrExtCodetextBuilder builder = new QrExtCodetextBuilder();
        builder.AddPlainCodetext("Hello");
        builder.AddECICodetext(ECIEncodings.UTF8, "World");
        builder.AddPlainCodetext("2026");
        string extendedText = builder.ToString();

        // Create a QR Code generator with the extended text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, extendedText))
        {
            // Set the QR Code to use Extended encoding mode
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;

            // Save the generated QR Code as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}