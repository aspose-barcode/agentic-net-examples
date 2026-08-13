// Title: Batch conversion of AI strings to GS1 DataMatrix PNG files using parallel processing
// Description: Demonstrates how to generate GS1 DataMatrix barcodes from a list of Application Identifier (AI) strings and save them as PNG images in parallel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. It shows typical scenarios such as bulk barcode creation for inventory or logistics, where developers need to efficiently produce multiple barcode images with proper file naming. The snippet highlights parallel processing with Parallel.ForEach to speed up large‑scale barcode generation tasks.
// Prompt: Batch convert a list of AI strings to GS1 DataMatrix PNG files using parallel processing.
// Tags: gs1datamatrix, barcode generation, parallel processing, png output, aspose.barcode, encode types, bulk conversion

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point for generating GS1 DataMatrix barcodes from a collection of AI strings
/// and saving them as PNG files using parallel processing.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates the batch barcode generation.
    /// </summary>
    static void Main()
    {
        // Define a sample list of GS1 AI strings (each must contain AI (01) with 14 digits)
        List<string> aiStrings = new List<string>
        {
            "(01)00123456789012", // GTIN-12 padded to 14 digits
            "(01)01234567890123", // GTIN-13 padded to 14 digits
            "(01)12345678901231", // GTIN-14 with valid check digit
            "(01)00012345678905", // GTIN-12 padded
            "(01)00001234567890"  // GTIN-13 padded
        };

        // Prepare the output directory for generated PNG files
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "GS1DataMatrixOutput");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Perform barcode generation in parallel to improve performance
        Parallel.ForEach(aiStrings, (codeText) =>
        {
            // Create a safe file name by stripping characters illegal in file names
            string safeFileName = codeText.Replace("(", "").Replace(")", "").Replace(" ", "") + ".png";
            string outputPath = Path.Combine(outputFolder, safeFileName);

            // Initialize the barcode generator for GS1 DataMatrix with the current AI string
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
            {
                // Optional: adjust module size if required
                // generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Log the successful generation of the file
            Console.WriteLine($"Generated {outputPath}");
        });

        // Indicate that the batch process has finished
        Console.WriteLine("Batch conversion completed.");
    }
}