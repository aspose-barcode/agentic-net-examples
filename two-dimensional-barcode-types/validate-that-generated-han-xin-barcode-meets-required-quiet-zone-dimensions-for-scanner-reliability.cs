// Title: Validate Han Xin barcode quiet zone dimensions
// Description: Demonstrates generating a Han Xin barcode with explicit quiet zone padding and verifies that the resulting image meets the required quiet zone size for reliable scanning.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to configure barcode parameters such as XDimension and Padding, save the image, and use BarCodeReader to validate decodability. Developers working with 2D symbologies often need to ensure proper quiet zones to meet scanner specifications, and this snippet illustrates the typical workflow using BarcodeGenerator, BarCodeImageFormat, and DecodeType classes.
// Prompt: Validate that generated Han Xin barcode meets required quiet zone dimensions for scanner reliability.
// Tags: hanxin, quietzone, padding, barcode generation, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Han Xin barcode with required quiet zone padding,
/// validates image dimensions, and confirms decodability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, checks padding, reads the code, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "HanXinQuietZone_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "hanxin.png");

        // Required quiet zone (padding) in points (1 point ≈ 1/72 inch)
        const float requiredPadding = 10f; // 10 points ≈ 0.1389 inch

        // Generate Han Xin barcode with explicit padding on all sides
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "1234567890"))
        {
            // Set module (X) size in points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Apply the same quiet zone padding to each side
            generator.Parameters.Barcode.Padding.Left.Point = requiredPadding;
            generator.Parameters.Barcode.Padding.Top.Point = requiredPadding;
            generator.Parameters.Barcode.Padding.Right.Point = requiredPadding;
            generator.Parameters.Barcode.Padding.Bottom.Point = requiredPadding;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Load the generated image to inspect its pixel dimensions and resolution
        using (var bitmap = (Bitmap)Image.FromFile(barcodePath))
        {
            int imgWidth = bitmap.Width;
            int imgHeight = bitmap.Height;

            // Retrieve image resolution (dots per inch) for point‑to‑pixel conversion
            float dpiX, dpiY;
            using (var graphics = Graphics.FromImage(bitmap))
            {
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
            }

            // Convert required padding from points to pixels using the image DPI
            float pointsToPixelsX = dpiX / 72f;
            float pointsToPixelsY = dpiY / 72f;

            int leftPadPx = (int)Math.Round(requiredPadding * pointsToPixelsX);
            int rightPadPx = (int)Math.Round(requiredPadding * pointsToPixelsX);
            int topPadPx = (int)Math.Round(requiredPadding * pointsToPixelsY);
            int bottomPadPx = (int)Math.Round(requiredPadding * pointsToPixelsY);

            // Validate that the image dimensions are at least the sum of the padding values
            bool widthOk = imgWidth >= leftPadPx + rightPadPx;
            bool heightOk = imgHeight >= topPadPx + bottomPadPx;

            Console.WriteLine($"Image size: {imgWidth}x{imgHeight} pixels");
            Console.WriteLine($"Expected minimum width (padding only): {leftPadPx + rightPadPx} pixels");
            Console.WriteLine($"Expected minimum height (padding only): {topPadPx + bottomPadPx} pixels");
            Console.WriteLine($"Width validation: {(widthOk ? "PASS" : "FAIL")}");
            Console.WriteLine($"Height validation: {(heightOk ? "PASS" : "FAIL")}");

            // Additional verification: attempt to decode the barcode to ensure it is readable
            using (var reader = new BarCodeReader(barcodePath, DecodeType.HanXin))
            {
                var results = reader.ReadBarCodes();
                if (results.Length > 0)
                {
                    Console.WriteLine($"Decoded CodeText: {results[0].CodeText}");
                }
                else
                {
                    Console.WriteLine("Barcode could not be decoded.");
                }
            }
        }

        // Clean up temporary files (optional). Failures are ignored to avoid affecting validation result.
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect validation result
        }
    }
}