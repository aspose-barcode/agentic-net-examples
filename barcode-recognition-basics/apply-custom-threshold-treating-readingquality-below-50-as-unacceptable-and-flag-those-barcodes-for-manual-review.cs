// Title: Barcode Generation, Reading, and Quality Evaluation with Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode, reading it, and evaluating its reading quality, flagging low-quality scans for manual review.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include inventory management, shipping labels, and point‑of‑sale systems where developers need to ensure barcode readability and may need to flag poor‑quality scans for manual handling. The example highlights key API classes and common quality‑assessment patterns useful for developers working with barcode solutions.
// Prompt: Apply a custom threshold treating ReadingQuality below 50 as unacceptable and flag those barcodes for manual review.
// Tags: barcode symbology, generation, recognition, quality assessment, code128, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode image, reading it, and evaluating its reading quality using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, reads it, and flags low‑quality results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample barcode image.
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created before attempting to read it.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found: " + barcodePath);
            return;
        }

        // Read the barcode from the image and evaluate its reading quality.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            bool anyFlagged = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Apply custom quality threshold: flag results with ReadingQuality below 50.
                if (result.ReadingQuality < 50)
                {
                    Console.WriteLine("Status: Unacceptable – flagged for manual review.");
                    anyFlagged = true;
                }
                else
                {
                    Console.WriteLine("Status: Acceptable.");
                }

                Console.WriteLine();
            }

            if (!anyFlagged)
            {
                Console.WriteLine("All decoded barcodes meet the quality threshold.");
            }
        }

        // Clean up temporary files and directory.
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}