// Title: Measure barcode recognition speed for a 1‑MB PNG image
// Description: Demonstrates generating a large PNG barcode image and verifying that its recognition completes within 150 ms, useful for performance testing.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing how to use BarcodeGenerator, BarCodeReader, and Aspose.Drawing to create and recognize barcodes. Developers often need to ensure fast decoding of high‑resolution images in real‑time applications, and this snippet provides a baseline measurement approach.
// Prompt: Create a unit test that verifies recognition speed remains under 150 ms for 1‑MB PNG files.
// Tags: code128, barcode generation, barcode recognition, png, aspose.barcode, aspose.barcode.generation, aspose.barcode.barcoderecognition, aspose.drawing

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a large barcode PNG and measuring recognition speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a ~1 MB barcode image, reads it, and checks that recognition completes within 150 ms.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated PNG file
        string pngPath = Path.Combine(tempFolder, "test.png");

        // Generate a large barcode image (~1 MB) using Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test1234567890"))
        {
            // Create the base barcode bitmap
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Define target dimensions to increase file size (2000 × 2000 pixels)
                int targetSize = 2000;

                // Create a larger bitmap and draw the barcode centered on a white background
                using (Bitmap largeBmp = new Bitmap(targetSize, targetSize))
                {
                    using (Graphics graphics = Graphics.FromImage(largeBmp))
                    {
                        graphics.Clear(Color.White);
                        int x = (targetSize - barcodeBmp.Width) / 2;
                        int y = (targetSize - barcodeBmp.Height) / 2;
                        graphics.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                    }

                    // Save the large bitmap as a PNG file
                    largeBmp.Save(pngPath, ImageFormat.Png);
                }
            }
        }

        // Verify that the PNG file was created successfully
        if (!File.Exists(pngPath))
        {
            Console.WriteLine("FAILED: PNG file was not created.");
            return;
        }

        // Output the generated file size (optional diagnostic information)
        long fileSize = new FileInfo(pngPath).Length;
        Console.WriteLine($"Generated PNG size: {fileSize} bytes.");

        // Read the barcode from the PNG and measure recognition time
        using (var reader = new BarCodeReader(pngPath, DecodeType.AllSupportedTypes))
        {
            Stopwatch watch = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            watch.Stop();

            long elapsedMs = watch.ElapsedMilliseconds;
            bool success = results.Length > 0 && elapsedMs <= 150;

            // Output performance and detection results
            Console.WriteLine($"Recognition time: {elapsedMs} ms.");
            Console.WriteLine($"Barcodes detected: {results.Length}.");
            Console.WriteLine($"Test {(success ? "PASSED" : "FAILED")} (must be ≤150 ms).");
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}