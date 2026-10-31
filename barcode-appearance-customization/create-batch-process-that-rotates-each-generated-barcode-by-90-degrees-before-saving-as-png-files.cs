// Title: Batch generation of rotated Code128 barcodes saved as PNG files
// Description: Generates a set of Code128 barcodes, rotates each by 90 degrees, and saves them as PNG images in a temporary folder.
// Category-Description: Demonstrates Aspose.BarCode batch processing for barcode generation, rotation, and image export. Uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create Code128 barcodes, apply a rotation angle, and write PNG files. Ideal for developers needing to produce multiple rotated barcodes for labeling, packaging, or UI display.
// Prompt: Create a batch process that rotates each generated barcode by 90 degrees before saving as PNG files.
// Tags: code128, rotation, png, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a batch of Code128 barcodes, rotates each by 90°,
/// and saves the results as PNG files in a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs batch barcode creation, rotation, and saving.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample barcode data to be encoded
        List<string> codeTexts = new List<string>
        {
            "1234567890",
            "ABCDEF1234",
            "9876543210",
            "HELLO2026",
            "BARCODE5"
        };

        // Iterate over each barcode text, generate the barcode, rotate it, and save as PNG
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string codeText = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            try
            {
                // Initialize the generator with Code128 symbology and the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Apply a 90-degree rotation to the generated barcode
                    generator.Parameters.RotationAngle = 90;

                    // Save the rotated barcode as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Saved rotated barcode to: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation or saving
                Console.WriteLine($"Failed to generate barcode for '{codeText}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}