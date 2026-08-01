// Title: Real‑time barcode detection demo using FoundBarCodes
// Description: Generates sample barcodes, recognizes them, and displays detection details such as type, text, confidence, and region.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition workflow, focusing on the BarCodeReader class and its FoundBarCodes collection. This example shows how to create barcodes with BarcodeGenerator, decode them with BarCodeReader, and retrieve detailed detection results—common tasks for developers building scanning or verification features. Suitable for search queries about Aspose.BarCode recognition examples.
// Prompt: Design a UI component that displays real‑time barcode detection results using FoundBarCodes property updates.
// Tags: barcode generation, barcode recognition, foundbarcodes, real-time detection, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating various barcode types, recognizing them, and outputting detailed detection results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates sample barcodes, reads them, and prints detection information.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample barcodes with their symbology and data.
        var samples = new List<(BaseEncodeType EncodeType, string CodeText)>
        {
            (EncodeTypes.Code128, "ABC123456"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text")
        };

        // Process each sample: generate, recognize, and display results.
        foreach (var sample in samples)
        {
            // Generate a barcode image and store it in a memory stream.
            using (var generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                // Use default generation settings; customize here if needed.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Initialize a barcode reader to recognize the generated image.
                    using (var reader = new BarCodeReader())
                    {
                        // Configure the reader to detect all supported symbologies.
                        reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                        reader.SetBarCodeImage(ms);

                        // Perform the recognition operation.
                        reader.ReadBarCodes();

                        // Output detection summary for the current sample.
                        Console.WriteLine($"--- Results for {sample.EncodeType.TypeName} ---");
                        Console.WriteLine($"FoundCount: {reader.FoundCount}");

                        // Iterate through each detected barcode and display its details.
                        foreach (var result in reader.FoundBarCodes)
                        {
                            Console.WriteLine($"Type: {result.CodeTypeName}");
                            Console.WriteLine($"Text: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        // Indicate that the demo has finished executing.
        Console.WriteLine("Barcode detection demo completed.");
    }
}