// Title: Barcode Generation, Recognition, and Position Reporting
// Description: Generates a Code128 barcode image, reads it back, counts unique barcodes, and prints their positions.
// Prompt: Access FoundBarCodes collection after recognition to count unique barcodes and display their positions.
// Tags: code128, barcode generation, barcode recognition, unique count, position output, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a barcode, recognize it, count unique values,
/// and display the position of each detected barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it,
    /// and outputs detection details to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Step 1: Generate a sample barcode image (Code128, text "ABC123")
        // ------------------------------------------------------------
        string imagePath = "sample_barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Step 2: Verify that the image file was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Read barcodes from the generated image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform recognition of all supported barcode types
            reader.ReadBarCodes();

            // --------------------------------------------------------
            // Step 4: Access the FoundBarCodes collection
            // --------------------------------------------------------
            BarCodeResult[] results = reader.FoundBarCodes;
            int totalCount = results.Length;
            Console.WriteLine($"Total barcodes detected: {totalCount}");

            // --------------------------------------------------------
            // Step 5: Count unique barcodes based on their CodeText
            // --------------------------------------------------------
            var uniqueSet = new HashSet<string>();
            foreach (var result in results)
            {
                uniqueSet.Add(result.CodeText);
            }
            Console.WriteLine($"Unique barcodes count: {uniqueSet.Count}");

            // --------------------------------------------------------
            // Step 6: Display positions (region) of each detected barcode
            // --------------------------------------------------------
            for (int i = 0; i < totalCount; i++)
            {
                var result = results[i];
                var rect = result.Region.Rectangle;
                Console.WriteLine(
                    $"Barcode {i + 1}: Text = '{result.CodeText}', Position = (X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height})");
            }
        }
    }
}