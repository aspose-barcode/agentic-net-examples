// Title: Hide QR Code Text in Batch Generation
// Description: Demonstrates how to generate multiple QR code images while suppressing the human‑readable text using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize QR code output. Typical scenarios include creating batches of barcodes for printing or digital distribution where the textual representation should be omitted. Developers often need to adjust barcode appearance, format, and visibility of the code text, making this pattern a common reference for batch processing tasks.
// Prompt: Hide main barcode text for a batch of QR code images by setting CodetextParameters.Visible to false.
// Tags: qr, barcode, hide-text, batch, png, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a batch of QR code images with the main barcode text hidden.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes, hides the text, saves them, and prints file locations.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "QRBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define sample QR code texts
        List<string> texts = new List<string>
        {
            "Sample1",
            "Hello World",
            "https://example.com",
            "1234567890",
            "Aspose.BarCode"
        };

        // Collect generated file paths for later reporting
        List<string> generatedFiles = new List<string>();

        // Iterate over each text value and generate a QR code image
        for (int i = 0; i < texts.Count; i++)
        {
            string text = texts[i];
            string filePath = Path.Combine(batchFolder, $"qr_{i + 1}.png");

            // Initialize the barcode generator for QR code with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Hide the main barcode text by disabling its location rendering
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the QR code image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            generatedFiles.Add(filePath);
        }

        // Output the location of the generated images
        Console.WriteLine("Generated QR code images (text hidden) in folder:");
        Console.WriteLine(batchFolder);
        foreach (var file in generatedFiles)
        {
            Console.WriteLine(file);
        }
    }
}