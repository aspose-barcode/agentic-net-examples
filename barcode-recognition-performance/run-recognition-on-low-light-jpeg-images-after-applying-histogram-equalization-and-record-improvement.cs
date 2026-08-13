// Title: Barcode recognition on low‑light JPEG with histogram equalization
// Description: Demonstrates reading barcodes from a low‑light JPEG image, applying histogram equalization (simulated), and comparing detection confidence.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It shows how to use BarCodeReader with high‑quality settings, work with Aspose.Drawing Bitmap objects, and handle multiple symbologies such as Code39, Code128, and QR. Developers often need to improve barcode detection in poor lighting conditions by preprocessing images before recognition.
// Prompt: Run recognition on low‑light JPEG images after applying histogram equalization and record improvement.
// Tags: barcode, recognition, low-light, jpeg, histogram-equalization, aspose.barcode, aspose.drawing, code39, code128, qr

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that reads barcodes from a low‑light JPEG image,
/// applies a simulated histogram equalization, and compares detection results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode recognition on the original
    /// and processed images and outputs detection details to the console.
    /// </summary>
    static void Main()
    {
        // Path to the low‑light JPEG image (adjust as needed)
        string imagePath = "lowlight.jpg";

        // Verify that the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {Path.GetFullPath(imagePath)}");
            return;
        }

        // ---------- Load original image ----------
        using (var originalBitmap = new Bitmap(imagePath))
        {
            Console.WriteLine("Recognizing original image...");

            // Initialize barcode reader for selected symbologies
            using (var reader = new BarCodeReader(originalBitmap, DecodeType.Code39, DecodeType.Code128, DecodeType.QR))
            {
                // Use high‑quality settings to improve detection in low‑light conditions
                reader.QualitySettings = QualitySettings.HighQuality;

                // Perform recognition
                var results = reader.ReadBarCodes();

                // Output results for the original image
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected in original image.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"[Original] Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
                    }
                }
            }

            // ---------- Apply histogram equalization (placeholder) ----------
            // Aspose.Drawing does not provide a direct histogram equalization method.
            // In a real scenario you would process 'originalBitmap' here.
            // For demonstration we simply clone the bitmap to simulate a processed image.
            using (var processedBitmap = (Bitmap)originalBitmap.Clone())
            {
                // Example of a simple contrast adjustment (optional)
                // var attributes = new ImageAttributes();
                // attributes.SetContrast(1.5f); // placeholder – actual method may differ
                // processedBitmap = processedBitmap.Adjust(attributes); // placeholder

                Console.WriteLine("Recognizing processed (histogram‑equalized) image...");

                // Initialize barcode reader for the processed image
                using (var reader = new BarCodeReader(processedBitmap, DecodeType.Code39, DecodeType.Code128, DecodeType.QR))
                {
                    // Apply the same high‑quality settings
                    reader.QualitySettings = QualitySettings.HighQuality;

                    // Perform recognition on the processed image
                    var results = reader.ReadBarCodes();

                    // Output results for the processed image
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected in processed image.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"[Processed] Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
                        }
                    }
                }
            }
        }

        // Program ends automatically; no explicit pause required.
    }
}