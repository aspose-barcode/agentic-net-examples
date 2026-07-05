// Title: Generate Code128 Barcode with Anti-Aliasing and Export as PNG
// Description: Demonstrates creating a Code128 barcode, enabling anti‑aliasing, setting image size and resolution, and saving it as a PNG for clear screen display.
// Prompt: Create a barcode, apply anti‑aliasing settings, and export as PNG for crisp screen display.
// Tags: code128, barcode generation, anti-aliasing, png, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, applies anti‑aliasing,
/// configures image dimensions and resolution, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable anti‑aliasing to produce smoother edges when the image is displayed on screen.
            generator.Parameters.UseAntiAlias = true;

            // Use interpolation mode so the image size can be set directly without distortion.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the desired image width and height in points for a crisp visual appearance.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Optionally increase the resolution (dots per inch) to improve overall quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been successfully created.
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}