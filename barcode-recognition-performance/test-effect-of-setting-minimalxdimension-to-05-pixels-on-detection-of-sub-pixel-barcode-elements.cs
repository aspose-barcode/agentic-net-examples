// Title: Minimal X Dimension Effect on Sub‑Pixel Barcode Detection
// Description: Demonstrates how setting MinimalXDimension to 0.5 pixels influences the detection of barcode elements that are smaller than a pixel.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It showcases the use of BarcodeGenerator for creating a Code128 barcode with a tiny XDimension and BarCodeReader with QualitySettings to adjust MinimalXDimension. Developers working with low‑resolution scans or sub‑pixel barcode elements often need to fine‑tune these settings to improve read accuracy.
// Prompt: Test the effect of setting MinimalXDimension to 0.5 pixels on detection of sub‑pixel barcode elements.
// Tags: code128, minimalxdimension, png, barcodegenerator, barcodereader, qualitysettings, imagegeneration, imagerecognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a sub‑pixel XDimension and reads it back using
/// MinimalXDimension set to 0.5 pixels to observe detection behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image, then attempts to read it
    /// with specific quality settings that target sub‑pixel element detection.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated barcode image.
        string barcodePath = Path.Combine(Directory.GetCurrentDirectory(), "subpixel_barcode.png");

        // Remove any existing file to ensure a clean run.
        if (File.Exists(barcodePath))
        {
            File.Delete(barcodePath);
        }

        // ------------------------------------------------------------
        // Generate a Code128 barcode with an XDimension of 0.5 points
        // (approximately 0.5 pixels at 96 DPI) to create sub‑pixel elements.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the minimal module size.
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Use interpolation mode to control the final image dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Define foreground and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a PNG file.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using MinimalXDimension set to 0.5 pixels.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply high‑performance quality settings.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Instruct the reader to use the MinimalXDimension mode.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 0.5f; // 0.5 pixels

            // Speed up processing by using a fast deconvolution mode.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeType}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
            }
        }
    }
}