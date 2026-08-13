// Title: High‑Resolution Bitmap Source for Accurate Small DataMatrix Detection
// Description: Demonstrates how to upscale a low‑resolution DataMatrix barcode image to improve recognition of tiny symbols.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator to create a DataMatrix, Aspose.Drawing to manipulate bitmap resolution, and BarCodeReader with QualitySettings to enhance detection. Developers working with low‑resolution barcodes or needing higher detection reliability will find this pattern useful for preprocessing images before recognition.
// Prompt: Apply a high‑resolution bitmap source to improve detection accuracy of small‑sized DataMatrix codes.
// Tags: datamatrix, detection, png, barcodegenerator, barcodereader, imaging, qualitysettings, upscaling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a low‑resolution DataMatrix barcode, upscales it, and reads it using high‑performance settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates, upscales, and recognizes a small DataMatrix barcode.
    /// </summary>
    static void Main()
    {
        // Sample data for a small DataMatrix barcode
        const string data = "SmallDM";

        // Generate a low‑resolution DataMatrix barcode and keep it in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
        {
            // Enable automatic sizing using interpolation (no explicit BarHeight needed)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a small XDimension to keep the barcode compact
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Save the generated barcode to a memory stream in PNG format
            using (var originalStream = new MemoryStream())
            {
                generator.Save(originalStream, BarCodeImageFormat.Png);
                originalStream.Position = 0; // Reset stream position for reading

                // Load the generated image into a bitmap for manipulation
                using (var originalBitmap = new Bitmap(originalStream))
                {
                    // Define upscale factor (e.g., 4×) to increase resolution
                    int scale = 4;
                    int highResWidth = originalBitmap.Width * scale;
                    int highResHeight = originalBitmap.Height * scale;

                    // Create a new bitmap with the higher resolution dimensions
                    using (var highResBitmap = new Bitmap(highResWidth, highResHeight))
                    {
                        // Draw the original low‑resolution bitmap onto the larger canvas
                        using (var graphics = Graphics.FromImage(highResBitmap))
                        {
                            graphics.DrawImage(originalBitmap, 0, 0, highResWidth, highResHeight);
                        }

                        // Recognize the DataMatrix from the upscaled bitmap
                        using (var reader = new BarCodeReader(highResBitmap, DecodeType.DataMatrix))
                        {
                            // Configure quality settings to improve detection of small symbols
                            reader.QualitySettings = QualitySettings.HighPerformance;
                            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                            reader.QualitySettings.MinimalXDimension = 2f; // Minimum element size in pixels

                            // Iterate through detected barcodes and output their details
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Detected CodeType: {result.CodeType}");
                                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}