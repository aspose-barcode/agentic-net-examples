// Title: Compare barcode detection with and without UseMinimalXDimension on noisy images
// Description: Demonstrates generating multiple Code128 barcodes, adding random noise, and comparing detection counts when the X‑dimension setting is toggled. Shows practical impact of minimal X‑dimension mode on noisy scans.
// Category-Description: This example belongs to the Aspose.BarCode image recognition category, focusing on barcode detection quality settings. It uses BarCodeGenerator for creating barcodes, BarCodeReader with QualitySettings, and XDimensionMode to control X‑dimension handling. Developers often need to fine‑tune detection in low‑quality or noisy images, and this snippet illustrates how to evaluate the effect of the UseMinimalXDimension option.
// Prompt: Compare the number of detected barcodes when UseMinimalXDimension is toggled on versus off for noisy images.
// Tags: code128, barcode-detection, useminimalxdimension, noisy-image, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, noise addition, and detection comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, adds noise, saves image, and compares detection counts with different X‑dimension settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeComparison_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate individual barcode images and store them in a list
        List<Bitmap> barcodeBitmaps = new List<Bitmap>();
        int barcodeCount = 5;
        for (int i = 1; i <= barcodeCount; i++)
        {
            string codeText = $"CODE{i:D3}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Minimal configuration; let size adapt automatically
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                using (Bitmap bmp = generator.GenerateBarCodeImage())
                {
                    // Clone bitmap to keep after disposing generator
                    Bitmap clone = new Bitmap(bmp);
                    barcodeBitmaps.Add(clone);
                }
            }
        }

        // Determine combined image dimensions (max width and total height with spacing)
        int maxWidth = 0;
        int totalHeight = 0;
        int spacing = 10;
        foreach (var bmp in barcodeBitmaps)
        {
            if (bmp.Width > maxWidth) maxWidth = bmp.Width;
            totalHeight += bmp.Height + spacing;
        }

        // Create a combined image and draw each barcode centered horizontally
        using (Bitmap combined = new Bitmap(maxWidth, totalHeight))
        {
            using (Graphics g = Graphics.FromImage(combined))
            {
                g.Clear(Color.White);
                int y = 0;
                foreach (var bmp in barcodeBitmaps)
                {
                    g.DrawImage(bmp, (maxWidth - bmp.Width) / 2, y);
                    y += bmp.Height + spacing;
                }

                // Add random noise (gray lines) to simulate a noisy scan
                Random rnd = new Random();
                Pen noisePen = new Pen(Color.Gray, 1f);
                for (int n = 0; n < 100; n++)
                {
                    int x1 = rnd.Next(maxWidth);
                    int y1 = rnd.Next(totalHeight);
                    int x2 = rnd.Next(maxWidth);
                    int y2 = rnd.Next(totalHeight);
                    g.DrawLine(noisePen, x1, y1, x2, y2);
                }
            }

            // Save the noisy combined image to the temporary folder
            string noisyImagePath = Path.Combine(tempFolder, "noisy_barcodes.png");
            combined.Save(noisyImagePath, ImageFormat.Png);

            // Detect barcodes without UseMinimalXDimension (default Normal mode)
            int countNormal = ReadBarcodes(noisyImagePath, useMinimal: false);
            // Detect barcodes with UseMinimalXDimension enabled
            int countMinimal = ReadBarcodes(noisyImagePath, useMinimal: true);

            Console.WriteLine($"Barcodes detected (Normal XDimension): {countNormal}");
            Console.WriteLine($"Barcodes detected (UseMinimalXDimension): {countMinimal}");
        }

        // Cleanup generated bitmap resources
        foreach (var bmp in barcodeBitmaps)
        {
            bmp.Dispose();
        }

        // Optionally delete temporary folder (commented out to allow inspection)
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Reads barcodes from the specified image using the requested X‑dimension mode.
    /// </summary>
    /// <param name="imagePath">Path to the image file containing barcodes.</param>
    /// <param name="useMinimal">If true, enables UseMinimalXDimension; otherwise uses Normal mode.</param>
    /// <returns>The number of barcodes detected.</returns>
    static int ReadBarcodes(string imagePath, bool useMinimal)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return 0;
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure X‑dimension handling based on the useMinimal flag
            if (useMinimal)
            {
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 1f;
            }
            else
            {
                reader.QualitySettings.XDimension = XDimensionMode.Normal;
            }

            BarCodeResult[] results = reader.ReadBarCodes();
            return results?.Length ?? 0;
        }
    }
}