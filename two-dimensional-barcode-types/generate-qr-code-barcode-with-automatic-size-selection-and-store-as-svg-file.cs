// Title: Generate QR Code barcode and save as SVG with automatic size selection
// Description: Demonstrates creating a QR Code using Aspose.BarCode, letting the library choose the optimal QR version automatically, and saving the result as an SVG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the BarcodeGenerator class, EncodeTypes enumeration, QRVersion auto‑selection, and saving to vector formats like SVG. Developers use these APIs to embed scannable QR codes in web pages, reports, or documents where scalable graphics are required.
// Prompt: Generate a QR Code barcode with automatic size selection and store as SVG file.
// Tags: qr code, barcode generation, automatic size, svg output, aspose.barcode, encode types, qrversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with automatic version selection and saving it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output SVG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr.svg");

        // Initialize the barcode generator for a QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR version to Auto so the library selects the optimal size.
            generator.Parameters.Barcode.QR.Version = QRVersion.Auto;

            try
            {
                // Save the generated QR Code as an SVG image.
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"QR code saved to {outputPath}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during the save operation.
                Console.WriteLine($"Failed to save QR code as SVG: {ex.Message}");
            }
        }
    }
}