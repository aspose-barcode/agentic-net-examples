// Title: Generate QR Code with Green Background and Black Bars
// Description: Demonstrates creating a QR code with a green background and black bars using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance with colors and export formats. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a QR code, a common requirement for branding, marketing, or data sharing scenarios. Developers often need to adjust foreground and background colors to match visual designs while generating various barcode symbologies.
// Prompt: Create a QR code with a green background and black bars, then save as PNG.
// Tags: qr code, barcode generation, png, aspose.barcode, color customization

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code with a green background and black bars,
/// then saves the result as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the QR code, applies color settings,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the file path where the PNG image will be saved.
        string outputPath = "qr_green_background.png";

        // Initialize the barcode generator for a QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the foreground (bars) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color to green.
            generator.Parameters.BackColor = Color.Green;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}