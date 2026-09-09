// Title: Generate Han Xin Barcode with Default Square Module Size
// Description: Demonstrates creating a Han Xin barcode for alphanumeric data using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.HanXin. Typical use cases include generating machine-readable barcodes for inventory, tracking, or authentication purposes. Developers often need to create barcodes, customize their appearance, and export them to common image formats such as PNG.
// Prompt: Generate a Han Xin barcode with default square module size for alphanumeric input.
// Tags: hanxin, barcode, generation, png, aspose.barcode, encode types, bitmap, image saving

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Han Xin barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinDemo");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Full path for the PNG file that will contain the barcode.
        string outputPath = Path.Combine(outputDir, "hanxin.png");

        // Initialize the barcode generator with Han Xin symbology and the alphanumeric data.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "ABC123"))
        {
            // Generate the barcode image as a Bitmap.
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to the specified path in PNG format.
                image.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}