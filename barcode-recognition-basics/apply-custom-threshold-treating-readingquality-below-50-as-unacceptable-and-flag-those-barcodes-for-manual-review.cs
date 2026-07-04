// Title: Barcode Generation, Recognition, and Quality Threshold Evaluation
// Description: Generates a Code128 barcode, saves it as PNG, reads it back, and flags barcodes with reading quality below 50% for manual review.
// Prompt: Apply a custom threshold treating ReadingQuality below 50 as unacceptable and flag those barcodes for manual review.
// Tags: barcode symbology, generation, recognition, quality threshold, manual review, aspnet barcoderecognition, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a barcode, saving it as an image, reading it back,
/// and applying a custom quality threshold to flag low‑quality scans for manual review.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, verification, recognition,
    /// and quality assessment logic.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define the output path for the generated barcode image.
        // --------------------------------------------------------------------
        const string imagePath = "sample_barcode.png";

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode and persist it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode image in PNG format.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was successfully created.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Initialize a reader to decode all supported barcode types from the image.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition operation.
            var results = reader.ReadBarCodes();

            // Handle the case where no barcodes were detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected in the image.");
                return;
            }

            // ----------------------------------------------------------------
            // Iterate through each detected barcode and evaluate its reading quality.
            // ----------------------------------------------------------------
            int count = 0;
            foreach (var result in results)
            {
                count++;
                double quality = result.ReadingQuality; // ReadingQuality is a double representing a percentage.

                // Apply custom threshold: treat quality below 50% as unacceptable.
                bool isUnacceptable = quality < 50.0;
                string status = isUnacceptable
                    ? "UNACCEPTABLE – Flagged for manual review"
                    : "Acceptable";

                // Output detailed information for each barcode.
                Console.WriteLine($"Barcode #{count}:");
                Console.WriteLine($"  CodeText       : {result.CodeText}");
                Console.WriteLine($"  ReadingQuality : {quality:F2}%");
                Console.WriteLine($"  Status         : {status}");
                Console.WriteLine();
            }
        }
    }
}