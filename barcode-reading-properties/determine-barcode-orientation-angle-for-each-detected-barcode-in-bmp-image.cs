// Title: Determine barcode orientation angle in a BMP image
// Description: Loads a BMP image, detects all barcodes, and prints each barcode's orientation angle in degrees.
// Category-Description: This example demonstrates Aspose.BarCode barcode recognition capabilities, focusing on extracting orientation information from detected barcodes. It uses the BarCodeReader, BarCodeResult, and QualitySettings classes to read barcodes from an image, a common requirement for image preprocessing, automated sorting, and quality inspection scenarios. Developers working with barcode detection and analysis often need to know the rotation angle to correctly align or further process the scanned data.
// Prompt: Determine barcode orientation angle for each detected barcode in a BMP image.
// Tags: barcode orientation, detection, bmp, aspose.barcode, csharp, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that detects barcodes in a BMP image and reports their orientation angles.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional BMP file path; otherwise generates a rotated barcode sample,
    /// reads all barcodes, and outputs each barcode's text, symbology, and orientation angle.
    /// </summary>
    /// <param name="args">Command‑line arguments. First argument may be a path to a BMP image.</param>
    static void Main(string[] args)
    {
        // Determine input BMP path: use argument if provided, otherwise generate a sample rotated barcode.
        string bmpPath;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            bmpPath = args[0];
        }
        else
        {
            // Create a temporary folder for the sample image.
            string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeOrientationSample_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);
            bmpPath = Path.Combine(tempFolder, "rotated_barcode.bmp");

            // Generate a simple Code128 barcode.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save barcode to a memory stream as PNG.
                using (var pngStream = new MemoryStream())
                {
                    generator.Save(pngStream, BarCodeImageFormat.Png);
                    pngStream.Position = 0;

                    // Load PNG into Aspose.Drawing.Bitmap.
                    using (var bitmap = new Bitmap(pngStream))
                    {
                        // Rotate the bitmap by 90 degrees.
                        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

                        // Save the rotated image as BMP.
                        bitmap.Save(bmpPath, ImageFormat.Bmp);
                    }
                }
            }

            Console.WriteLine($"Generated rotated barcode image at: {bmpPath}");
        }

        // Verify the BMP file exists before attempting to read.
        if (!File.Exists(bmpPath))
        {
            Console.WriteLine($"Error: File not found - {bmpPath}");
            return;
        }

        // Read barcodes from the BMP image.
        using (var reader = new BarCodeReader(bmpPath, DecodeType.AllSupportedTypes))
        {
            // Optional: set high performance quality settings.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform barcode detection.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate through each detected barcode and display its details.
                for (int i = 0; i < results.Length; i++)
                {
                    BarCodeResult result = results[i];
                    double angle = result.Region.Angle; // Orientation angle in degrees.

                    Console.WriteLine($"Barcode {i + 1}:");
                    Console.WriteLine($"  Code Text : {result.CodeText}");
                    Console.WriteLine($"  Symbology : {result.CodeTypeName}");
                    Console.WriteLine($"  Angle     : {angle} degrees");
                }
            }
        }

        // Cleanup generated temporary files if we created them.
        if (args.Length == 0 && Directory.Exists(Path.GetDirectoryName(bmpPath)))
        {
            try
            {
                File.Delete(bmpPath);
                Directory.Delete(Path.GetDirectoryName(bmpPath));
            }
            catch
            {
                // Ignore cleanup errors.
            }
        }
    }
}