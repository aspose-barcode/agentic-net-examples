// Title: Generate QR Code without Quiet Zone and Save as BMP
// Description: Demonstrates how to create a QR Code barcode with the quiet zone disabled and export it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code symbology and image output customization. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode padding parameters to control quiet zone settings, a common requirement when integrating barcodes into tight layout designs or when precise image dimensions are needed. Developers often need to adjust padding to meet specific printing or UI constraints, and this snippet provides a concise reference.
// Prompt: Generate a QR Code barcode with quiet zone disabled and save as BMP.
// Tags: qr code, quiet zone, padding, bmp, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode with the quiet zone disabled
/// and saves the result as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, removes all padding (quiet zone),
    /// and writes the barcode to a BMP file.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR Code.
            generator.CodeText = "https://example.com";

            // Disable the quiet zone by setting padding on all sides to zero.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a BMP image file.
            generator.Save("qr.bmp");
        }
    }
}