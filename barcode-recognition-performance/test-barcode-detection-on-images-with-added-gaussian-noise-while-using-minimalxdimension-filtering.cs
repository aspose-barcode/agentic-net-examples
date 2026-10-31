// Title: Barcode detection on Gaussian‑noised image with MinimalXDimension filter
// Description: Generates a Code128 barcode, adds random gray noise to simulate Gaussian noise, then reads the barcode using MinimalXDimension filtering.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs, focusing on image quality settings such as XDimensionMode and MinimalXDimension. It shows how to create a barcode, corrupt it with noise, and reliably detect it—common tasks for developers building scanning solutions that must handle imperfect images.
// Prompt: Test barcode detection on images with added Gaussian noise while using MinimalXDimension filtering.
// Tags: barcode, code128, noise, gaussian, minimalxdimension, qualitysettings, generation, recognition, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, adding synthetic Gaussian‑like noise, and detecting the barcode using MinimalXDimension filtering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, noise addition, detection, and cleanup steps.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary working folder for intermediate files
        // ------------------------------------------------------------
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeNoiseTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Paths for the clean and noisy barcode images
        string barcodePath = Path.Combine(workFolder, "barcode.png");
        string noisyPath = Path.Combine(workFolder, "barcode_noisy.png");

        // Text to encode in the barcode
        string codeText = "1234567890";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Add synthetic Gaussian‑like noise by drawing random gray dots
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(barcodePath))
        {
            Random rnd = new Random();
            int width = bitmap.Width;
            int height = bitmap.Height;
            int noisePixels = (width * height) / 20; // Approx. 5 % of the image area

            for (int i = 0; i < noisePixels; i++)
            {
                int x = rnd.Next(width);
                int y = rnd.Next(height);
                // Random gray value for noise pixel
                int gray = rnd.Next(256);
                Color noiseColor = Color.FromArgb(gray, gray, gray);
                bitmap.SetPixel(x, y, noiseColor);
            }

            bitmap.Save(noisyPath, ImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the noisy image was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(noisyPath))
        {
            Console.WriteLine("Failed to create noisy image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the noisy image using MinimalXDimension filtering
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(noisyPath, decodeType))
        {
            // Configure quality settings for minimal X dimension detection
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f; // Minimum X dimension in pixels

            // Apply a high‑performance preset (optional)
            reader.QualitySettings = QualitySettings.HighPerformance;

            BarCodeResult[] results = null;
            try
            {
                results = reader.ReadBarCodes();
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine("Image loading failed: " + ex.Message);
                return;
            }

            // --------------------------------------------------------
            // Output detection results
            // --------------------------------------------------------
            if (results != null && results.Length > 0)
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
            else
            {
                Console.WriteLine("No barcode detected.");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and folder
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(noisyPath)) File.Delete(noisyPath);
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}