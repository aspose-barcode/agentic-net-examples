// Title: Barcode detection from in-memory image stream
// Description: Demonstrates generating a Code128 barcode, saving it to a memory stream, and detecting it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related classes to create and read barcodes in memory. Typical use cases include processing uploaded images in web APIs, validating scanned codes, and batch processing. Developers often need to configure quality settings and handle multiple symbologies efficiently.
// Prompt: Integrate barcode detection into a web API endpoint that accepts uploaded image streams for instant processing.
// Tags: barcode detection, code128, in-memory, aspnet, aspose.barcode, generation, recognition, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, stores it in a memory stream,
/// and then reads/detects the barcode using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample barcode, writes it to a
    /// memory stream, and uses <see cref="BarCodeReader"/> to detect and display
    /// information about the barcode(s) found.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Prepare a memory stream to hold the generated PNG image.
            using (var imageStream = new MemoryStream())
            {
                // Save the barcode image into the stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);

                // Reset the stream position so it can be read from the beginning.
                imageStream.Position = 0;

                // Initialize a BarCodeReader to detect any supported barcode types.
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Set high-quality detection settings (optional but improves accuracy).
                    reader.QualitySettings = QualitySettings.HighQuality;

                    // Perform the detection and retrieve all results.
                    var results = reader.ReadBarCodes();

                    // Limit processing to a maximum of 5 detected barcodes.
                    int maxToProcess = 5;
                    int count = 0;

                    foreach (var result in results)
                    {
                        if (count >= maxToProcess)
                            break;

                        // Output details of each detected barcode.
                        Console.WriteLine($"Detected Barcode {count + 1}:");
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                        Console.WriteLine();

                        count++;
                    }

                    // Inform the user if no barcodes were found.
                    if (count == 0)
                    {
                        Console.WriteLine("No barcodes were detected in the provided image.");
                    }
                }
            }
        }
    }
}