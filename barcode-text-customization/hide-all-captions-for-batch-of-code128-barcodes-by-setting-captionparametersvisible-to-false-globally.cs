// Title: Hide Captions for Batch of Code128 Barcodes
// Description: Demonstrates generating multiple Code128 barcodes while globally disabling the caption text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure CaptionParameters for batch barcode creation. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce PNG images. Developers often need to hide or customize captions when generating large sets of barcodes for labeling, inventory, or shipping applications.
// Prompt: Hide all captions for a batch of Code128 barcodes by setting CaptionParameters.Visible to false globally.
// Tags: code128, barcode, caption, hide, batch, generation, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a batch of Code128 barcodes with captions hidden.
/// </summary>
class Program
{
    /// <summary>
    /// Generates Code128 barcodes for a list of texts, disables captions globally, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define sample Code128 texts
        List<string> codeTexts = new List<string>
        {
            "CODE128_1",
            "CODE128_2",
            "CODE128_3",
            "CODE128_4",
            "CODE128_5"
        };

        // Collect paths of generated barcode images
        List<string> generatedFiles = new List<string>();

        // Iterate over each text, generate a barcode, hide captions, and save the image
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, text + ".png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Hide both above and below captions globally
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Output the list of generated files
        Console.WriteLine("Generated Code128 barcodes with captions hidden:");
        foreach (string file in generatedFiles)
        {
            Console.WriteLine(file);
        }
    }
}