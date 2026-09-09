// Title: Generate QR Code PNG with 300 DPI using Aspose.BarCode
// Description: This example creates a QR code containing the text "Hello World", sets the image resolution to 300 DPI, and saves it as a PNG file.
// Category-Description: Demonstrates the use of Aspose.BarCode's generation API to produce high‑resolution QR codes. The example utilizes the BarcodeGenerator class with EncodeTypes.QR, configures image parameters such as resolution, and saves the result in PNG format. Developers commonly employ these APIs for creating scannable graphics for marketing, authentication, or data sharing scenarios where image quality and format are important.
// Prompt: Generate a QR code and save it as a PNG file with 300 DPI resolution.
// Tags: qr code, png, resolution, aspose.barcode, generation, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates a QR code image with a specified DPI resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, sets its resolution to 300 DPI, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Create a BarcodeGenerator for QR encoding with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the image resolution to 300 DPI.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR code image.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}