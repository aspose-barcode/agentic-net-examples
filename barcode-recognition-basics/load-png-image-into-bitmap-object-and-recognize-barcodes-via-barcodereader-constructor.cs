// Title: Load PNG into Bitmap and recognize Code128 barcode using BarCodeReader
// Description: Demonstrates loading a generated PNG barcode image into a Bitmap object and using Aspose.BarCode's BarCodeReader to detect and read a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to work with image files, Bitmap objects, and the BarCodeReader API. It illustrates typical use cases such as scanning saved barcode images, extracting encoded data, and handling temporary files. Developers often need to integrate barcode reading into image processing pipelines, and this snippet provides a concise reference.
// Prompt: Load a PNG image into a Bitmap object and recognize barcodes via BarCodeReader constructor.
// Tags: code128, barcode recognition, png, bitmap, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as PNG, loading it into a Bitmap,
/// and recognizing the barcode using Aspose.BarCode's BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary PNG barcode image,
    /// reads it with BarCodeReader, outputs the result, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample PNG image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the PNG image into a Bitmap and initialize the BarCodeReader for Code128 decoding
        using (var bitmap = new Bitmap(imagePath))
        using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
        {
            // Iterate through all detected barcodes and display their type and text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}