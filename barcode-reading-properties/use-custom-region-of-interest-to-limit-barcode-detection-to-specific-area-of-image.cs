// Title: Detect Barcode Within a Specified Region of Interest
// Description: Demonstrates generating a Code128 barcode, placing it on a larger canvas, defining a custom region of interest (ROI), and reading the barcode only from that ROI.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and image manipulation classes (Bitmap, Graphics) to limit barcode detection to a specific area. Typical use cases include processing scanned documents where only a portion contains a barcode, improving performance and accuracy. Developers often need to define ROI rectangles, crop images, and feed the cropped stream to the reader.
// Prompt: Use custom region of interest to limit barcode detection to a specific area of an image.
// Tags: barcode symbology, detection, region of interest, code128, aspose.barcode, image processing, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode, embeds it in a larger image,
/// defines a region of interest, and reads the barcode only from that region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, ROI definition,
    /// and barcode recognition steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeROI_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for intermediate images
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string combinedPath = Path.Combine(tempFolder, "combined.png");

        // 1. Generate a simple barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // 2. Create a larger canvas and draw the barcode onto it at a known offset
        int canvasWidth = 500;
        int canvasHeight = 300;
        int barcodeOffsetX = 150;
        int barcodeOffsetY = 80;

        using (var canvas = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format24bppRgb))
        {
            // Fill canvas with white background
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);

                // Load the barcode image
                if (!File.Exists(barcodePath))
                {
                    Console.WriteLine("Generated barcode image not found.");
                    return;
                }

                using (var barcodeImg = (Bitmap)Image.FromFile(barcodePath))
                {
                    // Draw the barcode onto the canvas at the specified offset
                    graphics.DrawImage(barcodeImg, barcodeOffsetX, barcodeOffsetY, barcodeImg.Width, barcodeImg.Height);
                }
            }

            // Save the combined image (canvas with barcode)
            canvas.Save(combinedPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }

        // Verify combined image exists
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Combined image not created.");
            return;
        }

        // 3. Define the Region of Interest (ROI) that contains the barcode
        // The ROI rectangle matches the position and size where the barcode was drawn
        var roi = new Rectangle(barcodeOffsetX, barcodeOffsetY, 0, 0);
        // Load the barcode image to get its dimensions
        using (var tempBarcode = (Bitmap)Image.FromFile(barcodePath))
        {
            roi.Width = tempBarcode.Width;
            roi.Height = tempBarcode.Height;
        }

        // 4. Load the combined image and crop to the ROI
        using (var combinedImg = (Bitmap)Image.FromFile(combinedPath))
        {
            // Ensure ROI is within image bounds
            if (roi.Right > combinedImg.Width || roi.Bottom > combinedImg.Height)
            {
                Console.WriteLine("ROI exceeds image bounds.");
                return;
            }

            using (var cropped = combinedImg.Clone(roi, combinedImg.PixelFormat))
            {
                // 5. Convert cropped bitmap to a memory stream for the reader
                using (var ms = new MemoryStream())
                {
                    cropped.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                    ms.Position = 0;

                    // 6. Read barcodes from the cropped region
                    // DecodeType.Code128 is a BaseDecodeType static member
                    using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        // No additional settings are required; the reader will scan the provided image
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine("No barcode detected in the ROI.");
                        }
                        else
                        {
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                                Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                                // Region of the detected barcode within the cropped image
                                var bounds = result.Region.Rectangle;
                                Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                            }
                        }
                    }
                }
            }
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually reclaim temp files
        }
    }
}