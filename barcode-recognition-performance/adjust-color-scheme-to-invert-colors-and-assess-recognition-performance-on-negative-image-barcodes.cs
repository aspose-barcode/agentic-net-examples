// Title: Invert barcode image colors and evaluate recognition performance
// Description: Demonstrates generating a QR barcode, creating a negative‑image version by inverting colors, and comparing recognition results with the InverseImage setting enabled and disabled.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It shows how to use BarcodeGenerator to create barcodes, Aspose.Drawing to manipulate pixel data, and BarCodeReader with QualitySettings.InverseImage to handle negative‑image barcodes. Developers often need to assess scanner robustness on inverted colors, making this pattern useful for testing and preprocessing pipelines.
// Prompt: Adjust color scheme to invert colors and assess recognition performance on negative‑image barcodes.
// Tags: qr, barcode, image inversion, inverseimage, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a QR barcode, creates an inverted‑color version, and evaluates
/// recognition performance using different InverseImage settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, color inversion,
    /// recognition testing, and cleanup of temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory for generated files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeNeg_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the original and inverted images
        string originalPath = Path.Combine(workDir, "original.png");
        string invertedPath = Path.Combine(workDir, "inverted.png");

        // ------------------------------------------------------------
        // Generate a QR barcode and save it as a PNG image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Invert the colors of the generated barcode image to produce a negative image
        // ------------------------------------------------------------
        using (Bitmap bitmap = new Bitmap(originalPath))
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color src = bitmap.GetPixel(x, y);
                    Color inv = Color.FromArgb(255 - src.R, 255 - src.G, 255 - src.B);
                    bitmap.SetPixel(x, y, inv);
                }
            }
            bitmap.Save(invertedPath, ImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Local function to read barcodes from an image with a specified InverseImage mode
        // ------------------------------------------------------------
        int ReadBarcodes(string path, InverseImageMode mode)
        {
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings.InverseImage = mode;
                BarCodeResult[] results = reader.ReadBarCodes();
                return results?.Length ?? 0;
            }
        }

        // ------------------------------------------------------------
        // Assess recognition on the original image (baseline)
        // ------------------------------------------------------------
        int originalCountDisabled = ReadBarcodes(originalPath, InverseImageMode.Disabled);
        int originalCountEnabled = ReadBarcodes(originalPath, InverseImageMode.Enabled);

        // ------------------------------------------------------------
        // Assess recognition on the inverted (negative) image
        // ------------------------------------------------------------
        int invertedCountDisabled = ReadBarcodes(invertedPath, InverseImageMode.Disabled);
        int invertedCountEnabled = ReadBarcodes(invertedPath, InverseImageMode.Enabled);

        // Output the comparison results
        Console.WriteLine("Recognition results:");
        Console.WriteLine($"Original image - InverseImage Disabled: {originalCountDisabled}");
        Console.WriteLine($"Original image - InverseImage Enabled : {originalCountEnabled}");
        Console.WriteLine($"Inverted image - InverseImage Disabled: {invertedCountDisabled}");
        Console.WriteLine($"Inverted image - InverseImage Enabled : {invertedCountEnabled}");

        // ------------------------------------------------------------
        // Cleanup temporary files and directory
        // ------------------------------------------------------------
        try
        {
            File.Delete(originalPath);
            File.Delete(invertedPath);
            Directory.Delete(workDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}