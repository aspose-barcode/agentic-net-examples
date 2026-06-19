using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation at different resolutions and validates the reading quality.
/// </summary>
class Program
{
    // Minimum DPI required for a ReadingQuality of 100
    const float MinResolutionDpi = 200f;

    /// <summary>
    /// Entry point of the application.
    /// Generates low‑ and high‑resolution QR codes and validates their reading quality.
    /// </summary>
    static void Main()
    {
        // Paths for temporary barcode images
        string lowResPath = "low_res.png";
        string highResPath = "high_res.png";

        // Generate a low‑resolution barcode (100 DPI)
        GenerateBarcode(EncodeTypes.QR, "LowResTest", 100f, lowResPath);

        // Generate a high‑resolution barcode (300 DPI)
        GenerateBarcode(EncodeTypes.QR, "HighResTest", 300f, highResPath);

        // Validate the reading quality for each generated image
        ValidateReadingQuality(lowResPath, 100f);
        ValidateReadingQuality(highResPath, 300f);
    }

    // Generates a barcode image with the specified resolution and saves it to the given path.
    static void GenerateBarcode(BaseEncodeType type, string codeText, float resolutionDpi, string outputPath)
    {
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Set the image resolution (DPI) for the barcode generator
            generator.Parameters.Resolution = resolutionDpi;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }
    }

    // Reads a barcode image and checks that ReadingQuality == 100 only when resolution meets the threshold.
    static void ValidateReadingQuality(string imagePath, float imageResolutionDpi)
    {
        // Ensure the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a barcode reader for all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // ReadingQuality is a value from 0 to 100 indicating detection confidence
                double readingQuality = result.ReadingQuality;

                // Output basic information about the image and its reading quality
                Console.WriteLine(
                    $"Image: {Path.GetFileName(imagePath)} | Resolution: {imageResolutionDpi} DPI | ReadingQuality: {readingQuality}");

                // Warn if a perfect reading quality is reported despite low resolution
                if (readingQuality == 100 && imageResolutionDpi < MinResolutionDpi)
                {
                    Console.WriteLine(
                        "Warning: ReadingQuality reached 100 despite resolution being below the minimum threshold.");
                }
                // Note if high resolution does not yield perfect reading quality
                else if (readingQuality < 100 && imageResolutionDpi >= MinResolutionDpi)
                {
                    Console.WriteLine("Note: High resolution but ReadingQuality is below 100.");
                }
            }
        }
    }
}