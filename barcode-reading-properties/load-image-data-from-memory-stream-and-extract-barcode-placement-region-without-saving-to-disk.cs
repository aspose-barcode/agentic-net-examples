// Title: In-Memory Barcode Generation and Region Extraction
// Description: Demonstrates generating a Code128 barcode, loading it from a memory stream, and retrieving the barcode's placement region without writing to disk.
// Prompt: Load image data from a memory stream and extract barcode placement region without saving to disk.
// Tags: barcode, code128, in-memory, region extraction, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode in memory, reads it, and outputs its location.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it from a memory stream, and prints barcode details and region.
    /// </summary>
    static void Main()
    {
        // Generate a sample Code128 barcode and write it to a memory stream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position before reading.
                memoryStream.Position = 0;

                // Create a barcode reader and load the image from the memory stream.
                using (var reader = new BarCodeReader())
                {
                    // Assign the image stream to the reader.
                    reader.SetBarCodeImage(memoryStream);

                    // Detect all supported barcode types.
                    reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                    // Iterate through detected barcodes.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Extract the bounding rectangle of the barcode.
                        var rect = result.Region.Rectangle;

                        // Output barcode type, text, and region coordinates.
                        Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Barcode Text: {result.CodeText}");
                        Console.WriteLine($"Region - X: {rect.X}, Y: {rect.Y}, Width: {rect.Width}, Height: {rect.Height}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}