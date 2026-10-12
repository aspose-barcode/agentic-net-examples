// Title: Generate QR Code with Custom Colors and Save as PNG
// Description: This example creates a QR Code barcode with a blue foreground and white background, then saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation techniques, focusing on QR Code creation, color customization, and image export. Uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure barcode appearance and save to common image formats. Ideal for developers needing to embed styled QR codes in applications, reports, or web pages.
// Prompt: Generate a QR Code barcode with foreground color blue and background color white, saved as PNG.
// Tags: qr code, barcode generation, color customization, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code with custom colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_blue.png");

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the foreground (barcode) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR code image.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}