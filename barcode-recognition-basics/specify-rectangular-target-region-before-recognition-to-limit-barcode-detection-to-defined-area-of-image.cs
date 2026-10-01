// Title: Specify Rectangular Target Region for Barcode Recognition
// Description: Demonstrates how to limit barcode detection to a defined rectangular area of an image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to work with BarCodeReader, BarcodeGenerator, and System.Drawing (Aspose.Drawing) objects to generate, crop, and recognize barcodes within a specific region. Typical use cases include processing large images where only a portion contains a barcode, improving performance and accuracy. Developers often need to define target regions, handle image bounds, and extract barcode metadata from the cropped area.
// Prompt: Specify a rectangular target region before recognition to limit barcode detection to a defined area of the image.
// Tags: barcode, code128, recognition, region, rectangle, aspose.barcode, csharp, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, cropping a specific region, and recognizing the barcode only within that region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a Code128 barcode, defines a target rectangle, crops the image,
    /// and runs recognition on the cropped bitmap.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for demo files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original and cropped images
        string originalPath = Path.Combine(workFolder, "original.png");
        string croppedPath = Path.Combine(workFolder, "cropped.png");

        // Generate a sample Code128 barcode image and save it to disk
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(originalPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Define the rectangular region (x, y, width, height) to limit recognition
        var targetRegion = new Rectangle(50, 50, 200, 100);

        // Load the original image and crop it to the target region
        using (var originalBitmap = new Bitmap(originalPath))
        {
            // Ensure the target region does not exceed the image bounds
            Rectangle validRegion = Rectangle.Intersect(
                targetRegion,
                new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height));

            // Clone the valid region into a new bitmap
            using (var croppedBitmap = originalBitmap.Clone(validRegion, originalBitmap.PixelFormat))
            {
                // Save the cropped image (optional, useful for visual inspection)
                croppedBitmap.Save(croppedPath, Aspose.Drawing.Imaging.ImageFormat.Png);

                // Initialize the barcode reader for the cropped bitmap, looking for Code128 symbology
                using (var reader = new BarCodeReader(croppedBitmap, DecodeType.Code128))
                {
                    // Perform recognition on the cropped area
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output recognition results
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected in the specified region.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");

                            // Display the location and size of the detected barcode within the cropped image
                            var bounds = result.Region.Rectangle;
                            Console.WriteLine($"Barcode Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                            Console.WriteLine($"Barcode Angle: {result.Region.Angle}");
                        }
                    }
                }
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            File.Delete(originalPath);
            File.Delete(croppedPath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this demo
        }
    }
}