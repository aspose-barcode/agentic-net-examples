// Title: Invert barcode colors and compare recognition with InverseImage setting
// Description: Demonstrates generating a QR barcode, creating a negative‑image version by inverting colors, and evaluating detection using Aspose.BarCode's InverseImage quality setting.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, Bitmap manipulation to produce a negative image, and BarCodeReader with QualitySettings.InverseImage to handle inverted color schemes. Developers often need to process scanned documents or photos where barcodes appear as negative images; this pattern illustrates typical API usage for such scenarios.
// Prompt: Adjust color scheme to invert colors and assess recognition performance on negative‑image barcodes.
// Tags: qr, barcode, color inversion, inverseimage, recognition, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a QR barcode, inverting its colors, and comparing recognition results
/// with the InverseImage quality setting enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, creates an inverted image,
    /// runs recognition with different InverseImage modes, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeInvertDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the original and inverted barcode images
        string originalPath = Path.Combine(tempDir, "original.png");
        string invertedPath = Path.Combine(tempDir, "inverted.png");

        // Generate a QR barcode and save the original image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose Test"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Generate a bitmap from the barcode for pixel‑level manipulation
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Invert each pixel's RGB values to produce a negative image
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        Color inverted = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                        bitmap.SetPixel(x, y, inverted);
                    }
                }

                // Save the inverted bitmap as a PNG file
                bitmap.Save(invertedPath, ImageFormat.Png);
            }
        }

        // Verify that the inverted image was created successfully
        if (!File.Exists(invertedPath))
        {
            Console.WriteLine("Inverted image was not created.");
            return;
        }

        // Recognize the inverted barcode with InverseImage disabled
        using (var readerDisabled = new BarCodeReader(invertedPath, DecodeType.QR))
        {
            readerDisabled.QualitySettings.InverseImage = InverseImageMode.Disabled;
            BarCodeResult[] results = readerDisabled.ReadBarCodes();
            Console.WriteLine($"InverseImage Disabled: {results.Length} barcode(s) detected.");
        }

        // Recognize the inverted barcode with InverseImage enabled
        using (var readerEnabled = new BarCodeReader(invertedPath, DecodeType.QR))
        {
            readerEnabled.QualitySettings.InverseImage = InverseImageMode.Enabled;
            BarCodeResult[] results = readerEnabled.ReadBarCodes();
            Console.WriteLine($"InverseImage Enabled: {results.Length} barcode(s) detected.");
        }

        // Clean up temporary files and directory
        try { File.Delete(originalPath); } catch { }
        try { File.Delete(invertedPath); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}