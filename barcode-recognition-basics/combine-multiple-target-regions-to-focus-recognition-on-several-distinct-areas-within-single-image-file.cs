// Title: Recognize Multiple Barcodes in Specified Image Regions
// Description: Demonstrates generating two barcodes, combining them into a single image, and scanning only defined target regions to detect each barcode.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to use BarcodeGenerator for barcode creation and BarCodeReader with region‑based scanning. Developers often need to focus recognition on particular areas of a complex image to improve performance or avoid false positives; this snippet shows how to define multiple target rectangles and process them individually.
// Prompt: Combine multiple target regions to focus recognition on several distinct areas within a single image file.
// Tags: barcode generation, barcode recognition, region scanning, qr code, code128, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates combining multiple barcodes into a single image and recognizing them within defined target regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, creates a composite image, defines target regions, and reads barcodes from each region.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Path for the combined image that will contain both barcodes
        string combinedImagePath = Path.Combine(workFolder, "combined.png");

        // -------------------------------------------------
        // Generate a QR code and a Code128 barcode in memory
        // -------------------------------------------------
        MemoryStream qrStream = new MemoryStream();
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "QR Sample"))
        {
            qrGenerator.Save(qrStream, BarCodeImageFormat.Png);
        }
        qrStream.Position = 0;

        MemoryStream code128Stream = new MemoryStream();
        using (var code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128"))
        {
            code128Generator.Save(code128Stream, BarCodeImageFormat.Png);
        }
        code128Stream.Position = 0;

        // Load the generated barcode images into Bitmap objects
        Bitmap qrBitmap;
        using (qrBitmap = new Bitmap(qrStream))
        {
            // Scope placeholder – bitmap remains usable after the using block
        }

        Bitmap code128Bitmap;
        using (code128Bitmap = new Bitmap(code128Stream))
        {
            // Scope placeholder – bitmap remains usable after the using block
        }

        // -------------------------------------------------
        // Create a larger canvas and draw both barcodes onto it
        // -------------------------------------------------
        using (Bitmap canvas = new Bitmap(500, 300))
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
                // Draw QR code at position (50,50)
                g.DrawImage(qrBitmap, new Rectangle(50, 50, qrBitmap.Width, qrBitmap.Height));
                // Draw Code128 barcode at position (250,150)
                g.DrawImage(code128Bitmap, new Rectangle(250, 150, code128Bitmap.Width, code128Bitmap.Height));
            }

            // Save the combined image to disk
            canvas.Save(combinedImagePath, ImageFormat.Png);

            // -------------------------------------------------
            // Define target regions where each barcode is expected
            // -------------------------------------------------
            List<Rectangle> targetRegions = new List<Rectangle>
            {
                new Rectangle(40, 40, 200, 200),   // Region surrounding the QR code
                new Rectangle(240, 140, 200, 200) // Region surrounding the Code128 barcode
            };

            Console.WriteLine($"Combined image saved to: {combinedImagePath}");
            Console.WriteLine("Scanning defined regions...");

            // -------------------------------------------------
            // Process each region separately and read barcodes
            // -------------------------------------------------
            int regionIndex = 1;
            foreach (var region in targetRegions)
            {
                // Crop the defined region from the canvas
                using (Bitmap regionBitmap = canvas.Clone(region, canvas.PixelFormat))
                {
                    using (MemoryStream regionStream = new MemoryStream())
                    {
                        regionBitmap.Save(regionStream, ImageFormat.Png);
                        regionStream.Position = 0;

                        // Use BarCodeReader to decode any barcode present in the cropped region
                        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                        using (var reader = new BarCodeReader(regionStream, decodeType))
                        {
                            BarCodeResult[] results = reader.ReadBarCodes();
                            Console.WriteLine($"Region {regionIndex}: Found {results.Length} barcode(s).");
                            foreach (var result in results)
                            {
                                Console.WriteLine($"  CodeText: {result.CodeText}");
                                Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                                // Region of the detected barcode within the cropped image
                                var bounds = result.Region.Rectangle;
                                Console.WriteLine($"  Detected Region - X:{bounds.X}, Y:{bounds.Y}, W:{bounds.Width}, H:{bounds.Height}");
                                Console.WriteLine($"  Angle: {result.Region.Angle}");
                            }
                        }
                    }
                }
                regionIndex++;
            }
        }

        // Cleanup temporary files (optional)
        // File.Delete(combinedImagePath);
        // Directory.Delete(workFolder, true);
    }
}