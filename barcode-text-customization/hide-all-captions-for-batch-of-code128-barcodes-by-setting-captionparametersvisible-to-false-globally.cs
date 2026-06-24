using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes using Aspose.BarCode and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of sample Code128 barcodes and writes them to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        // Ensure the directory exists.
        Directory.CreateDirectory(outputDir);

        // Sample texts to encode as Code128 barcodes.
        string[] samples = new string[]
        {
            "ABC123",
            "9876543210",
            "CODE128TEST",
            "12345",
            "HELLOWORLD"
        };

        // Iterate over each sample text and generate a barcode.
        foreach (string text in samples)
        {
            // Initialize a barcode generator for Code128 with the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Hide both the above and below captions for a cleaner image.
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Build the full file path for the PNG output.
                string filePath = Path.Combine(outputDir, $"{text}.png");
                // Save the generated barcode image to disk.
                generator.Save(filePath);
                // Inform the user that the barcode was saved.
                Console.WriteLine($"Saved barcode for '{text}' to '{filePath}'");
            }
        }

        // Indicate that all barcode generation tasks are complete.
        Console.WriteLine("All barcodes generated.");
    }
}