// Title: Parallel batch generation of GS1 DataMatrix barcodes to PNG files
// Description: This example creates multiple GS1 DataMatrix barcodes from a collection of AI strings and saves each as a PNG image.
// Category-Description: The sample belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1DataMatrix, configure parameters such as XDimension, and employ parallel processing for high‑throughput scenarios like inventory labeling or product tracking. Developers often need to generate large numbers of barcodes quickly and store them in common image formats.
// Prompt: Batch convert a list of AI strings to GS1 DataMatrix PNG files using parallel processing.
// Tags: gs1, datamatrix, barcode, generation, parallel, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates parallel batch creation of GS1 DataMatrix barcodes and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes from predefined AI strings using parallel processing.
    /// </summary>
    static void Main()
    {
        // Define a list of GS1 DataMatrix code texts (AI (01) must be 14 digits)
        List<string> codeTexts = new List<string>
        {
            "(01)12345678901231(21)ABC123",
            "(01)00123456789012(21)XYZ789",
            "(01)00012345678901(21)ITEM001",
            "(01)98765432109876(21)PROD456",
            "(01)55555555555555(21)CODE999"
        };

        // Create a unique temporary output folder for the generated PNG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Process each code text in parallel to maximize throughput
        Parallel.For(0, codeTexts.Count, i =>
        {
            string text = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            try
            {
                // Initialize the barcode generator for GS1 DataMatrix with the current text
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, text))
                {
                    // Set the X-dimension (module size) to 2 pixels for better readability
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Save the generated barcode as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation
                Console.WriteLine($"Failed to generate barcode for index {i}: {ex.Message}");
            }
        });

        Console.WriteLine("Batch processing completed.");
    }
}