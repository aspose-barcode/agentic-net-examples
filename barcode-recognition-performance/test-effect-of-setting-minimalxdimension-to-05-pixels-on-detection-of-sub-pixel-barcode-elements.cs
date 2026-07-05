// Title: Demonstrate MinimalXDimension effect on sub-pixel barcode detection
// Description: Generates a Code128 barcode with a 0.5‑pixel module size and reads it using MinimalXDimension=0.5 to show detection of sub‑pixel elements.
// Prompt: Test the effect of setting MinimalXDimension to 0.5 pixels on detection of sub‑pixel barcode elements.
// Tags: barcode, code128, minimalxdimension, subpixel, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a sub‑pixel Code128 barcode and reads it using MinimalXDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, then reads it with MinimalXDimension set to 0.5 pixels.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string barcodePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode with a very small XDimension (sub‑pixel)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a tiny module size (0.5 points ≈ 0.5 pixels)
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Optionally set image dimensions to keep the barcode visible
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated barcode image to the specified path
            generator.Save(barcodePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{barcodePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using MinimalXDimension set to 0.5 pixels
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Configure recognition to use the minimal XDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 0.5f;

            // Perform recognition and iterate over all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");

                // Output the region (bounding rectangle) of the detected barcode
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
            }
        }
    }
}