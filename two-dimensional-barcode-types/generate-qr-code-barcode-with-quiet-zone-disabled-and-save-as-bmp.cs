// Title: Generate QR Code without Quiet Zone and Save as BMP
// Description: Demonstrates how to create a QR Code barcode with the quiet zone disabled and save it as a BMP image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include generating QR codes for embedding in documents or UI elements where space is limited, and developers often need to control padding (quiet zone) to fit design constraints.
// Prompt: Generate a QR Code barcode with quiet zone disabled and save as BMP.
// Tags: qr code, barcode generation, quiet zone, bmp, aspose.barcode, encoding, image format

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode with the quiet zone disabled
/// and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, disables padding, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "qr_without_quietzone.bmp";

        // Initialize the barcode generator for QR Code symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = "Hello, QR!";

            // Disable quiet zone by setting all paddings to zero points
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}