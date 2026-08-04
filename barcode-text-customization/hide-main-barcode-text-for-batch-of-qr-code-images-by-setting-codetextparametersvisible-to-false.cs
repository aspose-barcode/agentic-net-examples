// Title: Hide QR Code Text for Batch Generation
// Description: Demonstrates how to generate multiple QR code images while suppressing the human‑readable text using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include creating batches of barcodes for marketing or inventory where the textual representation is not required. Developers often need to control visibility, location, and styling of barcode text for various output formats.
// Prompt: Hide main barcode text for a batch of QR code images by setting CodetextParameters.Visible to false.
// Tags: qr code, hide text, batch, png, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of QR code images with the human‑readable text hidden.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates QR codes from a list of URLs and saves them as PNG files without displaying the code text.
    /// </summary>
    static void Main()
    {
        // Define the output directory for the generated QR code images.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "QrCodes");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // List of sample QR code texts (e.g., URLs) to encode.
        List<string> qrTexts = new List<string>
        {
            "https://example.com/1",
            "https://example.com/2",
            "https://example.com/3",
            "https://example.com/4",
            "https://example.com/5"
        };

        int index = 1; // Counter for naming output files.

        // Iterate over each text value and generate a corresponding QR code.
        foreach (string text in qrTexts)
        {
            // Build the full file path for the current QR code image.
            string filePath = Path.Combine(outputFolder, $"qr_{index}.png");

            // Initialize the barcode generator with QR encoding and the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Hide the human‑readable text by setting its location to None.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the generated QR code image as a PNG file.
                generator.Save(filePath);
            }

            // Output a confirmation message to the console.
            Console.WriteLine($"Generated QR code without text: {filePath}");
            index++;
        }

        // Indicate that all QR codes have been processed.
        Console.WriteLine("All QR codes have been generated.");
    }
}