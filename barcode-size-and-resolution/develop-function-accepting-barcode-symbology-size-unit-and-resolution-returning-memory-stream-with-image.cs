// Title: Generate barcode image as PNG in memory stream
// Description: Demonstrates creating a barcode with a specified symbology, size unit, and resolution, returning the image as a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and image parameter settings to produce barcode graphics. Developers often need to generate barcodes on the fly for reports, PDFs, or web responses, and this snippet shows the typical workflow for configuring size, resolution, and output format.
// Prompt: Develop function accepting barcode symbology, size unit, and resolution, returning memory stream with image.
// Tags: barcode, symbology, image generation, memory stream, aspose.barcode, png, resolution, size unit

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of generating a barcode image in memory using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Demonstrates calling <see cref="GenerateBarcode"/> and reports the resulting stream size.
    /// </summary>
    static void Main()
    {
        // Example usage of the GenerateBarcode function
        using (MemoryStream stream = GenerateBarcode("Code128", "Point", 300f))
        {
            // Output the size of the generated PNG image (in bytes)
            Console.WriteLine($"Generated barcode image size: {stream.Length} bytes");

            // The stream contains a PNG image; you could write it to a file for verification:
            // File.WriteAllBytes("barcode.png", stream.ToArray());
        }
    }

    /// <summary>
    /// Generates a barcode image and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128", "QR").</param>
    /// <param name="sizeUnit">Unit for image dimensions: "Point", "Pixels", or "Millimeters".</param>
    /// <param name="resolution">Resolution (dpi) for the generated image.</param>
    /// <returns>MemoryStream containing the PNG image.</returns>
    static MemoryStream GenerateBarcode(string symbologyName, string sizeUnit, float resolution)
    {
        // Resolve the symbology name to a BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the barcode generator with the resolved symbology
        var generator = new BarcodeGenerator(encodeType);
        generator.CodeText = "Sample123";

        // Set the desired image resolution (dpi)
        generator.Parameters.Resolution = resolution;

        // Configure image size using the specified unit (example dimensions: 200 x 100)
        switch (sizeUnit?.Trim().ToLowerInvariant())
        {
            case "point":
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 100f;
                break;
            case "pixel":
            case "pixels":
                generator.Parameters.ImageWidth.Pixels = 200f;
                generator.Parameters.ImageHeight.Pixels = 100f;
                break;
            case "millimeter":
            case "millimeters":
                generator.Parameters.ImageWidth.Millimeters = 50f; // approx 200 points
                generator.Parameters.ImageHeight.Millimeters = 25f; // approx 100 points
                break;
            default:
                throw new ArgumentException($"Unsupported size unit: {sizeUnit}");
        }

        // Disable automatic sizing so the explicit dimensions are used
        generator.Parameters.AutoSizeMode = AutoSizeMode.None;

        // Generate the barcode image and save it to a memory stream in PNG format
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0; // Reset stream position for downstream consumers
            return ms;
        }
    }
}