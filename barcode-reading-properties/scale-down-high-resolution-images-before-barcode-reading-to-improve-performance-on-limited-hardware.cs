using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Input high‑resolution image path (replace with an existing file for real test)
        string inputPath = "highres.png";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired maximum dimension after scaling (e.g., 800 pixels)
        const int maxDimension = 800;

        // Load the original high‑resolution bitmap
        using (Bitmap original = new Bitmap(inputPath))
        {
            // Determine scaling factor while preserving aspect ratio
            float scale = 1f;
            if (original.Width > maxDimension || original.Height > maxDimension)
            {
                scale = Math.Min((float)maxDimension / original.Width, (float)maxDimension / original.Height);
            }

            int newWidth = (int)(original.Width * scale);
            int newHeight = (int)(original.Height * scale);

            // If scaling is not needed, reuse the original bitmap
            if (scale >= 1f)
            {
                ProcessBitmap(original);
                return;
            }

            // Create a new bitmap with the scaled size
            using (Bitmap scaled = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb))
            {
                // Draw the original image onto the scaled bitmap with high‑quality interpolation
                using (Graphics graphics = Graphics.FromImage(scaled))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(original, new Rectangle(0, 0, newWidth, newHeight));
                }

                // Optionally, save the scaled image for verification
                // scaled.Save("scaled.png", ImageFormat.Png);

                // Perform barcode reading on the scaled bitmap
                ProcessBitmap(scaled);
            }
        }
    }

    // Reads barcodes from the provided bitmap and writes results to console
    static void ProcessBitmap(Bitmap bitmap)
    {
        // Choose a set of common decode types; adjust as needed
        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.Code128, DecodeType.QR, DecodeType.Code39))
        {
            // Use high‑performance mode to speed up recognition on limited hardware
            reader.QualitySettings = QualitySettings.HighPerformance;

            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}