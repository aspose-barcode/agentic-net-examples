// Title: Batch convert AI strings to GS1 DataMatrix PNG files using parallel processing
// Description: Demonstrates how to encode a list of GS1 Application Identifier strings into DataMatrix barcodes and save them as PNG images in parallel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 DataMatrix encoding. It showcases the use of BarcodeGenerator, EncodeTypes, and image format classes to create high‑resolution PNG files. Developers often need to batch‑process multiple barcode values efficiently, and this pattern illustrates parallel execution with safe file naming.
// Prompt: Batch convert a list of AI strings to GS1 DataMatrix PNG files using parallel processing.
// Tags: gs1 datamatrix, batch, parallel, png, barcode generation, aspose.barcode, encode types, image output

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that batch‑processes a collection of GS1 Application Identifier strings,
/// generating GS1 DataMatrix barcodes and saving each as a PNG file using parallel execution.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates the barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Define a sample list of AI (Application Identifier) strings to encode as GS1 DataMatrix.
        List<string> aiStrings = new List<string>
        {
            "(01)01234567890128(10)ABC123",
            "(01)09876543210987(21)XYZ789",
            "(01)12345678901231(17)221231",
            "(01)55555555555555(3103)001500",
            "(01)99999999999999(3102)000750"
        };

        // Ensure the output directory exists.
        string outputFolder = "OutputDataMatrix";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each AI string in parallel to improve performance on multi‑core systems.
        Parallel.ForEach(aiStrings, aiString =>
        {
            // Generate a file‑system‑safe name from the AI string (remove invalid characters).
            string safeFileName = GetSafeFileName(aiString) + ".png";
            string outputPath = Path.Combine(outputFolder, safeFileName);

            // Create and configure the barcode generator for GS1 DataMatrix.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, aiString))
            {
                // Set image size using interpolation mode for high‑quality scaling.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Output the result to the console for tracking.
            Console.WriteLine($"Generated: {outputPath}");
        });
    }

    // Helper method to create a file‑system‑safe name from the AI string.
    private static string GetSafeFileName(string input)
    {
        // Replace any characters that are invalid in file names.
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, '_');
        }

        // Remove parentheses and spaces that are unnecessary for the file name.
        return input.Replace("(", "").Replace(")", "").Replace(" ", "_");
    }
}