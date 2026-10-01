// Title: Load BMP image with metadata and compare barcode recognition speed
// Description: Demonstrates generating a Code128 barcode, embedding dummy metadata into a BMP file, and measuring whether the metadata impacts barcode recognition performance.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to work with bitmap images, embed EXIF metadata using Aspose.Drawing, and evaluate recognition speed with BarCodeReader. Developers often need to handle image metadata while ensuring fast barcode scanning in applications such as inventory systems, document processing, and mobile capture.
// Prompt: Load BMP images with embedded metadata and verify that metadata does not affect recognition speed.
// Tags: code128, barcode, metadata, bmp, recognition, speed, aspose.barcode, aspose.drawing, imageprocessing

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates loading BMP images with embedded metadata and measuring barcode recognition speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, adds dummy metadata, measures recognition times, and outputs the comparison.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for test images
        string workDir = Path.Combine(Path.GetTempPath(), "BmpMetaTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the image with metadata and the stripped version
        string originalPath = Path.Combine(workDir, "barcode_with_meta.bmp");
        string strippedPath = Path.Combine(workDir, "barcode_without_meta.bmp");

        // Generate a simple Code128 barcode and save it as BMP
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional visual tuning
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(originalPath, BarCodeImageFormat.Bmp);
        }

        // Attempt to embed dummy metadata into the BMP using Aspose.Drawing
        // (adds a comment EXIF property if supported)
        try
        {
            using (var bmp = (Bitmap)Image.FromFile(originalPath))
            {
                const int PropertyTagComment = 0x9286; // EXIF comment tag
                byte[] commentBytes = System.Text.Encoding.UTF8.GetBytes("Dummy metadata");

                // Ensure even length as required by the format
                if (commentBytes.Length % 2 != 0)
                {
                    Array.Resize(ref commentBytes, commentBytes.Length + 1);
                }

                // Create a PropertyItem via reflection (constructor is internal)
                var propItem = (PropertyItem)Activator.CreateInstance(typeof(PropertyItem), true);
                propItem.Id = PropertyTagComment;
                propItem.Type = 2; // ASCII
                propItem.Len = commentBytes.Length;
                propItem.Value = commentBytes;

                bmp.SetPropertyItem(propItem);
                bmp.Save(strippedPath, ImageFormat.Bmp);
            }
        }
        catch (Exception ex)
        {
            // Fallback: copy the original image if metadata embedding fails
            Console.WriteLine("Metadata embedding failed: " + ex.Message);
            File.Copy(originalPath, strippedPath, true);
        }

        // Verify that both test images were created successfully
        if (!File.Exists(originalPath) || !File.Exists(strippedPath))
        {
            Console.WriteLine("Failed to create test images.");
            return;
        }

        // Measure recognition speed for the image containing metadata
        long timeWithMeta = MeasureRecognitionTime(originalPath);

        // Measure recognition speed for the image without metadata
        long timeWithoutMeta = MeasureRecognitionTime(strippedPath);

        // Output the timing results
        Console.WriteLine($"Recognition time (with metadata): {timeWithMeta} ms");
        Console.WriteLine($"Recognition time (without metadata): {timeWithoutMeta} ms");
        Console.WriteLine("Metadata does not significantly affect recognition speed if the times are comparable.");

        // Clean up temporary files and directory
        try
        {
            File.Delete(originalPath);
            File.Delete(strippedPath);
            Directory.Delete(workDir);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }

    /// <summary>
    /// Measures the time taken to recognize barcodes in the specified image file.
    /// </summary>
    /// <param name="imagePath">Path to the image containing a barcode.</param>
    /// <returns>Elapsed time in milliseconds, or -1 if the file does not exist.</returns>
    static long MeasureRecognitionTime(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return -1;
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Use BarCodeReader to decode all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();

            // Output decoded text to verify successful recognition
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded text: {result.CodeText}");
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}