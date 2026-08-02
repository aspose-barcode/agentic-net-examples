// Title: Load BMP barcode image with metadata and compare recognition speed
// Description: Demonstrates loading a BMP barcode image, adding EXIF metadata, and measuring whether the metadata impacts barcode recognition performance.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to generate a barcode, embed image metadata using Aspose.Drawing, and evaluate recognition speed with BarCodeReader. Developers working with barcode scanning in image files often need to ensure that added metadata (e.g., EXIF) does not degrade detection performance. The key API classes used are BarcodeGenerator, BarCodeReader, Image, and PropertyItem.
// Prompt: Load BMP images with embedded metadata and verify that metadata does not affect recognition speed.
// Tags: code128, bmp, metadata, performance, generation, recognition, aspose.barcode, aspose.drawing

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates loading BMP images with embedded metadata and verifying that metadata does not affect recognition speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, adds metadata, and measures recognition times.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define file paths for the plain and metadata‑enhanced images
        string plainPath = Path.Combine(outputDir, "barcode_plain.bmp");
        string metaPath = Path.Combine(outputDir, "barcode_meta.bmp");

        // Generate a simple Code128 barcode and save it as a BMP file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(plainPath);
        }

        // Measure recognition time for the plain BMP image
        double plainTime = MeasureRecognitionTime(plainPath);
        Console.WriteLine($"Recognition time for plain BMP: {plainTime:F2} ms");

        // Embed a text metadata property into the BMP image and save as a new file
        AddMetadataToBmp(plainPath, metaPath);

        // Measure recognition time for the BMP image that contains metadata
        double metaTime = MeasureRecognitionTime(metaPath);
        Console.WriteLine($"Recognition time for BMP with metadata: {metaTime:F2} ms");

        // Simple verification output comparing the two timings
        if (Math.Abs(metaTime - plainTime) < 5.0)
        {
            Console.WriteLine("Metadata does not significantly affect recognition speed.");
        }
        else
        {
            Console.WriteLine("Metadata appears to affect recognition speed.");
        }
    }

    /// <summary>
    /// Measures the time (in milliseconds) required to recognize barcodes in the specified image file.
    /// </summary>
    /// <param name="imagePath">Path to the image file to be processed.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static double MeasureRecognitionTime(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return 0.0;
        }

        var stopwatch = new Stopwatch();

        // Initialize the barcode reader for all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Use normal quality preset for consistent measurement
            reader.QualitySettings = QualitySettings.NormalQuality;

            stopwatch.Start();
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Output detected barcodes (if any) for verification
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: Type={result.CodeTypeName}, Text={result.CodeText}");
            }
        }

        return stopwatch.Elapsed.TotalMilliseconds;
    }

    /// <summary>
    /// Adds a simple text metadata property to a BMP image and saves it to a new file.
    /// </summary>
    /// <param name="sourcePath">Path to the original BMP image.</param>
    /// <param name="destinationPath">Path where the metadata‑enhanced BMP will be saved.</param>
    static void AddMetadataToBmp(string sourcePath, string destinationPath)
    {
        using (var image = Image.FromFile(sourcePath))
        {
            // Create an uninitialized PropertyItem (required for setting custom properties)
            var propItem = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
            propItem.Id = 0x010E; // Image Description tag
            propItem.Type = 2;    // ASCII string type

            // Prepare the description string (null‑terminated as required by BMP metadata)
            string description = "Sample metadata for testing";
            byte[] valueBytes = Encoding.ASCII.GetBytes(description + '\0');
            propItem.Value = valueBytes;
            propItem.Len = valueBytes.Length;

            // Assign the property to the image
            image.SetPropertyItem(propItem);

            // Save the image with the new metadata
            image.Save(destinationPath);
        }
    }
}