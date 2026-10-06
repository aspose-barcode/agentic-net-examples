// Title: Compare barcode image file sizes at different DPI settings
// Description: Demonstrates how to set barcode resolution to 200 DPI and 300 DPI, generate PNG images, and compare their file sizes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to control image resolution for printing or display quality, and comparing file sizes helps assess storage impact. The snippet shows typical steps: configure resolution, save images, and evaluate output size.
// Prompt: Set barcode resolution to 200 DPI, generate image, and compare file size against 300 DPI version.
// Tags: barcode, resolution, dpi, image generation, file size comparison, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes at different resolutions and comparing the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary output folder, generates 200 DPI and 300 DPI barcode PNGs, and prints size comparison.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeResolutionDemo");
        Directory.CreateDirectory(outputDir);

        // Barcode data to encode
        string codeText = "1234567890";

        // Paths for the two output files
        string file200 = Path.Combine(outputDir, "barcode_200dpi.png");
        string file300 = Path.Combine(outputDir, "barcode_300dpi.png");

        // Generate a 200 DPI barcode image
        using (var generator200 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator200.Parameters.Resolution = 200f; // Set resolution to 200 DPI
            generator200.Save(file200, BarCodeImageFormat.Png); // Save as PNG
        }

        // Generate a 300 DPI barcode image
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f; // Set resolution to 300 DPI
            generator300.Save(file300, BarCodeImageFormat.Png); // Save as PNG
        }

        // Retrieve file sizes for comparison
        long size200 = new FileInfo(file200).Length;
        long size300 = new FileInfo(file300).Length;

        // Output the sizes to the console
        Console.WriteLine($"200 DPI file size: {size200} bytes");
        Console.WriteLine($"300 DPI file size: {size300} bytes");

        // Compare and report which image is larger
        if (size200 < size300)
            Console.WriteLine("200 DPI image is smaller than 300 DPI image.");
        else if (size200 > size300)
            Console.WriteLine("200 DPI image is larger than 300 DPI image.");
        else
            Console.WriteLine("Both images have the same file size.");
    }
}