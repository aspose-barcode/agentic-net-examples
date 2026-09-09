// Title: Read barcode from a rotated webcam frame and log orientation angle
// Description: Demonstrates generating a QR barcode, rotating it to simulate a webcam capture, and using Aspose.BarCode to read the barcode while retrieving its orientation angle.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, System.Drawing for image manipulation, and BarCodeReader for decoding barcodes from images. Typical scenarios include scanning barcodes from live camera feeds, handling rotated frames, and extracting metadata such as orientation. Developers often need to generate test barcodes, apply transformations, and reliably read them in real‑time applications.
// Prompt: Read barcodes from a video frame captured by a webcam and log orientation angles.
// Tags: barcode, qr, orientation, rotation, webcam, generation, recognition, aspose.barcode, c#, .net

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, rotating it to mimic a webcam frame,
/// and reading the barcode with Aspose.BarCode while logging its orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary folder, generates and rotates a QR code,
    /// reads the barcode from the rotated image, outputs its type, text, and angle,
    /// and finally cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated images
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeWebcamDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original and rotated images
        string originalPath = Path.Combine(workFolder, "original.png");
        string rotatedPath = Path.Combine(workFolder, "rotated.png");

        // --------------------------------------------------------------------
        // Generate a sample QR barcode image and save it as PNG
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Rotate the image to simulate a webcam frame with a 45° orientation
        // --------------------------------------------------------------------
        using (Bitmap original = (Bitmap)Image.FromFile(originalPath))
        {
            // Create a bitmap with the same dimensions to hold the rotated image
            using (Bitmap rotated = new Bitmap(original.Width, original.Height))
            {
                using (Graphics g = Graphics.FromImage(rotated))
                {
                    // Fill background with white to avoid transparent corners after rotation
                    g.Clear(Color.White);

                    // Translate to the center, rotate, then translate back
                    g.TranslateTransform(original.Width / 2f, original.Height / 2f);
                    g.RotateTransform(45f); // rotate 45 degrees
                    g.TranslateTransform(-original.Width / 2f, -original.Height / 2f);

                    // Draw the original image onto the rotated canvas
                    g.DrawImage(original, 0, 0, original.Width, original.Height);
                }

                // Save the rotated image to disk
                rotated.Save(rotatedPath, ImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Read barcodes from the rotated image and log orientation angles
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(rotatedPath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Angle: {result.Region.Angle}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(originalPath);
            File.Delete(rotatedPath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}