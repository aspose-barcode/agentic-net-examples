// Title: Generate QR Code with Fixed Dimensions Using Aspose.BarCode
// Description: Demonstrates how to create a QR Code barcode, disable automatic sizing, and set a fixed module size to produce a PNG image with predetermined dimensions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control barcode appearance. Developers often need to generate barcodes with exact sizes for layout consistency in documents, labels, or UI elements. The snippet shows typical steps: initializing the generator, configuring parameters, and saving the image.
// Prompt: Generate QR Code barcode and disable automatic size to enforce fixed dimensions.
// Tags: qr code, barcode generation, fixed size, autosizemode, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode with a fixed module size,
/// disables automatic sizing, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_fixed.png");

        // Initialize a QR Code generator with the desired encoded text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Turn off automatic sizing so the barcode dimensions are controlled manually.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set a fixed module size (XDimension) – 2 points per QR module.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}