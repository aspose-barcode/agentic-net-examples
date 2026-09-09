// Title: Recognize Barcode from PNG Image Using Aspose.BarCodeReader
// Description: Demonstrates loading a PNG file into a Bitmap and using BarCodeReader to detect barcodes. Shows how to generate a sample barcode, read it, and output the results.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to work with image-based barcode detection using the BarCodeReader class. It covers generating a barcode image, loading it via Aspose.Drawing.Bitmap, and decoding all supported symbologies. Developers often need to process scanned images or files to extract barcode data in automation, inventory, or document processing scenarios.
// Prompt: Load a PNG image into a Bitmap object and recognize barcodes via BarCodeReader constructor.
// Tags: barcode recognition, png, bitmap, aspose.barcode, decode, alltypes, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as PNG, loading it into a Bitmap,
/// and recognizing it using Aspose.BarCode's BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, image loading, recognition,
    /// and cleanup of temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode and save it as a PNG file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the PNG image into a Bitmap and use BarCodeReader to detect barcodes
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output detection results
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
        }

        // Attempt to clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}