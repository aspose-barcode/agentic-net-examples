// Title: Barcode Orientation Detection from Rotated Image
// Description: Generates a Code128 barcode, rotates it to simulate a tilted webcam capture, then reads the barcode and logs its orientation angle.
// Prompt: Read barcodes from a video frame captured by a webcam and log orientation angles.
// Tags: barcode, orientation, code128, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode, rotate it, and read its orientation angle using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, applies a rotation, reads it back, and prints the detected angle.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            using (var originalBmp = generator.GenerateBarCodeImage())
            {
                // Create a bitmap to hold the rotated image.
                using (var rotatedBmp = new Bitmap(originalBmp.Width, originalBmp.Height))
                {
                    // Obtain a graphics object for drawing onto the rotated bitmap.
                    using (var graphics = Graphics.FromImage(rotatedBmp))
                    {
                        // Set high‑quality rendering options.
                        graphics.SmoothingMode = Aspose.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.InterpolationMode = Aspose.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        // Translate the origin to the center, rotate, then translate back.
                        graphics.TranslateTransform(originalBmp.Width / 2f, originalBmp.Height / 2f);
                        graphics.RotateTransform(45f);
                        graphics.TranslateTransform(-originalBmp.Width / 2f, -originalBmp.Height / 2f);

                        // Draw the original barcode onto the rotated canvas.
                        graphics.DrawImage(originalBmp, 0, 0, originalBmp.Width, originalBmp.Height);
                    }

                    // Initialize a barcode reader for the rotated image, supporting all barcode types.
                    using (var reader = new BarCodeReader(rotatedBmp, DecodeType.AllSupportedTypes))
                    {
                        // Iterate through all detected barcodes.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output the decoded text.
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            // Output the orientation angle of the barcode region.
                            Console.WriteLine($"Detected Angle: {result.Region.Angle}");
                        }
                    }
                }
            }
        }
    }
}