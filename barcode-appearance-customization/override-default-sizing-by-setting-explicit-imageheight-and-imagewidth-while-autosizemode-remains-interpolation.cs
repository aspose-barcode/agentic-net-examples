// Title: Explicit Image Size with Interpolation AutoSizeMode
// Description: Demonstrates overriding the default barcode image dimensions by setting ImageWidth and ImageHeight while keeping AutoSizeMode set to Interpolation.
// Prompt: Override default sizing by setting explicit ImageHeight and ImageWidth while AutoSizeMode remains Interpolation.
// Tags: barcode, code128, explicit sizing, interpolation, aspose.barcode, image generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with custom image dimensions while using the Interpolation auto‑size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, sets explicit size parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Keep automatic sizing mode as Interpolation (default behavior)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Override default size by setting explicit image dimensions (points)
            generator.Parameters.ImageWidth.Point = 300f;   // Width = 300 points
            generator.Parameters.ImageHeight.Point = 150f;  // Height = 150 points

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}