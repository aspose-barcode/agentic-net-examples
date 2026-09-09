// Title: Barcode recognition improvement on low‑light images using histogram equalization
// Description: Demonstrates generating a QR barcode, creating a low‑light JPEG, applying histogram equalization, and comparing recognition results before and after enhancement.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It shows how to use Aspose.BarCode.Generation to create barcodes, Aspose.Drawing for image manipulation, and Aspose.BarCode.BarCodeRecognition to read barcodes from degraded images. Developers often need to improve detection on low‑quality scans by applying image processing techniques such as histogram equalization before recognition.
// Prompt: Run recognition on low‑light JPEG images after applying histogram equalization and record improvement.
// Tags: qr, low-light, histogram-equalization, barcode-recognition, image-processing, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, low‑light image simulation, histogram equalization,
/// and recognition comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, creates a dimmed version,
    /// enhances it with histogram equalization, and reports detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeLowLight_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the original, low‑light, and equalized images
        string originalPath = Path.Combine(tempDir, "barcode.png");
        string lowLightPath = Path.Combine(tempDir, "lowlight.jpg");
        string equalizedPath = Path.Combine(tempDir, "equalized.jpg");

        // ------------------------------------------------------------
        // Generate a QR barcode and save it as PNG
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Load the original image and create a low‑light JPEG version
        // ------------------------------------------------------------
        using (Bitmap original = new Bitmap(originalPath))
        {
            using (Bitmap lowLight = new Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb))
            {
                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color c = original.GetPixel(x, y);
                        // Dim each color channel to simulate low lighting (30% brightness)
                        int r = (int)(c.R * 0.3);
                        int g = (int)(c.G * 0.3);
                        int b = (int)(c.B * 0.3);
                        Color dark = Color.FromArgb(r, g, b);
                        lowLight.SetPixel(x, y, dark);
                    }
                }
                lowLight.Save(lowLightPath, ImageFormat.Jpeg);
            }

            // ------------------------------------------------------------
            // Apply histogram equalization to the low‑light image
            // ------------------------------------------------------------
            using (Bitmap lowLight = new Bitmap(lowLightPath))
            {
                int width = lowLight.Width;
                int height = lowLight.Height;
                int totalPixels = width * height;
                int[] histogram = new int[256];

                // Build histogram of grayscale intensities
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color c = lowLight.GetPixel(x, y);
                        int intensity = (c.R + c.G + c.B) / 3;
                        histogram[intensity]++;
                    }
                }

                // Compute cumulative distribution function (CDF)
                int[] cdf = new int[256];
                cdf[0] = histogram[0];
                for (int i = 1; i < 256; i++)
                {
                    cdf[i] = cdf[i - 1] + histogram[i];
                }

                // Find the minimum non‑zero CDF value
                int cdfMin = 0;
                for (int i = 0; i < 256; i++)
                {
                    if (cdf[i] != 0)
                    {
                        cdfMin = cdf[i];
                        break;
                    }
                }

                // Build lookup table (LUT) for equalization
                byte[] lut = new byte[256];
                for (int i = 0; i < 256; i++)
                {
                    lut[i] = (byte)Math.Round(((double)(cdf[i] - cdfMin) / (totalPixels - cdfMin)) * 255.0);
                }

                // Apply the LUT to create the equalized image
                using (Bitmap equalized = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Color c = lowLight.GetPixel(x, y);
                            int intensity = (c.R + c.G + c.B) / 3;
                            byte newVal = lut[intensity];
                            Color newColor = Color.FromArgb(newVal, newVal, newVal);
                            equalized.SetPixel(x, y, newColor);
                        }
                    }
                    equalized.Save(equalizedPath, ImageFormat.Jpeg);
                }
            }
        }

        // ------------------------------------------------------------
        // Local function: reads a barcode from an image and reports the result
        // ------------------------------------------------------------
        void ReadAndReport(string imagePath, string label)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"{label}: File not found.");
                return;
            }

            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Adjust quality settings for low‑quality images
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                BarCodeResult[] results = reader.ReadBarCodes();
                bool detected = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                Console.WriteLine($"{label}: {(detected ? "Detected" : "Not detected")}");
                if (detected)
                {
                    Console.WriteLine($"  CodeText: {results[0].CodeText}");
                    Console.WriteLine($"  Quality: {results[0].ReadingQuality}");
                }
            }
        }

        // ------------------------------------------------------------
        // Perform recognition on the low‑light and equalized images
        // ------------------------------------------------------------
        ReadAndReport(lowLightPath, "Low‑light image");
        ReadAndReport(equalizedPath, "Equalized image");

        // ------------------------------------------------------------
        // Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}