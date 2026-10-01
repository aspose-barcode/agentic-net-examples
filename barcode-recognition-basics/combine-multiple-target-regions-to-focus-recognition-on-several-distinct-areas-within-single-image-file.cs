// Title: Combine Multiple Target Regions for Barcode Recognition in a Single Image
// Description: Demonstrates generating two different barcodes, merging them into one image, defining distinct target regions, and recognizing each barcode separately.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator to create barcodes, Bitmap and Graphics for image composition, and BarCodeReader for decoding. Typical scenarios include scanning composite documents where barcodes appear in known sub‑areas, requiring region‑based processing to improve accuracy and performance. Developers often need to define multiple rectangles, crop, and decode each region using the Aspose.BarCode API.
/// Prompt: Combine multiple target regions to focus recognition on several distinct areas within a single image file.
/// Tags: code128, qr, barcode-recognition, region-processing, png, aspose.barcode, aspose.drawing, barcode-generator, barcode-reader

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to combine multiple target regions within a single image
/// and perform barcode recognition on each region individually.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates two barcodes, merges them,
    /// defines target regions, and reads each barcode from its region.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Path for the combined image
        string combinedImagePath = Path.Combine(workDir, "combined.png");

        // Generate two separate barcode images (Code128 and QR)
        byte[] firstBarcode = GenerateBarcodeImage(EncodeTypes.Code128, "First", BarCodeImageFormat.Png);
        byte[] secondBarcode = GenerateBarcodeImage(EncodeTypes.QR, "Second", BarCodeImageFormat.Png);

        // Create a combined bitmap and draw both barcodes side by side
        using (var combinedBitmap = new Bitmap(400, 200))
        {
            using (var graphics = Graphics.FromImage(combinedBitmap))
            {
                graphics.Clear(Color.White);

                // Draw first barcode on the left half
                using (var ms = new MemoryStream(firstBarcode))
                using (var bmp = new Bitmap(ms))
                {
                    graphics.DrawImage(bmp, new Rectangle(0, 0, 200, 200));
                }

                // Draw second barcode on the right half
                using (var ms = new MemoryStream(secondBarcode))
                using (var bmp = new Bitmap(ms))
                {
                    graphics.DrawImage(bmp, new Rectangle(200, 0, 200, 200));
                }
            }

            // Save the combined image to disk
            combinedBitmap.Save(combinedImagePath, ImageFormat.Png);
        }

        // Verify that the combined image was created successfully
        if (!File.Exists(combinedImagePath))
        {
            Console.WriteLine("Failed to create the combined image.");
            return;
        }

        // Define the target regions (left and right halves of the image)
        var regions = new List<Rectangle>
        {
            new Rectangle(0, 0, 200, 200),   // First barcode region
            new Rectangle(200, 0, 200, 200) // Second barcode region
        };

        // Process each region separately by cropping and decoding
        using (var sourceBitmap = new Bitmap(combinedImagePath))
        {
            int regionIndex = 1;
            foreach (var rect in regions)
            {
                // Crop the region from the source bitmap
                using (var croppedBitmap = sourceBitmap.Clone(rect, sourceBitmap.PixelFormat))
                {
                    // Convert the cropped bitmap to a byte array
                    byte[] croppedBytes;
                    using (var ms = new MemoryStream())
                    {
                        croppedBitmap.Save(ms, ImageFormat.Png);
                        croppedBytes = ms.ToArray();
                    }

                    // Decode barcodes within the cropped region
                    using (var reader = new BarCodeReader(new MemoryStream(croppedBytes), DecodeType.AllSupportedTypes))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        Console.WriteLine($"Region {regionIndex}:");
                        if (results.Length == 0)
                        {
                            Console.WriteLine("  No barcode detected.");
                        }
                        else
                        {
                            foreach (var result in results)
                            {
                                Console.WriteLine($"  CodeText: {result.CodeText}");
                                Console.WriteLine($"  Symbology: {result.CodeTypeName}");
                                var bounds = result.Region.Rectangle;
                                Console.WriteLine($"  Region Bounds: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                                Console.WriteLine($"  Angle: {result.Region.Angle}");
                            }
                        }
                    }
                }

                regionIndex++;
            }
        }

        // Clean up temporary files (optional)
        try
        {
            Directory.Delete(workDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect demo execution
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encode type and returns the image bytes.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="format">The image format for the generated barcode.</param>
    /// <returns>Byte array containing the barcode image.</returns>
    private static byte[] GenerateBarcodeImage(BaseEncodeType encodeType, string codeText, BarCodeImageFormat format)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set X-dimension for better readability
            generator.Parameters.Barcode.XDimension.Point = 2.5f;

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}