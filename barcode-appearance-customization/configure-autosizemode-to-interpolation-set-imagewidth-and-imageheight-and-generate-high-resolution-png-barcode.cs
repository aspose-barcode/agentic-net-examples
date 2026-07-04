// Title: High‑Resolution QR Code Barcode Generation with Interpolation Auto‑Size
// Description: Demonstrates configuring AutoSizeMode to Interpolation, setting image dimensions, and saving a high‑resolution PNG barcode using Aspose.BarCode.
// Prompt: Configure AutoSizeMode to Interpolation, set ImageWidth and ImageHeight, and generate a high‑resolution PNG barcode.
// Tags: qr, barcode, autosizemode, interpolation, highresolution, png, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a high‑resolution QR code barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures the barcode generator and saves a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for a QR code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the output resolution to 300 DPI for high‑quality rendering
            generator.Parameters.Resolution = 300f;

            // Enable Interpolation auto‑size mode to improve scaling quality
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the canvas size in points (1 point = 1/72 inch)
            generator.Parameters.ImageWidth.Point = 600f;   // Width: 600 points
            generator.Parameters.ImageHeight.Point = 600f;  // Height: 600 points

            // Optional: specify foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a high‑resolution PNG file
            generator.Save("high_res_barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated successfully.");
    }
}