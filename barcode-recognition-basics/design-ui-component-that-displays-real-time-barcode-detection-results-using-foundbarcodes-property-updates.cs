// Title: Real‑time barcode detection demo
// Description: This console example generates a Code128 barcode, reads it, and displays detection results, illustrating how the FoundBarCodes property can be used for UI updates.
// Prompt: Design a UI component that displays real‑time barcode detection results using FoundBarCodes property updates.
// Tags: barcode symbology, generation, recognition, foundbarcodes, console

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and how to access detection results via the
/// <c>FoundBarCodes</c> property, which can be bound to a UI component for real‑time updates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates a barcode, reads it, and prints detection details.
    /// </summary>
    static void Main()
    {
        // NOTE: The original task mentions a UI component for real‑time updates.
        // In this console example we simulate the process by generating a barcode,
        // reading it, and printing the detection results immediately.

        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode image in memory.
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a reader that scans for all supported symbologies.
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.AllSupportedTypes))
                {
                    // Perform the recognition.
                    var results = reader.ReadBarCodes();

                    // The FoundBarCodes property holds the same results after reading.
                    // Display each detected barcode's details.
                    Console.WriteLine($"Detected {reader.FoundCount} barcode(s):");
                    int index = 0;
                    foreach (var result in results)
                    {
                        Console.WriteLine($"--- Barcode #{++index} ---");
                        Console.WriteLine($"Type       : {result.CodeTypeName}");
                        Console.WriteLine($"CodeText   : {result.CodeText}");
                        Console.WriteLine($"Confidence : {result.Confidence}");
                        Console.WriteLine($"Quality    : {result.ReadingQuality}");
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"Region     : X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                        Console.WriteLine($"Angle      : {result.Region.Angle}");
                    }

                    // Demonstrate accessing the FoundBarCodes array directly.
                    Console.WriteLine("\nAccessing FoundBarCodes property directly:");
                    for (int i = 0; i < reader.FoundCount; i++)
                    {
                        var fb = reader.FoundBarCodes[i];
                        Console.WriteLine($"FoundBarCodes[{i}] Type={fb.CodeTypeName}, Text={fb.CodeText}");
                    }
                }
            }
        }

        // Program ends here; no external input or infinite loops are used.
    }
}