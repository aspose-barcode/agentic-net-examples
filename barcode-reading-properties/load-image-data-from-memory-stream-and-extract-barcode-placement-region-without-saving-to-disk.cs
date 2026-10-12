// Title: Generate barcode in memory and read its placement region
// Description: Demonstrates generating a Code128 barcode, storing it in a memory stream, and using BarCodeReader to extract the barcode's location information without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them directly from streams. Developers often need to process barcodes in-memory for web services, document workflows, or automated testing, requiring access to placement data such as quadrangle, angle, and rectangle. The key API classes include BarcodeGenerator, BarCodeReader, BarCodeResult, and the Region property.
// Prompt: Load image data from a memory stream and extract barcode placement region without saving to disk.
// Tags: barcode, generation, recognition, memorystream, placement, region, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, reads it from a memory stream,
/// and outputs detailed placement region information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, reads it from memory,
    /// and prints barcode type, text, and region data to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode text to encode.
        const string codeText = "1234567890";

        // Create a BarcodeGenerator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Prepare an in‑memory stream to hold the generated image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset stream position to the beginning for reading.
                memoryStream.Position = 0;

                // Initialize a BarCodeReader to decode all supported barcode types from the stream.
                using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                {
                    // Read all detected barcodes.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Iterate through each detection result.
                    foreach (BarCodeResult result in results)
                    {
                        // Output basic barcode information.
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");

                        // Output region details: quadrangle, angle, and bounding rectangle.
                        Console.WriteLine($"Quadrangle: {result.Region.Quadrangle}");
                        Console.WriteLine($"Angle: {result.Region.Angle}");
                        Console.WriteLine($"Rectangle: {result.Region.Rectangle}");

                        // Output each corner point of the barcode region.
                        var points = result.Region.Points;
                        for (int i = 0; i < points.Length; i++)
                        {
                            Console.WriteLine($"Point {i}: {points[i]}");
                        }
                    }
                }
            }
        }
    }
}