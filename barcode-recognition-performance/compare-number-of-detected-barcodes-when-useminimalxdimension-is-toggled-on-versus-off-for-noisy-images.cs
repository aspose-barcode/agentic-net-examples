using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, adding noise, and detecting it with different quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, adds random noise, and counts detected barcodes using default
    /// and minimal X-dimension settings.
    /// </summary>
    static void Main()
    {
        // Generate a clean barcode image (Code128) and store it in a memory stream.
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Save the generated barcode as PNG into the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning before reading.
            barcodeStream.Position = 0;

            // Load the PNG into a bitmap so we can manipulate pixel data.
            using (var bitmap = new Bitmap(barcodeStream))
            {
                // Add 5% random noise to the bitmap.
                AddNoise(bitmap, 0.05f);

                // Save the noisy bitmap back to a new memory stream for later processing.
                using (var noisyStream = new MemoryStream())
                {
                    bitmap.Save(noisyStream, ImageFormat.Png);
                    byte[] noisyImageBytes = noisyStream.ToArray();

                    // Count barcodes using default quality settings.
                    int defaultCount = CountBarcodes(noisyImageBytes, useMinimal: false);

                    // Count barcodes with UseMinimalXDimension enabled.
                    int minimalCount = CountBarcodes(noisyImageBytes, useMinimal: true);

                    // Output the detection results.
                    Console.WriteLine($"Detected barcodes (default settings): {defaultCount}");
                    Console.WriteLine($"Detected barcodes (UseMinimalXDimension): {minimalCount}");
                }
            }
        }
    }

    /// <summary>
    /// Adds random noise to a bitmap. The <paramref name="noiseRatio"/> specifies the fraction of pixels to alter.
    /// </summary>
    /// <param name="bitmap">The bitmap to modify.</param>
    /// <param name="noiseRatio">Fraction of total pixels to replace with random colors (e.g., 0.05 for 5%).</param>
    static void AddNoise(Bitmap bitmap, float noiseRatio)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;
        int totalPixels = width * height;
        int noisyPixels = (int)(totalPixels * noiseRatio);
        var rand = new Random();

        // Randomly select pixels and assign them a random color.
        for (int i = 0; i < noisyPixels; i++)
        {
            int x = rand.Next(width);
            int y = rand.Next(height);
            var color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            bitmap.SetPixel(x, y, color);
        }
    }

    /// <summary>
    /// Reads a barcode image from a byte array and returns the number of detected barcodes.
    /// </summary>
    /// <param name="imageBytes">Byte array containing the barcode image.</param>
    /// <param name="useMinimal">
    /// If true, configures the reader to use minimal X-dimension settings; otherwise, default settings are used.
    /// </param>
    /// <returns>The count of detected barcodes.</returns>
    static int CountBarcodes(byte[] imageBytes, bool useMinimal)
    {
        using (var stream = new MemoryStream(imageBytes))
        using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
        {
            if (useMinimal)
            {
                // Adjust quality settings to use a minimal X-dimension.
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2f; // Example minimal size in pixels.
            }

            // Perform barcode detection.
            var results = reader.ReadBarCodes();

            // Return the number of detected barcodes (0 if none).
            return results?.Length ?? 0;
        }
    }
}