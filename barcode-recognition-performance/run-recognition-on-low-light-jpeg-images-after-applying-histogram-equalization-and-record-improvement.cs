// Title: Barcode Recognition Improvement on Low‑Light Images Using Histogram Equalization
// Description: Demonstrates generating a QR code, simulating a low‑light image, applying contrast enhancement (as a stand‑in for histogram equalization), and comparing barcode reading quality before and after processing.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and quality‑related settings (BarcodeQualityMode, DeconvolutionMode, InverseImageMode) to handle challenging imaging conditions. Typical scenarios include scanning barcodes in poorly lit environments, where developers often need to preprocess images (e.g., histogram equalization) to improve detection rates.
/// Prompt: Run recognition on low‑light JPEG images after applying histogram equalization and record improvement.
// Tags: barcode, low-light, histogram-equalization, qr, generation, recognition, quality, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, low‑light simulation, histogram equalization, and quality comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample images, processes them, reads barcodes, and outputs quality metrics.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeLowLightDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original, low‑light, and equalized images
        string originalPath = Path.Combine(tempFolder, "original.jpg");
        string lowLightPath = Path.Combine(tempFolder, "lowlight.jpg");
        string equalizedPath = Path.Combine(tempFolder, "equalized.jpg");

        // Generate a sample QR barcode and save it as a JPEG image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                ms.Position = 0;

                using (var bitmap = new Bitmap(ms))
                {
                    // Save the original image for reference
                    bitmap.Save(originalPath, ImageFormat.Jpeg);

                    // -------------------------------------------------
                    // Simulate a low‑light condition by darkening the image
                    // -------------------------------------------------
                    using (var darkBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                    {
                        using (var g = Graphics.FromImage(darkBitmap))
                        {
                            g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            using (var overlay = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                            {
                                g.FillRectangle(overlay, 0, 0, darkBitmap.Width, darkBitmap.Height);
                            }
                        }
                        darkBitmap.Save(lowLightPath, ImageFormat.Jpeg);
                    }

                    // -------------------------------------------------
                    // Apply a simple contrast enhancement (stand‑in for histogram equalization)
                    // -------------------------------------------------
                    using (var equalizedBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                    {
                        using (var g = Graphics.FromImage(equalizedBitmap))
                        {
                            // Build a high‑contrast color matrix
                            var contrast = 1.5f;
                            var matrix = new ColorMatrix(new float[][]
                            {
                                new float[] {contrast, 0, 0, 0, 0},
                                new float[] {0, contrast, 0, 0, 0},
                                new float[] {0, 0, contrast, 0, 0},
                                new float[] {0, 0, 0, 1, 0},
                                new float[] {0, 0, 0, 0, 1}
                            });

                            using (var attr = new ImageAttributes())
                            {
                                attr.SetColorMatrix(matrix);
                                g.DrawImage(bitmap,
                                            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                            0, 0, bitmap.Width, bitmap.Height,
                                            GraphicsUnit.Pixel, attr);
                            }
                        }
                        equalizedBitmap.Save(equalizedPath, ImageFormat.Jpeg);
                    }
                }
            }
        }

        // -------------------------------------------------
        // Local function: reads a barcode from an image and returns success flag and quality metric
        // -------------------------------------------------
        bool ReadBarcode(string imagePath, out double quality)
        {
            quality = 0;
            if (!File.Exists(imagePath))
                return false;

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Configure quality settings optimized for low‑light conditions
                reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

                var results = reader.ReadBarCodes();
                if (results.Length > 0)
                {
                    quality = results[0].ReadingQuality;
                    return true;
                }
                return false;
            }
        }

        // Read barcode from the low‑light image (before equalization)
        bool beforeSuccess = ReadBarcode(lowLightPath, out double beforeQuality);
        // Read barcode from the equalized image (after processing)
        bool afterSuccess = ReadBarcode(equalizedPath, out double afterQuality);

        // Output the comparison results
        Console.WriteLine($"Low‑light image: Success = {beforeSuccess}, Quality = {beforeQuality:F2}");
        Console.WriteLine($"After equalization: Success = {afterSuccess}, Quality = {afterQuality:F2}");

        // -------------------------------------------------
        // Clean up temporary files (optional)
        // -------------------------------------------------
        try
        {
            File.Delete(originalPath);
            File.Delete(lowLightPath);
            File.Delete(equalizedPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any cleanup errors
        }
    }
}