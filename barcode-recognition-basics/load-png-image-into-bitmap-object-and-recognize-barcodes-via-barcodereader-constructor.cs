// Title: Barcode generation and recognition from a PNG bitmap
// Description: Demonstrates creating a Code128 barcode, loading it into a Bitmap, and recognizing it using BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related classes to produce a barcode image in memory, load it via Aspose.Drawing.Bitmap, and decode it. Developers often need to process barcode images without writing to disk, making in‑memory operations essential for web services and automated pipelines.
// Prompt: Load a PNG image into a Bitmap object and recognize barcodes via BarCodeReader constructor.
// Tags: code128, barcode generation, barcode recognition, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, loads it into a Bitmap,
/// and reads the barcode using Aspose.BarCode's BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, creates a bitmap from it,
    /// and prints the detected barcode type and text to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode content.
        string codeText = "1234567890";

        // Generate the barcode image in memory using a PNG format.
        using (MemoryStream ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.
            }

            // Load the generated PNG image into an Aspose.Drawing.Bitmap.
            using (Bitmap bitmap = new Bitmap(ms))
            {
                // Initialize the BarCodeReader to decode Code128 barcodes from the bitmap.
                using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                {
                    // Iterate through all detected barcodes and output their details.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}