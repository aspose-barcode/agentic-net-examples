// Title: Generate QR Code with Manual Module Size and Save as PNG
// Description: Demonstrates how to create a QR Code barcode with a custom module size of 4 pixels using Aspose.BarCode and export it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure barcode parameters such as XDimension for QR Code symbology. It uses the BarcodeGenerator and related parameter classes to produce high‑quality images, a common requirement for developers integrating QR codes into web, mobile, or desktop applications. Typical use cases include generating printable QR codes, embedding them in documents, or serving them via APIs.
// Prompt: Generate a QR Code barcode with manual module size of 4 pixels and export as PNG.
// Tags: qr code, barcode generation, manual module size, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code with a manually set module size and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr.png");

        // Initialize a BarcodeGenerator for QR Code symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure the module size (XDimension) to 4 pixels.
            generator.Parameters.Barcode.XDimension.Point = 4f;

            // Render and save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}