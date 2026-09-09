// Title: Batch QR Code Generation with Hidden Text
// Description: Generates multiple QR code images while suppressing the visible barcode text.
// Category-Description: This example demonstrates batch generation of QR codes using Aspose.BarCode. It showcases the BarcodeGenerator class, CodeTextParameters for controlling text visibility, and common settings like XDimension. Developers often need to create multiple barcodes without displaying the encoded text, such as for UI‑less scanning or embedding in documents.
// Prompt: Hide main barcode text for a batch of QR code images by setting CodetextParameters.Visible to false.
// Tags: qr, barcode, batch, hide-text, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a batch of QR code images with the main barcode text hidden.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates QR codes without visible text, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory for the batch
        string outputFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        int batchSize = 5; // Number of QR codes to generate

        // Loop to generate each QR code image
        for (int i = 1; i <= batchSize; i++)
        {
            // Define the code text for the current QR code
            string codeText = $"SampleQR{i}";

            // Build the full file path for the PNG image
            string filePath = Path.Combine(outputFolder, $"QR_{i}.png");

            // Initialize the barcode generator with QR symbology and the specified code text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Hide the main barcode text by setting its location to None
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Optional: adjust the size of the QR modules (pixels per module)
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                // Save the generated QR code image to the specified path in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated file
            Console.WriteLine($"Generated QR code without text: {filePath}");
        }

        // Indicate that the batch generation process has finished
        Console.WriteLine("Batch generation completed.");
    }
}