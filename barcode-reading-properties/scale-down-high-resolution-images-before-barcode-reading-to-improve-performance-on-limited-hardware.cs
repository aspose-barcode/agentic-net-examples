using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

/// <summary>
/// Demonstrates loading a high‑resolution image, scaling it down,
/// and reading any barcodes present using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scales an input image to a maximum dimension and extracts barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the high‑resolution image (replace with an actual file if available)
        string inputPath = "highres.png";

        // Verify that the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the original image from file
        using (var original = new Bitmap(inputPath))
        {
            // Determine scaling factor to fit within maxDimension while preserving aspect ratio
            float maxDimension = 1000f;
            float scale = Math.Min(maxDimension / original.Width, maxDimension / original.Height);
            if (scale > 1f) scale = 1f; // Prevent up‑scaling if image is already smaller

            // Calculate new dimensions based on scaling factor
            int newWidth = (int)Math.Round(original.Width * scale);
            int newHeight = (int)Math.Round(original.Height * scale);

            // Create a new bitmap with the calculated dimensions
            using (var scaled = new Bitmap(newWidth, newHeight))
            {
                // Draw the original image onto the scaled bitmap using high‑quality interpolation
                using (var graphics = Graphics.FromImage(scaled))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(original, 0, 0, newWidth, newHeight);
                }

                // Encode the scaled bitmap to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    scaled.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Initialize barcode reader to process all supported barcode types
                    using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                    {
                        // Iterate through all detected barcodes and output their details
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Code Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}