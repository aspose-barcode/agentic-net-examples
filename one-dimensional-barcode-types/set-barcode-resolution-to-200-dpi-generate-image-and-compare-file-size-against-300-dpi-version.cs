// Title: Compare barcode image file sizes at different DPI settings
// Description: Generates a Code128 barcode image at 200 DPI and 300 DPI, saves them as PNG files, and reports their file sizes to illustrate the impact of resolution on output size.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, demonstrating how to configure the resolution parameter of BarcodeGenerator, save images in PNG format, and perform basic file‑system validation. Developers working with barcode rendering often need to adjust DPI for print quality or file‑size optimization, using classes such as BarcodeGenerator, BarCodeImageFormat, and the Parameters property.
// Prompt: Set barcode resolution to 200 DPI, generate image, and compare file size against 300 DPI version.
// Tags: barcode, code128, resolution, dpi, image generation, file size comparison, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate barcode images at different DPI settings
/// and compare their resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two PNG barcode images
    /// (200 DPI and 300 DPI) and prints their file sizes for comparison.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and the output file names.
        string codeText = "123456";
        string file200 = "barcode_200dpi.png";
        string file300 = "barcode_300dpi.png";

        // ------------------------------------------------------------
        // Generate a barcode image with a resolution of 200 DPI.
        // ------------------------------------------------------------
        using (var generator200 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the resolution (dots per inch) for the image.
            generator200.Parameters.Resolution = 200f;
            // Save the generated barcode as a PNG file.
            generator200.Save(file200, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Generate a barcode image with a resolution of 300 DPI.
        // ------------------------------------------------------------
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f;
            generator300.Save(file300, BarCodeImageFormat.Png);
        }

        // Verify that both image files were successfully created.
        if (!File.Exists(file200) || !File.Exists(file300))
        {
            Console.WriteLine("Failed to create one or both barcode images.");
            return;
        }

        // Retrieve the file sizes (in bytes) for each image.
        long size200 = new FileInfo(file200).Length;
        long size300 = new FileInfo(file300).Length;

        // Output the file sizes to the console.
        Console.WriteLine($"200 DPI file size: {size200} bytes");
        Console.WriteLine($"300 DPI file size: {size300} bytes");

        // Compare the sizes and report which image is smaller.
        if (size200 < size300)
        {
            Console.WriteLine("The 200 DPI image is smaller than the 300 DPI image.");
        }
        else if (size200 > size300)
        {
            Console.WriteLine("The 300 DPI image is smaller than the 200 DPI image.");
        }
        else
        {
            Console.WriteLine("Both images have the same file size.");
        }
    }
}