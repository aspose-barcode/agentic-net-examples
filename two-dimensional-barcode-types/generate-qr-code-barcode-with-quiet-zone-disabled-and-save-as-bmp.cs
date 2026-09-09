// Title: Generate QR Code without Quiet Zone and Save as BMP
// Description: This example creates a QR Code barcode with the quiet zone (padding) disabled and saves the image as a BMP file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation focusing on QR Code symbology, padding configuration, and BMP image output. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for developers needing custom barcode appearance without default quiet zones, such as embedding barcodes in tight layouts or UI elements.
// Prompt: Generate a QR Code barcode with quiet zone disabled and save as BMP.
// Tags: qr code,quiet zone,disable padding,barcode generation,bmp,aspose.barcode,encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode with the quiet zone disabled
/// and saves it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.bmp");

        // Initialize the barcode generator for QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Disable the quiet zone by setting all padding sides to zero points.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a BMP image.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}