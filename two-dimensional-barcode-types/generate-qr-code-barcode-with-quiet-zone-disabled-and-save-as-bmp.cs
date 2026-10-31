// Title: Generate QR Code without Quiet Zone and Save as BMP
// Description: This example creates a QR Code barcode with the quiet zone (padding) disabled and saves the image as a BMP file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.QR. It shows how to customize barcode parameters—specifically disabling the quiet zone by setting padding to zero—before saving the result in BMP format. Useful for developers needing compact QR codes for embedding in UI elements, reports, or printed media where space is limited.
// Prompt: Generate a QR Code barcode with quiet zone disabled and save as BMP.
// Tags: qr code, barcode generation, quiet zone, bmp, aspose.barcode, c#

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
        // Define the full path for the output BMP file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_noquiet.bmp");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Disable the quiet zone by setting all padding sides to zero points.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}