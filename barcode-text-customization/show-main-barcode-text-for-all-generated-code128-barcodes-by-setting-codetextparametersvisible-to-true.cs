using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates a set of Code128 barcodes and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates barcodes for predefined sample texts
    /// and writes them to the "Barcodes" folder in the current directory.
    /// </summary>
    static void Main()
    {
        // Sample texts to encode as Code128 barcodes.
        string[] samples = { "ABC123", "9876543210", "CODE128TEST" };

        // Determine the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Iterate over each sample text, generate a barcode, and save it.
        foreach (string text in samples)
        {
            // Build the full file path for the PNG image.
            string filePath = Path.Combine(outputDir, $"{text}.png");

            // Create a BarcodeGenerator for Code128 with the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // The human‑readable text is displayed by default (Location = Below),
                // so no additional configuration is required.
                generator.Save(filePath);
            }

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Generated barcode saved to: {filePath}");
        }

        // Final status message.
        Console.WriteLine("All Code128 barcodes have been generated with visible human‑readable text.");
    }
}