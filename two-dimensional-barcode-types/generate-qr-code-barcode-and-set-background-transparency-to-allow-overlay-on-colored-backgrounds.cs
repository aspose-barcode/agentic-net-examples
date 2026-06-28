using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Generates a QR code image with transparent background and saves it to the current directory.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_transparent.png");

        // Initialize the QR code generator with the desired text (URL in this case).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the color of the QR code modules (foreground) to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Configure the background color to be fully transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated QR code as a PNG file, which supports transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}