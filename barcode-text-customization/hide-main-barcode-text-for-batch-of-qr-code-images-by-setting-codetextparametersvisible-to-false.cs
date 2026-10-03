// Title: Batch QR Code Generation with Hidden Text
// Description: Demonstrates generating multiple QR code images while suppressing the human‑readable text beneath the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and CodeTextParameters to create barcodes in bulk. Typical use cases include batch processing of QR codes for inventory, marketing, or authentication where the visual text is not required. Developers often need to hide the code text to produce cleaner images, and this snippet illustrates the standard approach.
// Prompt: Hide main barcode text for a batch of QR code images by setting CodetextParameters.Visible to false.
// Tags: qr, barcode, generation, hide-text, batch, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that generates a batch of QR code images with the main barcode text hidden.
/// </summary>
class Program
{
    /// <summary>
    /// Generates QR code images for a predefined list of texts, hides the human‑readable code text,
    /// and saves the images to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "QRBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define the list of QR code texts to encode
        List<string> codeTexts = new List<string>
        {
            "Sample001",
            "Sample002",
            "Sample003",
            "Sample004",
            "Sample005"
        };

        int index = 1;
        // Iterate over each text, generate a QR code, hide its text, and save the image
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, $"qr_{index}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Hide the human‑readable barcode text by setting its location to None
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the QR code image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR code image: {filePath}");
            index++;
        }

        Console.WriteLine($"All QR code images have been saved to: {batchFolder}");
    }
}