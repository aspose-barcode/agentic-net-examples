// Title: Validate ReadingQuality based on image resolution
// Description: Demonstrates generating a QR barcode at a specific DPI, then reading it to ensure ReadingQuality reaches 100 only when the image meets the minimum resolution.
// Prompt: Validate that ReadingQuality reaches 100 only when the barcode image meets a minimum resolution threshold.
// Tags: qr, barcode, readingquality, resolution, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, checks its image resolution,
/// and validates that the ReadingQuality reported by the reader is 100 only
/// when the image meets a defined minimum DPI threshold.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        const int minResolutionDpi = 300;
        const string barcodePath = "barcode.png";

        // Generate a QR barcode with the specified resolution (DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Parameters.Resolution = minResolutionDpi;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Load the image to inspect its horizontal and vertical DPI values
        float imageHorizontalDpi;
        float imageVerticalDpi;
        using (var image = Image.FromFile(barcodePath))
        {
            imageHorizontalDpi = image.HorizontalResolution;
            imageVerticalDpi = image.VerticalResolution;
        }

        // Read the barcode from the image and evaluate the ReadingQuality metric
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                double readingQuality = result.ReadingQuality;
                Console.WriteLine($"ReadingQuality: {readingQuality}");
                Console.WriteLine($"Image Resolution: {imageHorizontalDpi} DPI (H), {imageVerticalDpi} DPI (V)");

                // Validate that ReadingQuality is 100 only when both DPI dimensions meet the minimum threshold
                if (readingQuality == 100.0)
                {
                    if (imageHorizontalDpi >= minResolutionDpi && imageVerticalDpi >= minResolutionDpi)
                    {
                        Console.WriteLine("Validation passed: ReadingQuality is 100 and image meets the minimum resolution.");
                    }
                    else
                    {
                        Console.WriteLine("Validation failed: ReadingQuality is 100 but image resolution is below the required threshold.");
                    }
                }
                else
                {
                    Console.WriteLine("ReadingQuality is below 100; no resolution validation required.");
                }
            }
        }
    }
}