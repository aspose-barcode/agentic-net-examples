using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image with a transparent background and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "transparent_barcode.png";

        // Initialize a BarcodeGenerator for a QR code with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the barcode appearance.

            // Set the background color to transparent so the image can be overlaid on other media.
            generator.Parameters.BackColor = Color.Transparent;

            // Set the foreground (bar) color; default is black, but we set it explicitly for clarity.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG file, which supports transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}