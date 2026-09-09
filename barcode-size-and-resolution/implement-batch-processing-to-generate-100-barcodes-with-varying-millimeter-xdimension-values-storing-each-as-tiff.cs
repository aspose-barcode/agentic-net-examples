// Title: Generate 100 Code128 barcodes with varying XDimension and save as TIFF
// Description: Demonstrates batch generation of 100 Code128 barcodes, each with a unique millimeter XDimension, and saves them as TIFF images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as XDimension in millimeters, use the BarcodeGenerator class, and export images in TIFF format. Developers often need to produce large sets of barcodes with varying visual properties for printing, labeling, or testing purposes; this snippet shows a typical loop‑based approach for bulk creation.
// Prompt: Implement batch processing to generate 100 barcodes with varying Millimeter XDimension values, storing each as TIFF.
// Tags: code128, xdimension, tiff, batch-processing, barcode-generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes with varying XDimension values and saves them as TIFF files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates 100 barcodes with incremental XDimension, and writes them to TIFF files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        const int totalBarcodes = 100;

        // Loop to generate each barcode with a distinct XDimension
        for (int i = 0; i < totalBarcodes; i++)
        {
            // Calculate XDimension in millimeters: start at 0.5 mm, increase by 0.01 mm per iteration
            float xDim = 0.5f + i * 0.01f;
            string codeText = $"Sample{i:D3}";
            string filePath = Path.Combine(outputFolder, $"barcode_{i:D3}.tiff");

            try
            {
                // Initialize the generator with Code128 symbology and the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Apply the calculated XDimension (millimeter precision)
                    generator.Parameters.Barcode.XDimension.Millimeters = xDim;

                    // Save the barcode image as a TIFF file
                    generator.Save(filePath, BarCodeImageFormat.Tiff);
                }

                // Log successful generation
                Console.WriteLine($"Generated {filePath} with XDimension {xDim} mm");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation
                Console.WriteLine($"Error generating barcode {i}: {ex.Message}");
            }
        }

        // Inform the user where all barcodes have been saved
        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }
}