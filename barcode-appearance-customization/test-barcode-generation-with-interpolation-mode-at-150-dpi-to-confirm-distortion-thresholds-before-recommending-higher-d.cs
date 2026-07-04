// Title: Barcode generation with interpolation at different DPI
// Description: Demonstrates generating a Code128 barcode using interpolation auto-size mode at 150 dpi and 300 dpi to evaluate image distortion.
// Prompt: Test barcode generation with Interpolation mode at 150 dpi to confirm distortion thresholds before recommending higher DPI.
// Tags: code128, barcode, interpolation, dpi, image generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates Code128 barcodes at two different DPI settings
/// using the Interpolation auto‑size mode to compare visual quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images and prompts the user to compare them.
    /// </summary>
    static void Main()
    {
        // Barcode content and output file names
        const string codeText = "1234567890";
        const string output150 = "barcode_150dpi.png";
        const string output300 = "barcode_300dpi.png";

        // ------------------------------------------------------------
        // Generate barcode with Interpolation mode at 150 DPI
        // ------------------------------------------------------------
        using (var generator150 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable interpolation auto‑size mode
            generator150.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set image resolution to 150 DPI
            generator150.Parameters.Resolution = 150f;

            // Define target image dimensions in points (1 point = 1/72 inch)
            generator150.Parameters.ImageWidth.Point = 300f;
            generator150.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image
            generator150.Save(output150);
            Console.WriteLine($"Generated barcode at 150 dpi: {output150}");
        }

        // ------------------------------------------------------------
        // Generate the same barcode at a higher DPI (300) for comparison
        // ------------------------------------------------------------
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Use the same interpolation mode
            generator300.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Increase resolution to 300 DPI
            generator300.Parameters.Resolution = 300f;

            // Keep image dimensions identical to the 150 dpi version
            generator300.Parameters.ImageWidth.Point = 300f;
            generator300.Parameters.ImageHeight.Point = 150f;

            // Save the higher‑resolution barcode image
            generator300.Save(output300);
            Console.WriteLine($"Generated barcode at 300 dpi: {output300}");
        }

        // Simple visual check hint
        Console.WriteLine("Compare the two images to assess distortion at 150 dpi.");
    }
}