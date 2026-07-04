// Title: Scaling High‑Resolution Images for Efficient Barcode Reading
// Description: Demonstrates how to downscale a high‑resolution image before barcode recognition to improve performance on constrained hardware.
// Prompt: Scale down high‑resolution images before barcode reading to improve performance on limited hardware.
// Tags: barcode, scaling, image processing, performance, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that scales down a high‑resolution image and reads barcodes from it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads an image, optionally scales it, and processes it for barcode detection.
    /// </summary>
    static void Main()
    {
        // Path to the high‑resolution image containing barcodes
        const string inputImagePath = "highres.png";

        // Verify that the image file exists before proceeding
        if (!File.Exists(inputImagePath))
        {
            Console.WriteLine($"File not found: {inputImagePath}");
            return;
        }

        // Desired maximum dimension (width or height) after scaling, in pixels
        const int maxDimension = 800;

        // Load the original high‑resolution image into a Bitmap object
        using (var original = new Bitmap(inputImagePath))
        {
            // Determine the scaling factor while preserving the aspect ratio
            float scale = 1f;
            if (original.Width > original.Height)
            {
                // Landscape orientation: limit width
                if (original.Width > maxDimension)
                    scale = (float)maxDimension / original.Width;
            }
            else
            {
                // Portrait orientation: limit height
                if (original.Height > maxDimension)
                    scale = (float)maxDimension / original.Height;
            }

            // If the image is already within the desired size, process it directly
            if (scale >= 1f)
            {
                ProcessImage(original);
                return;
            }

            // Calculate new dimensions based on the scaling factor
            int newWidth = (int)(original.Width * scale);
            int newHeight = (int)(original.Height * scale);

            // Create a new bitmap with the reduced size
            using (var scaled = new Bitmap(newWidth, newHeight))
            {
                // Render the original image onto the scaled bitmap
                using (var graphics = Graphics.FromImage(scaled))
                {
                    graphics.DrawImage(original, 0, 0, newWidth, newHeight);
                }

                // Perform barcode recognition on the scaled image
                ProcessImage(scaled);
            }
        }
    }

    // Reads barcodes from the provided bitmap and prints the detection results
    private static void ProcessImage(Bitmap bitmap)
    {
        // Initialize the reader to detect all supported barcode types
        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
        {
            // Apply a high‑performance preset to speed up recognition
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Iterate through each detected barcode and output its details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}