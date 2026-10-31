// Title: Barcode resolution comparison between 200 DPI and 300 DPI
// Description: Demonstrates how to set barcode image resolution using Aspose.BarCode, generate PNG images at 200 DPI and 300 DPI, and compare their file sizes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to control image resolution for printing quality, storage optimization, or compliance with scanning equipment. The snippet illustrates typical steps: configuring resolution, saving to PNG, and evaluating file size differences.
// Prompt: Set barcode resolution to 200 DPI, generate image, and compare file size against 300 DPI version.
// Tags: barcode, resolution, dpi, image generation, aspose.barcode, code128, png, file size comparison

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that generates Code128 barcodes at different resolutions
/// and compares the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary files, generates barcodes at 200 DPI and 300 DPI,
    /// and outputs a size comparison to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeResolutionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the 200 DPI and 300 DPI images
        string path200 = Path.Combine(tempFolder, "barcode_200dpi.png");
        string path300 = Path.Combine(tempFolder, "barcode_300dpi.png");

        // Barcode content to encode
        const string codeText = "1234567890";

        // Generate a 200 DPI barcode image
        using (var generator200 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator200.Parameters.Resolution = 200f; // Set resolution to 200 DPI
            generator200.Save(path200, BarCodeImageFormat.Png); // Save as PNG
        }

        // Generate a 300 DPI barcode image
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f; // Set resolution to 300 DPI
            generator300.Save(path300, BarCodeImageFormat.Png); // Save as PNG
        }

        // Retrieve file sizes for both images
        long size200 = new FileInfo(path200).Length;
        long size300 = new FileInfo(path300).Length;

        // Output the sizes to the console
        Console.WriteLine($"200 DPI image size: {size200} bytes");
        Console.WriteLine($"300 DPI image size: {size300} bytes");

        // Compare and report which image is larger
        if (size200 < size300)
            Console.WriteLine("200 DPI image is smaller than 300 DPI image.");
        else if (size200 > size300)
            Console.WriteLine("200 DPI image is larger than 300 DPI image.");
        else
            Console.WriteLine("Both images have the same file size.");

        // Optional cleanup: uncomment to delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}