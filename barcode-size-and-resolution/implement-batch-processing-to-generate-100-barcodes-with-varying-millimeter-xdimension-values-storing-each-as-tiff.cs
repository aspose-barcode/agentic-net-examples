// Title: Batch generation of 100 Code128 barcodes with varying XDimension saved as TIFF
// Description: Demonstrates how to generate a batch of 100 Code128 barcodes, each with a different XDimension measured in millimeters, and save them as TIFF images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include bulk barcode creation for inventory, labeling, or testing different size parameters. Developers often need to automate barcode production with varying visual properties, such as XDimension, while controlling output format.
// Prompt: Implement batch processing to generate 100 barcodes with varying Millimeter XDimension values, storing each as TIFF.
// Tags: code128, barcode, batch-processing, tiff, xdimension, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of 100 Code128 barcodes with incrementally changing XDimension values
/// and saves each barcode as a TIFF file in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output directory, iterates 100 times,
    /// configures the XDimension for each barcode, and saves the result as a TIFF image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        Console.WriteLine($"Generating 100 barcodes in: {outputFolder}");

        // Loop to generate 100 barcodes with varying XDimension values
        for (int i = 1; i <= 100; i++)
        {
            // Calculate XDimension in millimeters (e.g., 0.5 mm + 0.1*i)
            float xDimMm = 0.5f + i * 0.1f;

            // Prepare barcode text and output file path
            string codeText = $"CODE{i:D3}";
            string filePath = Path.Combine(outputFolder, $"barcode_{i:D3}.tiff");

            try
            {
                // Initialize the barcode generator for Code128 symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Set XDimension in millimeters
                    generator.Parameters.Barcode.XDimension.Millimeters = xDimMm;

                    // Prevent exceptions for potentially invalid code text (optional)
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                    // Save the generated barcode as a TIFF image
                    generator.Save(filePath, BarCodeImageFormat.Tiff);
                }

                Console.WriteLine($"Generated {filePath} (XDimension = {xDimMm} mm)");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation
                Console.WriteLine($"Error generating barcode {i}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}