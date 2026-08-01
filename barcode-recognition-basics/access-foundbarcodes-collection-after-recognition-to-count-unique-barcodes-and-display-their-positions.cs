// Title: Count Unique Barcodes and Display Their Positions
// Description: Generates a Code128 barcode image, recognizes all barcodes in the image, counts unique entries, and prints each barcode's text with its location.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes and BarCodeReader to detect them, covering typical use cases such as inventory scanning, document processing, and quality control where developers need to extract barcode data and spatial information from images.
// Prompt: Access FoundBarCodes collection after recognition to count unique barcodes and display their positions.
// Tags: code128, barcode recognition, console output, barcodelibrary, barcodelgeneration, barcoderecognition

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, recognition, unique count, and position reporting using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it, counts unique codes, and prints their positions.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image.
        string imagePath = "sample_barcode.png";

        // Generate a Code128 barcode image with the text "ABC123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader to detect all supported barcode types in the image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition process.
            reader.ReadBarCodes();

            // Retrieve the collection of detected barcodes.
            var foundBarCodes = reader.FoundBarCodes;
            int totalDetected = foundBarCodes?.Length ?? 0;
            Console.WriteLine($"Total barcodes detected: {totalDetected}");

            // Exit early if no barcodes were found.
            if (totalDetected == 0)
            {
                return;
            }

            // Determine the number of unique barcodes based on their CodeText values.
            var uniqueBarCodes = foundBarCodes
                .GroupBy(r => r.CodeText)
                .Select(g => g.First())
                .ToArray();

            Console.WriteLine($"Unique barcodes count: {uniqueBarCodes.Length}");

            // Iterate through all detected barcodes and display their text and bounding rectangle.
            foreach (var result in foundBarCodes)
            {
                var rect = result.Region.Rectangle;
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Position - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                Console.WriteLine();
            }
        }

        // Clean up the temporary image file.
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}