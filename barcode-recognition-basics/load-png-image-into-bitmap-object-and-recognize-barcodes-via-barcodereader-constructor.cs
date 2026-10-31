// Title: Load PNG into Bitmap and Recognize Barcodes with BarCodeReader
// Description: Demonstrates loading a generated PNG barcode image into a Bitmap object and using Aspose.BarCode's BarCodeReader to detect and read all supported barcode types.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image, Aspose.Drawing.Bitmap to load the image, and BarCodeReader to recognize barcodes. Typical scenarios include scanning saved images, processing scanned documents, and integrating barcode reading into automated workflows. Developers often need to combine these APIs to handle image-based barcode detection efficiently.
// Prompt: Load a PNG image into a Bitmap object and recognize barcodes via BarCodeReader constructor.
// Tags: code128, barcode recognition, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a Code128 barcode, saves it as PNG,
/// loads the image into a Bitmap, and reads the barcode using BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image file exists
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the PNG image into a Bitmap and recognize barcodes
        using (Bitmap bitmap = new Bitmap(barcodePath))
        {
            // Initialize BarCodeReader to scan all supported barcode types
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the recognition results
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

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}