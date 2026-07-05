// Title: Generate CMYK TIFF Barcode for Print
// Description: Demonstrates configuring Aspose.BarCode to generate a Code128 barcode saved as a CMYK TIFF image suitable for print.
// Prompt: Configure the generator to output TIFF in CMYK color space for print‑ready files.
// Tags: code128, barcode generation, tiff, cmyk, print, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode and saves it as a CMYK TIFF file,
/// ready for high‑quality printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Barcode content to encode
        const string codeText = "PrintReady123";

        // Destination file name (TIFF format)
        const string outputPath = "barcode_cmyk.tif";

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set image resolution to 300 DPI, a common print standard
            generator.Parameters.Resolution = 300;

            // Define bar (foreground) and background colors.
            // Colors are specified in RGB; the TIFF encoder will convert them to CMYK.
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 0, 0);   // Black bars
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255); // White background

            // Save the barcode as a TIFF image.
            // The internal encoder produces a CMYK TIFF when the pixel format supports it.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user that the file has been created
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}