using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates barcode images from a JSON array of code texts using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Parses JSON, creates output directory,
    /// generates barcodes, and saves them as JPEG files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Sample JSON array of code texts. Replace with actual JSON if needed.
        // --------------------------------------------------------------------
        string json = "[\"ABC123\",\"DEF456\",\"GHI789\",\"JKL012\",\"MNO345\"]";

        // --------------------------------------------------------------
        // Deserialize the JSON string into a List<string>.
        // --------------------------------------------------------------
        List<string> codeTexts;
        try
        {
            codeTexts = JsonSerializer.Deserialize<List<string>>(json);
        }
        catch (Exception ex)
        {
            // Output error message if JSON parsing fails and exit.
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------
        // Validate that we have at least one code text to process.
        // --------------------------------------------------------------
        if (codeTexts == null || codeTexts.Count == 0)
        {
            Console.WriteLine("No code texts found in the JSON array.");
            return;
        }

        // --------------------------------------------------------------
        // Prepare the output directory for barcode images.
        // --------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------
        // Iterate over each code text, generate a barcode, and save it.
        // --------------------------------------------------------------
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string codeText = codeTexts[i];
            string fileName = $"barcode_{i + 1}.jpeg";
            string outputPath = Path.Combine(outputDir, fileName);

            // Create a BarcodeGenerator for Code128 with the current text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the generated barcode as a JPEG image.
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            }

            // Inform the user about the saved file.
            Console.WriteLine($"Saved barcode for \"{codeText}\" to \"{outputPath}\"");
        }

        // --------------------------------------------------------------
        // Indicate that the barcode generation process has finished.
        // --------------------------------------------------------------
        Console.WriteLine("Barcode generation completed.");
    }
}